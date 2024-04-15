namespace BenpilsBarcodeSystem.Dialogs
{
    partial class LoadingDialog
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.AcceptBtn = new System.Windows.Forms.Button();
            this.TitleLbl = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.AcceptBtn);
            this.panel2.Controls.Add(this.TitleLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(319, 150);
            this.panel2.TabIndex = 86;
            // 
            // AcceptBtn
            // 
            this.AcceptBtn.BackColor = System.Drawing.Color.White;
            this.AcceptBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AcceptBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AcceptBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AcceptBtn.FlatAppearance.BorderSize = 0;
            this.AcceptBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptBtn.ForeColor = System.Drawing.Color.Black;
            this.AcceptBtn.Location = new System.Drawing.Point(87, 88);
            this.AcceptBtn.Name = "AcceptBtn";
            this.AcceptBtn.Size = new System.Drawing.Size(133, 35);
            this.AcceptBtn.TabIndex = 86;
            this.AcceptBtn.Text = "OK";
            this.AcceptBtn.UseVisualStyleBackColor = false;
            this.AcceptBtn.Visible = false;
            this.AcceptBtn.Click += new System.EventHandler(this.AcceptBtn_Click);
            // 
            // TitleLbl
            // 
            this.TitleLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLbl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.TitleLbl.Location = new System.Drawing.Point(0, 0);
            this.TitleLbl.Name = "TitleLbl";
            this.TitleLbl.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.TitleLbl.Size = new System.Drawing.Size(319, 73);
            this.TitleLbl.TabIndex = 85;
            this.TitleLbl.Text = "Backing up Data. Please wait ...";
            this.TitleLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoadingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(321, 152);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingDialog";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoadingDialog";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadingDialog_FormClosing);
            this.Load += new System.EventHandler(this.LoadingDialog_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label TitleLbl;
        private System.Windows.Forms.Button AcceptBtn;
    }
}