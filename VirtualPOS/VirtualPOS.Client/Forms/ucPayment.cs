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
    public partial class ucPayment : UserControl
    {
        public ucPayment()
        {
            InitializeComponent();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            string pin = txtPIN.Text.Trim();
            var loginResult = Helper.UserManager.FindAsync(SessionVariables.CardNumber, pin).Result;
            if (loginResult == null)
            {
                MessageBox.Show("Mã PIN không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dynamic result = Processing.Helper.PayBill(txtBillNo.Text.Trim(), long.Parse(txtBillAmount.Text));
            if (result.error_code == "00")
            {
                MessageBox.Show("Giao dịch thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ((ucMain)(this.Parent)).ScanCard();
            }
            else
            {
                MessageBox.Show(result.error_message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
