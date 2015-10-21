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
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (new frmScanCard().ShowDialog() != DialogResult.OK)
                return;
            string pin = txtPIN.Text.Trim();
            var loginResult = Helper.UserManager.FindAsync(SessionVariables.CardId, pin).Result;
            if (loginResult == null)
            {
                MessageBox.Show("Mã PIN không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPIN.Text = "";
                Helper.AddLogCard("Transaction", "thanh toan thanh cong", SessionVariables.FinanceAccount.available_balance, SessionVariables.FinanceAccount.available_balance, amount);
                ((ucMain)(this.Parent)).EnableControl();
                return;           
            }
            amount = long.Parse(txtBillAmount.Text);
            long a = SessionVariables.FinanceAccount.available_balance;
            int kqx = (int)a - (int)amount;
            bill_no = txtBillNo.Text.Trim();
            dynamic result = Processing.Helper.PayBill(bill_no, amount);
            if (result.error_code == "00")
            {
                MessageBox.Show("Giao dịch thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                trans_id = result.trans_id;
                print();
                ((ucMain)(this.Parent)).ScanCard();               
                txtBillAmount.Text = "";
                txtBillNo.Text = "";
                txtPIN.Text = "";
                Helper.AddLogCard("Transaction", "thanh toan thanh cong", SessionVariables.FinanceAccount.available_balance,kqx,amount);
                ((ucMain)(this.Parent)).EnableControl();
            }
            else
            {
                MessageBox.Show(result.error_message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            graphics.DrawString("THÔNG TIN GIAO DỊCH", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ:" + SessionVariables.CardId,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng :" + SessionVariables.CardOwner,
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Loại :" + SessionVariables.CardType,
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Mã GD :" + trans_id.Substring(0, 8),
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Thời gian GD :" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Loại GD : THANH TOÁN",
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số Hóa đơn : " + bill_no,
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số dư đầu :" + String.Concat(SessionVariables.FinanceAccount.available_balance.ToString("N0"), " VNĐ"),
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số tiền :" + amount.ToString("N0") + " VNĐ",
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            long a = SessionVariables.FinanceAccount.available_balance;
            int kqx = (int)a - (int)amount;
            graphics.DrawString("Số dư còn lại :" + kqx.ToString("N0") + " VNĐ",
                  new Font("Courier New", 12),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            underLine = "------------------------------------------";
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
            graphics.DrawString("GDV - " + SessionVariables.TellerUser.UserName, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
        }
    }
}
