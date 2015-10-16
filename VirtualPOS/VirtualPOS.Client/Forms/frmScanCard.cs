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
            Helper.CheckCard();

            //SessionVariables.CardNumber = "9704310315011516";// "970431031501" + DateTime.Now.ToString("HHmm");
            //SessionVariables.CardOwner = "CARD OWNER 1516";// "CARD OWNER " + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            //SessionVariables.MobileNumber = "8490981516";// "09098" + SessionVariables.CardNumber.Substring(SessionVariables.CardNumber.Length - 4);
            //SessionVariables.CardValidDate = "10/15";
            // Check thong tin the tren server
            var cardProfile = Helper.GetProfile();
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
            //ScanCard();
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
    }
}
