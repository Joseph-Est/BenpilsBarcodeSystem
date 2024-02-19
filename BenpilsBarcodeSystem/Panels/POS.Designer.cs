namespace BenpilsBarcodeSystem
{
    partial class POS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS));
            this.button1 = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.Changelbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.TransactionNumberlbl = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TotalLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MotorBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Void = new System.Windows.Forms.DataGridViewButtonColumn();
            this.RefreshBtn = new System.Windows.Forms.PictureBox();
            this.PaymentrichTxt = new System.Windows.Forms.RichTextBox();
            this.BarcoderichTxt = new System.Windows.Forms.RichTextBox();
            this.QuantityRichTxt = new System.Windows.Forms.RichTextBox();
            this.Addttocartbtn = new System.Windows.Forms.Button();
            this.lbldate = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshBtn)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(847, 440);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(305, 83);
            this.button1.TabIndex = 198;
            this.button1.Text = "BUY";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(361, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(98, 25);
            this.label16.TabIndex = 196;
            this.label16.Text = "Quantity:";
            // 
            // Changelbl
            // 
            this.Changelbl.AutoSize = true;
            this.Changelbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Changelbl.Location = new System.Drawing.Point(942, 350);
            this.Changelbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Changelbl.Name = "Changelbl";
            this.Changelbl.Size = new System.Drawing.Size(62, 37);
            this.Changelbl.TabIndex = 195;
            this.Changelbl.Text = ".....";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(818, 359);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(93, 25);
            this.label14.TabIndex = 194;
            this.label14.Text = "Change:";
            // 
            // TransactionNumberlbl
            // 
            this.TransactionNumberlbl.AutoSize = true;
            this.TransactionNumberlbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransactionNumberlbl.Location = new System.Drawing.Point(978, 159);
            this.TransactionNumberlbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TransactionNumberlbl.Name = "TransactionNumberlbl";
            this.TransactionNumberlbl.Size = new System.Drawing.Size(62, 37);
            this.TransactionNumberlbl.TabIndex = 193;
            this.TransactionNumberlbl.Text = ".....";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(49, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 25);
            this.label12.TabIndex = 189;
            this.label12.Text = "Barcode:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(809, 287);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 25);
            this.label11.TabIndex = 188;
            this.label11.Text = "Payment:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(809, 216);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 25);
            this.label10.TabIndex = 187;
            this.label10.Text = "Date:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(809, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(164, 25);
            this.label9.TabIndex = 186;
            this.label9.Text = "Transaction No:";
            // 
            // TotalLbl
            // 
            this.TotalLbl.AutoSize = true;
            this.TotalLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLbl.Location = new System.Drawing.Point(910, 78);
            this.TotalLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TotalLbl.Name = "TotalLbl";
            this.TotalLbl.Size = new System.Drawing.Size(67, 39);
            this.TotalLbl.TabIndex = 185;
            this.TotalLbl.Text = ".....";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(807, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 42);
            this.label7.TabIndex = 184;
            this.label7.Text = "Total:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemName,
            this.Brand,
            this.MotorBrand,
            this.Quantity,
            this.Subtotal,
            this.Void});
            this.dataGridView1.Location = new System.Drawing.Point(36, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(717, 600);
            this.dataGridView1.TabIndex = 183;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.MinimumWidth = 6;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            // 
            // Brand
            // 
            this.Brand.DataPropertyName = "Brand";
            this.Brand.HeaderText = "Brand";
            this.Brand.MinimumWidth = 6;
            this.Brand.Name = "Brand";
            this.Brand.ReadOnly = true;
            // 
            // MotorBrand
            // 
            this.MotorBrand.DataPropertyName = "MotorBrand";
            this.MotorBrand.HeaderText = "MotorBrand";
            this.MotorBrand.MinimumWidth = 6;
            this.MotorBrand.Name = "MotorBrand";
            this.MotorBrand.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Subtotal
            // 
            this.Subtotal.DataPropertyName = "Subtotal";
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.MinimumWidth = 6;
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            // 
            // Void
            // 
            this.Void.HeaderText = "Void";
            this.Void.MinimumWidth = 6;
            this.Void.Name = "Void";
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RefreshBtn.BackColor = System.Drawing.Color.White;
            this.RefreshBtn.Image = ((System.Drawing.Image)(resources.GetObject("RefreshBtn.Image")));
            this.RefreshBtn.Location = new System.Drawing.Point(1274, 76);
            this.RefreshBtn.Margin = new System.Windows.Forms.Padding(2);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(38, 39);
            this.RefreshBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.RefreshBtn.TabIndex = 182;
            this.RefreshBtn.TabStop = false;
            this.RefreshBtn.UseWaitCursor = true;
            // 
            // PaymentrichTxt
            // 
            this.PaymentrichTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PaymentrichTxt.Location = new System.Drawing.Point(917, 278);
            this.PaymentrichTxt.Name = "PaymentrichTxt";
            this.PaymentrichTxt.Size = new System.Drawing.Size(202, 42);
            this.PaymentrichTxt.TabIndex = 199;
            this.PaymentrichTxt.Text = "";
            // 
            // BarcoderichTxt
            // 
            this.BarcoderichTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarcoderichTxt.Location = new System.Drawing.Point(153, 27);
            this.BarcoderichTxt.Name = "BarcoderichTxt";
            this.BarcoderichTxt.Size = new System.Drawing.Size(202, 42);
            this.BarcoderichTxt.TabIndex = 200;
            this.BarcoderichTxt.Text = "";
            this.BarcoderichTxt.TextChanged += new System.EventHandler(this.BarcoderichTxt_TextChanged);
            // 
            // QuantityRichTxt
            // 
            this.QuantityRichTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuantityRichTxt.Location = new System.Drawing.Point(465, 26);
            this.QuantityRichTxt.Name = "QuantityRichTxt";
            this.QuantityRichTxt.Size = new System.Drawing.Size(202, 42);
            this.QuantityRichTxt.TabIndex = 201;
            this.QuantityRichTxt.Text = "";
            // 
            // Addttocartbtn
            // 
            this.Addttocartbtn.Location = new System.Drawing.Point(673, 24);
            this.Addttocartbtn.Name = "Addttocartbtn";
            this.Addttocartbtn.Size = new System.Drawing.Size(111, 41);
            this.Addttocartbtn.TabIndex = 202;
            this.Addttocartbtn.Text = "Add to Cart";
            this.Addttocartbtn.UseVisualStyleBackColor = true;
            this.Addttocartbtn.Click += new System.EventHandler(this.Addttocartbtn_Click);
            // 
            // lbldate
            // 
            this.lbldate.AutoSize = true;
            this.lbldate.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldate.Location = new System.Drawing.Point(915, 207);
            this.lbldate.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(62, 37);
            this.lbldate.TabIndex = 203;
            this.lbldate.Text = ".....";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbldate);
            this.panel1.Controls.Add(this.Addttocartbtn);
            this.panel1.Controls.Add(this.QuantityRichTxt);
            this.panel1.Controls.Add(this.BarcoderichTxt);
            this.panel1.Controls.Add(this.PaymentrichTxt);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.Changelbl);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.TransactionNumberlbl);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.TotalLbl);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1207, 710);
            this.panel1.TabIndex = 204;
            // 
            // POS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 710);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RefreshBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POS";
            this.Text = "POSS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshBtn)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label Changelbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label TransactionNumberlbl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label TotalLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox RefreshBtn;
        private System.Windows.Forms.RichTextBox PaymentrichTxt;
        private System.Windows.Forms.RichTextBox BarcoderichTxt;
        private System.Windows.Forms.RichTextBox QuantityRichTxt;
        private System.Windows.Forms.Button Addttocartbtn;
        private System.Windows.Forms.Label lbldate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn MotorBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.DataGridViewButtonColumn Void;
        private System.Windows.Forms.Panel panel1;
    }
}