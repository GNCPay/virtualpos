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
            lblChuThe.Text = SessionVariables.CardOwner;
            lblCardBalance.Text = (SessionVariables.IsRegister) ? String.Concat(SessionVariables.FinanceAccount.available_balance.ToString("N0"), " VNĐ") : "CHƯA ĐĂNG KÝ";
            lblCardNumber.Text = SessionVariables.CardNumber;
            lblCardType.Text = SessionVariables.CardType;
            lblPrepaidAmount.Text = SessionVariables.CardPrepaidAmount.ToString("N0") + " VNĐ";

        }
    }
}
