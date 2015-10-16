namespace VirtualPOS.Client
{
    partial class frmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.pHeader = new System.Windows.Forms.Panel();
            this.pContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(11, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(390, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "GNC VIRTUAL POS CLIENT";
            // 
            // pHeader
            // 
            this.pHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(159)))), ((int)(((byte)(255)))));
            this.pHeader.Controls.Add(this.label1);
            this.pHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pHeader.Location = new System.Drawing.Point(0, 0);
            this.pHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pHeader.Name = "pHeader";
            this.pHeader.Size = new System.Drawing.Size(764, 65);
            this.pHeader.TabIndex = 0;
            // 
            // pContent
            // 
            this.pContent.Location = new System.Drawing.Point(0, 68);
            this.pContent.Name = "pContent";
            this.pContent.Size = new System.Drawing.Size(764, 464);
            this.pContent.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 535);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(764, 38);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "F2 - ĐỌC THẺ KH";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 573);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pContent);
            this.Controls.Add(this.pHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GNC Virtual POS";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listenShortCuts);
            this.pHeader.ResumeLayout(false);
            this.pHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pHeader;
        private System.Windows.Forms.Panel pContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
    }
}

