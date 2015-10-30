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
    public partial class frmCashInOut : Form
    {
        public frmCashInOut()
        {
            InitializeComponent();
        }
        bool IsCashOut = false;
        public frmCashInOut(bool IsCashOut)
        {
            InitializeComponent();
            this.IsCashOut = IsCashOut;
            if (IsCashOut)
            {
                lblPIN.Text = "MÃ PIN KH";
                lblTitle.Text = "RÚT TIỀN TỪ THẺ";
            }
        }
        long amount = 0;
        string trans_id = String.Empty;
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            amount = long.Parse(txtAmount.Text);
            string pin = txtPIN.Text.Trim();
            dynamic result;
            if (IsCashOut)
            {
                var loginResult = Helper.UserManager.FindAsync(SessionVariables.CardId, pin).Result;
                if (loginResult == null)
                {
                    MessageBox.Show("Mã PIN không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                result = Processing.Helper.CashOut(amount);
            }
            else
            {
                var loginResult = Helper.UserManager.FindAsync(SessionVariables.gduser, pin).Result;
                if (loginResult == null)
                {
                    MessageBox.Show("Mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                result = Processing.Helper.CashIn(amount);
            }
            if (result.error_code == "00")
            {
                trans_id = result.trans_id;
                MessageBox.Show("Giao dịch thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                print();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(result.error_message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
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
            graphics.DrawString("THÔNG TIN GIAO DỊCH", new Font("Arial", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ:" + SessionVariables.CardNumber,
                     new Font("Arial", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng :" + SessionVariables.CardOwner,
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Loại :" + SessionVariables.CardType,
                     new Font("Arial", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Mã GD :" + trans_id.Substring(0,8),
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Thời gian GD :" +DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString("Loại GD :" + ((IsCashOut)?"RÚT TIỀN":"NỘP TIỀN"),
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số tiền :" + amount.ToString("N0") + " VNĐ",
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("HotLine: 0949.898.222", new Font("Arial", 10),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("GDV - " + SessionVariables.gduser, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
        }
    }
}
