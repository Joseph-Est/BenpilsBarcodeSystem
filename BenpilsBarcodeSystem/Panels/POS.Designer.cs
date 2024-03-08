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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(POS));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CartTbl = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.BarcodeTxt = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.CheckoutBtn = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.ChangeLbl = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel13 = new System.Windows.Forms.Panel();
            this.PaymentTxt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.TotalLbl = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.RefreshBtn = new System.Windows.Forms.PictureBox();
            this.DisplayItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SellingPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.decrease = new System.Windows.Forms.DataGridViewImageColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.increase = new System.Windows.Forms.DataGridViewImageColumn();
            this.PriceTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CartTbl)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(30, 45);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.label12.Size = new System.Drawing.Size(75, 18);
            this.label12.TabIndex = 189;
            this.label12.Text = "Barcode : ";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1207, 710);
            this.panel1.TabIndex = 204;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(764, 710);
            this.panel3.TabIndex = 205;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.CartTbl);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 67);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(30, 10, 10, 30);
            this.panel5.Size = new System.Drawing.Size(764, 643);
            this.panel5.TabIndex = 204;
            // 
            // CartTbl
            // 
            this.CartTbl.AllowUserToAddRows = false;
            this.CartTbl.AllowUserToDeleteRows = false;
            this.CartTbl.AllowUserToResizeColumns = false;
            this.CartTbl.AllowUserToResizeRows = false;
            this.CartTbl.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.CartTbl.BackgroundColor = System.Drawing.Color.White;
            this.CartTbl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CartTbl.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CartTbl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.CartTbl.ColumnHeadersHeight = 30;
            this.CartTbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.CartTbl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DisplayItemName,
            this.SellingPrice,
            this.decrease,
            this.Quantity,
            this.increase,
            this.PriceTotal});
            this.CartTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CartTbl.EnableHeadersVisualStyles = false;
            this.CartTbl.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CartTbl.Location = new System.Drawing.Point(30, 10);
            this.CartTbl.MultiSelect = false;
            this.CartTbl.Name = "CartTbl";
            this.CartTbl.ReadOnly = true;
            this.CartTbl.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CartTbl.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.CartTbl.RowHeadersVisible = false;
            this.CartTbl.RowHeadersWidth = 51;
            this.CartTbl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Black;
            this.CartTbl.RowsDefaultCellStyle = dataGridViewCellStyle28;
            this.CartTbl.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CartTbl.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
            this.CartTbl.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.CartTbl.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.CartTbl.RowTemplate.Height = 30;
            this.CartTbl.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CartTbl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.CartTbl.Size = new System.Drawing.Size(724, 603);
            this.CartTbl.TabIndex = 181;
            this.CartTbl.TabStop = false;
            this.CartTbl.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CartTbl_CellContentClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.BarcodeTxt);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(30, 45, 0, 0);
            this.panel4.Size = new System.Drawing.Size(764, 67);
            this.panel4.TabIndex = 203;
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(291, 45);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.panel6.Size = new System.Drawing.Size(473, 22);
            this.panel6.TabIndex = 203;
            // 
            // BarcodeTxt
            // 
            this.BarcodeTxt.Dock = System.Windows.Forms.DockStyle.Left;
            this.BarcodeTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BarcodeTxt.Location = new System.Drawing.Point(105, 45);
            this.BarcodeTxt.Name = "BarcodeTxt";
            this.BarcodeTxt.Size = new System.Drawing.Size(186, 22);
            this.BarcodeTxt.TabIndex = 1;
            this.BarcodeTxt.TextChanged += new System.EventHandler(this.BarcodeTxt_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(764, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 0, 30, 20);
            this.panel2.Size = new System.Drawing.Size(443, 710);
            this.panel2.TabIndex = 204;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 77);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panel8.Size = new System.Drawing.Size(413, 613);
            this.panel8.TabIndex = 205;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(1);
            this.panel9.Size = new System.Drawing.Size(413, 603);
            this.panel9.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.SystemColors.Control;
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Controls.Add(this.panel12);
            this.panel10.Controls.Add(this.panel13);
            this.panel10.Controls.Add(this.panel14);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(1, 1);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panel10.Size = new System.Drawing.Size(411, 601);
            this.panel10.TabIndex = 0;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.CheckoutBtn);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel11.Location = new System.Drawing.Point(10, 534);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(391, 57);
            this.panel11.TabIndex = 217;
            // 
            // CheckoutBtn
            // 
            this.CheckoutBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CheckoutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CheckoutBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CheckoutBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckoutBtn.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckoutBtn.ForeColor = System.Drawing.Color.White;
            this.CheckoutBtn.Location = new System.Drawing.Point(0, 0);
            this.CheckoutBtn.Name = "CheckoutBtn";
            this.CheckoutBtn.Size = new System.Drawing.Size(391, 57);
            this.CheckoutBtn.TabIndex = 198;
            this.CheckoutBtn.TabStop = false;
            this.CheckoutBtn.Text = "Checkout";
            this.CheckoutBtn.UseVisualStyleBackColor = false;
            this.CheckoutBtn.Click += new System.EventHandler(this.CheckoutBtn_Click);
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.SystemColors.Control;
            this.panel12.Controls.Add(this.ChangeLbl);
            this.panel12.Controls.Add(this.label14);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel12.Location = new System.Drawing.Point(10, 102);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panel12.Size = new System.Drawing.Size(391, 65);
            this.panel12.TabIndex = 216;
            // 
            // ChangeLbl
            // 
            this.ChangeLbl.AutoSize = true;
            this.ChangeLbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.ChangeLbl.Font = new System.Drawing.Font("Arial", 14.25F);
            this.ChangeLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ChangeLbl.Location = new System.Drawing.Point(338, 10);
            this.ChangeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ChangeLbl.Name = "ChangeLbl";
            this.ChangeLbl.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.ChangeLbl.Size = new System.Drawing.Size(53, 22);
            this.ChangeLbl.TabIndex = 195;
            this.ChangeLbl.Text = "0.00";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Left;
            this.label14.Font = new System.Drawing.Font("Arial", 14.25F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(0, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 22);
            this.label14.TabIndex = 194;
            this.label14.Text = "Change :";
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.SystemColors.Control;
            this.panel13.Controls.Add(this.PaymentTxt);
            this.panel13.Controls.Add(this.label11);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel13.Location = new System.Drawing.Point(10, 65);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(0, 10, 11, 0);
            this.panel13.Size = new System.Drawing.Size(391, 37);
            this.panel13.TabIndex = 215;
            // 
            // PaymentTxt
            // 
            this.PaymentTxt.Dock = System.Windows.Forms.DockStyle.Right;
            this.PaymentTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PaymentTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.PaymentTxt.Location = new System.Drawing.Point(194, 10);
            this.PaymentTxt.Name = "PaymentTxt";
            this.PaymentTxt.Size = new System.Drawing.Size(186, 26);
            this.PaymentTxt.TabIndex = 2;
            this.PaymentTxt.TabStop = false;
            this.PaymentTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PaymentTxt.TextChanged += new System.EventHandler(this.PaymentTxt_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Left;
            this.label11.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(0, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(161, 22);
            this.label11.TabIndex = 188;
            this.label11.Text = "Payment amount :";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.SystemColors.Control;
            this.panel14.Controls.Add(this.TotalLbl);
            this.panel14.Controls.Add(this.label7);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel14.Location = new System.Drawing.Point(10, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(391, 65);
            this.panel14.TabIndex = 214;
            // 
            // TotalLbl
            // 
            this.TotalLbl.AutoSize = true;
            this.TotalLbl.BackColor = System.Drawing.Color.Transparent;
            this.TotalLbl.Dock = System.Windows.Forms.DockStyle.Right;
            this.TotalLbl.Font = new System.Drawing.Font("Arial", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TotalLbl.Location = new System.Drawing.Point(306, 0);
            this.TotalLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TotalLbl.Name = "TotalLbl";
            this.TotalLbl.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.TotalLbl.Size = new System.Drawing.Size(85, 51);
            this.TotalLbl.TabIndex = 185;
            this.TotalLbl.Text = "0.00";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.label7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.label7.Size = new System.Drawing.Size(60, 42);
            this.label7.TabIndex = 184;
            this.label7.Text = "Total :";
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(413, 77);
            this.panel7.TabIndex = 204;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_minus_30;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Width = 50;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.HeaderText = "";
            this.dataGridViewImageColumn2.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_add_30;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
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
            // DisplayItemName
            // 
            this.DisplayItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DisplayItemName.DataPropertyName = "DisplayItemName";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.DisplayItemName.DefaultCellStyle = dataGridViewCellStyle23;
            this.DisplayItemName.HeaderText = "Item Name";
            this.DisplayItemName.Name = "DisplayItemName";
            this.DisplayItemName.ReadOnly = true;
            // 
            // SellingPrice
            // 
            this.SellingPrice.DataPropertyName = "SellingPrice";
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle24.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.SellingPrice.DefaultCellStyle = dataGridViewCellStyle24;
            this.SellingPrice.HeaderText = "Price";
            this.SellingPrice.Name = "SellingPrice";
            this.SellingPrice.ReadOnly = true;
            // 
            // decrease
            // 
            this.decrease.DataPropertyName = "decrease";
            this.decrease.HeaderText = "";
            this.decrease.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_minus_30;
            this.decrease.Name = "decrease";
            this.decrease.ReadOnly = true;
            this.decrease.Width = 50;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle25;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 70;
            // 
            // increase
            // 
            this.increase.DataPropertyName = "increase";
            this.increase.HeaderText = "";
            this.increase.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_add_30;
            this.increase.Name = "increase";
            this.increase.ReadOnly = true;
            this.increase.Width = 50;
            // 
            // PriceTotal
            // 
            this.PriceTotal.DataPropertyName = "PriceTotal";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle26.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.PriceTotal.DefaultCellStyle = dataGridViewCellStyle26;
            this.PriceTotal.HeaderText = "Subtotal";
            this.PriceTotal.Name = "PriceTotal";
            this.PriceTotal.ReadOnly = true;
            this.PriceTotal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PriceTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // POS
            // 
            this.AcceptButton = this.CheckoutBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 710);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RefreshBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "POS";
            this.Text = "POSS";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.POS_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CartTbl)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RefreshBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox RefreshBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox BarcodeTxt;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.DataGridView CartTbl;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button CheckoutBtn;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label ChangeLbl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.TextBox PaymentTxt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label TotalLbl;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SellingPrice;
        private System.Windows.Forms.DataGridViewImageColumn decrease;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewImageColumn increase;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceTotal;
    }
}