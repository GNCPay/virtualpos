namespace VirtualPOS.Client.Forms
{
    partial class ucMain
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMain));
            this.pTransaction = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ngay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amont = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.pAction = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnthanhtoan = new System.Windows.Forms.Button();
            this.btnStatement = new System.Windows.Forms.Button();
            this.btnChangePIN = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnCashIn = new System.Windows.Forms.Button();
            this.btnCashOut = new System.Windows.Forms.Button();
            this.dataSet1 = new System.Data.DataSet();
            this.dataSet2 = new System.Data.DataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbuser2 = new System.Windows.Forms.Label();
            this.lbuser = new System.Windows.Forms.Label();
            this.lbtime = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.pAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pTransaction
            // 
            this.pTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.pTransaction.Controls.Add(this.dataGridView2);
            this.pTransaction.Controls.Add(this.label1);
            this.pTransaction.Location = new System.Drawing.Point(15, 356);
            this.pTransaction.Name = "pTransaction";
            this.pTransaction.Size = new System.Drawing.Size(740, 208);
            this.pTransaction.TabIndex = 3;
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Ngay,
            this.amont,
            this.status});
            this.dataGridView2.Location = new System.Drawing.Point(2, 37);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(737, 167);
            this.dataGridView2.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "created_by";
            this.Column1.HeaderText = "Người Tạo";
            this.Column1.Name = "Column1";
            this.Column1.Width = 170;
            // 
            // Ngay
            // 
            this.Ngay.DataPropertyName = "amount";
            this.Ngay.HeaderText = "Số Tiền";
            this.Ngay.Name = "Ngay";
            this.Ngay.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Ngay.Width = 170;
            // 
            // amont
            // 
            this.amont.DataPropertyName = "system_created_time";
            this.amont.HeaderText = "Ngày Tạo";
            this.amont.Name = "amont";
            this.amont.Width = 190;
            // 
            // status
            // 
            this.status.DataPropertyName = "status";
            this.status.HeaderText = "Trạng Thái";
            this.status.Name = "status";
            this.status.Width = 164;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Giao dịch gần nhất";
            // 
            // pAction
            // 
            this.pAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.pAction.Controls.Add(this.pictureBox4);
            this.pAction.Controls.Add(this.btnthanhtoan);
            this.pAction.Controls.Add(this.btnStatement);
            this.pAction.Controls.Add(this.btnChangePIN);
            this.pAction.Controls.Add(this.btnRegister);
            this.pAction.Location = new System.Drawing.Point(0, 297);
            this.pAction.Name = "pAction";
            this.pAction.Size = new System.Drawing.Size(770, 70);
            this.pAction.TabIndex = 2;
            // 
            // pictureBox4
            // 
            this.pictureBox4.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox4.ErrorImage")));
            this.pictureBox4.ImageLocation = "//";
            this.pictureBox4.Location = new System.Drawing.Point(3, 52);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(767, 18);
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // btnthanhtoan
            // 
            this.btnthanhtoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnthanhtoan.Location = new System.Drawing.Point(412, 10);
            this.btnthanhtoan.Name = "btnthanhtoan";
            this.btnthanhtoan.Size = new System.Drawing.Size(129, 36);
            this.btnthanhtoan.TabIndex = 4;
            this.btnthanhtoan.Text = "THANH TOÁN";
            this.btnthanhtoan.UseVisualStyleBackColor = true;
            this.btnthanhtoan.Click += new System.EventHandler(this.btnthanhtoan_Click);
            // 
            // btnStatement
            // 
            this.btnStatement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatement.Location = new System.Drawing.Point(591, 10);
            this.btnStatement.Name = "btnStatement";
            this.btnStatement.Size = new System.Drawing.Size(109, 36);
            this.btnStatement.TabIndex = 3;
            this.btnStatement.Text = "SAO KÊ GD";
            this.btnStatement.UseVisualStyleBackColor = true;
            this.btnStatement.Click += new System.EventHandler(this.statement);
            // 
            // btnChangePIN
            // 
            this.btnChangePIN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePIN.Location = new System.Drawing.Point(255, 10);
            this.btnChangePIN.Name = "btnChangePIN";
            this.btnChangePIN.Size = new System.Drawing.Size(109, 36);
            this.btnChangePIN.TabIndex = 2;
            this.btnChangePIN.Text = "ĐỔI PIN";
            this.btnChangePIN.UseVisualStyleBackColor = true;
            this.btnChangePIN.Click += new System.EventHandler(this.changePIN);
            // 
            // btnRegister
            // 
            this.btnRegister.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(255)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(255)))));
            this.btnRegister.Location = new System.Drawing.Point(46, 10);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(180, 36);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "BÁN THẺ / CẬP NHẬT";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.registerCard);
            // 
            // btnLock
            // 
            this.btnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLock.Location = new System.Drawing.Point(412, 249);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(109, 36);
            this.btnLock.TabIndex = 1;
            this.btnLock.Text = "KHÓA THẺ";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Visible = false;
            this.btnLock.Click += new System.EventHandler(this.lockAccount);
            // 
            // btnCashIn
            // 
            this.btnCashIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashIn.Location = new System.Drawing.Point(642, 249);
            this.btnCashIn.Name = "btnCashIn";
            this.btnCashIn.Size = new System.Drawing.Size(109, 36);
            this.btnCashIn.TabIndex = 4;
            this.btnCashIn.Text = "Nạp tiền";
            this.btnCashIn.UseVisualStyleBackColor = true;
            this.btnCashIn.Visible = false;
            this.btnCashIn.Click += new System.EventHandler(this.cashIn);
            // 
            // btnCashOut
            // 
            this.btnCashOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCashOut.Location = new System.Drawing.Point(527, 249);
            this.btnCashOut.Name = "btnCashOut";
            this.btnCashOut.Size = new System.Drawing.Size(109, 36);
            this.btnCashOut.TabIndex = 5;
            this.btnCashOut.Text = "Rút tiền";
            this.btnCashOut.UseVisualStyleBackColor = true;
            this.btnCashOut.Visible = false;
            this.btnCashOut.Click += new System.EventHandler(this.cashOut);
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // dataSet2
            // 
            this.dataSet2.DataSetName = "NewDataSet";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lbuser2);
            this.panel1.Controls.Add(this.lbuser);
            this.panel1.Controls.Add(this.lbtime);
            this.panel1.Location = new System.Drawing.Point(3, 568);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 36);
            this.panel1.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(595, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Thời gian :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Giao dịch viên :";
            // 
            // lbuser2
            // 
            this.lbuser2.AutoSize = true;
            this.lbuser2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbuser2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(255)))));
            this.lbuser2.Location = new System.Drawing.Point(121, 5);
            this.lbuser2.Name = "lbuser2";
            this.lbuser2.Size = new System.Drawing.Size(24, 25);
            this.lbuser2.TabIndex = 7;
            this.lbuser2.Text = "...";
            // 
            // lbuser
            // 
            this.lbuser.AutoSize = true;
            this.lbuser.Location = new System.Drawing.Point(545, -16);
            this.lbuser.Name = "lbuser";
            this.lbuser.Size = new System.Drawing.Size(51, 20);
            this.lbuser.TabIndex = 0;
            this.lbuser.Text = "label2";
            // 
            // lbtime
            // 
            this.lbtime.AutoSize = true;
            this.lbtime.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbtime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(255)))));
            this.lbtime.Location = new System.Drawing.Point(673, 5);
            this.lbtime.Name = "lbtime";
            this.lbtime.Size = new System.Drawing.Size(24, 25);
            this.lbtime.TabIndex = 8;
            this.lbtime.Text = "...";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.ImageLocation = "almaz.bmp";
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(316, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(439, 285);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(39, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 40);
            this.label2.TabIndex = 10;
            this.label2.Text = "WELCOME TO";
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.ErrorImage")));
            this.pictureBox2.ImageLocation = "//";
            this.pictureBox2.Location = new System.Drawing.Point(10, 73);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 197);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.ErrorImage")));
            this.pictureBox3.ImageLocation = "//";
            this.pictureBox3.Location = new System.Drawing.Point(7, 267);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(252, 24);
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // ucMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLock);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCashIn);
            this.Controls.Add(this.pAction);
            this.Controls.Add(this.pTransaction);
            this.Controls.Add(this.btnCashOut);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucMain";
            this.Size = new System.Drawing.Size(773, 607);
            this.Load += new System.EventHandler(this.ucMain_Load);
            this.pTransaction.ResumeLayout(false);
            this.pTransaction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.pAction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ucCardInfo pCardInfo;
        private ucPayment pPayment;
        private System.Windows.Forms.Panel pTransaction;
        private System.Windows.Forms.Panel pAction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCashOut;
        private System.Windows.Forms.Button btnCashIn;
        private System.Windows.Forms.Button btnLock;
        public System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnStatement;
        private System.Windows.Forms.Button btnChangePIN;
        private System.Data.DataSet dataSet1;
        private System.Data.DataSet dataSet2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbuser;
        private System.Windows.Forms.Label lbuser2;
        private System.Windows.Forms.Label lbtime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ngay;
        private System.Windows.Forms.DataGridViewTextBoxColumn amont;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.Button btnthanhtoan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
