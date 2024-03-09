namespace BenpilsBarcodeSystem.Dialogs
{
    partial class OrderDetails
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderDetails));
            this.panel2 = new System.Windows.Forms.Panel();
            this.TitleLbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.PrintBtn = new System.Windows.Forms.Button();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.TotalLbl = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.ItemsTbl = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisplayItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Brand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchasePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel6 = new System.Windows.Forms.Panel();
            this.DeliveryDateLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.OrderDateLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.SupplierLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OrderedByPanel = new System.Windows.Forms.Panel();
            this.OrdereByLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OrderNoPanel = new System.Windows.Forms.Panel();
            this.OrderNoLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsTbl)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.OrderedByPanel.SuspendLayout();
            this.OrderNoPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel2.Controls.Add(this.TitleLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.panel2.Size = new System.Drawing.Size(447, 33);
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
            this.TitleLbl.Size = new System.Drawing.Size(103, 18);
            this.TitleLbl.TabIndex = 84;
            this.TitleLbl.Text = "Order Details";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.panel9);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.OrderedByPanel);
            this.panel1.Controls.Add(this.OrderNoPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 34);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(447, 569);
            this.panel1.TabIndex = 88;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.PrintBtn);
            this.flowLayoutPanel1.Controls.Add(this.ConfirmBtn);
            this.flowLayoutPanel1.Controls.Add(this.CancelBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 490);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(427, 63);
            this.flowLayoutPanel1.TabIndex = 192;
            // 
            // PrintBtn
            // 
            this.PrintBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PrintBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PrintBtn.Location = new System.Drawing.Point(328, 23);
            this.PrintBtn.Name = "PrintBtn";
            this.PrintBtn.Size = new System.Drawing.Size(96, 35);
            this.PrintBtn.TabIndex = 0;
            this.PrintBtn.Text = "Print Receipt";
            this.PrintBtn.UseVisualStyleBackColor = true;
            this.PrintBtn.Visible = false;
            this.PrintBtn.Click += new System.EventHandler(this.PrintBtn_Click);
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConfirmBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConfirmBtn.Location = new System.Drawing.Point(218, 23);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(104, 35);
            this.ConfirmBtn.TabIndex = 1;
            this.ConfirmBtn.Text = "Confirm Order";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Visible = false;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelBtn.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.Location = new System.Drawing.Point(137, 23);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 35);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Visible = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.TotalLbl);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(10, 453);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel9.Size = new System.Drawing.Size(427, 37);
            this.panel9.TabIndex = 191;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Right;
            this.label7.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(325, 5);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label7.Size = new System.Drawing.Size(42, 21);
            this.label7.TabIndex = 5;
            this.label7.Text = "Total :";
            // 
            // TotalLbl
            // 
            this.TotalLbl.AutoSize = true;
            this.TotalLbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.TotalLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLbl.Location = new System.Drawing.Point(367, 5);
            this.TotalLbl.Name = "TotalLbl";
            this.TotalLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.TotalLbl.Size = new System.Drawing.Size(60, 21);
            this.TotalLbl.TabIndex = 4;
            this.TotalLbl.Text = "10000.00";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.ItemsTbl);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(10, 190);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(427, 263);
            this.panel7.TabIndex = 188;
            // 
            // ItemsTbl
            // 
            this.ItemsTbl.AllowUserToAddRows = false;
            this.ItemsTbl.AllowUserToDeleteRows = false;
            this.ItemsTbl.AllowUserToResizeColumns = false;
            this.ItemsTbl.AllowUserToResizeRows = false;
            this.ItemsTbl.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.ItemsTbl.BackgroundColor = System.Drawing.Color.White;
            this.ItemsTbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemsTbl.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsTbl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.ItemsTbl.ColumnHeadersHeight = 30;
            this.ItemsTbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ItemsTbl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ItemName,
            this.DisplayItemName,
            this.Quantity,
            this.Size,
            this.Brand,
            this.PurchasePrice,
            this.SellingPrice,
            this.TotalAmount});
            this.ItemsTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsTbl.EnableHeadersVisualStyles = false;
            this.ItemsTbl.GridColor = System.Drawing.Color.White;
            this.ItemsTbl.Location = new System.Drawing.Point(0, 0);
            this.ItemsTbl.MinimumSize = new System.Drawing.Size(427, 263);
            this.ItemsTbl.MultiSelect = false;
            this.ItemsTbl.Name = "ItemsTbl";
            this.ItemsTbl.ReadOnly = true;
            this.ItemsTbl.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemsTbl.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.ItemsTbl.RowHeadersVisible = false;
            this.ItemsTbl.RowHeadersWidth = 51;
            this.ItemsTbl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.ItemsTbl.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.ItemsTbl.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ItemsTbl.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.ItemsTbl.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.ItemsTbl.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ItemsTbl.RowTemplate.Height = 30;
            this.ItemsTbl.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsTbl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsTbl.Size = new System.Drawing.Size(427, 263);
            this.ItemsTbl.TabIndex = 183;
            this.ItemsTbl.TabStop = false;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ItemName
            // 
            this.ItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemName.DataPropertyName = "ItemName";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ItemName.DefaultCellStyle = dataGridViewCellStyle12;
            this.ItemName.FillWeight = 40F;
            this.ItemName.HeaderText = "Item";
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Visible = false;
            // 
            // DisplayItemName
            // 
            this.DisplayItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DisplayItemName.DataPropertyName = "DisplayItemName";
            this.DisplayItemName.FillWeight = 60F;
            this.DisplayItemName.HeaderText = "Item";
            this.DisplayItemName.Name = "DisplayItemName";
            this.DisplayItemName.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.FillWeight = 10F;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Size
            // 
            this.Size.DataPropertyName = "Size";
            this.Size.HeaderText = "Size";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.Visible = false;
            // 
            // Brand
            // 
            this.Brand.DataPropertyName = "Brand";
            this.Brand.HeaderText = "Brand";
            this.Brand.Name = "Brand";
            this.Brand.ReadOnly = true;
            this.Brand.Visible = false;
            // 
            // PurchasePrice
            // 
            this.PurchasePrice.DataPropertyName = "PurchasePrice";
            this.PurchasePrice.HeaderText = "Purchase Price";
            this.PurchasePrice.Name = "PurchasePrice";
            this.PurchasePrice.ReadOnly = true;
            this.PurchasePrice.Visible = false;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            this.SellingPrice.HeaderText = "Selling Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            this.SellingPrice.Visible = false;
            // 
            // TotalAmount
            // 
            this.TotalAmount.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle13;
            this.TotalAmount.FillWeight = 20F;
            this.TotalAmount.HeaderText = "Subtotal";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.DeliveryDateLbl);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(10, 154);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(427, 36);
            this.panel6.TabIndex = 187;
            // 
            // DeliveryDateLbl
            // 
            this.DeliveryDateLbl.AutoSize = true;
            this.DeliveryDateLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeliveryDateLbl.Location = new System.Drawing.Point(99, 0);
            this.DeliveryDateLbl.Name = "DeliveryDateLbl";
            this.DeliveryDateLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.DeliveryDateLbl.Size = new System.Drawing.Size(83, 21);
            this.DeliveryDateLbl.TabIndex = 4;
            this.DeliveryDateLbl.Text = "Delivery Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(91, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Delivery Date :";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.OrderDateLbl);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 118);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(427, 36);
            this.panel5.TabIndex = 186;
            // 
            // OrderDateLbl
            // 
            this.OrderDateLbl.AutoSize = true;
            this.OrderDateLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderDateLbl.Location = new System.Drawing.Point(99, 0);
            this.OrderDateLbl.Name = "OrderDateLbl";
            this.OrderDateLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.OrderDateLbl.Size = new System.Drawing.Size(70, 21);
            this.OrderDateLbl.TabIndex = 3;
            this.OrderDateLbl.Text = "Order Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Order Date :";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.SupplierLbl);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(10, 82);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(427, 36);
            this.panel4.TabIndex = 185;
            // 
            // SupplierLbl
            // 
            this.SupplierLbl.AutoSize = true;
            this.SupplierLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierLbl.Location = new System.Drawing.Point(99, 3);
            this.SupplierLbl.Name = "SupplierLbl";
            this.SupplierLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.SupplierLbl.Size = new System.Drawing.Size(58, 21);
            this.SupplierLbl.TabIndex = 2;
            this.SupplierLbl.Text = "Supplier:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(62, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Supplier :";
            // 
            // OrderedByPanel
            // 
            this.OrderedByPanel.Controls.Add(this.OrdereByLbl);
            this.OrderedByPanel.Controls.Add(this.label5);
            this.OrderedByPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderedByPanel.Location = new System.Drawing.Point(10, 46);
            this.OrderedByPanel.Name = "OrderedByPanel";
            this.OrderedByPanel.Size = new System.Drawing.Size(427, 36);
            this.OrderedByPanel.TabIndex = 190;
            // 
            // OrdereByLbl
            // 
            this.OrdereByLbl.AutoSize = true;
            this.OrdereByLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrdereByLbl.Location = new System.Drawing.Point(99, 0);
            this.OrdereByLbl.Name = "OrdereByLbl";
            this.OrdereByLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.OrdereByLbl.Size = new System.Drawing.Size(81, 21);
            this.OrdereByLbl.TabIndex = 3;
            this.OrdereByLbl.Text = "Ordered By :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(81, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ordered By :";
            // 
            // OrderNoPanel
            // 
            this.OrderNoPanel.Controls.Add(this.OrderNoLbl);
            this.OrderNoPanel.Controls.Add(this.label1);
            this.OrderNoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderNoPanel.Location = new System.Drawing.Point(10, 10);
            this.OrderNoPanel.Name = "OrderNoPanel";
            this.OrderNoPanel.Size = new System.Drawing.Size(427, 36);
            this.OrderNoPanel.TabIndex = 184;
            // 
            // OrderNoLbl
            // 
            this.OrderNoLbl.AutoSize = true;
            this.OrderNoLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderNoLbl.Location = new System.Drawing.Point(99, 0);
            this.OrderNoLbl.Name = "OrderNoLbl";
            this.OrderNoLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.OrderNoLbl.Size = new System.Drawing.Size(63, 21);
            this.OrderNoLbl.TabIndex = 1;
            this.OrderNoLbl.Text = "Order No:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(67, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order No :";
            // 
            // PrintPreview
            // 
            this.PrintPreview.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.PrintPreview.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.PrintPreview.ClientSize = new System.Drawing.Size(400, 300);
            this.PrintPreview.Enabled = true;
            this.PrintPreview.Icon = ((System.Drawing.Icon)(resources.GetObject("PrintPreview.Icon")));
            this.PrintPreview.Name = "testPrintPreview";
            this.PrintPreview.Visible = false;
            // 
            // PrintDocument
            // 
            this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDocument_PrintPage);
            // 
            // OrderDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(449, 604);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(449, 0);
            this.Name = "OrderDetails";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OrderDetails";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrderDetails_FormClosing);
            this.Load += new System.EventHandler(this.OrderDetails_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ItemsTbl)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.OrderedByPanel.ResumeLayout(false);
            this.OrderedByPanel.PerformLayout();
            this.OrderNoPanel.ResumeLayout(false);
            this.OrderNoPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label TitleLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridView ItemsTbl;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label TotalLbl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button PrintBtn;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label DeliveryDateLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label OrderDateLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label SupplierLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel OrderedByPanel;
        private System.Windows.Forms.Label OrdereByLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel OrderNoPanel;
        private System.Windows.Forms.Label OrderNoLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Brand;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchasePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.PrintPreviewDialog PrintPreview;
        private System.Drawing.Printing.PrintDocument PrintDocument;
    }
}