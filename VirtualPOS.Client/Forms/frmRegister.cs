using MongoDB.Driver.Builders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string a = txtMobileNumber.Text.Insert(0, "84");
            a = a.Remove(2, 1);
            dynamic profile = Helper.DataHelper.Get("profile", Query.EQ("mobile", a));
            try
            {
                  if (CheckPhoneSupport(txtMobileNumber.Text) == true)
                    {
                        if (CheckIphone(txtMobileNumber.Text) == true)
                        {
                            pin = (long.Parse(DateTime.Now.ToString("ssHHmm")) + 153103).ToString();
                            var user = new ApplicationUser() { UserName = SessionVariables.CardId };
                            var result = Helper.UserManager.CreateAsync(user, pin).Result;
                            if (!result.Succeeded)
                            {
                                MessageBox.Show(result.Errors.ToArray()[0], "Thông báo");
                                return;
                            }
                            else
                            {
                                SessionVariables.MobileNumber = txtMobileNumber.Text.Trim();
                                SessionVariables.CardOwner = (String.IsNullOrEmpty(txtCardHolder.Text.Trim())) ? "THẺ CHƯA ĐỊNH DANH" : txtCardHolder.Text.Trim();
                                SessionVariables.Email = txtEmail.Text.Trim();
                                SessionVariables.Personal_id = txtcmnd.Text;
                                SessionVariables.Address = txtdiachi.Text;
                                dynamic response = Helper.RegisterCard();
                                string error_code = response.error_code.ToString();
                                MessageBox.Show(response.error_message.ToString(), "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                if (error_code == "00")
                                {
                                    Helper.CashIn(SessionVariables.CardPrepaidAmount);
                                    var cardProfile = Helper.GetProfile();
                                    SessionVariables.ProfileId = cardProfile._id;
                                    SessionVariables.FinanceAccount = Helper.GetAccountInfo();
                                    SessionVariables.IsActived = true;
                                    SessionVariables.IsRegister = true;
                                    Helper.RegisterWalletToCard();

                                    dynamic pr = Helper.DataHelper.Get("profile", Query.EQ("mobile", a));
                                    if(pr!=null)
                                    {
                                        if (txtEmail.Text != "")
                                        {
                                            if (IsValidEmail(txtEmail.Text) == true)
                                            {
                                                pr.email = SessionVariables.Email;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Địa chỉ email không hợp lệ, xin mời nhập lại !", "Kết quả đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                                                if (DialogResult == DialogResult.No)
                                                {
                                                    this.Close();
                                                }
                                            }
                                        }
                                        pr.personal_id = SessionVariables.Personal_id;
                                        pr.address = SessionVariables.Address;
                                        pr.Pin = 1;
                                        Helper.DataHelper.SaveUpdate("profile", pr);
                                    }
                                    this.DialogResult = DialogResult.OK;
                                    print();
                                    Helper.AddLogCard("Register", "Đăng ký Thành công", SessionVariables.FinanceAccount.available_balance, SessionVariables.FinanceAccount.available_balance, 0, SessionVariables.CounterName, "null");
                                }
                                else
                                {
                                    this.DialogResult = DialogResult.Cancel;
                                }
                            }
                        }
                        else
                        {
                            DialogResult dialogResult = MessageBox.Show("Số điện thoại đã tồn tại bạn muốn tiếp tục đăng ký !", "Kết quả đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (dialogResult == DialogResult.Yes)
                            {
                                pin = (long.Parse(DateTime.Now.ToString("ssHHmm")) + 153103).ToString();
                                var user = new ApplicationUser() { UserName = SessionVariables.CardId };
                                var result = Helper.UserManager.CreateAsync(user, pin).Result;
                                if (!result.Succeeded)
                                {
                                    MessageBox.Show(result.Errors.ToArray()[0], "Thông báo");
                                    return;
                                }
                                else
                                {
                                    //Cash In here
                                    SessionVariables.MobileNumber = txtMobileNumber.Text;
                                    SessionVariables.CardOwner = txtCardHolder.Text;
                                    SessionVariables.Email = txtEmail.Text;
                                    SessionVariables.Personal_id = txtcmnd.Text;
                                    SessionVariables.Address = txtdiachi.Text;
                                    dynamic response = Helper.RegisterCard();
                                    Helper.CashIn(SessionVariables.CardPrepaidAmount);
                                    if (profile != null)
                                    {
                                        SessionVariables.ProfileId = profile._id;
                                        profile.user_name = SessionVariables.CardId;
                                        Helper.DataHelper.SaveUpdate("profile", profile);
                                    }
                                    SessionVariables.FinanceAccount = Helper.GetAccountInfo();
                                    SessionVariables.IsActived = true;
                                    SessionVariables.IsRegister = true;
                                    Helper.RegisterWalletToCard();
                                    dynamic pr = Helper.DataHelper.Get("profile", Query.EQ("mobile", a));
                                    if(pr!=null)
                                    {
                                        if (txtEmail.Text != "")
                                        {
                                            if (IsValidEmail(txtEmail.Text) == true)
                                            {
                                                pr.email = SessionVariables.Email;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Địa chỉ email không hợp lệ, xin mời nhập lại !", "Kết quả đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                                                if (DialogResult == DialogResult.No)
                                                {
                                                    this.Close();
                                                }
                                            }
                                        }
                                        pr.personal_id = SessionVariables.Personal_id;
                                        pr.address = SessionVariables.Address;
                                        pr.Pin = 1;
                                        Helper.DataHelper.SaveUpdate("profile", pr);
                                    }
                                    this.DialogResult = DialogResult.OK;
                                    print();
                                    Helper.AddLogCard("Register", "dang ky thanh cong", SessionVariables.FinanceAccount.available_balance, SessionVariables.FinanceAccount.available_balance, 0, SessionVariables.CounterName, "null");
                                }
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                MessageBox.Show("Bạn đã huỷ đăng ký thành công !", "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Số điện thoại không đúng, xin mời nhập lại !", "Kết quả đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                        if(DialogResult==DialogResult.No)
                        {
                            this.Close();
                        }
                    }
             }
            catch (Exception ex) { }      
       }

        static Regex ValidEmailRegex = CreateValidEmailRegex();
        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        internal bool IsValidEmail(string email)
        {
            bool isValid = ValidEmailRegex.IsMatch(email);
            return isValid;
        }

        private static bool CheckIphone(string iphone)
        {
            string a = iphone.Insert(0, "84");
            a = a.Remove(2, 1);
            dynamic profile= Helper.DataHelper.Get("profile",Query.EQ("mobile",a));
            if(profile!=null)
            {
                return false;
            }
            return true;
        }

        private static bool CheckPhoneSupport(string phone_number)
        {
            const int RegionConuntryCode = 84;

            if (phone_number.Length == 10)
            {
                if (phone_number.StartsWith("+"))
                {
                    phone_number = phone_number.Replace("+", "0");
                }

                if (phone_number.StartsWith("0" + RegionConuntryCode))
                {
                    phone_number = phone_number.Replace("0" + RegionConuntryCode, "0");
                }
                string[] networkSupport_2 = { "096", "097", "098", "090", "093", "091", "094", "092", "099" };
                const int networkLength = 3;
                var startphone_number = phone_number.Substring(0, networkLength);
                return networkSupport_2.Any(startphone_number.Equals);

            }
            if (phone_number.Length == 11)
            {
                if (phone_number.StartsWith("+"))
                {
                    phone_number = phone_number.Replace("+", "0");
                }

                if (phone_number.StartsWith("0" + RegionConuntryCode))
                {
                    phone_number = phone_number.Replace("0" + RegionConuntryCode, "0");
                }

                string[] networkSupport_1 = {"0162", "0163", "0164", "0165", "0166", "0167", "0168", "0169",
            "0120", "0121", "0122","0126","0128",
            "0123","0124","0125","0127","0129",
            "0188", "0186",
            "0199"};
                const int networkLength_2 = 4;
                var startphone_number_2 = phone_number.Substring(0, networkLength_2);
                return networkSupport_1.Any(startphone_number_2.Equals);
            }
            return false;
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            lblCardNumber.Text = SessionVariables.CardNumber;
            //txtMobileNumber.Text = SessionVariables.MobileNumber;
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
            graphics.DrawString("THÔNG TIN KÍCH HOẠT!", new Font("Arial", 11),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ:" + SessionVariables.CardNumber,
                     new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Loại Thẻ :" + SessionVariables.CardType,
                     new Font("Arial", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Mệnh giá :" + SessionVariables.CardPrepaidAmount.ToString("N0") + " VNĐ",
                    new Font("Arial", 10),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số dư :" + String.Concat(SessionVariables.FinanceAccount.available_balance.ToString("N0"), " VNĐ"),
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ngày hiệu lực :" + DateTime.Today.ToString("dd/MM/yyyy"),
                    new Font("Arial", 9),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Ngày hết hạn :" + DateTime.Today.AddMonths(6).ToString("dd/MM/yyyy"),
                  new Font("Arial", 9),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "-----------------------";
            graphics.DrawString(underLine, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng :",
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(SessionVariables.CardOwner,
                  new Font("Arial", 10),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("SDT :" + SessionVariables.MobileNumber,
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Email :" + SessionVariables.Email,
                   new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Địa Chỉ :",
                  new Font("Arial", 12),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            string dcl = SessionVariables.Address;
            int max_length = 40;
            int start_id = 0;
            int next_id = 0;
            int length = dcl.Length;
            while(next_id < length && length > max_length)
            {
                int idx = dcl.IndexOf(' ', next_id);
                while (idx < max_length && idx > 0)
                {
                    next_id = idx;
                    idx = dcl.IndexOf(' ', next_id+1);
                }
                string str_to_write = dcl.Substring(0, next_id);
                dcl = dcl.Substring(next_id+1);
                length = dcl.Length;
                next_id = 0;
                graphics.DrawString(str_to_write,
                      new Font("Arial", 11),
                      new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }
            graphics.DrawString(dcl,
                      new Font("Arial", 11),
                      new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 30;
            //string[] catchuoi = dcl.Split(new char[] { ' ', ',', '.', ':', '-' });
            //foreach(string s1 in catchuoi)
            //{

            //}
               
            //graphics.DrawString("Địa Chỉ :" + SessionVariables.Address,
            //       new Font("Arial", 12),
            //       new SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            graphics.DrawString("Mã PIN :" + pin,
                   new Font("Arial", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            underLine = "-----------------------";
            graphics.DrawString(underLine, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Quầy bán : " + SessionVariables.CounterName,
            new Font("Arial", 10),
            new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("HotLine: 0949.898.222", new Font("Arial", 10),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("GDV - " + SessionVariables.gduser, new Font("Arial", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font("Arial", 10),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Thank you & See you again!", new Font("Arial", 10),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
        }

        private void txtcmnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check number
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        }

        private void txtMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //check number
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn huỷ đăng ký thành công !", "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();

        }
    }
}
