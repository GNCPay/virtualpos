using MongoDB.Driver.Builders;
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
    public partial class frmChangePIN : Form
    {
        public frmChangePIN()
        {
            InitializeComponent();
        }

        private void btnChangePIN_Click(object sender, EventArgs e)
        {
            var user_name = Processing.SessionVariables.CardId;
            var password = txtOldPIN.Text.Trim();
            var new_password = txtNewPIN.Text.Trim();
            var user = Processing.Helper.UserManager.FindAsync(user_name, password).Result;
            try
            {
                if (user == null)
                {
                    MessageBox.Show("Mã PIN cũ không hợp lệ. Vui lòng kiểm tra và thử lại!", "Thông báo");
                    ((ucMain)(this.Parent)).EnableControl();
                    return;
                }
                 dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                 if (profile.Status != "LOCKED")
                 {
                     var result = Processing.Helper.UserManager.ChangePasswordAsync(
                     user.Id, password, new_password).Result;
                     if (result.Succeeded)
                     {
                         dynamic pr = Helper.DataHelper.Get("profile", Query.EQ("user_name", user_name));
                         pr.Pin = 0;
                         Helper.DataHelper.SaveUpdate("profile", pr);
                         MessageBox.Show("Đổi PIN thành công!", "Thông báo");
                         this.DialogResult = DialogResult.OK;
                         Helper.AddLogCard("Change PIN", "thay doi  thanh cong", SessionVariables.FinanceAccount.available_balance, SessionVariables.FinanceAccount.available_balance, 0,SessionVariables.CounterName,"null");
                         ((ucMain)(this.Parent)).EnableControl(); 
                     }
                     else
                     {
                         MessageBox.Show("Đổi PIN không thành công. Xin vui lòng thử lại sau!", "Thông báo");
                         this.DialogResult = DialogResult.No;
                         ((ucMain)(this.Parent)).EnableControl();
                     }
                 }
                else
                     MessageBox.Show("Tài khoản thẻ đang bị khoá vui lòng liên hệ GDV để được hỗ trợ !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.Close();
            }
            catch (Exception ex) { }   
        }
    }
}
