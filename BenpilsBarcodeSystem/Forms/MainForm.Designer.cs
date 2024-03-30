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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.CloseCb = new System.Windows.Forms.CheckBox();
            this.RestoreCb = new System.Windows.Forms.CheckBox();
            this.MinimizeCb = new System.Windows.Forms.CheckBox();
            this.SelectedModuleLbl = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.SidebarPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.DashboardCb = new System.Windows.Forms.CheckBox();
            this.InventoryCb = new System.Windows.Forms.CheckBox();
            this.PurchasingCb = new System.Windows.Forms.CheckBox();
            this.PosCb = new System.Windows.Forms.CheckBox();
            this.ReportsCb = new System.Windows.Forms.CheckBox();
            this.StatisticsCb = new System.Windows.Forms.CheckBox();
            this.UsersCb = new System.Windows.Forms.CheckBox();
            this.SettingsCb = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.LogoutCb = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SidebarPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
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
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.SelectedModuleLbl);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1456, 35);
            this.panel1.TabIndex = 15;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.CloseCb);
            this.flowLayoutPanel1.Controls.Add(this.RestoreCb);
            this.flowLayoutPanel1.Controls.Add(this.MinimizeCb);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1370, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(86, 35);
            this.flowLayoutPanel1.TabIndex = 27;
            // 
            // CloseCb
            // 
            this.CloseCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.CloseCb.AutoCheck = false;
            this.CloseCb.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_close_30;
            this.CloseCb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseCb.FlatAppearance.BorderSize = 0;
            this.CloseCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.CloseCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.CloseCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseCb.Location = new System.Drawing.Point(58, 8);
            this.CloseCb.Name = "CloseCb";
            this.CloseCb.Padding = new System.Windows.Forms.Padding(5);
            this.CloseCb.Size = new System.Drawing.Size(20, 20);
            this.CloseCb.TabIndex = 26;
            this.CloseCb.UseVisualStyleBackColor = true;
            this.CloseCb.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // RestoreCb
            // 
            this.RestoreCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.RestoreCb.AutoCheck = false;
            this.RestoreCb.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_restore_down_30;
            this.RestoreCb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RestoreCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RestoreCb.FlatAppearance.BorderSize = 0;
            this.RestoreCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.RestoreCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.RestoreCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RestoreCb.Location = new System.Drawing.Point(32, 8);
            this.RestoreCb.Name = "RestoreCb";
            this.RestoreCb.Padding = new System.Windows.Forms.Padding(5);
            this.RestoreCb.Size = new System.Drawing.Size(20, 20);
            this.RestoreCb.TabIndex = 25;
            this.RestoreCb.UseVisualStyleBackColor = true;
            this.RestoreCb.Click += new System.EventHandler(this.MaximizeBtn_Click);
            // 
            // MinimizeCb
            // 
            this.MinimizeCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.MinimizeCb.AutoCheck = false;
            this.MinimizeCb.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_subtract_30;
            this.MinimizeCb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MinimizeCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizeCb.FlatAppearance.BorderSize = 0;
            this.MinimizeCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.MinimizeCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.MinimizeCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeCb.Location = new System.Drawing.Point(6, 8);
            this.MinimizeCb.Name = "MinimizeCb";
            this.MinimizeCb.Padding = new System.Windows.Forms.Padding(5);
            this.MinimizeCb.Size = new System.Drawing.Size(20, 20);
            this.MinimizeCb.TabIndex = 24;
            this.MinimizeCb.UseVisualStyleBackColor = true;
            this.MinimizeCb.Click += new System.EventHandler(this.MinimizeBtn_Click);
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
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.SidebarPanel);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 781);
            this.panel2.TabIndex = 19;
            // 
            // SidebarPanel
            // 
            this.SidebarPanel.AutoSize = true;
            this.SidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.SidebarPanel.Controls.Add(this.DashboardCb);
            this.SidebarPanel.Controls.Add(this.InventoryCb);
            this.SidebarPanel.Controls.Add(this.PurchasingCb);
            this.SidebarPanel.Controls.Add(this.PosCb);
            this.SidebarPanel.Controls.Add(this.ReportsCb);
            this.SidebarPanel.Controls.Add(this.StatisticsCb);
            this.SidebarPanel.Controls.Add(this.UsersCb);
            this.SidebarPanel.Controls.Add(this.SettingsCb);
            this.SidebarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SidebarPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.SidebarPanel.Location = new System.Drawing.Point(0, 115);
            this.SidebarPanel.Name = "SidebarPanel";
            this.SidebarPanel.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.SidebarPanel.Size = new System.Drawing.Size(249, 613);
            this.SidebarPanel.TabIndex = 35;
            // 
            // DashboardCb
            // 
            this.DashboardCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.DashboardCb.AutoCheck = false;
            this.DashboardCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DashboardCb.FlatAppearance.BorderSize = 0;
            this.DashboardCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.DashboardCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.DashboardCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.DashboardCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DashboardCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DashboardCb.ForeColor = System.Drawing.SystemColors.Control;
            this.DashboardCb.Image = ((System.Drawing.Image)(resources.GetObject("DashboardCb.Image")));
            this.DashboardCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.DashboardCb.Location = new System.Drawing.Point(3, 21);
            this.DashboardCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.DashboardCb.Name = "DashboardCb";
            this.DashboardCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.DashboardCb.Size = new System.Drawing.Size(246, 50);
            this.DashboardCb.TabIndex = 0;
            this.DashboardCb.Text = "  Dashboard";
            this.DashboardCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DashboardCb.UseVisualStyleBackColor = true;
            this.DashboardCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // InventoryCb
            // 
            this.InventoryCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.InventoryCb.AutoCheck = false;
            this.InventoryCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.InventoryCb.FlatAppearance.BorderSize = 0;
            this.InventoryCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.InventoryCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.InventoryCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.InventoryCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.InventoryCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InventoryCb.ForeColor = System.Drawing.SystemColors.Control;
            this.InventoryCb.Image = ((System.Drawing.Image)(resources.GetObject("InventoryCb.Image")));
            this.InventoryCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.InventoryCb.Location = new System.Drawing.Point(3, 73);
            this.InventoryCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.InventoryCb.Name = "InventoryCb";
            this.InventoryCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.InventoryCb.Size = new System.Drawing.Size(246, 50);
            this.InventoryCb.TabIndex = 1;
            this.InventoryCb.Text = "  Inventory";
            this.InventoryCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.InventoryCb.UseVisualStyleBackColor = true;
            this.InventoryCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // PurchasingCb
            // 
            this.PurchasingCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.PurchasingCb.AutoCheck = false;
            this.PurchasingCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PurchasingCb.FlatAppearance.BorderSize = 0;
            this.PurchasingCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.PurchasingCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.PurchasingCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.PurchasingCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PurchasingCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PurchasingCb.ForeColor = System.Drawing.SystemColors.Control;
            this.PurchasingCb.Image = ((System.Drawing.Image)(resources.GetObject("PurchasingCb.Image")));
            this.PurchasingCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PurchasingCb.Location = new System.Drawing.Point(3, 125);
            this.PurchasingCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.PurchasingCb.Name = "PurchasingCb";
            this.PurchasingCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.PurchasingCb.Size = new System.Drawing.Size(246, 50);
            this.PurchasingCb.TabIndex = 2;
            this.PurchasingCb.Text = "  Purchasing";
            this.PurchasingCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PurchasingCb.UseVisualStyleBackColor = true;
            this.PurchasingCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // PosCb
            // 
            this.PosCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.PosCb.AutoCheck = false;
            this.PosCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PosCb.FlatAppearance.BorderSize = 0;
            this.PosCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.PosCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.PosCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.PosCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PosCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PosCb.ForeColor = System.Drawing.SystemColors.Control;
            this.PosCb.Image = ((System.Drawing.Image)(resources.GetObject("PosCb.Image")));
            this.PosCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PosCb.Location = new System.Drawing.Point(3, 177);
            this.PosCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.PosCb.Name = "PosCb";
            this.PosCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.PosCb.Size = new System.Drawing.Size(246, 50);
            this.PosCb.TabIndex = 3;
            this.PosCb.Text = "  Point of Sales";
            this.PosCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PosCb.UseVisualStyleBackColor = true;
            this.PosCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // ReportsCb
            // 
            this.ReportsCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.ReportsCb.AutoCheck = false;
            this.ReportsCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReportsCb.FlatAppearance.BorderSize = 0;
            this.ReportsCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.ReportsCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ReportsCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ReportsCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReportsCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportsCb.ForeColor = System.Drawing.SystemColors.Control;
            this.ReportsCb.Image = ((System.Drawing.Image)(resources.GetObject("ReportsCb.Image")));
            this.ReportsCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReportsCb.Location = new System.Drawing.Point(3, 229);
            this.ReportsCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.ReportsCb.Name = "ReportsCb";
            this.ReportsCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.ReportsCb.Size = new System.Drawing.Size(246, 50);
            this.ReportsCb.TabIndex = 4;
            this.ReportsCb.Text = "  Reports";
            this.ReportsCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ReportsCb.UseVisualStyleBackColor = true;
            this.ReportsCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // StatisticsCb
            // 
            this.StatisticsCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.StatisticsCb.AutoCheck = false;
            this.StatisticsCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StatisticsCb.FlatAppearance.BorderSize = 0;
            this.StatisticsCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.StatisticsCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.StatisticsCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.StatisticsCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StatisticsCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatisticsCb.ForeColor = System.Drawing.SystemColors.Control;
            this.StatisticsCb.Image = ((System.Drawing.Image)(resources.GetObject("StatisticsCb.Image")));
            this.StatisticsCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.StatisticsCb.Location = new System.Drawing.Point(3, 281);
            this.StatisticsCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.StatisticsCb.Name = "StatisticsCb";
            this.StatisticsCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.StatisticsCb.Size = new System.Drawing.Size(246, 50);
            this.StatisticsCb.TabIndex = 5;
            this.StatisticsCb.Text = "  Statistic Reports";
            this.StatisticsCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.StatisticsCb.UseVisualStyleBackColor = true;
            this.StatisticsCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // UsersCb
            // 
            this.UsersCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.UsersCb.AutoCheck = false;
            this.UsersCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UsersCb.FlatAppearance.BorderSize = 0;
            this.UsersCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.UsersCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.UsersCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.UsersCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UsersCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsersCb.ForeColor = System.Drawing.SystemColors.Control;
            this.UsersCb.Image = ((System.Drawing.Image)(resources.GetObject("UsersCb.Image")));
            this.UsersCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UsersCb.Location = new System.Drawing.Point(3, 333);
            this.UsersCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.UsersCb.Name = "UsersCb";
            this.UsersCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.UsersCb.Size = new System.Drawing.Size(246, 50);
            this.UsersCb.TabIndex = 6;
            this.UsersCb.Text = "  User Credentials";
            this.UsersCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.UsersCb.UseVisualStyleBackColor = true;
            this.UsersCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // SettingsCb
            // 
            this.SettingsCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.SettingsCb.AutoCheck = false;
            this.SettingsCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SettingsCb.FlatAppearance.BorderSize = 0;
            this.SettingsCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.SettingsCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.SettingsCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.SettingsCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SettingsCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SettingsCb.ForeColor = System.Drawing.SystemColors.Control;
            this.SettingsCb.Image = ((System.Drawing.Image)(resources.GetObject("SettingsCb.Image")));
            this.SettingsCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SettingsCb.Location = new System.Drawing.Point(3, 385);
            this.SettingsCb.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.SettingsCb.Name = "SettingsCb";
            this.SettingsCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.SettingsCb.Size = new System.Drawing.Size(246, 50);
            this.SettingsCb.TabIndex = 7;
            this.SettingsCb.Text = "  Settings";
            this.SettingsCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.SettingsCb.UseVisualStyleBackColor = true;
            this.SettingsCb.Click += new System.EventHandler(this.Checkbox_Clicked);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel6.Controls.Add(this.LogoutCb);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 728);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(249, 53);
            this.panel6.TabIndex = 34;
            // 
            // LogoutCb
            // 
            this.LogoutCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.LogoutCb.AutoCheck = false;
            this.LogoutCb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LogoutCb.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LogoutCb.FlatAppearance.BorderSize = 0;
            this.LogoutCb.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.LogoutCb.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.LogoutCb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.LogoutCb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LogoutCb.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.LogoutCb.ForeColor = System.Drawing.SystemColors.Control;
            this.LogoutCb.Image = ((System.Drawing.Image)(resources.GetObject("LogoutCb.Image")));
            this.LogoutCb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LogoutCb.Location = new System.Drawing.Point(0, 3);
            this.LogoutCb.Margin = new System.Windows.Forms.Padding(0);
            this.LogoutCb.Name = "LogoutCb";
            this.LogoutCb.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.LogoutCb.Size = new System.Drawing.Size(249, 50);
            this.LogoutCb.TabIndex = 8;
            this.LogoutCb.Text = "  Logout";
            this.LogoutCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.LogoutCb.UseVisualStyleBackColor = true;
            this.LogoutCb.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(249, 115);
            this.panel4.TabIndex = 33;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(49, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 77);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(249, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1207, 36);
            this.panel3.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 8, 0, 0);
            this.label1.Size = new System.Drawing.Size(90, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Right;
            this.label4.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1153, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 8, 10, 0);
            this.label4.Size = new System.Drawing.Size(54, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "Time:";
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(249, 71);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1207, 735);
            this.MainPanel.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(90, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1010, 36);
            this.label2.TabIndex = 4;
            this.label2.Text = "Designation:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1100, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 8, 10, 0);
            this.label3.Size = new System.Drawing.Size(53, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date:";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(249, 806);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1207, 10);
            this.panel5.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1456, 816);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Dashboard";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.SidebarPanel.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label SelectedModuleLbl;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.FlowLayoutPanel SidebarPanel;
        private System.Windows.Forms.CheckBox DashboardCb;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox InventoryCb;
        private System.Windows.Forms.CheckBox PurchasingCb;
        private System.Windows.Forms.CheckBox PosCb;
        private System.Windows.Forms.CheckBox ReportsCb;
        private System.Windows.Forms.CheckBox StatisticsCb;
        private System.Windows.Forms.CheckBox UsersCb;
        private System.Windows.Forms.CheckBox SettingsCb;
        private System.Windows.Forms.CheckBox LogoutCb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.CheckBox MinimizeCb;
        private System.Windows.Forms.CheckBox CloseCb;
        private System.Windows.Forms.CheckBox RestoreCb;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
    }
}