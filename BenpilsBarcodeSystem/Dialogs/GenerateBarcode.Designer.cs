namespace BenpilsBarcodeSystem
{
    partial class GenerateBarcode
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LvlBarcodeGenerator = new System.Windows.Forms.Label();
            this.ManualRegenratetxt = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.CopyBtn = new System.Windows.Forms.Button();
            this.generatedpicture = new System.Windows.Forms.PictureBox();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.ManualGenerateBtn = new System.Windows.Forms.Button();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.LvlBarcodeGenerator);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.panel1.Size = new System.Drawing.Size(289, 33);
            this.panel1.TabIndex = 13;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // LvlBarcodeGenerator
            // 
            this.LvlBarcodeGenerator.AutoSize = true;
            this.LvlBarcodeGenerator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LvlBarcodeGenerator.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
            this.LvlBarcodeGenerator.ForeColor = System.Drawing.Color.White;
            this.LvlBarcodeGenerator.Location = new System.Drawing.Point(8, 8);
            this.LvlBarcodeGenerator.Name = "LvlBarcodeGenerator";
            this.LvlBarcodeGenerator.Size = new System.Drawing.Size(145, 18);
            this.LvlBarcodeGenerator.TabIndex = 110;
            this.LvlBarcodeGenerator.Text = "Barcode Generator";
            // 
            // ManualRegenratetxt
            // 
            this.ManualRegenratetxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ManualRegenratetxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManualRegenratetxt.Location = new System.Drawing.Point(21, 195);
            this.ManualRegenratetxt.MaxLength = 15;
            this.ManualRegenratetxt.Multiline = true;
            this.ManualRegenratetxt.Name = "ManualRegenratetxt";
            this.ManualRegenratetxt.Size = new System.Drawing.Size(247, 20);
            this.ManualRegenratetxt.TabIndex = 1;
            this.ManualRegenratetxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ManualRegenratetxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ManualRegenratetxt_KeyPress);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.CopyBtn);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.ClearBtn);
            this.panel2.Controls.Add(this.ManualGenerateBtn);
            this.panel2.Controls.Add(this.GenerateBtn);
            this.panel2.Controls.Add(this.ManualRegenratetxt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(1, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 302);
            this.panel2.TabIndex = 119;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(79)))), ((int)(((byte)(79)))));
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(1);
            this.panel3.Size = new System.Drawing.Size(291, 337);
            this.panel3.TabIndex = 120;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.generatedpicture);
            this.panel4.Location = new System.Drawing.Point(21, 18);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(247, 150);
            this.panel4.TabIndex = 119;
            // 
            // CopyBtn
            // 
            this.CopyBtn.BackColor = System.Drawing.Color.White;
            this.CopyBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CopyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CopyBtn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_documents_15;
            this.CopyBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CopyBtn.Location = new System.Drawing.Point(21, 258);
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(247, 25);
            this.CopyBtn.TabIndex = 2;
            this.CopyBtn.Text = "  Copy and Close";
            this.CopyBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.CopyBtn.UseVisualStyleBackColor = false;
            this.CopyBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // generatedpicture
            // 
            this.generatedpicture.BackColor = System.Drawing.Color.White;
            this.generatedpicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.generatedpicture.Location = new System.Drawing.Point(64, 30);
            this.generatedpicture.Name = "generatedpicture";
            this.generatedpicture.Size = new System.Drawing.Size(127, 127);
            this.generatedpicture.TabIndex = 109;
            this.generatedpicture.TabStop = false;
            this.generatedpicture.Click += new System.EventHandler(this.generatedpicture_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.BackColor = System.Drawing.Color.White;
            this.ClearBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ClearBtn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_erase_15;
            this.ClearBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ClearBtn.Location = new System.Drawing.Point(190, 224);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(78, 25);
            this.ClearBtn.TabIndex = 5;
            this.ClearBtn.Text = " Clear";
            this.ClearBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ClearBtn.UseVisualStyleBackColor = false;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // ManualGenerateBtn
            // 
            this.ManualGenerateBtn.BackColor = System.Drawing.Color.White;
            this.ManualGenerateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ManualGenerateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ManualGenerateBtn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManualGenerateBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_pencil_15;
            this.ManualGenerateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ManualGenerateBtn.Location = new System.Drawing.Point(21, 224);
            this.ManualGenerateBtn.Name = "ManualGenerateBtn";
            this.ManualGenerateBtn.Size = new System.Drawing.Size(78, 25);
            this.ManualGenerateBtn.TabIndex = 3;
            this.ManualGenerateBtn.Text = " Manual";
            this.ManualGenerateBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ManualGenerateBtn.UseVisualStyleBackColor = false;
            this.ManualGenerateBtn.Click += new System.EventHandler(this.ManualGenerateBtn_Click);
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.BackColor = System.Drawing.Color.White;
            this.GenerateBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GenerateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GenerateBtn.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerateBtn.Image = global::BenpilsBarcodeSystem.Properties.Resources.icons8_random_15;
            this.GenerateBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.GenerateBtn.Location = new System.Drawing.Point(105, 224);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(78, 25);
            this.GenerateBtn.TabIndex = 4;
            this.GenerateBtn.Text = " Random";
            this.GenerateBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.GenerateBtn.UseVisualStyleBackColor = false;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackgroundImage = global::BenpilsBarcodeSystem.Properties.Resources.icons8_close_30;
            this.CloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseBtn.Location = new System.Drawing.Point(262, 7);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(20, 20);
            this.CloseBtn.TabIndex = 7;
            this.CloseBtn.TabStop = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Window;
            this.panel5.Location = new System.Drawing.Point(21, 191);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(247, 5);
            this.panel5.TabIndex = 121;
            // 
            // GenerateBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(291, 337);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GenerateBarcode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GenerateBarcode";
            this.Load += new System.EventHandler(this.GenerateBarcode_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox CloseBtn;
        private System.Windows.Forms.Label LvlBarcodeGenerator;
        private System.Windows.Forms.PictureBox generatedpicture;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.TextBox ManualRegenratetxt;
        private System.Windows.Forms.Button ManualGenerateBtn;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button CopyBtn;
        private System.Windows.Forms.Panel panel5;
    }
}