namespace VirtualPOS.Client.Forms
{
    partial class frmLogin
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
            this.ucAlmaz1 = new VirtualPOS.Client.Forms.ucAlmaz();
            this.SuspendLayout();
            // 
            // ucAlmaz1
            // 
            this.ucAlmaz1.BackColor = System.Drawing.Color.White;
            this.ucAlmaz1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucAlmaz1.Location = new System.Drawing.Point(0, 0);
            this.ucAlmaz1.Name = "ucAlmaz1";
            this.ucAlmaz1.Size = new System.Drawing.Size(409, 189);
            this.ucAlmaz1.TabIndex = 0;
            this.ucAlmaz1.Load += new System.EventHandler(this.ucAlmaz1_Load);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 189);
            this.ControlBox = false;
            this.Controls.Add(this.ucAlmaz1);
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập Hệ thống";
            this.ResumeLayout(false);

        }

        #endregion

        private ucAlmaz ucAlmaz1;
    }
}