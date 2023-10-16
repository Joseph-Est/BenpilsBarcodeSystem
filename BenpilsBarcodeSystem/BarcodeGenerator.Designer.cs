namespace BenpilsBarcodeSystem
{
    partial class BarcodeGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BarcodeGenerator));
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.BarcodeLabel = new System.Windows.Forms.Label();
            this.Encodetxt = new System.Windows.Forms.TextBox();
            this.EncodeBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.DecodeBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DecodeTxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox13
            // 
            this.pictureBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox13.Location = new System.Drawing.Point(120, 119);
            this.pictureBox13.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(297, 80);
            this.pictureBox13.TabIndex = 1;
            this.pictureBox13.TabStop = false;
            this.pictureBox13.UseWaitCursor = true;
            // 
            // BarcodeLabel
            // 
            this.BarcodeLabel.AutoSize = true;
            this.BarcodeLabel.Location = new System.Drawing.Point(29, 262);
            this.BarcodeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BarcodeLabel.Name = "BarcodeLabel";
            this.BarcodeLabel.Size = new System.Drawing.Size(57, 16);
            this.BarcodeLabel.TabIndex = 2;
            this.BarcodeLabel.Text = "Encode:";
            this.BarcodeLabel.UseWaitCursor = true;
            // 
            // Encodetxt
            // 
            this.Encodetxt.Location = new System.Drawing.Point(104, 254);
            this.Encodetxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Encodetxt.Name = "Encodetxt";
            this.Encodetxt.Size = new System.Drawing.Size(296, 22);
            this.Encodetxt.TabIndex = 3;
            this.Encodetxt.UseWaitCursor = true;
            // 
            // EncodeBtn
            // 
            this.EncodeBtn.Location = new System.Drawing.Point(417, 248);
            this.EncodeBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.EncodeBtn.Name = "EncodeBtn";
            this.EncodeBtn.Size = new System.Drawing.Size(100, 28);
            this.EncodeBtn.TabIndex = 4;
            this.EncodeBtn.Text = "Encode";
            this.EncodeBtn.UseVisualStyleBackColor = true;
            this.EncodeBtn.UseWaitCursor = true;
            this.EncodeBtn.Click += new System.EventHandler(this.EncodeBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 43);
            this.panel1.TabIndex = 16;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(499, 4);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(41, 39);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // DecodeBtn
            // 
            this.DecodeBtn.Location = new System.Drawing.Point(417, 284);
            this.DecodeBtn.Margin = new System.Windows.Forms.Padding(4);
            this.DecodeBtn.Name = "DecodeBtn";
            this.DecodeBtn.Size = new System.Drawing.Size(100, 28);
            this.DecodeBtn.TabIndex = 17;
            this.DecodeBtn.Text = "Decode";
            this.DecodeBtn.UseVisualStyleBackColor = true;
            this.DecodeBtn.UseWaitCursor = true;
            this.DecodeBtn.Click += new System.EventHandler(this.DecodeBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 290);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "Decode:";
            this.label1.UseWaitCursor = true;
            // 
            // DecodeTxt
            // 
            this.DecodeTxt.Location = new System.Drawing.Point(104, 290);
            this.DecodeTxt.Margin = new System.Windows.Forms.Padding(4);
            this.DecodeTxt.Name = "DecodeTxt";
            this.DecodeTxt.Size = new System.Drawing.Size(296, 22);
            this.DecodeTxt.TabIndex = 19;
            this.DecodeTxt.UseWaitCursor = true;
            // 
            // BarcodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(548, 373);
            this.Controls.Add(this.DecodeTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DecodeBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.EncodeBtn);
            this.Controls.Add(this.Encodetxt);
            this.Controls.Add(this.BarcodeLabel);
            this.Controls.Add(this.pictureBox13);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "BarcodeGenerator";
            this.Text = "BarcodeGenerator";
            this.Load += new System.EventHandler(this.BarcodeGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.Label BarcodeLabel;
        private System.Windows.Forms.TextBox Encodetxt;
        private System.Windows.Forms.Button EncodeBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button DecodeBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DecodeTxt;
    }
}