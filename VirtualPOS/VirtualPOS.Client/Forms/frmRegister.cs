using Newtonsoft.Json.Linq;
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
    public partial class frmRegister : Form
    {
        public string CardNumber {
            get { return this.lblCardNumber.Text; }
            set { this.lblCardNumber.Text = value; }
        }

        public string CardOwner
        {
            get { return this.lblCardOwner.Text; }
            set { this.lblCardOwner.Text = value; }
        }

        public string CardValidDate
        {
            get { return this.lblCardValidDate.Text; }
            set { this.lblCardValidDate.Text = value; }
        }

        public string MobileNumber
        {
            get { return this.txtMobileNumber.Text; }
            set { this.txtMobileNumber.Text = value; }
        }

        public frmRegister()
        {
            InitializeComponent();
        }
        
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string request = @"{system:'web_frontend', module:'profile', function:'register', type:'two_way', request:{full_name:'" 
            + CardOwner 
            + "', id:'" + CardNumber 
            + "', mobile:'" + MobileNumber + "'}}";
            dynamic response = JObject.Parse(Helper.RequestToServer(request));
            //MessageBox.Show("Keets")
        }

        private void frmRegister_Load(object sender, EventArgs e)
        {
            
        }
    }
}
