﻿using MongoDB.Driver.Builders;
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
    public partial class frmScanCard : Form
    {
        public frmScanCard()
        {
            InitializeComponent();
        }

        private void ScanCard()
        {
            SessionVariables.CardId = card_id;
            //SessionVariables.CardId = "0009897205";
            Helper.CheckCard();
            if (!String.IsNullOrEmpty(SessionVariables.CardNumber))
            {
                var cardProfile = Helper.GetProfile();
                SessionVariables.IsRegister = (cardProfile != null);
                SessionVariables.FinanceAccount = Helper.GetAccountInfo();
                if (cardProfile != null)
                {
                    if (cardProfile.Pin != 1)
                    {
                        SessionVariables.ProfileId = cardProfile._id; 
                        this.pCardInfo.Reload();
                        RefreshUI(false);
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng đổi mã pin cho lần giao dịch đầu tiên !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        frmChangePIN frmp = new frmChangePIN();
                        DialogResult changepin = frmp.ShowDialog();
                    }

                }
                else
                {
                    MessageBox.Show("Thẻ chưa được đăng ký. Vui lòng đăng ký để sử dụng!", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //((ucMain)(this.Parent)).EnableControl();
                    //frmRegister frmRegister = new frmRegister();
                    //DialogResult registerResult = frmRegister.ShowDialog();
                    //((ucMain)(this.Parent)).EnableControl();


                    //MessageBox.Show("Bạn chưa đăng ký tài khoản, đăng ký ngay !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //frmRegister frmRegister = new frmRegister();
                    //DialogResult registerResult = frmRegister.ShowDialog();
                    
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Số thẻ không tồn tại liên hệ admin để được hỗ trợ !", "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
            //SessionVariables.CardNumber = "9704310315011516";// "970431031501" + DateTime.Now.ToString("HHmm");
            //SessionVariables.CardOwner = "CARD OWNER 1516";// "CARD OWNER " + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            //SessionVariables.MobileNumber = "8490981516";// "09098" + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            //SessionVariables.CardValidDate = "10/15";
            // Check thong tin the tren server
            //this.DialogResult = DialogResult.OK;
        }

        private void RefreshUI(bool requireScan)
        {
            btnOK.Visible = !requireScan;
            panel1.Visible = !requireScan;
            lblNotif.Visible = requireScan;
        }

        private void frmScanCard_Load(object sender, EventArgs e)
        {
            //card_id = "0013438726";
            //ScanCard();
            //ScanCard();
            RefreshUI(true);
        }
        private string card_id = String.Empty;
        private string temp_card_id = string.Empty;
        private KeysConverter keyConvert = new KeysConverter();
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            
            if (keyData == Keys.Enter)
            {
                card_id = temp_card_id;
                temp_card_id = String.Empty;
                ScanCard();
            }
            else
            {
                temp_card_id += Helper.GetCharFromKeys(keyData).ToString();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}
