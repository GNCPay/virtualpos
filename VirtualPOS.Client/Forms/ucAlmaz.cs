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
    public partial class ucAlmaz : UserControl
    {
        public ucAlmaz()
        {
            InitializeComponent();
            timer1.Tick += timer1_Tick;
            timer1.Interval = 5000;
            timer1.Start(); 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            try
            {
                ((frmMain)this.ParentForm).LoadControl(new ucLogin());
            }
            catch (Exception ex) { }
            timer1.Stop();
        }
    }
}
