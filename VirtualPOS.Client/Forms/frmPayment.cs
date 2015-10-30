using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VirtualPOS.Client.Processing;

namespace VirtualPOS.Client.Forms
{
    public partial class frmPayment : Form
    {       
        public frmPayment()
        {
            InitializeComponent();
        }
        public static class payment
        {
            public static long amount;
            public static string bill_no;
        }
        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBillAmount.Text == "" || txtBillNo.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập Số hoá đơn và số tiền thanh toán!", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    string mkaka = txtBillAmount.Text.Replace(".", "").Replace(",", "");
                    payment.amount = long.Parse(mkaka);
                    payment.bill_no = txtBillNo.Text;
                    this.Hide();
                    DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thanh toán hoá đơn số : "+ txtBillNo.Text + ", số tiền thanh toán: "+txtBillAmount.Text+ " ?", "Thông Báo !", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (new frmScanCard().ShowDialog() == DialogResult.OK)
                        {
                            PinRequest pir = new PinRequest();
                            DialogResult dpir = pir.ShowDialog();
                            ((ucMain)(this.Parent)).EnableControl();

                        }
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        MessageBox.Show("Bạn đã huỷ giao dịch thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        this.Close();
                        ((ucMain)(this.Parent)).EnableControl();
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void txtBillAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtBillAmount.Text.Equals("0"))
                    return;
                double temp = Convert.ToDouble(txtBillAmount.Text);
                txtBillAmount.Text = temp.ToString("#,###");
            }
            catch (Exception ex) { }
        }

        private void txtBillNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //check number
                e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
            }
            catch (Exception ex) { }    
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            
        }

        private void btnhuy_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Bạn đã huỷ thanh toán thành công!", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Close();
                ((ucMain)(this.Parent)).EnableControl();
            }
            catch (Exception ex) { } 
        }

        private void txtBillAmount_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                //check number
                e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)8;
            }
            catch (Exception ex) { }
        }

        private void txtBillAmount_Leave_1(object sender, EventArgs e)
        {
            //try
            //{
            //    if (txtBillAmount.Text.Equals("0"))
            //        return;
            //    double temp = Convert.ToDouble(txtBillAmount.Text);
            //    txtBillAmount.Text = temp.ToString("#,###");
            //}
            //catch (Exception ex) { }
        }

        //chèn ký tự ',' vào giữa số
        public void TachSo(TextBox luong)
        {
            try
            {
                string txt, txt1;
                txt1 = luong.Text.Replace(",", "");
                txt = "";
                int n = txt1.Length;
                int dem = 0;
                for (int i = n - 1; i >= 0; i--)
                {
                    if (dem == 2 && i != 0)
                    {
                        txt = "," + txt1.Substring(i, 1) + txt;
                        dem = 0;
                    }
                    else
                    {
                        txt = txt1.Substring(i, 1) + txt;
                        dem += 1;
                    }
                }
                luong.Text = txt;
                luong.SelectionStart = luong.Text.Length;
            }
            catch (Exception ex) { }
        }


        private void txtBillAmount_TextChanged(object sender, EventArgs e)
        {
            TachSo(txtBillAmount);
        }
    }
}
