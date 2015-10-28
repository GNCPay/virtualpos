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
    public partial class ucAlmaz : UserControl
    {
        public ucAlmaz()
        {
            InitializeComponent();
            //timer1.Tick += timer1_Tick;
            //timer1.Interval = 5000;
            //timer1.Start(); 
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    this.Hide();
        //    ucLogin.usergd.userGD = "0123213";
        //    try
        //    {
        //        ((frmMain)this.ParentForm).LoadControl(new ucLogin());
        //    }
        //    catch (Exception ex) { }
        //    timer1.Stop();
        //}
        string card_track = String.Empty;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            MessageBox.Show(card_track);
            if (keyData == Keys.Enter)
            {
                CheckProfile();
                card_track = String.Empty;
            }
            else
            {
                card_track += Helper.GetCharFromKeys(keyData).ToString();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CheckProfile()
        {
            try
            {
                card_track = card_track.Replace("?", "").Replace("%", "");
                Helper.GetUserConfig(card_track);
                if (SessionVariables.UserConfig == null)
                {
                    MessageBox.Show("Thẻ nhân viên chưa được đăng ký. Vui lòng kiểm tra và liên hệ với IT để được hỗ trợ");
                    return;
                }
                ucLogin.usergd.userGD = SessionVariables.UserConfig.Id.ToString();// "0123213";
                try
                {
                    ((frmMain)this.ParentForm).LoadControl(new ucLogin());
                }
                catch (Exception ex) { }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thẻ nhân viên chưa được đăng ký. Vui lòng kiểm tra và liên hệ với IT để được hỗ trợ");
                return;
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            //Helper.GetUserConfig(card_track);
            //if (SessionVariables.UserConfig == null)
            //{
            //    MessageBox.Show("Thẻ nhân viên chưa được đăng ký. Vui lòng kiểm tra và liên hệ với IT để được hỗ trợ");
            //    return;
            //}
            ucLogin.usergd.userGD = "";
            try
            {
                ((frmMain)this.ParentForm).LoadControl(new ucLogin());
            }
            catch (Exception ex) { }
        }
    }
}
