namespace BenpilsBarcodeSystem
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectedModuleLbl = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.DashboardBtn = new System.Windows.Forms.Button();
            this.InventoryBtn = new System.Windows.Forms.Button();
            this.PurchasingBtn = new System.Windows.Forms.Button();
            this.PointOfSalesBtn = new System.Windows.Forms.Button();
            this.ReportsBtn = new System.Windows.Forms.Button();
            this.StatisticsBtn = new System.Windows.Forms.Button();
            this.UsercredentialsBtn = new System.Windows.Forms.Button();
            this.SettingsBtn = new System.Windows.Forms.Button();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MaximizeBtn = new System.Windows.Forms.PictureBox();
            this.CloseBtn = new System.Windows.Forms.PictureBox();
            this.MinimizeBtn = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximizeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(526, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(404, 19);
            this.label5.TabIndex = 10;
            this.label5.Text = "BENPIL MOTORCYCLE PARTS AND ACCESSORIES";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.MaximizeBtn);
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.SelectedModuleLbl);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.MinimizeBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1456, 35);
            this.panel1.TabIndex = 15;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // SelectedModuleLbl
            // 
            this.SelectedModuleLbl.AutoSize = true;
            this.SelectedModuleLbl.BackColor = System.Drawing.Color.Transparent;
            this.SelectedModuleLbl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedModuleLbl.ForeColor = System.Drawing.Color.White;
            this.SelectedModuleLbl.Location = new System.Drawing.Point(9, 8);
            this.SelectedModuleLbl.Name = "SelectedModuleLbl";
            this.SelectedModuleLbl.Size = new System.Drawing.Size(80, 17);
            this.SelectedModuleLbl.TabIndex = 21;
            this.SelectedModuleLbl.Text = "Dashboard";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1310, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Time:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel5.Controls.Add(this.label4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 781);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1456, 35);
            this.panel5.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 746);
            this.panel2.TabIndex = 19;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.DashboardBtn);
            this.flowLayoutPanel1.Controls.Add(this.InventoryBtn);
            this.flowLayoutPanel1.Controls.Add(this.PurchasingBtn);
            this.flowLayoutPanel1.Controls.Add(this.PointOfSalesBtn);
            this.flowLayoutPanel1.Controls.Add(this.ReportsBtn);
            this.flowLayoutPanel1.Controls.Add(this.StatisticsBtn);
            this.flowLayoutPanel1.Controls.Add(this.UsercredentialsBtn);
            this.flowLayoutPanel1.Controls.Add(this.SettingsBtn);
            this.flowLayoutPanel1.Controls.Add(this.LogoutBtn);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 115);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(249, 631);
            this.flowLayoutPanel1.TabIndex = 34;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(249, 115);
            this.panel4.TabIndex = 33;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(249, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1207, 36);
            this.panel3.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1061, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(516, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Designation:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(34, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            // 
            // mainPanel
            // 
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(249, 71);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1207, 710);
            this.mainPanel.TabIndex = 21;
            // 
            // DashboardBtn
            // 
            this.DashboardBtn.BackColor = System.Drawing.Color.White;
            this.DashboardBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DashboardBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DashboardBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_dashboard_30;
            this.DashboardBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DashboardBtn.Location = new System.Drawing.Point(23, 23);
            this.DashboardBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.DashboardBtn.Name = "DashboardBtn";
            this.DashboardBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.DashboardBtn.Size = new System.Drawing.Size(205, 52);
            this.DashboardBtn.TabIndex = 9;
            this.DashboardBtn.Text = "   Dashboard";
            this.DashboardBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DashboardBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DashboardBtn.UseVisualStyleBackColor = false;
            this.DashboardBtn.Click += new System.EventHandler(this.DashboardBtn_Click);
            // 
            // InventoryBtn
            // 
            this.InventoryBtn.BackColor = System.Drawing.Color.White;
            this.InventoryBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InventoryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_inventory_30;
            this.InventoryBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InventoryBtn.Location = new System.Drawing.Point(23, 83);
            this.InventoryBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.InventoryBtn.Name = "InventoryBtn";
            this.InventoryBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.InventoryBtn.Size = new System.Drawing.Size(205, 52);
            this.InventoryBtn.TabIndex = 14;
            this.InventoryBtn.Text = "   Inventory";
            this.InventoryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InventoryBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.InventoryBtn.UseVisualStyleBackColor = false;
            this.InventoryBtn.Click += new System.EventHandler(this.InventoryBtn_Click);
            // 
            // PurchasingBtn
            // 
            this.PurchasingBtn.BackColor = System.Drawing.Color.White;
            this.PurchasingBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PurchasingBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PurchasingBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_basket_30;
            this.PurchasingBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PurchasingBtn.Location = new System.Drawing.Point(23, 143);
            this.PurchasingBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PurchasingBtn.Name = "PurchasingBtn";
            this.PurchasingBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.PurchasingBtn.Size = new System.Drawing.Size(205, 52);
            this.PurchasingBtn.TabIndex = 16;
            this.PurchasingBtn.Text = "   Purchasing";
            this.PurchasingBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PurchasingBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PurchasingBtn.UseVisualStyleBackColor = false;
            this.PurchasingBtn.Click += new System.EventHandler(this.PurchasingBtn_Click);
            // 
            // PointOfSalesBtn
            // 
            this.PointOfSalesBtn.BackColor = System.Drawing.Color.White;
            this.PointOfSalesBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PointOfSalesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PointOfSalesBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_point_of_sales_32;
            this.PointOfSalesBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PointOfSalesBtn.Location = new System.Drawing.Point(23, 203);
            this.PointOfSalesBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.PointOfSalesBtn.Name = "PointOfSalesBtn";
            this.PointOfSalesBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.PointOfSalesBtn.Size = new System.Drawing.Size(205, 52);
            this.PointOfSalesBtn.TabIndex = 32;
            this.PointOfSalesBtn.Text = "   Point of Sales";
            this.PointOfSalesBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PointOfSalesBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PointOfSalesBtn.UseVisualStyleBackColor = false;
            this.PointOfSalesBtn.Click += new System.EventHandler(this.PointOfSalesBtn_Click_1);
            // 
            // ReportsBtn
            // 
            this.ReportsBtn.BackColor = System.Drawing.Color.White;
            this.ReportsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReportsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportsBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_graph_report_30;
            this.ReportsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReportsBtn.Location = new System.Drawing.Point(23, 263);
            this.ReportsBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.ReportsBtn.Name = "ReportsBtn";
            this.ReportsBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ReportsBtn.Size = new System.Drawing.Size(205, 52);
            this.ReportsBtn.TabIndex = 17;
            this.ReportsBtn.Text = "   Reports";
            this.ReportsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReportsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ReportsBtn.UseVisualStyleBackColor = false;
            this.ReportsBtn.Click += new System.EventHandler(this.ReportsBtn_Click);
            // 
            // StatisticsBtn
            // 
            this.StatisticsBtn.BackColor = System.Drawing.Color.White;
            this.StatisticsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StatisticsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatisticsBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_statistics_30;
            this.StatisticsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatisticsBtn.Location = new System.Drawing.Point(23, 323);
            this.StatisticsBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.StatisticsBtn.Name = "StatisticsBtn";
            this.StatisticsBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.StatisticsBtn.Size = new System.Drawing.Size(205, 52);
            this.StatisticsBtn.TabIndex = 18;
            this.StatisticsBtn.Text = "   Statistic Report";
            this.StatisticsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatisticsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.StatisticsBtn.UseVisualStyleBackColor = false;
            this.StatisticsBtn.Click += new System.EventHandler(this.StatisticsBtn_Click);
            // 
            // UsercredentialsBtn
            // 
            this.UsercredentialsBtn.BackColor = System.Drawing.Color.White;
            this.UsercredentialsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UsercredentialsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsercredentialsBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_user_301;
            this.UsercredentialsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UsercredentialsBtn.Location = new System.Drawing.Point(23, 383);
            this.UsercredentialsBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.UsercredentialsBtn.Name = "UsercredentialsBtn";
            this.UsercredentialsBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.UsercredentialsBtn.Size = new System.Drawing.Size(205, 52);
            this.UsercredentialsBtn.TabIndex = 19;
            this.UsercredentialsBtn.Text = "   User Credentials";
            this.UsercredentialsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UsercredentialsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.UsercredentialsBtn.UseVisualStyleBackColor = false;
            this.UsercredentialsBtn.Click += new System.EventHandler(this.UsercredentialsBtn_Click);
            // 
            // SettingsBtn
            // 
            this.SettingsBtn.BackColor = System.Drawing.Color.White;
            this.SettingsBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_settings_30;
            this.SettingsBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsBtn.Location = new System.Drawing.Point(23, 443);
            this.SettingsBtn.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.SettingsBtn.Name = "SettingsBtn";
            this.SettingsBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.SettingsBtn.Size = new System.Drawing.Size(205, 52);
            this.SettingsBtn.TabIndex = 20;
            this.SettingsBtn.Text = "   Settings";
            this.SettingsBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SettingsBtn.UseVisualStyleBackColor = false;
            this.SettingsBtn.Click += new System.EventHandler(this.SettingsBtn_Click);
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.BackColor = System.Drawing.Color.White;
            this.LogoutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogoutBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogoutBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_logout_30;
            this.LogoutBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LogoutBtn.Location = new System.Drawing.Point(23, 503);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.LogoutBtn.Size = new System.Drawing.Size(205, 52);
            this.LogoutBtn.TabIndex = 28;
            this.LogoutBtn.Text = "   Logout";
            this.LogoutBtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LogoutBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.LogoutBtn.UseVisualStyleBackColor = false;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(49, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 77);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // MaximizeBtn
            // 
            this.MaximizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaximizeBtn.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_restore_down_30;
            this.MaximizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MaximizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MaximizeBtn.Location = new System.Drawing.Point(1399, 7);
            this.MaximizeBtn.Name = "MaximizeBtn";
            this.MaximizeBtn.Size = new System.Drawing.Size(20, 20);
            this.MaximizeBtn.TabIndex = 23;
            this.MaximizeBtn.TabStop = false;
            this.MaximizeBtn.Click += new System.EventHandler(this.MaximizeBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBtn.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_close_30;
            this.CloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.Location = new System.Drawing.Point(1426, 7);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(20, 20);
            this.CloseBtn.TabIndex = 22;
            this.CloseBtn.TabStop = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinimizeBtn.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_subtract_30;
            this.MinimizeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MinimizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizeBtn.Location = new System.Drawing.Point(1372, 7);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(20, 20);
            this.MinimizeBtn.TabIndex = 8;
            this.MinimizeBtn.TabStop = false;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 816);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaximizeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox MinimizeBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label SelectedModuleLbl;
        private System.Windows.Forms.PictureBox CloseBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button PointOfSalesBtn;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.Button PurchasingBtn;
        private System.Windows.Forms.Button SettingsBtn;
        private System.Windows.Forms.Button UsercredentialsBtn;
        private System.Windows.Forms.Button InventoryBtn;
        private System.Windows.Forms.Button StatisticsBtn;
        private System.Windows.Forms.Button DashboardBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ReportsBtn;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.PictureBox MaximizeBtn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
    }
}