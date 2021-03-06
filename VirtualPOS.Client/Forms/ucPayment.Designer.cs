﻿namespace VirtualPOS.Client.Forms
{
    partial class ucPayment
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPayment = new System.Windows.Forms.Button();
            this.txtBillAmount = new System.Windows.Forms.MaskedTextBox();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.lblPIN = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(37, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "THANH TOÁN DỊCH VỤ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(15, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "SỐ HÓA ĐƠN";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Enabled = false;
            this.txtBillNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBillNo.Location = new System.Drawing.Point(19, 70);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(280, 31);
            this.txtBillNo.TabIndex = 0;
            this.txtBillNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBillNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillNo_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(15, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "SỐ TIỀN";
            // 
            // btnPayment
            // 
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPayment.Location = new System.Drawing.Point(236, 235);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(63, 34);
            this.btnPayment.TabIndex = 2;
            this.btnPayment.Text = "OK";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // txtBillAmount
            // 
            this.txtBillAmount.Enabled = false;
            this.txtBillAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBillAmount.Location = new System.Drawing.Point(19, 132);
            this.txtBillAmount.Name = "txtBillAmount";
            this.txtBillAmount.Size = new System.Drawing.Size(280, 35);
            this.txtBillAmount.TabIndex = 1;
            this.txtBillAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBillAmount.TextChanged += new System.EventHandler(this.txtBillAmount_TextChanged);
            this.txtBillAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBillAmount_KeyPress);
            this.txtBillAmount.Leave += new System.EventHandler(this.txtBillAmount_Leave);
            // 
            // txtPIN
            // 
            this.txtPIN.Enabled = false;
            this.txtPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtPIN.Location = new System.Drawing.Point(19, 198);
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.PasswordChar = '*';
            this.txtPIN.Size = new System.Drawing.Size(280, 31);
            this.txtPIN.TabIndex = 14;
            this.txtPIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPIN
            // 
            this.lblPIN.AutoSize = true;
            this.lblPIN.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblPIN.Location = new System.Drawing.Point(15, 175);
            this.lblPIN.Name = "lblPIN";
            this.lblPIN.Size = new System.Drawing.Size(89, 20);
            this.lblPIN.TabIndex = 15;
            this.lblPIN.Text = "MÃ PIN KH";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(19, 235);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(196, 34);
            this.progressBar1.TabIndex = 16;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ucPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtPIN);
            this.Controls.Add(this.lblPIN);
            this.Controls.Add(this.txtBillAmount);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ucPayment";
            this.Size = new System.Drawing.Size(318, 282);
            this.Load += new System.EventHandler(this.ucPayment_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.MaskedTextBox txtBillAmount;
        private System.Windows.Forms.TextBox txtPIN;
        private System.Windows.Forms.Label lblPIN;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
    }
}
