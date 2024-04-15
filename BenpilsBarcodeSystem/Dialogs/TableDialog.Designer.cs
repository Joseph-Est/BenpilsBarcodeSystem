namespace BenpilsBarcodeSystem.Dialogs
{
    partial class TableDialog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.CloseCb = new System.Windows.Forms.CheckBox();
            this.TitleLbl = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TableTbl = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TotalLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableTbl)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.TitleLbl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.panel2.Size = new System.Drawing.Size(646, 33);
            this.panel2.TabIndex = 89;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.CloseCb);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(608, -1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(37, 33);
            this.flowLayoutPanel1.TabIndex = 85;
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
            this.CloseCb.Location = new System.Drawing.Point(9, 8);
            this.CloseCb.Name = "CloseCb";
            this.CloseCb.Padding = new System.Windows.Forms.Padding(5);
            this.CloseCb.Size = new System.Drawing.Size(20, 20);
            this.CloseCb.TabIndex = 26;
            this.CloseCb.UseVisualStyleBackColor = true;
            this.CloseCb.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // TitleLbl
            // 
            this.TitleLbl.AutoSize = true;
            this.TitleLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleLbl.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.TitleLbl.ForeColor = System.Drawing.Color.White;
            this.TitleLbl.Location = new System.Drawing.Point(8, 8);
            this.TitleLbl.Name = "TitleLbl";
            this.TitleLbl.Size = new System.Drawing.Size(40, 18);
            this.TitleLbl.TabIndex = 84;
            this.TitleLbl.Text = "Title";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel5.Controls.Add(this.TableTbl);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(1, 34);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(20, 20, 20, 0);
            this.panel5.Size = new System.Drawing.Size(646, 278);
            this.panel5.TabIndex = 94;
            // 
            // TableTbl
            // 
            this.TableTbl.AllowUserToAddRows = false;
            this.TableTbl.AllowUserToDeleteRows = false;
            this.TableTbl.AllowUserToResizeColumns = false;
            this.TableTbl.AllowUserToResizeRows = false;
            this.TableTbl.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.TableTbl.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.TableTbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TableTbl.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.TableTbl.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TableTbl.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TableTbl.ColumnHeadersHeight = 30;
            this.TableTbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TableTbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableTbl.EnableHeadersVisualStyles = false;
            this.TableTbl.GridColor = System.Drawing.Color.LightGray;
            this.TableTbl.Location = new System.Drawing.Point(20, 20);
            this.TableTbl.MultiSelect = false;
            this.TableTbl.Name = "TableTbl";
            this.TableTbl.ReadOnly = true;
            this.TableTbl.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableTbl.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.TableTbl.RowHeadersVisible = false;
            this.TableTbl.RowHeadersWidth = 51;
            this.TableTbl.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.TableTbl.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.TableTbl.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.TableTbl.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableTbl.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.TableTbl.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(10, 5, 20, 5);
            this.TableTbl.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.TableTbl.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.TableTbl.RowTemplate.Height = 30;
            this.TableTbl.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TableTbl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TableTbl.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TableTbl.Size = new System.Drawing.Size(606, 258);
            this.TableTbl.TabIndex = 184;
            this.TableTbl.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel1.Controls.Add(this.TotalLbl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 312);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(20, 5, 20, 0);
            this.panel1.Size = new System.Drawing.Size(646, 74);
            this.panel1.TabIndex = 95;
            // 
            // TotalLbl
            // 
            this.TotalLbl.BackColor = System.Drawing.Color.Transparent;
            this.TotalLbl.Cursor = System.Windows.Forms.Cursors.Default;
            this.TotalLbl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TotalLbl.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.TotalLbl.Location = new System.Drawing.Point(20, 27);
            this.TotalLbl.Name = "TotalLbl";
            this.TotalLbl.Size = new System.Drawing.Size(606, 38);
            this.TotalLbl.TabIndex = 133;
            this.TotalLbl.Text = "69,420";
            this.TotalLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(20, 5);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(4, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(606, 22);
            this.label1.TabIndex = 134;
            this.label1.Text = "Total";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TableDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.ClientSize = new System.Drawing.Size(648, 384);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(648, 0);
            this.Name = "TableDialog";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TableDialog";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableTbl)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label TitleLbl;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView TableTbl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox CloseCb;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label TotalLbl;
        private System.Windows.Forms.Label label1;
    }
}