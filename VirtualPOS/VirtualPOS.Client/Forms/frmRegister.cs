using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            var user = new ApplicationUser() { UserName = SessionVariables.CardId };
            var result = Helper.UserManager.CreateAsync(user, "123456").Result;
            if (!result.Succeeded)
            {
                MessageBox.Show(result.Errors.ToArray()[0], "Thông báo");
                return;
            }
            SessionVariables.MobileNumber = txtMobileNumber.Text;
            SessionVariables.CardOwner = txtCardHolder.Text;

            dynamic response = Helper.RegisterCard();
            string error_code = response.error_code.ToString();
            MessageBox.Show(response.error_message.ToString(), "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            if (error_code == "00")
            {
                this.DialogResult = DialogResult.OK;
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
    }
}
