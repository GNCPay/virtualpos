using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualPOS.Client.Processing;
using System.Drawing.Printing;
using MongoDB.Driver.Builders;


namespace VirtualPOS.Client.Forms
{
    public partial class ucPayment : UserControl
    {
        long amount = 0;
        string bill_no = String.Empty;
        string trans_id = String.Empty;
        public ucPayment()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (new frmScanCard().ShowDialog() != DialogResult.OK)
                    return;
                progressBar1.Visible = true;
                this.timer1.Enabled = true;
                for (int i = 0; i <= timer1.Interval; i++)
                {
                    progressBar1.Value = i;
                }
                string pin = txtPIN.Text.Trim();
                var loginResult = Helper.UserManager.FindAsync(SessionVariables.CardId, pin).Result;
                if (loginResult == null)
                {          
                    MessageBox.Show("Mã PIN không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar1.Visible = false;
                    txtPIN.Text = "";
                    Helper.AddLogCard("Transaction", "thanh toan khong thanh cong", SessionVariables.FinanceAccount.available_balance, SessionVariables.FinanceAccount.available_balance,0);
                    ((ucMain)(this.Parent)).EnableControl();
                    return;
                }

                var user_name = Processing.SessionVariables.CardId;
                dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                if (profile.Status != "LOCKED")
                {
                    string kaka = txtBillAmount.Text;
                    string mkaka = kaka.Replace(".", "").Replace(",", "");
                    amount = long.Parse(mkaka);
                    long a = SessionVariables.FinanceAccount.available_balance;
                    int kqx = (int)a - (int)amount;
                    bill_no = txtBillNo.Text.Trim();
                    dynamic result = Processing.Helper.PayBill(bill_no, amount);
                    if (result.error_code == "00")
                    {
                        MessageBox.Show("Giao dịch thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        progressBar1.Visible = false;
                        trans_id = result.trans_id;                     
                        print();
                        ((ucMain)(this.Parent)).ScanCard();
                        txtBillAmount.Text = "";
                        txtBillNo.Text = "";
                        txtPIN.Text = "";
                        Helper.AddLogCard("Transaction", "thanh toan thanh cong", a, kqx, amount);
                        ((ucMain)(this.Parent)).EnableControl();
                    }
                    else
                    {
                        MessageBox.Show(result.error_message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Tài khoản thẻ đang bị khoá vui lòng liên hệ GDV để được hỗ trợ !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtBillAmount.Text = "";
                    txtBillNo.Text = "";
                    txtPIN.Text = "";
            }
            catch (Exception ex) { }  
        }

        public void print()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 820;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pdoc.Print();
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to Almaz!", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("THÔNG TIN GIAO DỊCH", new Font("Courier New", 12),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ : " + SessionVariables.CardId,
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng : ",
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(SessionVariables.CardOwner,
                   new Font("Courier New", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            
            graphics.DrawString("Loại : " + SessionVariables.CardType,
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "-----------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("HotLine: 094.9898.222", new Font("Courier New", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Mã GD : " + trans_id.Substring(0, 8),
                   new Font("Courier New", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            //graphics.DrawString("Thời gian : " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
            //       new Font("Courier New", 8),
            //       new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;

            graphics.DrawString("Loại GD : THANH TOÁN",
                   new Font("Courier New", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số Hóa đơn : " + bill_no,
                   new Font("Courier New", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số dư đầu :" + String.Concat(SessionVariables.FinanceAccount.available_balance.ToString("N0"), " VNĐ"),
                   new Font("Courier New",8),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số tiền :" + amount.ToString("N0") + " VNĐ",
                   new Font("Courier New", 9),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            long a = SessionVariables.FinanceAccount.available_balance;
            int kqx = (int)a - (int)amount;
            graphics.DrawString("Số dư cuối :" + kqx.ToString("N0") + " VNĐ",
                  new Font("Courier New", 8),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            underLine = "-----------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Quầy bán:" + SessionVariables.CounterName,
                   new Font("Courier New", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("HotLine: 094.9898.222", new Font("Courier New", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("GDV - " + SessionVariables.TellerUser.UserName, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Thank You! And See You Again.", new Font("Courier New", 10),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
        }

        private void txtBillAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBillAmount.Text.Equals("0"))
                    return;
                double temp = Convert.ToDouble(txtBillAmount.Text);
                txtBillAmount.Text = temp.ToString("#,###");
            }
            catch (Exception ex) { }
        }

        private void txtBillAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //check number
                e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
            }
            catch (Exception ex) { }    
        }

        private void txtBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //check number
                e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
            }
            catch (Exception ex) { }    
        }

        private void txtBillAmount_TextChanged(object sender, EventArgs e)
        {
        }

        private void ucPayment_Load(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = 300;
            this.progressBar1.Minimum = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Start();
        }
    }
}
