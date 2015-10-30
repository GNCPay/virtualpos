namespace VirtualPOS.Client.Forms
{
    partial class frmPayment
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPayment = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnhuy = new System.Windows.Forms.Button();
            this.txtBillAmount = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnPayment
            // 
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnPayment.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPayment.Location = new System.Drawing.Point(237, 197);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(63, 34);
            this.btnPayment.TabIndex = 19;
            this.btnPayment.Text = "OK";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(16, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "SỐ TIỀN";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBillNo.Location = new System.Drawing.Point(20, 71);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(280, 31);
            this.txtBillNo.TabIndex = 17;
            this.txtBillNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBillNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillNo_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(16, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "SỐ HÓA ĐƠN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(38, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 24);
            this.label1.TabIndex = 20;
            this.label1.Text = "THANH TOÁN DỊCH VỤ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnhuy
            // 
            this.btnhuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnhuy.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnhuy.Location = new System.Drawing.Point(158, 197);
            this.btnhuy.Name = "btnhuy";
            this.btnhuy.Size = new System.Drawing.Size(63, 34);
            this.btnhuy.TabIndex = 26;
            this.btnhuy.Text = "HUỶ";
            this.btnhuy.UseVisualStyleBackColor = true;
            this.btnhuy.Click += new System.EventHandler(this.btnhuy_Click);
            // 
            // txtBillAmount
            // 
            this.txtBillAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBillAmount.Location = new System.Drawing.Point(20, 142);
            this.txtBillAmount.Name = "txtBillAmount";
            this.txtBillAmount.Size = new System.Drawing.Size(280, 31);
            this.txtBillAmount.TabIndex = 27;
            this.txtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBillAmount.TextChanged += new System.EventHandler(this.txtBillAmount_TextChanged);
            this.txtBillAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillAmount_KeyPress_1);
            this.txtBillAmount.Leave += new System.EventHandler(this.txtBillAmount_Leave_1);
            // 
            // frmPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(316, 247);
            this.ControlBox = false;
            this.Controls.Add(this.txtBillAmount);
            this.Controls.Add(this.btnhuy);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmPayment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnhuy;
        private System.Windows.Forms.TextBox txtBillAmount;
    }
}