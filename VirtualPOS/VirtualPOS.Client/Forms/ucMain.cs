using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualPOS.Client.Forms
{
    public partial class ucMain : UserControl
    {
        public ucMain()
        {
            InitializeComponent();
        }

        private void lockAccount(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn khóa tài khoản này?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
        }

        private void registerCard(object sender, EventArgs e)
        {

        }

        private void changePIN(object sender, EventArgs e)
        {

        }

        private void statement(object sender, EventArgs e)
        {

        }

        private void cashIn(object sender, EventArgs e)
        {

        }

        private void cashOut(object sender, EventArgs e)
        {

        }
    }
}
