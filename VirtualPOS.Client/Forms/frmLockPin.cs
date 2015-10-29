using MongoDB.Bson;
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
    public partial class frmLockPin : Form
    {
        public frmLockPin()
        {
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                var user_name = Processing.SessionVariables.CardId;
                dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));

                progressBar1.Visible = true;
                this.timer1.Enabled = true;
                for (int i = 0; i <= timer1.Interval; i++)
                {
                    progressBar1.Value = i;
                }
                string pin = txtpin.Text.Trim();
                var loginResult = Helper.UserManager.FindAsync(SessionVariables.CardId, pin).Result;
                if (loginResult == null)
                {
                    MessageBox.Show("Mã PIN không đúng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    progressBar1.Visible = false;
                    txtpin.Text = "";
                    ((ucMain)(this.Parent)).EnableControl();
                    return;
                }
                else
                {

                    if (profile.Status != "LOCKED")
                    {
                        updateLock(profile._id, "LOCKED");
                        MessageBox.Show("Bạn đã khoá tài khoản thành công !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                        ((ucMain)(this.Parent)).EnableControl();
                        ((ucMain)(this.Parent)).DisibleButton = false;
                    }
                    else
                        MessageBox.Show("Tài khoản đã bị khoá !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ((ucMain)(this.Parent)).DisibleButton = false;
                    this.Close();
                }
            }
            catch (Exception ex) { }  
        }

        private void updateLock(ObjectId _id, string Status)
        {
            //var id = new ObjectId(_id);
            dynamic user = Helper.DataHelper.Get("users", Query.EQ("_id", _id));
            user.Status = Status;
            Helper.DataHelper.SaveUpdate("users", user);
        }

        private void frmLockPin_Load(object sender, EventArgs e)
        {
            this.progressBar1.Maximum = 300;
            this.progressBar1.Minimum = 0;
        }
    }
}
