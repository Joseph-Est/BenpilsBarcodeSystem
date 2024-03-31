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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.DisplayItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReceivedQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemarksPanel = new System.Windows.Forms.Panel();
            this.RemarksLbl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.FulfilledByPanel = new System.Windows.Forms.Panel();
            this.FulfilledByLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DateFulfilledPanel = new System.Windows.Forms.Panel();
            this.DateFulfilledLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.StatusLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SupplierPanel = new System.Windows.Forms.Panel();
            this.SupplierLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.OrderedByPanel = new System.Windows.Forms.Panel();
            this.OrderedByLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DeliveryDatePanel = new System.Windows.Forms.Panel();
            this.DeliveryDateLbl = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.OrderDatePanel = new System.Windows.Forms.Panel();
            this.OrderDateLbl = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.OrderNoPanel = new System.Windows.Forms.Panel();
            this.OrderNoLbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.PrintPreview = new System.Windows.Forms.PrintPreviewDialog();
            this.PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsTbl)).BeginInit();
            this.RemarksPanel.SuspendLayout();
            this.FulfilledByPanel.SuspendLayout();
            this.DateFulfilledPanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.SupplierPanel.SuspendLayout();
            this.OrderedByPanel.SuspendLayout();
            this.DeliveryDatePanel.SuspendLayout();
            this.OrderDatePanel.SuspendLayout();
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
            this.panel1.Controls.Add(this.RemarksPanel);
            this.panel1.Controls.Add(this.FulfilledByPanel);
            this.panel1.Controls.Add(this.DateFulfilledPanel);
            this.panel1.Controls.Add(this.StatusPanel);
            this.panel1.Controls.Add(this.SupplierPanel);
            this.panel1.Controls.Add(this.OrderedByPanel);
            this.panel1.Controls.Add(this.DeliveryDatePanel);
            this.panel1.Controls.Add(this.OrderDatePanel);
            this.panel1.Controls.Add(this.OrderNoPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 34);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(447, 654);
            this.panel1.TabIndex = 88;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.PrintBtn);
            this.flowLayoutPanel1.Controls.Add(this.ConfirmBtn);
            this.flowLayoutPanel1.Controls.Add(this.CancelBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 580);
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
            this.panel9.Location = new System.Drawing.Point(10, 543);
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
            this.panel7.Location = new System.Drawing.Point(10, 280);
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsTbl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemsTbl.ColumnHeadersHeight = 30;
            this.ItemsTbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ItemsTbl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.DisplayItemName,
            this.Quantity,
            this.TotalAmount,
            this.ReceivedQuantity});
            this.ItemsTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsTbl.EnableHeadersVisualStyles = false;
            this.ItemsTbl.GridColor = System.Drawing.Color.White;
            this.ItemsTbl.Location = new System.Drawing.Point(0, 0);
            this.ItemsTbl.MinimumSize = new System.Drawing.Size(427, 263);
            this.ItemsTbl.MultiSelect = false;
            this.ItemsTbl.Name = "ItemsTbl";
            this.ItemsTbl.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemsTbl.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.ItemsTbl.RowHeadersVisible = false;
            this.ItemsTbl.RowHeadersWidth = 51;
            this.ItemsTbl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.ItemsTbl.RowsDefaultCellStyle = dataGridViewCellStyle6;
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
            this.ItemsTbl.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemsTbl_CellClick);
            this.ItemsTbl.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ItemsTbl_CellValidating);
            this.ItemsTbl.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.ItemsTbl_DataBindingComplete);
            this.ItemsTbl.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.ItemsTbl_EditingControlShowing);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // DisplayItemName
            // 
            this.DisplayItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DisplayItemName.DataPropertyName = "DisplayItemName";
            this.DisplayItemName.HeaderText = "Item";
            this.DisplayItemName.Name = "DisplayItemName";
            this.DisplayItemName.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 49;
            // 
            // TotalAmount
            // 
            this.TotalAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.TotalAmount.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle3;
            this.TotalAmount.HeaderText = "Subtotal";
            this.TotalAmount.Name = "TotalAmount";
            this.TotalAmount.ReadOnly = true;
            this.TotalAmount.Width = 77;
            // 
            // ReceivedQuantity
            // 
            this.ReceivedQuantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ReceivedQuantity.DataPropertyName = "ReceivedQuantity";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ReceivedQuantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.ReceivedQuantity.HeaderText = "Delivered";
            this.ReceivedQuantity.MinimumWidth = 10;
            this.ReceivedQuantity.Name = "ReceivedQuantity";
            this.ReceivedQuantity.Visible = false;
            this.ReceivedQuantity.Width = 85;
            // 
            // RemarksPanel
            // 
            this.RemarksPanel.Controls.Add(this.RemarksLbl);
            this.RemarksPanel.Controls.Add(this.label8);
            this.RemarksPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.RemarksPanel.Location = new System.Drawing.Point(10, 250);
            this.RemarksPanel.Name = "RemarksPanel";
            this.RemarksPanel.Size = new System.Drawing.Size(427, 30);
            this.RemarksPanel.TabIndex = 193;
            // 
            // RemarksLbl
            // 
            this.RemarksLbl.AutoEllipsis = true;
            this.RemarksLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RemarksLbl.Location = new System.Drawing.Point(110, 0);
            this.RemarksLbl.Name = "RemarksLbl";
            this.RemarksLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.RemarksLbl.Size = new System.Drawing.Size(303, 21);
            this.RemarksLbl.TabIndex = 4;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label8.Size = new System.Drawing.Size(70, 21);
            this.label8.TabIndex = 3;
            this.label8.Text = "Remarks :";
            // 
            // FulfilledByPanel
            // 
            this.FulfilledByPanel.Controls.Add(this.FulfilledByLbl);
            this.FulfilledByPanel.Controls.Add(this.label4);
            this.FulfilledByPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.FulfilledByPanel.Location = new System.Drawing.Point(10, 220);
            this.FulfilledByPanel.Name = "FulfilledByPanel";
            this.FulfilledByPanel.Size = new System.Drawing.Size(427, 30);
            this.FulfilledByPanel.TabIndex = 187;
            // 
            // FulfilledByLbl
            // 
            this.FulfilledByLbl.AutoSize = true;
            this.FulfilledByLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FulfilledByLbl.Location = new System.Drawing.Point(110, 0);
            this.FulfilledByLbl.Name = "FulfilledByLbl";
            this.FulfilledByLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.FulfilledByLbl.Size = new System.Drawing.Size(0, 21);
            this.FulfilledByLbl.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label4.Size = new System.Drawing.Size(91, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Fullfilled By :";
            // 
            // DateFulfilledPanel
            // 
            this.DateFulfilledPanel.Controls.Add(this.DateFulfilledLbl);
            this.DateFulfilledPanel.Controls.Add(this.label3);
            this.DateFulfilledPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.DateFulfilledPanel.Location = new System.Drawing.Point(10, 190);
            this.DateFulfilledPanel.Name = "DateFulfilledPanel";
            this.DateFulfilledPanel.Size = new System.Drawing.Size(427, 30);
            this.DateFulfilledPanel.TabIndex = 186;
            // 
            // DateFulfilledLbl
            // 
            this.DateFulfilledLbl.AutoSize = true;
            this.DateFulfilledLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateFulfilledLbl.Location = new System.Drawing.Point(110, 0);
            this.DateFulfilledLbl.Name = "DateFulfilledLbl";
            this.DateFulfilledLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.DateFulfilledLbl.Size = new System.Drawing.Size(0, 21);
            this.DateFulfilledLbl.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label3.Size = new System.Drawing.Size(100, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date Fulfilled :";
            // 
            // StatusPanel
            // 
            this.StatusPanel.Controls.Add(this.StatusLbl);
            this.StatusPanel.Controls.Add(this.label2);
            this.StatusPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.StatusPanel.Location = new System.Drawing.Point(10, 160);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(427, 30);
            this.StatusPanel.TabIndex = 185;
            // 
            // StatusLbl
            // 
            this.StatusLbl.AutoSize = true;
            this.StatusLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusLbl.Location = new System.Drawing.Point(110, 0);
            this.StatusLbl.Name = "StatusLbl";
            this.StatusLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.StatusLbl.Size = new System.Drawing.Size(0, 21);
            this.StatusLbl.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label2.Size = new System.Drawing.Size(54, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Status :";
            // 
            // SupplierPanel
            // 
            this.SupplierPanel.Controls.Add(this.SupplierLbl);
            this.SupplierPanel.Controls.Add(this.label5);
            this.SupplierPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SupplierPanel.Location = new System.Drawing.Point(10, 130);
            this.SupplierPanel.Name = "SupplierPanel";
            this.SupplierPanel.Size = new System.Drawing.Size(427, 30);
            this.SupplierPanel.TabIndex = 190;
            // 
            // SupplierLbl
            // 
            this.SupplierLbl.AutoSize = true;
            this.SupplierLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierLbl.Location = new System.Drawing.Point(110, 0);
            this.SupplierLbl.Name = "SupplierLbl";
            this.SupplierLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.SupplierLbl.Size = new System.Drawing.Size(0, 21);
            this.SupplierLbl.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label5.Size = new System.Drawing.Size(69, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "Supplier :";
            // 
            // OrderedByPanel
            // 
            this.OrderedByPanel.Controls.Add(this.OrderedByLbl);
            this.OrderedByPanel.Controls.Add(this.label1);
            this.OrderedByPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderedByPanel.Location = new System.Drawing.Point(10, 100);
            this.OrderedByPanel.Name = "OrderedByPanel";
            this.OrderedByPanel.Size = new System.Drawing.Size(427, 30);
            this.OrderedByPanel.TabIndex = 184;
            // 
            // OrderedByLbl
            // 
            this.OrderedByLbl.AutoSize = true;
            this.OrderedByLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderedByLbl.Location = new System.Drawing.Point(110, 0);
            this.OrderedByLbl.Name = "OrderedByLbl";
            this.OrderedByLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.OrderedByLbl.Size = new System.Drawing.Size(0, 21);
            this.OrderedByLbl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(87, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ordered By :";
            // 
            // DeliveryDatePanel
            // 
            this.DeliveryDatePanel.Controls.Add(this.DeliveryDateLbl);
            this.DeliveryDatePanel.Controls.Add(this.label10);
            this.DeliveryDatePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.DeliveryDatePanel.Location = new System.Drawing.Point(10, 70);
            this.DeliveryDatePanel.Name = "DeliveryDatePanel";
            this.DeliveryDatePanel.Size = new System.Drawing.Size(427, 30);
            this.DeliveryDatePanel.TabIndex = 196;
            // 
            // DeliveryDateLbl
            // 
            this.DeliveryDateLbl.AutoSize = true;
            this.DeliveryDateLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeliveryDateLbl.Location = new System.Drawing.Point(110, 0);
            this.DeliveryDateLbl.Name = "DeliveryDateLbl";
            this.DeliveryDateLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.DeliveryDateLbl.Size = new System.Drawing.Size(0, 21);
            this.DeliveryDateLbl.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label10.Size = new System.Drawing.Size(100, 21);
            this.label10.TabIndex = 3;
            this.label10.Text = "Delivery Date :";
            // 
            // OrderDatePanel
            // 
            this.OrderDatePanel.Controls.Add(this.OrderDateLbl);
            this.OrderDatePanel.Controls.Add(this.label12);
            this.OrderDatePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderDatePanel.Location = new System.Drawing.Point(10, 40);
            this.OrderDatePanel.Name = "OrderDatePanel";
            this.OrderDatePanel.Size = new System.Drawing.Size(427, 30);
            this.OrderDatePanel.TabIndex = 195;
            // 
            // OrderDateLbl
            // 
            this.OrderDateLbl.AutoSize = true;
            this.OrderDateLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderDateLbl.Location = new System.Drawing.Point(110, 0);
            this.OrderDateLbl.Name = "OrderDateLbl";
            this.OrderDateLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.OrderDateLbl.Size = new System.Drawing.Size(0, 21);
            this.OrderDateLbl.TabIndex = 4;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label12.Size = new System.Drawing.Size(84, 21);
            this.label12.TabIndex = 3;
            this.label12.Text = "Order Date :";
            // 
            // OrderNoPanel
            // 
            this.OrderNoPanel.Controls.Add(this.OrderNoLbl);
            this.OrderNoPanel.Controls.Add(this.label14);
            this.OrderNoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OrderNoPanel.Location = new System.Drawing.Point(10, 10);
            this.OrderNoPanel.Name = "OrderNoPanel";
            this.OrderNoPanel.Size = new System.Drawing.Size(427, 30);
            this.OrderNoPanel.TabIndex = 194;
            // 
            // OrderNoLbl
            // 
            this.OrderNoLbl.AutoSize = true;
            this.OrderNoLbl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OrderNoLbl.Location = new System.Drawing.Point(110, 0);
            this.OrderNoLbl.Name = "OrderNoLbl";
            this.OrderNoLbl.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.OrderNoLbl.Size = new System.Drawing.Size(0, 21);
            this.OrderNoLbl.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(0, 0);
            this.label14.Name = "label14";
            this.label14.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.label14.Size = new System.Drawing.Size(72, 21);
            this.label14.TabIndex = 2;
            this.label14.Text = "Order No :";
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
            this.ClientSize = new System.Drawing.Size(449, 689);
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
            this.RemarksPanel.ResumeLayout(false);
            this.RemarksPanel.PerformLayout();
            this.FulfilledByPanel.ResumeLayout(false);
            this.FulfilledByPanel.PerformLayout();
            this.DateFulfilledPanel.ResumeLayout(false);
            this.DateFulfilledPanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            this.SupplierPanel.ResumeLayout(false);
            this.SupplierPanel.PerformLayout();
            this.OrderedByPanel.ResumeLayout(false);
            this.OrderedByPanel.PerformLayout();
            this.DeliveryDatePanel.ResumeLayout(false);
            this.DeliveryDatePanel.PerformLayout();
            this.OrderDatePanel.ResumeLayout(false);
            this.OrderDatePanel.PerformLayout();
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
        private System.Windows.Forms.Panel FulfilledByPanel;
        private System.Windows.Forms.Label FulfilledByLbl;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel DateFulfilledPanel;
        private System.Windows.Forms.Label DateFulfilledLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Label StatusLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel SupplierPanel;
        private System.Windows.Forms.Label SupplierLbl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel OrderedByPanel;
        private System.Windows.Forms.Label OrderedByLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PrintPreviewDialog PrintPreview;
        private System.Drawing.Printing.PrintDocument PrintDocument;
        private System.Windows.Forms.Panel RemarksPanel;
        private System.Windows.Forms.Label RemarksLbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel DeliveryDatePanel;
        private System.Windows.Forms.Label DeliveryDateLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel OrderDatePanel;
        private System.Windows.Forms.Label OrderDateLbl;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel OrderNoPanel;
        private System.Windows.Forms.Label OrderNoLbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReceivedQuantity;
    }
}