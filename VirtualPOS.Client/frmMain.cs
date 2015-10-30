using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualPOS.Client.Forms;
using VirtualPOS.Client.Processing;

namespace VirtualPOS.Client
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        public void LoadControl(UserControl controlToLoad)
        {
            this.pContent.Controls.Clear();
            controlToLoad.Top = (this.pContent.Height - controlToLoad.Height) / 2;
            controlToLoad.Left = (this.pContent.Width - controlToLoad.Width) / 2;
            this.pContent.Controls.Add(controlToLoad);          
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                //LoadControl(new Forms.ucAlmaz());
                frmLogin frmLogin = new frmLogin();
                frmLogin.ShowDialog();

                if (SessionVariables.gduser != null)
                {
                    LoadControl(new ucMain());
                }
                else
                {
                    LoadControl(new ucLogin());
                }
            }catch (Exception ex) { }      
        }

        //private void listenShortCuts(object sender, KeyEventArgs e)
        //{
        //    if(e.KeyCode== Keys.F2)
        //    {
        //        MessageBox.Show("F2 pressed");
        //    }
        //}

        private void btnConfig_Click(object sender, EventArgs e)
        {

        }
    }
}
