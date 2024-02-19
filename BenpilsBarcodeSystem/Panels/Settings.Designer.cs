namespace BenpilsBarcodeSystem
{
    partial class Settings
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabArchive = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabInventory = new System.Windows.Forms.TabPage();
            this.RetrievedItemBtn = new System.Windows.Forms.Button();
            this.dataGridArchived = new System.Windows.Forms.DataGridView();
            this.TxtSearchBar = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabSupplier = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabUsercredential = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabAutomatic = new System.Windows.Forms.TabPage();
            this.tabManual = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabArchive.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabInventory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridArchived)).BeginInit();
            this.tabSupplier.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabUsercredential.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabArchive);
            this.tabControl1.Controls.Add(this.tabAutomatic);
            this.tabControl1.Controls.Add(this.tabManual);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1207, 710);
            this.tabControl1.TabIndex = 0;
            // 
            // tabArchive
            // 
            this.tabArchive.Controls.Add(this.tabControl2);
            this.tabArchive.Location = new System.Drawing.Point(4, 22);
            this.tabArchive.Name = "tabArchive";
            this.tabArchive.Padding = new System.Windows.Forms.Padding(3);
            this.tabArchive.Size = new System.Drawing.Size(1199, 684);
            this.tabArchive.TabIndex = 0;
            this.tabArchive.Text = "Archive Data";
            this.tabArchive.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabInventory);
            this.tabControl2.Controls.Add(this.tabSupplier);
            this.tabControl2.Controls.Add(this.tabUsercredential);
            this.tabControl2.Location = new System.Drawing.Point(6, 18);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1185, 649);
            this.tabControl2.TabIndex = 0;
            // 
            // tabInventory
            // 
            this.tabInventory.Controls.Add(this.RetrievedItemBtn);
            this.tabInventory.Controls.Add(this.dataGridArchived);
            this.tabInventory.Controls.Add(this.TxtSearchBar);
            this.tabInventory.Controls.Add(this.label14);
            this.tabInventory.Location = new System.Drawing.Point(4, 22);
            this.tabInventory.Name = "tabInventory";
            this.tabInventory.Padding = new System.Windows.Forms.Padding(3);
            this.tabInventory.Size = new System.Drawing.Size(1177, 623);
            this.tabInventory.TabIndex = 0;
            this.tabInventory.Text = "Inventory";
            this.tabInventory.UseVisualStyleBackColor = true;
            // 
            // RetrievedItemBtn
            // 
            this.RetrievedItemBtn.Location = new System.Drawing.Point(1005, 555);
            this.RetrievedItemBtn.Name = "RetrievedItemBtn";
            this.RetrievedItemBtn.Size = new System.Drawing.Size(169, 47);
            this.RetrievedItemBtn.TabIndex = 46;
            this.RetrievedItemBtn.Text = "Retrieved Items";
            this.RetrievedItemBtn.UseVisualStyleBackColor = true;
            this.RetrievedItemBtn.Click += new System.EventHandler(this.RetrievedItemBtn_Click);
            // 
            // dataGridArchived
            // 
            this.dataGridArchived.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridArchived.Location = new System.Drawing.Point(6, 40);
            this.dataGridArchived.Name = "dataGridArchived";
            this.dataGridArchived.RowHeadersWidth = 51;
            this.dataGridArchived.Size = new System.Drawing.Size(1168, 497);
            this.dataGridArchived.TabIndex = 45;
            // 
            // TxtSearchBar
            // 
            this.TxtSearchBar.Location = new System.Drawing.Point(61, 17);
            this.TxtSearchBar.Margin = new System.Windows.Forms.Padding(2);
            this.TxtSearchBar.Name = "TxtSearchBar";
            this.TxtSearchBar.Size = new System.Drawing.Size(130, 20);
            this.TxtSearchBar.TabIndex = 43;
            this.TxtSearchBar.UseWaitCursor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(8, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 15);
            this.label14.TabIndex = 44;
            this.label14.Text = "Search:";
            this.label14.UseWaitCursor = true;
            // 
            // tabSupplier
            // 
            this.tabSupplier.Controls.Add(this.dataGridView2);
            this.tabSupplier.Controls.Add(this.textBox1);
            this.tabSupplier.Controls.Add(this.label4);
            this.tabSupplier.Location = new System.Drawing.Point(4, 22);
            this.tabSupplier.Name = "tabSupplier";
            this.tabSupplier.Padding = new System.Windows.Forms.Padding(3);
            this.tabSupplier.Size = new System.Drawing.Size(1177, 623);
            this.tabSupplier.TabIndex = 1;
            this.tabSupplier.Text = "Supplier";
            this.tabSupplier.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(7, 42);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.Size = new System.Drawing.Size(1309, 569);
            this.dataGridView2.TabIndex = 48;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(61, 11);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 20);
            this.textBox1.TabIndex = 46;
            this.textBox1.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 47;
            this.label4.Text = "Search:";
            this.label4.UseWaitCursor = true;
            // 
            // tabUsercredential
            // 
            this.tabUsercredential.Controls.Add(this.dataGridView3);
            this.tabUsercredential.Controls.Add(this.textBox2);
            this.tabUsercredential.Controls.Add(this.label7);
            this.tabUsercredential.Location = new System.Drawing.Point(4, 22);
            this.tabUsercredential.Name = "tabUsercredential";
            this.tabUsercredential.Size = new System.Drawing.Size(1177, 623);
            this.tabUsercredential.TabIndex = 2;
            this.tabUsercredential.Text = "UserCredential";
            this.tabUsercredential.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(7, 42);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.Size = new System.Drawing.Size(1309, 569);
            this.dataGridView3.TabIndex = 48;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(61, 11);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(130, 20);
            this.textBox2.TabIndex = 46;
            this.textBox2.UseWaitCursor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 12);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 15);
            this.label7.TabIndex = 47;
            this.label7.Text = "Search:";
            this.label7.UseWaitCursor = true;
            // 
            // tabAutomatic
            // 
            this.tabAutomatic.Location = new System.Drawing.Point(4, 22);
            this.tabAutomatic.Name = "tabAutomatic";
            this.tabAutomatic.Padding = new System.Windows.Forms.Padding(3);
            this.tabAutomatic.Size = new System.Drawing.Size(1199, 684);
            this.tabAutomatic.TabIndex = 1;
            this.tabAutomatic.Text = "Automatic Backup";
            this.tabAutomatic.UseVisualStyleBackColor = true;
            // 
            // tabManual
            // 
            this.tabManual.Location = new System.Drawing.Point(4, 22);
            this.tabManual.Name = "tabManual";
            this.tabManual.Size = new System.Drawing.Size(1199, 684);
            this.tabManual.TabIndex = 2;
            this.tabManual.Text = "Manual Backup";
            this.tabManual.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 710);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Settings";
            this.Text = "Settings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Settings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabArchive.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabInventory.ResumeLayout(false);
            this.tabInventory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridArchived)).EndInit();
            this.tabSupplier.ResumeLayout(false);
            this.tabSupplier.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabUsercredential.ResumeLayout(false);
            this.tabUsercredential.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabArchive;
        private System.Windows.Forms.TabPage tabAutomatic;
        private System.Windows.Forms.TabPage tabManual;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabInventory;
        private System.Windows.Forms.TabPage tabSupplier;
        private System.Windows.Forms.TabPage tabUsercredential;
        private System.Windows.Forms.DataGridView dataGridArchived;
        private System.Windows.Forms.TextBox TxtSearchBar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button RetrievedItemBtn;
    }
}