﻿using System;
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
            frmRegister frmRegister = new frmRegister();
            frmRegister.ShowDialog();
        }

        private void changePIN(object sender, EventArgs e)
        {
            frmChangePIN frmChangePIN = new frmChangePIN();
            frmChangePIN.ShowDialog();
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F2)
            {
                ScanCard();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ScanCard()
        {
                var cardReaderResult = new frmScanCard().ShowDialog();
                if (cardReaderResult == DialogResult.OK) { pCardInfo.Reload(); }
        }
        private void ucMain_Load(object sender, EventArgs e)
        {
            ScanCard();
        }
    }
}
