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
namespace VirtualPOS.Client.Forms
{
    public partial class ucLogin : UserControl
    {
        public ucLogin()
        {
            InitializeComponent();
        }
        public static class usergd
        {
            public static string userGD;
        }
        private void processLogin(object sender, EventArgs e)
        {
            usergd.userGD = txtUserName.Text.Trim();
            var loginResult = Helper.UserManager.FindAsync(txtUserName.Text.Trim(),txtPassword.Text.Trim()).Result;
            if (loginResult != null)
            {
                ((frmMain)this.ParentForm).LoadControl(new ucMain());                
                SessionVariables.TellerUser = loginResult;
                return;
            }
            else {
                MessageBox.Show("Sai thông tin đăng nhập. Vui lòng kiểm tra và thử lại!", "Đăng nhập không thành công!");
            }
        }
    }
}
