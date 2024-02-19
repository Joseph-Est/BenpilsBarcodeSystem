namespace BenpilsBarcodeSystem
{
    partial class PurchaseOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PurchaseOrder));
            this.SupplierTab = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.RefreshBtn = new System.Windows.Forms.PictureBox();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.ArchiveBtn = new System.Windows.Forms.Button();
            this.UpdateBtn = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            this.ContactNoTxt = new System.Windows.Forms.TextBox();
            this.AddressTxt = new System.Windows.Forms.TextBox();
            this.ContactNameTxt = new System.Windows.Forms.TextBox();
            this.LblContactNo = new System.Windows.Forms.Label();
            this.LblAddress = new System.Windows.Forms.Label();
            this.LblContactName = new System.Windows.Forms.Label();
            this.dataGridSupplier = new System.Windows.Forms.DataGridView();
            this.SupplierID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeLbl = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.paymentTxt = new System.Windows.Forms.TextBox();
            this.TransactionNumberLbl = new System.Windows.Forms.Label();
            this.asdlabel = new System.Windows.Forms.Label();
            this.BuyBtn = new System.Windows.Forms.Button();
            this.totallbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Datelbl = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MotorBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Add = new System.Windows.Forms.DataGridViewButtonColumn();
            this.AdddBtn = new System.Windows.Forms.Button();
            this.SupplierTab.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSupplier)).BeginInit();
            this.ChangeLbl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datelbl)).BeginInit();
            this.SuspendLayout();
            // 
            // SupplierTab
            // 
            this.SupplierTab.Controls.Add(this.tabPage1);
            this.SupplierTab.Controls.Add(this.ChangeLbl);
            this.SupplierTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SupplierTab.Location = new System.Drawing.Point(0, 0);
            this.SupplierTab.Name = "SupplierTab";
            this.SupplierTab.SelectedIndex = 0;
            this.SupplierTab.Size = new System.Drawing.Size(1207, 710);
            this.SupplierTab.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.RefreshBtn);
            this.tabPage1.Controls.Add(this.ClearBtn);
            this.tabPage1.Controls.Add(this.ArchiveBtn);
            this.tabPage1.Controls.Add(this.UpdateBtn);
            this.tabPage1.Controls.Add(this.AddBtn);
            this.tabPage1.Controls.Add(this.ContactNoTxt);
            this.tabPage1.Controls.Add(this.AddressTxt);
            this.tabPage1.Controls.Add(this.ContactNameTxt);
            this.tabPage1.Controls.Add(this.LblContactNo);
            this.tabPage1.Controls.Add(this.LblAddress);
            this.tabPage1.Controls.Add(this.LblContactName);
            this.tabPage1.Controls.Add(this.dataGridSupplier);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1199, 684);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Supplier Master Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.BackColor = System.Drawing.Color.White;
            this.RefreshBtn.Image = ((System.Drawing.Image)(resources.GetObject("RefreshBtn.Image")));
            this.RefreshBtn.Location = new System.Drawing.Point(1200, 29);
            this.RefreshBtn.Margin = new System.Windows.Forms.Padding(4);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(40, 39);
            this.RefreshBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.RefreshBtn.TabIndex = 35;
            this.RefreshBtn.TabStop = false;
            this.RefreshBtn.UseWaitCursor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(722, 585);
            this.ClearBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(84, 57);
            this.ClearBtn.TabIndex = 10;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // ArchiveBtn
            // 
            this.ArchiveBtn.Location = new System.Drawing.Point(609, 585);
            this.ArchiveBtn.Margin = new System.Windows.Forms.Padding(4);
            this.ArchiveBtn.Name = "ArchiveBtn";
            this.ArchiveBtn.Size = new System.Drawing.Size(84, 57);
            this.ArchiveBtn.TabIndex = 9;
            this.ArchiveBtn.Text = "Archive";
            this.ArchiveBtn.UseVisualStyleBackColor = true;
            // 
            // UpdateBtn
            // 
            this.UpdateBtn.Location = new System.Drawing.Point(722, 505);
            this.UpdateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.UpdateBtn.Name = "UpdateBtn";
            this.UpdateBtn.Size = new System.Drawing.Size(84, 57);
            this.UpdateBtn.TabIndex = 8;
            this.UpdateBtn.Text = "Update";
            this.UpdateBtn.UseVisualStyleBackColor = true;
            this.UpdateBtn.Click += new System.EventHandler(this.UpdateBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(609, 505);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(4);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(84, 57);
            this.AddBtn.TabIndex = 7;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // ContactNoTxt
            // 
            this.ContactNoTxt.Location = new System.Drawing.Point(112, 585);
            this.ContactNoTxt.Margin = new System.Windows.Forms.Padding(4);
            this.ContactNoTxt.Name = "ContactNoTxt";
            this.ContactNoTxt.Size = new System.Drawing.Size(175, 20);
            this.ContactNoTxt.TabIndex = 6;
            this.ContactNoTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ContactNoTxt_KeyPress);
            // 
            // AddressTxt
            // 
            this.AddressTxt.Location = new System.Drawing.Point(113, 542);
            this.AddressTxt.Margin = new System.Windows.Forms.Padding(4);
            this.AddressTxt.Name = "AddressTxt";
            this.AddressTxt.Size = new System.Drawing.Size(175, 20);
            this.AddressTxt.TabIndex = 5;
            // 
            // ContactNameTxt
            // 
            this.ContactNameTxt.Location = new System.Drawing.Point(113, 498);
            this.ContactNameTxt.Margin = new System.Windows.Forms.Padding(4);
            this.ContactNameTxt.Name = "ContactNameTxt";
            this.ContactNameTxt.Size = new System.Drawing.Size(175, 20);
            this.ContactNameTxt.TabIndex = 4;
            // 
            // LblContactNo
            // 
            this.LblContactNo.AutoSize = true;
            this.LblContactNo.Location = new System.Drawing.Point(45, 592);
            this.LblContactNo.Name = "LblContactNo";
            this.LblContactNo.Size = new System.Drawing.Size(61, 13);
            this.LblContactNo.TabIndex = 3;
            this.LblContactNo.Text = "ContactNo:";
            // 
            // LblAddress
            // 
            this.LblAddress.AutoSize = true;
            this.LblAddress.Location = new System.Drawing.Point(58, 542);
            this.LblAddress.Name = "LblAddress";
            this.LblAddress.Size = new System.Drawing.Size(48, 13);
            this.LblAddress.TabIndex = 2;
            this.LblAddress.Text = "Address:";
            // 
            // LblContactName
            // 
            this.LblContactName.AutoSize = true;
            this.LblContactName.Location = new System.Drawing.Point(28, 498);
            this.LblContactName.Name = "LblContactName";
            this.LblContactName.Size = new System.Drawing.Size(78, 13);
            this.LblContactName.TabIndex = 1;
            this.LblContactName.Text = "Contact Name:";
            // 
            // dataGridSupplier
            // 
            this.dataGridSupplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSupplier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SupplierID,
            this.ContactName,
            this.Address,
            this.ContactNo});
            this.dataGridSupplier.Location = new System.Drawing.Point(19, 29);
            this.dataGridSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridSupplier.Name = "dataGridSupplier";
            this.dataGridSupplier.RowHeadersWidth = 51;
            this.dataGridSupplier.Size = new System.Drawing.Size(1042, 443);
            this.dataGridSupplier.TabIndex = 0;
            this.dataGridSupplier.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSupplier_CellClick);
            // 
            // SupplierID
            // 
            this.SupplierID.DataPropertyName = "SupplierID";
            this.SupplierID.HeaderText = "SupplierID";
            this.SupplierID.MinimumWidth = 6;
            this.SupplierID.Name = "SupplierID";
            this.SupplierID.ReadOnly = true;
            this.SupplierID.Width = 125;
            // 
            // ContactName
            // 
            this.ContactName.DataPropertyName = "ContactName";
            this.ContactName.HeaderText = "ContactName";
            this.ContactName.MinimumWidth = 6;
            this.ContactName.Name = "ContactName";
            this.ContactName.ReadOnly = true;
            this.ContactName.Width = 125;
            // 
            // Address
            // 
            this.Address.DataPropertyName = "Address";
            this.Address.HeaderText = "Address";
            this.Address.MinimumWidth = 6;
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            this.Address.Width = 125;
            // 
            // ContactNo
            // 
            this.ContactNo.DataPropertyName = "ContactNo";
            this.ContactNo.HeaderText = "ContactNo";
            this.ContactNo.MinimumWidth = 6;
            this.ContactNo.Name = "ContactNo";
            this.ContactNo.ReadOnly = true;
            this.ContactNo.Width = 125;
            // 
            // ChangeLbl
            // 
            this.ChangeLbl.Controls.Add(this.label12);
            this.ChangeLbl.Controls.Add(this.label11);
            this.ChangeLbl.Controls.Add(this.label10);
            this.ChangeLbl.Controls.Add(this.label9);
            this.ChangeLbl.Controls.Add(this.label8);
            this.ChangeLbl.Controls.Add(this.paymentTxt);
            this.ChangeLbl.Controls.Add(this.TransactionNumberLbl);
            this.ChangeLbl.Controls.Add(this.asdlabel);
            this.ChangeLbl.Controls.Add(this.BuyBtn);
            this.ChangeLbl.Controls.Add(this.totallbl);
            this.ChangeLbl.Controls.Add(this.label7);
            this.ChangeLbl.Controls.Add(this.dataGridView2);
            this.ChangeLbl.Controls.Add(this.Datelbl);
            this.ChangeLbl.Controls.Add(this.AdddBtn);
            this.ChangeLbl.Location = new System.Drawing.Point(4, 22);
            this.ChangeLbl.Margin = new System.Windows.Forms.Padding(4);
            this.ChangeLbl.Name = "ChangeLbl";
            this.ChangeLbl.Padding = new System.Windows.Forms.Padding(3);
            this.ChangeLbl.Size = new System.Drawing.Size(1199, 684);
            this.ChangeLbl.TabIndex = 1;
            this.ChangeLbl.Text = "Purchase Order List";
            this.ChangeLbl.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(902, 159);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "...";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(839, 159);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(30, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Date";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(902, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(16, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "...";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(839, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Change";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(839, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "payment";
            // 
            // paymentTxt
            // 
            this.paymentTxt.Location = new System.Drawing.Point(905, 82);
            this.paymentTxt.Name = "paymentTxt";
            this.paymentTxt.Size = new System.Drawing.Size(111, 20);
            this.paymentTxt.TabIndex = 8;
            this.paymentTxt.TextChanged += new System.EventHandler(this.paymentTxt_TextChanged);
            // 
            // TransactionNumberLbl
            // 
            this.TransactionNumberLbl.AutoSize = true;
            this.TransactionNumberLbl.Location = new System.Drawing.Point(902, 53);
            this.TransactionNumberLbl.Name = "TransactionNumberLbl";
            this.TransactionNumberLbl.Size = new System.Drawing.Size(16, 13);
            this.TransactionNumberLbl.TabIndex = 7;
            this.TransactionNumberLbl.Text = "...";
            // 
            // asdlabel
            // 
            this.asdlabel.AutoSize = true;
            this.asdlabel.Location = new System.Drawing.Point(801, 53);
            this.asdlabel.Name = "asdlabel";
            this.asdlabel.Size = new System.Drawing.Size(103, 13);
            this.asdlabel.TabIndex = 6;
            this.asdlabel.Text = "TransactionNumber:";
            // 
            // BuyBtn
            // 
            this.BuyBtn.Location = new System.Drawing.Point(1016, 227);
            this.BuyBtn.Name = "BuyBtn";
            this.BuyBtn.Size = new System.Drawing.Size(75, 23);
            this.BuyBtn.TabIndex = 5;
            this.BuyBtn.Text = "Buy";
            this.BuyBtn.UseVisualStyleBackColor = true;
            this.BuyBtn.Click += new System.EventHandler(this.BuyBtn_Click);
            // 
            // totallbl
            // 
            this.totallbl.AutoSize = true;
            this.totallbl.Location = new System.Drawing.Point(902, 29);
            this.totallbl.Name = "totallbl";
            this.totallbl.Size = new System.Drawing.Size(16, 13);
            this.totallbl.TabIndex = 4;
            this.totallbl.Text = "...";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(855, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Total";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(7, 18);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(788, 232);
            this.dataGridView2.TabIndex = 2;
            // 
            // Datelbl
            // 
            this.Datelbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Datelbl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.ProductID,
            this.dataGridViewTextBoxColumn1,
            this.Barcode,
            this.ItemName,
            this.MotorBrand,
            this.Brand,
            this.UnitPrice,
            this.Category,
            this.Add});
            this.Datelbl.Location = new System.Drawing.Point(7, 268);
            this.Datelbl.Margin = new System.Windows.Forms.Padding(4);
            this.Datelbl.Name = "Datelbl";
            this.Datelbl.RowHeadersWidth = 51;
            this.Datelbl.Size = new System.Drawing.Size(1084, 395);
            this.Datelbl.TabIndex = 1;
            this.Datelbl.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "SupplierID";
            this.Column1.HeaderText = "SupplierID";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 75;
            // 
            // ProductID
            // 
            this.ProductID.DataPropertyName = "ProductID";
            this.ProductID.HeaderText = "ProductID";
            this.ProductID.MinimumWidth = 6;
            this.ProductID.Name = "ProductID";
            this.ProductID.ReadOnly = true;
            this.ProductID.Width = 75;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ContactName";
            this.dataGridViewTextBoxColumn1.HeaderText = "ContactName";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // Barcode
            // 
            this.Barcode.DataPropertyName = "Barcode";
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.MinimumWidth = 6;
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            this.Barcode.Width = 125;
            // 
            // ItemName
            // 
            this.ItemName.DataPropertyName = "ItemName";
            this.ItemName.HeaderText = "ItemName";
            this.ItemName.MinimumWidth = 6;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 125;
            // 
            // MotorBrand
            // 
            this.MotorBrand.DataPropertyName = "MotorBrand";
            this.MotorBrand.HeaderText = "MotorBrand";
            this.MotorBrand.MinimumWidth = 6;
            this.MotorBrand.Name = "MotorBrand";
            this.MotorBrand.ReadOnly = true;
            this.MotorBrand.Width = 125;
            // 
            // Brand
            // 
            this.Brand.DataPropertyName = "Brand";
            this.Brand.HeaderText = "Brand";
            this.Brand.MinimumWidth = 6;
            this.Brand.Name = "Brand";
            this.Brand.ReadOnly = true;
            this.Brand.Width = 125;
            // 
            // UnitPrice
            // 
            this.UnitPrice.DataPropertyName = "UnitPrice";
            this.UnitPrice.HeaderText = "UnitPrice";
            this.UnitPrice.MinimumWidth = 6;
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.ReadOnly = true;
            this.UnitPrice.Width = 65;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "Category";
            this.Category.HeaderText = "Category";
            this.Category.MinimumWidth = 6;
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            this.Category.Width = 70;
            // 
            // Add
            // 
            this.Add.HeaderText = "Add";
            this.Add.MinimumWidth = 6;
            this.Add.Name = "Add";
            this.Add.Text = "Add";
            this.Add.UseColumnTextForButtonValue = true;
            this.Add.Width = 125;
            // 
            // AdddBtn
            // 
            this.AdddBtn.Location = new System.Drawing.Point(1115, 621);
            this.AdddBtn.Margin = new System.Windows.Forms.Padding(4);
            this.AdddBtn.Name = "AdddBtn";
            this.AdddBtn.Size = new System.Drawing.Size(75, 54);
            this.AdddBtn.TabIndex = 0;
            this.AdddBtn.Text = "Add";
            this.AdddBtn.UseVisualStyleBackColor = true;
            this.AdddBtn.Click += new System.EventHandler(this.AdddBtn_Click);
            // 
            // PurchaseOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 710);
            this.Controls.Add(this.SupplierTab);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PurchaseOrder";
            this.Text = "Purchaserr";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.SupplierTab.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSupplier)).EndInit();
            this.ChangeLbl.ResumeLayout(false);
            this.ChangeLbl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datelbl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl SupplierTab;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage ChangeLbl;
        private System.Windows.Forms.TextBox ContactNoTxt;
        private System.Windows.Forms.TextBox AddressTxt;
        private System.Windows.Forms.TextBox ContactNameTxt;
        private System.Windows.Forms.Label LblContactNo;
        private System.Windows.Forms.Label LblAddress;
        private System.Windows.Forms.Label LblContactName;
        private System.Windows.Forms.DataGridView dataGridSupplier;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.Button ArchiveBtn;
        private System.Windows.Forms.Button UpdateBtn;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactNo;
        private System.Windows.Forms.Button AdddBtn;
        private System.Windows.Forms.DataGridView Datelbl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MotorBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn Brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewButtonColumn Add;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label totallbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BuyBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox paymentTxt;
        private System.Windows.Forms.Label TransactionNumberLbl;
        private System.Windows.Forms.Label asdlabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox RefreshBtn;
    }
}