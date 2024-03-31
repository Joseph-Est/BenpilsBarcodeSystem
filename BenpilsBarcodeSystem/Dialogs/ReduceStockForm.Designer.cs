namespace BenpilsBarcodeSystem
{
    partial class ReduceStockForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ReduceTxt = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ErrorTxt = new System.Windows.Forms.TextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.AcceptBtn = new System.Windows.Forms.Button();
            this.ReasonCb = new System.Windows.Forms.ComboBox();
            this.StockLbl = new System.Windows.Forms.Label();
            this.SizeLbl = new System.Windows.Forms.Label();
            this.ItemNameLbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.panel1.Size = new System.Drawing.Size(330, 35);
            this.panel1.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(322, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reduce Stock Confirmation";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(24, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Item :";
            // 
            // ReduceTxt
            // 
            this.ReduceTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReduceTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReduceTxt.Location = new System.Drawing.Point(142, 93);
            this.ReduceTxt.Margin = new System.Windows.Forms.Padding(2);
            this.ReduceTxt.Multiline = true;
            this.ReduceTxt.Name = "ReduceTxt";
            this.ReduceTxt.Size = new System.Drawing.Size(163, 21);
            this.ReduceTxt.TabIndex = 18;
            this.ReduceTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ReduceTxt.Enter += new System.EventHandler(this.ReduceTxt_Enter);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.ErrorTxt);
            this.panel2.Controls.Add(this.CancelBtn);
            this.panel2.Controls.Add(this.AcceptBtn);
            this.panel2.Controls.Add(this.ReasonCb);
            this.panel2.Controls.Add(this.StockLbl);
            this.panel2.Controls.Add(this.SizeLbl);
            this.panel2.Controls.Add(this.ItemNameLbl);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.ReduceTxt);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 247);
            this.panel2.TabIndex = 21;
            // 
            // ErrorTxt
            // 
            this.ErrorTxt.BackColor = System.Drawing.SystemColors.Control;
            this.ErrorTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErrorTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ErrorTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.ErrorTxt.Location = new System.Drawing.Point(27, 156);
            this.ErrorTxt.Multiline = true;
            this.ErrorTxt.Name = "ErrorTxt";
            this.ErrorTxt.Size = new System.Drawing.Size(278, 29);
            this.ErrorTxt.TabIndex = 31;
            this.ErrorTxt.TabStop = false;
            this.ErrorTxt.Visible = false;
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.White;
            this.CancelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.Location = new System.Drawing.Point(231, 195);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(74, 35);
            this.CancelBtn.TabIndex = 30;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // AcceptBtn
            // 
            this.AcceptBtn.BackColor = System.Drawing.Color.White;
            this.AcceptBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AcceptBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AcceptBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AcceptBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptBtn.Location = new System.Drawing.Point(142, 195);
            this.AcceptBtn.Name = "AcceptBtn";
            this.AcceptBtn.Size = new System.Drawing.Size(74, 35);
            this.AcceptBtn.TabIndex = 29;
            this.AcceptBtn.Text = "Confirm";
            this.AcceptBtn.UseVisualStyleBackColor = false;
            this.AcceptBtn.Click += new System.EventHandler(this.AcceptBtn_Click);
            // 
            // ReasonCb
            // 
            this.ReasonCb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReasonCb.FormattingEnabled = true;
            this.ReasonCb.Items.AddRange(new object[] {
            "Damaged",
            "Lost"});
            this.ReasonCb.Location = new System.Drawing.Point(142, 122);
            this.ReasonCb.Name = "ReasonCb";
            this.ReasonCb.Size = new System.Drawing.Size(163, 23);
            this.ReasonCb.TabIndex = 28;
            this.ReasonCb.SelectedIndexChanged += new System.EventHandler(this.ReasonCb_SelectedIndexChanged);
            // 
            // StockLbl
            // 
            this.StockLbl.AutoSize = true;
            this.StockLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.StockLbl.ForeColor = System.Drawing.Color.Black;
            this.StockLbl.Location = new System.Drawing.Point(139, 51);
            this.StockLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.StockLbl.Name = "StockLbl";
            this.StockLbl.Size = new System.Drawing.Size(69, 16);
            this.StockLbl.TabIndex = 27;
            this.StockLbl.Text = "Item Stock";
            // 
            // SizeLbl
            // 
            this.SizeLbl.AutoSize = true;
            this.SizeLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.SizeLbl.ForeColor = System.Drawing.Color.Black;
            this.SizeLbl.Location = new System.Drawing.Point(139, 35);
            this.SizeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SizeLbl.Name = "SizeLbl";
            this.SizeLbl.Size = new System.Drawing.Size(61, 16);
            this.SizeLbl.TabIndex = 26;
            this.SizeLbl.Text = "Item Size";
            // 
            // ItemNameLbl
            // 
            this.ItemNameLbl.AutoSize = true;
            this.ItemNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.ItemNameLbl.ForeColor = System.Drawing.Color.Black;
            this.ItemNameLbl.Location = new System.Drawing.Point(139, 19);
            this.ItemNameLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ItemNameLbl.Name = "ItemNameLbl";
            this.ItemNameLbl.Size = new System.Drawing.Size(72, 16);
            this.ItemNameLbl.TabIndex = 25;
            this.ItemNameLbl.Text = "Item Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(24, 125);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "Reason:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(24, 93);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 16);
            this.label5.TabIndex = 23;
            this.label5.Text = "Reduce stock by :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(24, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "Current Stock :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(24, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 16);
            this.label3.TabIndex = 21;
            this.label3.Text = "Size :";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(1);
            this.panel3.Size = new System.Drawing.Size(332, 284);
            this.panel3.TabIndex = 22;
            // 
            // ReduceStockForm
            // 
            this.AcceptButton = this.AcceptBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(332, 284);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ReduceStockForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quantity";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReduceStockForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ReduceTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox ReasonCb;
        private System.Windows.Forms.Label StockLbl;
        private System.Windows.Forms.Label SizeLbl;
        private System.Windows.Forms.Label ItemNameLbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button AcceptBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox ErrorTxt;
    }
}