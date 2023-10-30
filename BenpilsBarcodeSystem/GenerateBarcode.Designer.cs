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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateBarcode));
            this.panel1 = new System.Windows.Forms.Panel();
            this.MinimizeBtn = new System.Windows.Forms.PictureBox();
            this.CloseBtn = new System.Windows.Forms.PictureBox();
            this.LvlBarcodeGenerator = new System.Windows.Forms.Label();
            this.generatedpicture = new System.Windows.Forms.PictureBox();
            this.GeneratedBarcodeTxt = new System.Windows.Forms.TextBox();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.RandomRegenLvl = new System.Windows.Forms.Label();
            this.ManualRegenLbl = new System.Windows.Forms.Label();
            this.ManualRegenratetxt = new System.Windows.Forms.TextBox();
            this.ManualGenerateBtn = new System.Windows.Forms.Button();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.MinimizeBtn);
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 38);
            this.panel1.TabIndex = 13;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeBtn.Image")));
            this.MinimizeBtn.Location = new System.Drawing.Point(355, 6);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(38, 32);
            this.MinimizeBtn.TabIndex = 8;
            this.MinimizeBtn.TabStop = false;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Image = ((System.Drawing.Image)(resources.GetObject("CloseBtn.Image")));
            this.CloseBtn.Location = new System.Drawing.Point(399, 7);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(31, 28);
            this.CloseBtn.TabIndex = 7;
            this.CloseBtn.TabStop = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // LvlBarcodeGenerator
            // 
            this.LvlBarcodeGenerator.AutoSize = true;
            this.LvlBarcodeGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LvlBarcodeGenerator.ForeColor = System.Drawing.Color.Black;
            this.LvlBarcodeGenerator.Location = new System.Drawing.Point(172, 93);
            this.LvlBarcodeGenerator.Name = "LvlBarcodeGenerator";
            this.LvlBarcodeGenerator.Size = new System.Drawing.Size(111, 15);
            this.LvlBarcodeGenerator.TabIndex = 110;
            this.LvlBarcodeGenerator.Text = "Barcode Generator";
            this.LvlBarcodeGenerator.UseWaitCursor = true;
            // 
            // generatedpicture
            // 
            this.generatedpicture.Location = new System.Drawing.Point(125, 128);
            this.generatedpicture.Name = "generatedpicture";
            this.generatedpicture.Size = new System.Drawing.Size(199, 72);
            this.generatedpicture.TabIndex = 109;
            this.generatedpicture.TabStop = false;
            this.generatedpicture.UseWaitCursor = true;
            // 
            // GeneratedBarcodeTxt
            // 
            this.GeneratedBarcodeTxt.Location = new System.Drawing.Point(125, 248);
            this.GeneratedBarcodeTxt.Name = "GeneratedBarcodeTxt";
            this.GeneratedBarcodeTxt.ReadOnly = true;
            this.GeneratedBarcodeTxt.Size = new System.Drawing.Size(199, 20);
            this.GeneratedBarcodeTxt.TabIndex = 108;
            this.GeneratedBarcodeTxt.UseWaitCursor = true;
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(341, 248);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(75, 23);
            this.GenerateBtn.TabIndex = 107;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // RandomRegenLvl
            // 
            this.RandomRegenLvl.AutoSize = true;
            this.RandomRegenLvl.Location = new System.Drawing.Point(12, 251);
            this.RandomRegenLvl.Name = "RandomRegenLvl";
            this.RandomRegenLvl.Size = new System.Drawing.Size(109, 13);
            this.RandomRegenLvl.TabIndex = 114;
            this.RandomRegenLvl.Text = "Random Regenerate:";
            // 
            // ManualRegenLbl
            // 
            this.ManualRegenLbl.AutoSize = true;
            this.ManualRegenLbl.Location = new System.Drawing.Point(15, 222);
            this.ManualRegenLbl.Name = "ManualRegenLbl";
            this.ManualRegenLbl.Size = new System.Drawing.Size(104, 13);
            this.ManualRegenLbl.TabIndex = 115;
            this.ManualRegenLbl.Text = "Manual Regenerate:";
            // 
            // ManualRegenratetxt
            // 
            this.ManualRegenratetxt.Location = new System.Drawing.Point(125, 219);
            this.ManualRegenratetxt.Name = "ManualRegenratetxt";
            this.ManualRegenratetxt.Size = new System.Drawing.Size(199, 20);
            this.ManualRegenratetxt.TabIndex = 116;
            this.ManualRegenratetxt.UseWaitCursor = true;
            this.ManualRegenratetxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ManualRegenratetxt_KeyPress);
            // 
            // ManualGenerateBtn
            // 
            this.ManualGenerateBtn.Location = new System.Drawing.Point(341, 219);
            this.ManualGenerateBtn.Name = "ManualGenerateBtn";
            this.ManualGenerateBtn.Size = new System.Drawing.Size(75, 23);
            this.ManualGenerateBtn.TabIndex = 117;
            this.ManualGenerateBtn.Text = "Generate";
            this.ManualGenerateBtn.UseVisualStyleBackColor = true;
            this.ManualGenerateBtn.Click += new System.EventHandler(this.ManualGenerateBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(341, 278);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearBtn.TabIndex = 118;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // GenerateBarcode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(439, 432);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.ManualGenerateBtn);
            this.Controls.Add(this.ManualRegenratetxt);
            this.Controls.Add(this.ManualRegenLbl);
            this.Controls.Add(this.RandomRegenLvl);
            this.Controls.Add(this.LvlBarcodeGenerator);
            this.Controls.Add(this.generatedpicture);
            this.Controls.Add(this.GeneratedBarcodeTxt);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "GenerateBarcode";
            this.Text = "GenerateBarcode";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CloseBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox MinimizeBtn;
        private System.Windows.Forms.PictureBox CloseBtn;
        private System.Windows.Forms.Label LvlBarcodeGenerator;
        private System.Windows.Forms.PictureBox generatedpicture;
        private System.Windows.Forms.TextBox GeneratedBarcodeTxt;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.Label RandomRegenLvl;
        private System.Windows.Forms.Label ManualRegenLbl;
        private System.Windows.Forms.TextBox ManualRegenratetxt;
        private System.Windows.Forms.Button ManualGenerateBtn;
        private System.Windows.Forms.Button ClearBtn;
    }
}