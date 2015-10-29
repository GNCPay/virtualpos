using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualPOS.Client.Processing;

namespace VirtualPOS.Client.Forms
{
    public partial class PinRequest : Form
    {
        long amount;
        string bill_no;
        string trans_id = String.Empty;
        public PinRequest()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn đã huỷ thành công !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                var user_name = Processing.SessionVariables.CardId;
                if (user_name != null)
                {
                    dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                    if (profile.Status == "LOCKED")
                    {
                        MessageBox.Show("Tài khoản thẻ đang bị khoá vui lòng liên hệ GDV để được hỗ trợ !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                    }
                    else
                    {
                        progressBar1.Visible = true;
                        this.timer1.Enabled = true;
                        for (int i = 0; i <= timer1.Interval; i++)
                        {
                            progressBar1.Value = i;
                        }
                        string pin = txtpin.Text.Trim();
                        var loginResult = Helper.UserManager.FindAsync(SessionVariables.CardId, pin).Result;
                        if (loginResult == null)
                        {
                            MessageBox.Show("Mã PIN không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            progressBar1.Visible = false;
                            txtpin.Text = "";
                            Helper.AddLogCard("Transaction", "thanh toan khong thanh cong", SessionVariables.FinanceAccount.available_balance, SessionVariables.FinanceAccount.available_balance, 0, SessionVariables.CounterName, bill_no);
                            ((ucMain)(this.Parent)).EnableControl();
                            return;
                        }
                       
                        if (profile.Status != "LOCKED")
                        {
                            dynamic tran = Helper.DataHelper.Get("transactions", Query.EQ("ref_id", frmPayment.payment.bill_no));
                            if(tran==null)
                            {
                                string kaka = frmPayment.payment.amount.ToString();
                                string mkaka = kaka.Replace(".", "").Replace(",", "");
                                amount = long.Parse(mkaka);
                                long a = SessionVariables.FinanceAccount.available_balance;
                                int kqx = (int)a - (int)amount;
                                bill_no = frmPayment.payment.bill_no;
                                dynamic result = Processing.Helper.PayBill(bill_no, amount);
                                if (result.error_code == "00")
                                {
                                    MessageBox.Show("Giao dịch thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    Helper.AddLogCard("Transaction", "thanh toan thanh cong", a, kqx, amount, SessionVariables.CounterName, bill_no);
                                    progressBar1.Visible = false;
                                    trans_id = result.trans_id;
                                    print();
                                    this.Close();
                                    ((ucMain)(this.Parent)).EnableControl();
                                }
                                else
                                {
                                    MessageBox.Show(result.error_message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Close();
                                    ((ucMain)(this.Parent)).EnableControl();
                                }
                            }  
                            else
                            {
                                MessageBox.Show("Số hoá đơn đã được thanh toán !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                                ((ucMain)(this.Parent)).EnableControl();
                            }
                        }
                        else
                            MessageBox.Show("Tài khoản thẻ đang bị khoá vui lòng liên hệ GDV để được hỗ trợ !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                        ((ucMain)(this.Parent)).EnableControl();
                    }

                }
                else
                {
                    MessageBox.Show("Bạn chưa đăng ký tài khoản, đăng ký ngay !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    frmRegister frmRegister = new frmRegister();
                    DialogResult registerResult = frmRegister.ShowDialog();

                    //frmPayment frmp = new frmPayment();
                    //DialogResult dfrmp = frmp.ShowDialog();
                    //if (registerResult == DialogResult.OK)
                    //    EnableControl();
                    ((ucMain)(this.Parent)).EnableControl();
                }
            }
            catch (Exception ex) { }  
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void PinRequest_Load(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = 300;
            this.progressBar1.Minimum = 0;
        }

        public void print()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Arial", 15);


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
            Font font = new Font("Arial", 10);
            float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int Offset = 40;
            graphics.DrawString("Welcome to Almaz!", new Font("Arial", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("THÔNG TIN GIAO DỊCH", new Font("Arial", 12),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ : " + SessionVariables.CardNumber,
                     new Font("Arial", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng : ",
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(SessionVariables.CardOwner,
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Loại Thẻ : " + SessionVariables.CardType,
                     new Font("Arial", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "-----------------------";
            graphics.DrawString(underLine, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("HotLine: 094.9898.222", new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Mã GD : " + trans_id.Substring(0, 8),
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Loại GD : THANH TOÁN",
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số Hóa đơn : " + bill_no,
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số dư đầu :" + String.Concat(SessionVariables.FinanceAccount.available_balance.ToString("N0"), " VNĐ"),
                   new Font("Arial", 8),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số tiền :" + amount.ToString("N0") + " VNĐ",
                   new Font("Arial", 9),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            long a = SessionVariables.FinanceAccount.available_balance;
            int kqx = (int)a - (int)amount;
            graphics.DrawString("Số dư cuối :" + kqx.ToString("N0") + " VNĐ",
                  new Font("Arial", 8),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            underLine = "-----------------------";
            graphics.DrawString(underLine, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Quầy bán:" + SessionVariables.CounterName,
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("HotLine: 094.9898.222", new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("GDV - " + SessionVariables.TellerUser.UserName, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Thank you & See you again!", new Font("Arial", 10),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
        }

    }
}
