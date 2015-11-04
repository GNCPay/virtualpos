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
        //
       public static class acount
       {
           public static string role;
       }
        private void processLogin(object sender, EventArgs e)
        {
            SessionVariables.gduser = txtUserName.Text.Trim();
            try
            {
                var loginResult = Helper.UserManager.FindAsync(txtUserName.Text.Trim(), txtPassword.Text.Trim()).Result;
                if (loginResult != null)
                {
                    acount.role = "G";
                    if (Helper.UserManager.IsInRoleAsync(loginResult.Id, "SYSTEM").Result)
                    {
                        acount.role = "F";
                    }
                    ((frmMain)this.ParentForm).LoadControl(new ucMain());
                    SessionVariables.TellerUser = loginResult;                      
                    return;
                }
                else
                {
                    MessageBox.Show("Sai thông tin đăng nhập. Vui lòng kiểm tra và thử lại!", "Đăng nhập không thành công!");
                }
            }
            catch (Exception ex) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void ucLogin_Load(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(SessionVariables.gduser))
            {
                txtUserName.ReadOnly = false;
                txtUserName.Text = "";
            }
            else
            {
                txtUserName.Text = SessionVariables.gduser;
                txtUserName.ReadOnly = true;
            }
        }
    }
}
