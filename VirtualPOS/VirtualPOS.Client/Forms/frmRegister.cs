using Newtonsoft.Json.Linq;
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
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        private string pin = String.Empty;
        private void btnRegister_Click(object sender, EventArgs e)
        {
            pin = (long.Parse(DateTime.Now.ToString("ssHHmm")) + 153103).ToString();
            var user = new ApplicationUser() { UserName = SessionVariables.CardId };
            var result = Helper.UserManager.CreateAsync(user, pin).Result;
            if (!result.Succeeded)
            {
                MessageBox.Show(result.Errors.ToArray()[0], "Thông báo");
                return;
            }
            SessionVariables.MobileNumber = txtMobileNumber.Text;
            SessionVariables.CardOwner = txtCardHolder.Text;
            SessionVariables.Email = txtEmail.Text;
            dynamic response = Helper.RegisterCard();
            string error_code = response.error_code.ToString();

            MessageBox.Show(response.error_message.ToString(), "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            if (error_code == "00")
            {
                //Cash In here
                Helper.CashIn(SessionVariables.CardPrepaidAmount);
                var cardProfile = Helper.GetProfile();
                    SessionVariables.ProfileId = cardProfile._id;
                    SessionVariables.FinanceAccount = Helper.GetAccountInfo();
                SessionVariables.IsActived = true;
                SessionVariables.IsRegister = true;
                Helper.RegisterWalletToCard();
                this.DialogResult = DialogResult.OK;
                print();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            lblCardNumber.Text = SessionVariables.CardNumber;
            txtMobileNumber.Text = SessionVariables.MobileNumber;
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
            graphics.DrawString("THÔNG TIN KÍCH HOẠT!", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ:" + SessionVariables.CardNumber,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Loại :" + SessionVariables.CardType,
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Mệnh giá :" + SessionVariables.CardPrepaidAmount.ToString("N0") + " VNĐ",
                    new Font("Courier New", 12),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ngày hiệu lực :" + DateTime.Today.ToString("dd/MM/yyyy"),
                    new Font("Courier New", 12),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ngày hết hạn :" + DateTime.Today.AddMonths(6).ToString("dd/MM/yyyy"),
                  new Font("Courier New", 12),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng :" + SessionVariables.CardOwner,
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số điện thoại :" + SessionVariables.MobileNumber,
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Email :" + SessionVariables.Email,
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Mã PIN :" + pin,
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
            graphics.DrawString("GDV - " + SessionVariables.TellerUser.UserName, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
        }
    }
}
