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
            SessionVariables.CardNumber = "970431031501" + DateTime.Now.ToString("HHmm");
            SessionVariables.CardOwner = "CARD OWNER " + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            SessionVariables.MobileNumber = "09098" + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            SessionVariables.CardValidDate = "10/15";
            this.DialogResult = DialogResult.OK;
        }

        private void frmScanCard_Load(object sender, EventArgs e)
        {
            ScanCard();
        }
    }
}
