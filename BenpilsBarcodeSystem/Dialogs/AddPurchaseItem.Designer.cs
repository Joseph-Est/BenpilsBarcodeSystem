namespace BenpilsBarcodeSystem.Dialogs
{
    partial class AddPurchaseItem
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.panel15 = new System.Windows.Forms.Panel();
            this.AcceptBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.QuantityTxt = new System.Windows.Forms.TextBox();
            this.ExistingItemPanel = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ItemsCb = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TitleLbl = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel4.SuspendLayout();
            this.ExistingItemPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(1);
            this.panel1.Size = new System.Drawing.Size(356, 235);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.panel14);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.ExistingItemPanel);
            this.panel3.Controls.Add(this.ItemsCb);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 34);
            this.panel3.MinimumSize = new System.Drawing.Size(354, 200);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.panel3.Size = new System.Drawing.Size(354, 200);
            this.panel3.TabIndex = 87;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.panel16);
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(20, 125);
            this.panel14.MinimumSize = new System.Drawing.Size(314, 75);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(314, 75);
            this.panel14.TabIndex = 136;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.CancelBtn);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(157, 0);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(10, 20, 0, 20);
            this.panel16.Size = new System.Drawing.Size(157, 75);
            this.panel16.TabIndex = 130;
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.White;
            this.CancelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CancelBtn.FlatAppearance.BorderSize = 0;
            this.CancelBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.ForeColor = System.Drawing.Color.Black;
            this.CancelBtn.Location = new System.Drawing.Point(10, 20);
            this.CancelBtn.MaximumSize = new System.Drawing.Size(0, 35);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(147, 35);
            this.CancelBtn.TabIndex = 128;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.AcceptBtn);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Padding = new System.Windows.Forms.Padding(0, 20, 10, 20);
            this.panel15.Size = new System.Drawing.Size(157, 75);
            this.panel15.TabIndex = 129;
            // 
            // AcceptBtn
            // 
            this.AcceptBtn.BackColor = System.Drawing.Color.White;
            this.AcceptBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AcceptBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AcceptBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AcceptBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AcceptBtn.FlatAppearance.BorderSize = 0;
            this.AcceptBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptBtn.ForeColor = System.Drawing.Color.Black;
            this.AcceptBtn.Location = new System.Drawing.Point(0, 20);
            this.AcceptBtn.MaximumSize = new System.Drawing.Size(0, 35);
            this.AcceptBtn.Name = "AcceptBtn";
            this.AcceptBtn.Size = new System.Drawing.Size(147, 35);
            this.AcceptBtn.TabIndex = 127;
            this.AcceptBtn.Text = "Add";
            this.AcceptBtn.UseVisualStyleBackColor = false;
            this.AcceptBtn.Click += new System.EventHandler(this.AcceptBtn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.QuantityTxt);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(20, 90);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel4.Size = new System.Drawing.Size(314, 35);
            this.panel4.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quantity :";
            // 
            // QuantityTxt
            // 
            this.QuantityTxt.Dock = System.Windows.Forms.DockStyle.Right;
            this.QuantityTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityTxt.Location = new System.Drawing.Point(76, 10);
            this.QuantityTxt.MaxLength = 9;
            this.QuantityTxt.Multiline = true;
            this.QuantityTxt.Name = "QuantityTxt";
            this.QuantityTxt.Size = new System.Drawing.Size(238, 25);
            this.QuantityTxt.TabIndex = 2;
            // 
            // ExistingItemPanel
            // 
            this.ExistingItemPanel.Controls.Add(this.comboBox1);
            this.ExistingItemPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ExistingItemPanel.Location = new System.Drawing.Point(20, 44);
            this.ExistingItemPanel.Name = "ExistingItemPanel";
            this.ExistingItemPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.ExistingItemPanel.Size = new System.Drawing.Size(314, 46);
            this.ExistingItemPanel.TabIndex = 137;
            this.ExistingItemPanel.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Arial", 9.75F);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(0, 10);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(314, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // ItemsCb
            // 
            this.ItemsCb.Dock = System.Windows.Forms.DockStyle.Top;
            this.ItemsCb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ItemsCb.Font = new System.Drawing.Font("Arial", 9.75F);
            this.ItemsCb.FormattingEnabled = true;
            this.ItemsCb.Location = new System.Drawing.Point(20, 20);
            this.ItemsCb.MinimumSize = new System.Drawing.Size(314, 0);
            this.ItemsCb.Name = "ItemsCb";
            this.ItemsCb.Size = new System.Drawing.Size(314, 24);
            this.ItemsCb.TabIndex = 1;
            this.ItemsCb.SelectedIndexChanged += new System.EventHandler(this.ItemsCb_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel2.Controls.Add(this.TitleLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.panel2.Size = new System.Drawing.Size(354, 33);
            this.panel2.TabIndex = 86;
            // 
            // TitleLbl
            // 
            this.TitleLbl.AutoSize = true;
            this.TitleLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLbl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.TitleLbl.ForeColor = System.Drawing.Color.White;
            this.TitleLbl.Location = new System.Drawing.Point(8, 8);
            this.TitleLbl.Name = "TitleLbl";
            this.TitleLbl.Size = new System.Drawing.Size(133, 18);
            this.TitleLbl.TabIndex = 84;
            this.TitleLbl.Text = "Add Supplier Item";
            // 
            // AddPurchaseItem
            // 
            this.AcceptButton = this.AcceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(356, 235);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(356, 200);
            this.Name = "AddPurchaseItem";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddPurchaseItem_FormClosing);
            this.Load += new System.EventHandler(this.AddPurchaseItem_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel15.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ExistingItemPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label TitleLbl;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox QuantityTxt;
        private System.Windows.Forms.ComboBox ItemsCb;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button AcceptBtn;
        private System.Windows.Forms.Panel ExistingItemPanel;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}