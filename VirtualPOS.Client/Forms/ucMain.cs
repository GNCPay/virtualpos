﻿using System;
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
using Newtonsoft.Json.Linq;
using System.IO;
using MongoDB.Bson;

namespace VirtualPOS.Client.Forms
{
    public partial class ucMain : UserControl
    {
        public ucMain()
        {
            InitializeComponent();
            lbuser2.Text = "GDV : " + ucLogin.usergd.userGD;
            try
            {
                Image img = Image.FromFile(@"img\almaz.bmp");
                pictureBox1.Image = img;
            }
            catch (Exception ex) { }
            timer1.Enabled = true;
            timer1.Interval = 1000;
        }

        private void lockAccount(object sender, EventArgs e)
        {
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn khóa tài khoản này?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    if (new frmScanCard().ShowDialog() == DialogResult.OK)
                    {
                         var user_name = Processing.SessionVariables.CardId;
                         dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                        if (user_name != null)
                        {
                            if (profile.Status != "LOCKED")
                            {
                                updateLock(profile._id, "LOCKED");
                                MessageBox.Show("Bạn đã khoá tài khoản thành công !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                btnLock.Enabled = false;
                            }
                            else
                                MessageBox.Show("Tài khoản đã bị khoá !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                btnLock.Enabled = false;
                        }
                        else
                            MessageBox.Show("Vui lòng đăng nhập để thực hiện khoá tài khoản !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                catch (Exception ex) { }
            }
            else
                MessageBox.Show("Bạn đã huỷ thành công !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void updateLock(ObjectId _id, string Status)
        {
            //var id = new ObjectId(_id);
            dynamic user = Helper.DataHelper.Get("users", Query.EQ("_id", _id));
            user.Status = Status;
            Helper.DataHelper.SaveUpdate("users", user);
        }

        private void registerCard(object sender, EventArgs e)
        {
            try
            {
                if (new frmScanCard().ShowDialog() == DialogResult.OK)
                {
                    var user_name = Processing.SessionVariables.CardId;
                    if (user_name != null)
                    {
                        dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                        if (profile.Status == "LOCKED")
                        {
                            MessageBox.Show("Tài khoản thẻ đang bị khoá vui lòng liên hệ GDV để được hỗ trợ !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                        else
                            EnableControl();
                    }
                    else
                    {
                        frmRegister frmRegister = new frmRegister();
                        DialogResult registerResult = frmRegister.ShowDialog();
                        //if (registerResult == DialogResult.OK)
                        //    EnableControl();
                        EnableControl();
                    }
                }
            }
            catch (Exception ex) { }         
        }

        private void changePIN(object sender, EventArgs e)
        {
            if (new frmScanCard().ShowDialog() == DialogResult.OK)
            {
                frmChangePIN frmChangePIN = new frmChangePIN();
                frmChangePIN.ShowDialog();
                try
                {
                    var user_name = Processing.SessionVariables.CardId;
                    dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                    if (profile.Status != "LOCKED")
                    {
                        EnableControl();
                    }
                }
                catch (Exception ex) { } 
            }
        }

        private void statement(object sender, EventArgs e)
        {
            if (new frmScanCard().ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var user_name = Processing.SessionVariables.CardId;
                    dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                    if (profile.Status != "LOCKED")
                    {
                        print();
                        EnableControl(); 
                    }
                    else
                        MessageBox.Show("Tài khoản thẻ đang bị khoá vui lòng liên hệ GDV để được hỗ trợ !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception ex) { }     
            }
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
            //dynamic profile= Helper.DataHelper.Get("transactions", Query.EQ("created_by", SessionVariables.CardId));

            //long total_page = 0;
            //IMongoQuery query = null;
            //string create_by = SessionVariables.CardId;
            //if (!string.IsNullOrEmpty(create_by))
            //    query = (query == null) ? Query.EQ("created_by", create_by) : Query.And(
            //        query,
            //        Query.EQ("created_by", create_by)
            //        );

            //var transactions = Helper.DataHelper.ListPagging("transactions", query, SortBy.Descending("system_created_time"),
            // 5, 1,
            // out total_page);
            //var list_accounts = (from a in transactions select a).Select(p => new
            //{
            //    created_by = p.created_by,
            //    amount = p.amount.ToString("N0") + " VNĐ",
            //    system_created_time = DateTime.ParseExact(p.system_created_time, "yyyyMMddHHmmss", null).ToString("HH:mm:ss dd/MM/yyyy"),
            //    transaction_type = p.transaction_type,
            //    status = p.status
            //}).ToArray();

            JArray transactions = Helper.GetStatement();
            var list_accounts = (from a in transactions select a).Select(p => new
            {
                created_by = p["ActionBy"].ToString(),
                amount = long.Parse(p["Amount"].ToString()).ToString("N0") + " VNĐ",
                start_balance = long.Parse(p["StartBalance"].ToString()).ToString("N0") + " VNĐ",
                end_balance = long.Parse(p["EndBalance"].ToString()).ToString("N0") + " VNĐ",
                transaction_type = p["ActionCode"].ToString(),
                system_created_time = ((DateTime)p["ActionTime"]).ToString("HH:mm:ss dd/MM/yyyy"),
                status = "COMPLETED"
            }).ToArray();
            
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 50;
            int startY = 55;
            int Offset = 40;
            graphics.DrawString("Welcome to Almaz!", new Font("Courier New", 14),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("THÔNG TIN GIAO DỊCH", new Font("Courier New", 11),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Số thẻ:" + SessionVariables.CardNumber,
                     new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Khách hàng :",
                   new Font("Courier New", 12),
                   new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString(SessionVariables.CardOwner,
                  new Font("Courier New", 10),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Loại Thẻ:" + SessionVariables.CardType,
                     new Font("Courier New", 12),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            String underLine = "-----------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 40;
            //graphics.DrawString("Số tiền        |   Loại Giao Dịch  |  Ngày tạo", new Font("Courier New", 9), new
            //          SolidBrush(Color.Black), startX, startY + Offset);
            //Offset = Offset + 20;
            try
            {
                for (int i = 0; i < list_accounts.Length; i++)
                {
                    var p = list_accounts[i];
                    //graphics.DrawString(p.amount + p.transaction_type.ToString().PadLeft(15) + p.system_created_time.ToString().PadLeft(35), new Font("Courier New", 8), new
                    //SolidBrush(Color.Black), startX, startY + Offset);
                    //Offset = Offset + 10;


                    graphics.DrawString("Số tiền: " + p.amount, new Font("Courier New", 8), new
                       SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;


                    graphics.DrawString("Loại Giao Dịch: " + p.transaction_type, new Font("Courier New", 8), new
                        SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 20;

                    graphics.DrawString("Ngày tạo: " + p.system_created_time, new Font("Courier New", 8), new
                        SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 30;
                }
            }
            catch (Exception ex) { }
            
           
            Offset = Offset + 20;
            underLine = "-----------------------";
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
            Offset = Offset + 20;
            graphics.DrawString(underLine, new Font("Courier New", 10),
                    new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Thank You! And See You Again.", new Font("Courier New", 10),
                  new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
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
            if (keyData == Keys.F2)
            {
                ScanCard();               
            }         
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void ScanCard()
        {
            var cardReaderResult = new frmScanCard().ShowDialog();
            try
            {
                var user_name = Processing.SessionVariables.CardId;
                dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", user_name));
                if (profile.Status != "LOCKED")
                {
                    if (cardReaderResult == DialogResult.OK) { EnableControl(); }
                }
                else
                    MessageBox.Show("Tài khoản thẻ đang bị khoá vui lòng liên hệ GDV để được hỗ trợ !", "Thông Báo !", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex) { }
          
            
        }
        public void EnableControl()
        {           
            try
            {
                //long total_page = 0;
                //IMongoQuery query = null;
                //string create_by = SessionVariables.CardId;
                //if (!string.IsNullOrEmpty(create_by))
                //    query = (query == null) ? Query.EQ("created_by", create_by) : Query.And(
                //        query,
                //        Query.EQ("created_by", create_by)
                //        );

                //var transactions = Helper.DataHelper.ListPagging("transactions", query, SortBy.Descending("system_created_time"),
                // 5, 1,
                // out total_page);
                                
                //var list_accounts = (from e in transactions select e).Select(p => new
                //{
                //    created_by = p.created_by,
                //    amount = p.amount.ToString("N0") + " VNĐ",
                //    system_created_time = DateTime.ParseExact(p.system_created_time, "yyyyMMddHHmmss", null).ToString("HH:mm:ss dd/MM/yyyy"),
                //    status = p.status
                //}).ToArray();

                JArray transactions = Helper.GetStatement();
                var list_accounts = (from a in transactions select a).Select(p => new
                {
                    created_by = p["ActionBy"].ToString(),
                    amount = long.Parse(p["Amount"].ToString()).ToString("N0") + " VNĐ",
                    //end_balance = long.Parse(p["EndBalance"].ToString()).ToString("N0") + " VNĐ",
                    //transaction_type = p["ActionCode"].ToString(),
                    system_created_time = ((DateTime)p["ActionTime"]).ToString("HH:mm:ss dd/MM/yyyy"),
                    status = "COMPLETED"
                }).ToArray();

                if (!Processing.SessionVariables.IsRegister)
                {
                    MessageBox.Show("Thẻ chưa được đăng ký. Vui lòng đăng ký để có thể sử dụng các dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnRegister.Text = "Đăng ký";
                }
                else
                {
                    //btnRegister.Text = "Welcome";
                    btnRegister.Enabled = false;
                    //pCardInfo.Reload();                  
                    dataGridView2.DataSource = list_accounts;
                }             
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
            //ScanCard();                     
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbtime.Text ="Thời gian : "+ DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
        }

        private void pPayment_Load(object sender, EventArgs e)
        {
        }
    }
}
