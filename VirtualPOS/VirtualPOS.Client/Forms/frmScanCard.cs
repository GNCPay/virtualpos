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
            SessionVariables.CardNumber = "9704310315011516";// "970431031501" + DateTime.Now.ToString("HHmm");
            SessionVariables.CardOwner = "CARD OWNER 1516";// "CARD OWNER " + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            SessionVariables.MobileNumber = "8490981516";// "09098" + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            SessionVariables.CardValidDate = "10/15";
            // Check thong tin the tren server
            var cardProfile = Helper.GetProfile(SessionVariables.CardNumber);
            SessionVariables.IsRegister = (cardProfile != null);
            if (cardProfile != null)
            {
                SessionVariables.ProfileId = cardProfile._id;
                SessionVariables.FinanceAccount = Helper.GetAccountInfo();
            } 
            
            this.DialogResult = DialogResult.OK;
        }

        private void frmScanCard_Load(object sender, EventArgs e)
        {
            ScanCard();
        }
    }
}
