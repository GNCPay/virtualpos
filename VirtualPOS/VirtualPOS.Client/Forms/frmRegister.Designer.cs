﻿namespace VirtualPOS.Client.Forms
{
    partial class frmRegister
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
            this.pScanned = new System.Windows.Forms.Panel();
            this.txtMobileNumber = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.lblCardValidDate = new System.Windows.Forms.Label();
            this.lblCardOwner = new System.Windows.Forms.Label();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pScanned.SuspendLayout();
            this.SuspendLayout();
            // 
            // pScanned
            // 
            this.pScanned.Controls.Add(this.txtMobileNumber);
            this.pScanned.Controls.Add(this.btnCancel);
            this.pScanned.Controls.Add(this.btnRegister);
            this.pScanned.Controls.Add(this.lblCardValidDate);
            this.pScanned.Controls.Add(this.lblCardOwner);
            this.pScanned.Controls.Add(this.lblCardNumber);
            this.pScanned.Controls.Add(this.label5);
            this.pScanned.Controls.Add(this.label4);
            this.pScanned.Controls.Add(this.label3);
            this.pScanned.Controls.Add(this.label2);
            this.pScanned.Controls.Add(this.label1);
            this.pScanned.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pScanned.Location = new System.Drawing.Point(0, 0);
            this.pScanned.Name = "pScanned";
            this.pScanned.Size = new System.Drawing.Size(382, 194);
            this.pScanned.TabIndex = 0;
            // 
            // txtMobileNumber
            // 
            this.txtMobileNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNumber.Location = new System.Drawing.Point(172, 117);
            this.txtMobileNumber.Name = "txtMobileNumber";
            this.txtMobileNumber.Size = new System.Drawing.Size(164, 26);
            this.txtMobileNumber.TabIndex = 35;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(221, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 34);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Location = new System.Drawing.Point(298, 155);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(71, 34);
            this.btnRegister.TabIndex = 33;
            this.btnRegister.Text = "OK";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // lblCardValidDate
            // 
            this.lblCardValidDate.AutoSize = true;
            this.lblCardValidDate.Location = new System.Drawing.Point(168, 93);
            this.lblCardValidDate.Name = "lblCardValidDate";
            this.lblCardValidDate.Size = new System.Drawing.Size(89, 20);
            this.lblCardValidDate.TabIndex = 32;
            this.lblCardValidDate.Text = "07/10/2015";
            // 
            // lblCardOwner
            // 
            this.lblCardOwner.AutoSize = true;
            this.lblCardOwner.Location = new System.Drawing.Point(168, 65);
            this.lblCardOwner.Name = "lblCardOwner";
            this.lblCardOwner.Size = new System.Drawing.Size(180, 20);
            this.lblCardOwner.TabIndex = 31;
            this.lblCardOwner.Text = "NGUYEN QUANG HUY";
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCardNumber.Location = new System.Drawing.Point(168, 38);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(169, 20);
            this.lblCardNumber.TabIndex = 30;
            this.lblCardNumber.Text = "1234567890123456";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 20);
            this.label5.TabIndex = 29;
            this.label5.Text = "SỐ DI ĐỘNG";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "NGÀY MỞ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 20);
            this.label3.TabIndex = 27;
            this.label3.Text = "TÊN CHỦ THẺ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 20);
            this.label2.TabIndex = 26;
            this.label2.Text = "SỐ THẺ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(93, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 24);
            this.label1.TabIndex = 25;
            this.label1.Text = "ĐĂNG KÝ TÀI KHOẢN VÍ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(382, 194);
            this.ControlBox = false;
            this.Controls.Add(this.pScanned);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmRegister_Load);
            this.pScanned.ResumeLayout(false);
            this.pScanned.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pScanned;
        private System.Windows.Forms.TextBox txtMobileNumber;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label lblCardValidDate;
        private System.Windows.Forms.Label lblCardOwner;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}