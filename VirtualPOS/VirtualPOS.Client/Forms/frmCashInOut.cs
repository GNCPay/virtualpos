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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            long amount = long.Parse(txtAmount.Text);
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
                var loginResult = Helper.UserManager.FindAsync(SessionVariables.TellerUser.UserName, pin).Result;
                if (loginResult == null)
                {
                    MessageBox.Show("Mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                result = Processing.Helper.CashIn(amount);
            }
            if (result.error_code == "00")
            {
                MessageBox.Show("Giao dịch thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(result.error_message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
