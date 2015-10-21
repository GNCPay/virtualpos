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
            if(user == null)
            {
                MessageBox.Show("Mã PIN cũ không hợp lệ. Vui lòng kiểm tra và thử lại!", "Thông báo");
                Helper.AddLogCard("Change PIN", "thay doi khong thanh cong", SessionVariables.FinanceAccount.available_balance, 0, SessionVariables.FinanceAccount.available_balance);
                ((ucMain)(this.Parent)).EnableControl();
                return;
            }
            var result = Processing.Helper.UserManager.ChangePasswordAsync(
                user.Id, password, new_password).Result;
            if (result.Succeeded)
            {
                MessageBox.Show("Đổi PIN thành công!", "Thông báo");
                this.DialogResult = DialogResult.OK;
                Helper.AddLogCard("Change PIN", "thay doi  thanh cong", SessionVariables.FinanceAccount.available_balance, 0, SessionVariables.FinanceAccount.available_balance);
                ((ucMain)(this.Parent)).EnableControl();
            }
            else
            {
                MessageBox.Show("Đổi PIN không thành công. Xin vui lòng thử lại sau!", "Thông báo");
                this.DialogResult = DialogResult.No;
                Helper.AddLogCard("Change PIN", "thay doi khong thanh cong", SessionVariables.FinanceAccount.available_balance, 0, SessionVariables.FinanceAccount.available_balance);
                ((ucMain)(this.Parent)).EnableControl();
            }
        }
    }
}
