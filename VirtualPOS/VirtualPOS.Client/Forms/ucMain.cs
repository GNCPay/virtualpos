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
using MongoDB.Driver.Builders;
using MongoDB.Driver;
using System.Drawing.Printing;

namespace VirtualPOS.Client.Forms
{
    public partial class ucMain : UserControl
    {
        public ucMain()
        {
            InitializeComponent();           
        }

        private void lockAccount(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn khóa tài khoản này?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
        }

        private void registerCard(object sender, EventArgs e)
        {
            frmRegister frmRegister = new frmRegister();
            DialogResult registerResult = frmRegister.ShowDialog();
            if (registerResult == DialogResult.OK)
                EnableControl();
        }

        private void changePIN(object sender, EventArgs e)
        {
            frmChangePIN frmChangePIN = new frmChangePIN();
            frmChangePIN.ShowDialog();
        }

        private void statement(object sender, EventArgs e)
        {
            print();
        }

        public void print()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Courier New", 15);


            PaperSize psize = new PaperSize("Custom", 100, 200);
            //ps.DefaultPageSettings.PaperSize = psize;

            pd.Document = pdoc;
            pd.Document.DefaultPageSettings.PaperSize = psize;
            //pdoc.DefaultPageSettings.PaperSize.Height =320;
            pdoc.DefaultPageSettings.PaperSize.Height = 820;

            pdoc.DefaultPageSettings.PaperSize.Width = 520;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pdoc.Print();
        }

        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to Almaz!", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("THÔNG TIN GIAO DỊCH", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ:" + SessionVariables.CardNumber,
                     new Font("Courier New", 14),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng :" + SessionVariables.CardOwner,
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Loại :" + SessionVariables.CardType,
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "------------------------------------------";

           
            Offset = Offset + 20;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;

            graphics.DrawString(DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("HotLine: 094.9898.222", new Font("Courier New", 10),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("GDV - " + SessionVariables.TellerUser.UserName, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
        }

        private void cashIn(object sender, EventArgs e)
        {
            DialogResult cashInResult = new frmCashInOut(false).ShowDialog();
            if (cashInResult == DialogResult.OK) ScanCard();
        }

        private void cashOut(object sender, EventArgs e)
        {
            DialogResult cashInResult = new frmCashInOut(true).ShowDialog();
            if (cashInResult == DialogResult.OK) ScanCard();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.F2)
            {
                ScanCard();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void ScanCard()
        {
                var cardReaderResult = new frmScanCard().ShowDialog();
                if (cardReaderResult == DialogResult.OK) { EnableControl(); }
        }
        private void EnableControl()
        {
            try
            {
                if (!Processing.SessionVariables.IsRegister)
                {
                    MessageBox.Show("Thẻ chưa được đăng ký. Vui lòng đăng ký để có thể sử dụng các dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                pCardInfo.Reload();

                long total_page = 0;
                IMongoQuery query = null;
                string create_by = SessionVariables.CardId;
                if (!string.IsNullOrEmpty(create_by))
                    query = (query == null) ? Query.EQ("created_by", create_by) : Query.And(
                        query,
                        Query.EQ("created_by", create_by)
                        );

                var transactions = Helper.DataHelper.ListPagging("transactions", query, SortBy.Descending("system_created_time"),
                 5, 1,
                 out total_page);
                var list_accounts = (from e in transactions select e).Select(p => new
                {
                    created_by = p.created_by,
                    amount = p.amount.ToString("N0") + " VNĐ",
                    system_created_time = DateTime.ParseExact(p.system_created_time, "yyyyMMddHHmmss", null).ToString("HH:mm:ss dd/MM/yyyy"),
                    status = p.status
                }).ToArray();

                dataGridView2.DataSource = list_accounts;
                pPayment.Enabled = Processing.SessionVariables.IsRegister;
                btnCashIn.Enabled = Processing.SessionVariables.IsRegister;
                btnCashOut.Enabled = Processing.SessionVariables.IsRegister;
                btnChangePIN.Enabled = Processing.SessionVariables.IsRegister;
                btnLock.Enabled = Processing.SessionVariables.IsRegister;
                btnRegister.Enabled = !Processing.SessionVariables.IsRegister;
                btnStatement.Enabled = Processing.SessionVariables.IsRegister;
            }
            catch(Exception ex) { }
        }
        private void ucMain_Load(object sender, EventArgs e)
        {
            ScanCard();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
