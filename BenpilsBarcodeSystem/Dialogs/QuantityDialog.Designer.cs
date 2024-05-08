namespace BenpilsBarcodeSystem.Dialogs
{
    partial class QuantityDialog
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
            this.TitleLbl = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.QuantityTxt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StockLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.PriceLblTxt = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.SizeLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ItemLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel2.Controls.Add(this.TitleLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.panel2.Size = new System.Drawing.Size(270, 33);
            this.panel2.TabIndex = 87;
            // 
            // TitleLbl
            // 
            this.TitleLbl.AutoSize = true;
            this.TitleLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLbl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.TitleLbl.ForeColor = System.Drawing.Color.White;
            this.TitleLbl.Location = new System.Drawing.Point(8, 8);
            this.TitleLbl.Name = "TitleLbl";
            this.TitleLbl.Size = new System.Drawing.Size(110, 18);
            this.TitleLbl.TabIndex = 84;
            this.TitleLbl.Text = "Enter Quantity";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.panel14);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(1, 34);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.panel3.Size = new System.Drawing.Size(270, 202);
            this.panel3.TabIndex = 88;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.CancelBtn);
            this.panel14.Controls.Add(this.ConfirmBtn);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(20, 139);
            this.panel14.MinimumSize = new System.Drawing.Size(230, 54);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(0, 13, 0, 12);
            this.panel14.Size = new System.Drawing.Size(230, 60);
            this.panel14.TabIndex = 141;
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.White;
            this.CancelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.CancelBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CancelBtn.FlatAppearance.BorderSize = 0;
            this.CancelBtn.Font = new System.Drawing.Font("Arial", 9F);
            this.CancelBtn.Location = new System.Drawing.Point(120, 13);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(110, 35);
            this.CancelBtn.TabIndex = 143;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.Button1_Click);
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.BackColor = System.Drawing.Color.White;
            this.ConfirmBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConfirmBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.ConfirmBtn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.ConfirmBtn.FlatAppearance.BorderSize = 0;
            this.ConfirmBtn.Font = new System.Drawing.Font("Arial", 9F);
            this.ConfirmBtn.Location = new System.Drawing.Point(0, 13);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(110, 35);
            this.ConfirmBtn.TabIndex = 142;
            this.ConfirmBtn.Text = "Confirm";
            this.ConfirmBtn.UseVisualStyleBackColor = false;
            this.ConfirmBtn.Click += new System.EventHandler(this.AcceptBtn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.QuantityTxt);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(20, 114);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(230, 25);
            this.panel4.TabIndex = 4;
            // 
            // QuantityTxt
            // 
            this.QuantityTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QuantityTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuantityTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityTxt.Location = new System.Drawing.Point(0, 0);
            this.QuantityTxt.MaxLength = 9;
            this.QuantityTxt.Multiline = true;
            this.QuantityTxt.Name = "QuantityTxt";
            this.QuantityTxt.Size = new System.Drawing.Size(230, 25);
            this.QuantityTxt.TabIndex = 1;
            this.QuantityTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.QuantityTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.QuantityTxt_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.StockLbl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 34);
            this.panel1.TabIndex = 137;
            // 
            // StockLbl
            // 
            this.StockLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.StockLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StockLbl.ForeColor = System.Drawing.Color.Black;
            this.StockLbl.Location = new System.Drawing.Point(104, 0);
            this.StockLbl.Name = "StockLbl";
            this.StockLbl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.StockLbl.Size = new System.Drawing.Size(120, 34);
            this.StockLbl.TabIndex = 146;
            this.StockLbl.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.label1.Size = new System.Drawing.Size(104, 26);
            this.label1.TabIndex = 145;
            this.label1.Text = "Available Stock :";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.PriceLblTxt);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(20, 60);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(230, 20);
            this.panel8.TabIndex = 142;
            // 
            // PriceLblTxt
            // 
            this.PriceLblTxt.Dock = System.Windows.Forms.DockStyle.Left;
            this.PriceLblTxt.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLblTxt.ForeColor = System.Drawing.Color.Black;
            this.PriceLblTxt.Location = new System.Drawing.Point(45, 0);
            this.PriceLblTxt.Name = "PriceLblTxt";
            this.PriceLblTxt.Size = new System.Drawing.Size(182, 20);
            this.PriceLblTxt.TabIndex = 148;
            this.PriceLblTxt.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Left;
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 16);
            this.label6.TabIndex = 147;
            this.label6.Text = "Price :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.SizeLbl);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(20, 40);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(230, 20);
            this.panel5.TabIndex = 138;
            // 
            // SizeLbl
            // 
            this.SizeLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.SizeLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SizeLbl.ForeColor = System.Drawing.Color.Black;
            this.SizeLbl.Location = new System.Drawing.Point(41, 0);
            this.SizeLbl.Name = "SizeLbl";
            this.SizeLbl.Size = new System.Drawing.Size(183, 20);
            this.SizeLbl.TabIndex = 148;
            this.SizeLbl.Text = "Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 147;
            this.label3.Text = "Size :";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.ItemLbl);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(20, 20);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(230, 20);
            this.panel7.TabIndex = 140;
            // 
            // ItemLbl
            // 
            this.ItemLbl.AutoEllipsis = true;
            this.ItemLbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.ItemLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemLbl.ForeColor = System.Drawing.Color.Black;
            this.ItemLbl.Location = new System.Drawing.Point(40, 0);
            this.ItemLbl.Name = "ItemLbl";
            this.ItemLbl.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.ItemLbl.Size = new System.Drawing.Size(184, 20);
            this.ItemLbl.TabIndex = 149;
            this.ItemLbl.Text = "Item ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.label4.Size = new System.Drawing.Size(40, 21);
            this.label4.TabIndex = 148;
            this.label4.Text = "Item :";
            // 
            // QuantityDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(272, 237);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "QuantityDialog";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QuantityDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuantityDialog_FormClosing);
            this.Load += new System.EventHandler(this.QuantityDialog_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label TitleLbl;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox QuantityTxt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label StockLbl;
        private System.Windows.Forms.Label SizeLbl;
        private System.Windows.Forms.Label ItemLbl;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label PriceLblTxt;
        private System.Windows.Forms.Label label6;
    }
}