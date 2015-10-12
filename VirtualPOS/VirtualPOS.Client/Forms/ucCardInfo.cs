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
    public partial class ucCardInfo : UserControl
    {
        public ucCardInfo()
        {
            InitializeComponent();
        }

        private void ucCardInfo_Load(object sender, EventArgs e)
        {
            
        }
        public void Reload()
        {
            lblCardBalance.Text = String.Concat(SessionVariables.FinanceAccount.available_balance.ToString("N0"), " VNĐ");
            lblCardNumber.Text = SessionVariables.CardNumber;
            lblCardOwner.Text = SessionVariables.CardOwner;
            lblCardValidDate.Text = SessionVariables.CardValidDate;

        }
    }
}
