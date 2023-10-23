namespace BenpilsBarcodeSystem
{
    partial class AddSupplierItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSupplierItem));
            this.panel1 = new System.Windows.Forms.Panel();
            this.MinimizeBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SupplierLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lvlbarcode = new System.Windows.Forms.Label();
            this.lblitemname = new System.Windows.Forms.Label();
            this.ItemNameTxt = new System.Windows.Forms.TextBox();
            this.MotorbrandTxt = new System.Windows.Forms.TextBox();
            this.Brandtxt = new System.Windows.Forms.TextBox();
            this.PriceCodeTxt = new System.Windows.Forms.TextBox();
            this.UnitPriceTxt = new System.Windows.Forms.TextBox();
            this.lvlmotorbrand = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblPricecode = new System.Windows.Forms.Label();
            this.lblunitprice = new System.Windows.Forms.Label();
            this.LblQuantity = new System.Windows.Forms.Label();
            this.lblcategory = new System.Windows.Forms.Label();
            this.NumericQuantity = new System.Windows.Forms.NumericUpDown();
            this.CategoryTxt = new System.Windows.Forms.TextBox();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.GeneratedBarcodeTxt = new System.Windows.Forms.TextBox();
            this.generatedpicture = new System.Windows.Forms.PictureBox();
            this.LvlBarcodeGenerator = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MinimizeBtn);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 38);
            this.panel1.TabIndex = 13;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeBtn.Image")));
            this.MinimizeBtn.Location = new System.Drawing.Point(573, 3);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(38, 32);
            this.MinimizeBtn.TabIndex = 8;
            this.MinimizeBtn.TabStop = false;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(617, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 28);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(135, 88);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 21);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // SupplierLabel
            // 
            this.SupplierLabel.AutoSize = true;
            this.SupplierLabel.Location = new System.Drawing.Point(42, 91);
            this.SupplierLabel.Name = "SupplierLabel";
            this.SupplierLabel.Size = new System.Drawing.Size(87, 13);
            this.SupplierLabel.TabIndex = 15;
            this.SupplierLabel.Text = "Choose Supplier:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(135, 129);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 20);
            this.textBox1.TabIndex = 16;
            // 
            // lvlbarcode
            // 
            this.lvlbarcode.AutoSize = true;
            this.lvlbarcode.Location = new System.Drawing.Point(79, 132);
            this.lvlbarcode.Name = "lvlbarcode";
            this.lvlbarcode.Size = new System.Drawing.Size(50, 13);
            this.lvlbarcode.TabIndex = 17;
            this.lvlbarcode.Text = "Barcode:";
            // 
            // lblitemname
            // 
            this.lblitemname.AutoSize = true;
            this.lblitemname.Location = new System.Drawing.Point(68, 171);
            this.lblitemname.Name = "lblitemname";
            this.lblitemname.Size = new System.Drawing.Size(61, 13);
            this.lblitemname.TabIndex = 18;
            this.lblitemname.Text = "Item Name:";
            // 
            // ItemNameTxt
            // 
            this.ItemNameTxt.Location = new System.Drawing.Point(136, 171);
            this.ItemNameTxt.Name = "ItemNameTxt";
            this.ItemNameTxt.Size = new System.Drawing.Size(141, 20);
            this.ItemNameTxt.TabIndex = 19;
            // 
            // MotorbrandTxt
            // 
            this.MotorbrandTxt.Location = new System.Drawing.Point(136, 208);
            this.MotorbrandTxt.Name = "MotorbrandTxt";
            this.MotorbrandTxt.Size = new System.Drawing.Size(141, 20);
            this.MotorbrandTxt.TabIndex = 20;
            // 
            // Brandtxt
            // 
            this.Brandtxt.Location = new System.Drawing.Point(136, 244);
            this.Brandtxt.Name = "Brandtxt";
            this.Brandtxt.Size = new System.Drawing.Size(141, 20);
            this.Brandtxt.TabIndex = 21;
            // 
            // PriceCodeTxt
            // 
            this.PriceCodeTxt.Location = new System.Drawing.Point(135, 280);
            this.PriceCodeTxt.Name = "PriceCodeTxt";
            this.PriceCodeTxt.Size = new System.Drawing.Size(141, 20);
            this.PriceCodeTxt.TabIndex = 22;
            // 
            // UnitPriceTxt
            // 
            this.UnitPriceTxt.Location = new System.Drawing.Point(136, 321);
            this.UnitPriceTxt.Name = "UnitPriceTxt";
            this.UnitPriceTxt.Size = new System.Drawing.Size(141, 20);
            this.UnitPriceTxt.TabIndex = 23;
            // 
            // lvlmotorbrand
            // 
            this.lvlmotorbrand.AutoSize = true;
            this.lvlmotorbrand.Location = new System.Drawing.Point(61, 208);
            this.lvlmotorbrand.Name = "lvlmotorbrand";
            this.lvlmotorbrand.Size = new System.Drawing.Size(68, 13);
            this.lvlmotorbrand.TabIndex = 25;
            this.lvlmotorbrand.Text = "Motor Brand:";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(91, 247);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(38, 13);
            this.lblBrand.TabIndex = 26;
            this.lblBrand.Text = "Brand:";
            // 
            // lblPricecode
            // 
            this.lblPricecode.AutoSize = true;
            this.lblPricecode.Location = new System.Drawing.Point(70, 283);
            this.lblPricecode.Name = "lblPricecode";
            this.lblPricecode.Size = new System.Drawing.Size(59, 13);
            this.lblPricecode.TabIndex = 27;
            this.lblPricecode.Text = "PriceCode:";
            // 
            // lblunitprice
            // 
            this.lblunitprice.AutoSize = true;
            this.lblunitprice.Location = new System.Drawing.Point(75, 324);
            this.lblunitprice.Name = "lblunitprice";
            this.lblunitprice.Size = new System.Drawing.Size(53, 13);
            this.lblunitprice.TabIndex = 28;
            this.lblunitprice.Text = "UnitPrice:";
            // 
            // LblQuantity
            // 
            this.LblQuantity.AutoSize = true;
            this.LblQuantity.Location = new System.Drawing.Point(79, 363);
            this.LblQuantity.Name = "LblQuantity";
            this.LblQuantity.Size = new System.Drawing.Size(49, 13);
            this.LblQuantity.TabIndex = 29;
            this.LblQuantity.Text = "Quantity:";
            // 
            // lblcategory
            // 
            this.lblcategory.AutoSize = true;
            this.lblcategory.Location = new System.Drawing.Point(75, 404);
            this.lblcategory.Name = "lblcategory";
            this.lblcategory.Size = new System.Drawing.Size(52, 13);
            this.lblcategory.TabIndex = 31;
            this.lblcategory.Text = "Category:";
            // 
            // NumericQuantity
            // 
            this.NumericQuantity.Location = new System.Drawing.Point(136, 363);
            this.NumericQuantity.Name = "NumericQuantity";
            this.NumericQuantity.Size = new System.Drawing.Size(141, 20);
            this.NumericQuantity.TabIndex = 32;
            // 
            // CategoryTxt
            // 
            this.CategoryTxt.Location = new System.Drawing.Point(136, 404);
            this.CategoryTxt.Name = "CategoryTxt";
            this.CategoryTxt.Size = new System.Drawing.Size(141, 20);
            this.CategoryTxt.TabIndex = 33;
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(446, 324);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(75, 23);
            this.GenerateBtn.TabIndex = 34;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // GeneratedBarcodeTxt
            // 
            this.GeneratedBarcodeTxt.Location = new System.Drawing.Point(391, 298);
            this.GeneratedBarcodeTxt.Name = "GeneratedBarcodeTxt";
            this.GeneratedBarcodeTxt.Size = new System.Drawing.Size(199, 20);
            this.GeneratedBarcodeTxt.TabIndex = 81;
            this.GeneratedBarcodeTxt.UseWaitCursor = true;
            // 
            // generatedpicture
            // 
            this.generatedpicture.Location = new System.Drawing.Point(391, 208);
            this.generatedpicture.Name = "generatedpicture";
            this.generatedpicture.Size = new System.Drawing.Size(199, 72);
            this.generatedpicture.TabIndex = 82;
            this.generatedpicture.TabStop = false;
            this.generatedpicture.UseWaitCursor = true;
            // 
            // LvlBarcodeGenerator
            // 
            this.LvlBarcodeGenerator.AutoSize = true;
            this.LvlBarcodeGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LvlBarcodeGenerator.ForeColor = System.Drawing.Color.Black;
            this.LvlBarcodeGenerator.Location = new System.Drawing.Point(430, 176);
            this.LvlBarcodeGenerator.Name = "LvlBarcodeGenerator";
            this.LvlBarcodeGenerator.Size = new System.Drawing.Size(111, 15);
            this.LvlBarcodeGenerator.TabIndex = 83;
            this.LvlBarcodeGenerator.Text = "Barcode Generator";
            this.LvlBarcodeGenerator.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 18);
            this.label1.TabIndex = 84;
            this.label1.Text = "Add Supplier Item";
            // 
            // AddSupplierItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(661, 450);
            this.Controls.Add(this.LvlBarcodeGenerator);
            this.Controls.Add(this.generatedpicture);
            this.Controls.Add(this.GeneratedBarcodeTxt);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.CategoryTxt);
            this.Controls.Add(this.NumericQuantity);
            this.Controls.Add(this.lblcategory);
            this.Controls.Add(this.LblQuantity);
            this.Controls.Add(this.lblunitprice);
            this.Controls.Add(this.lblPricecode);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lvlmotorbrand);
            this.Controls.Add(this.UnitPriceTxt);
            this.Controls.Add(this.PriceCodeTxt);
            this.Controls.Add(this.Brandtxt);
            this.Controls.Add(this.MotorbrandTxt);
            this.Controls.Add(this.ItemNameTxt);
            this.Controls.Add(this.lblitemname);
            this.Controls.Add(this.lvlbarcode);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SupplierLabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddSupplierItem";
            this.Text = "AddSupplierItem";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox MinimizeBtn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label SupplierLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lvlbarcode;
        private System.Windows.Forms.Label lblitemname;
        private System.Windows.Forms.TextBox ItemNameTxt;
        private System.Windows.Forms.TextBox MotorbrandTxt;
        private System.Windows.Forms.TextBox Brandtxt;
        private System.Windows.Forms.TextBox PriceCodeTxt;
        private System.Windows.Forms.TextBox UnitPriceTxt;
        private System.Windows.Forms.Label lvlmotorbrand;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblPricecode;
        private System.Windows.Forms.Label lblunitprice;
        private System.Windows.Forms.Label LblQuantity;
        private System.Windows.Forms.Label lblcategory;
        private System.Windows.Forms.NumericUpDown NumericQuantity;
        private System.Windows.Forms.TextBox CategoryTxt;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.TextBox GeneratedBarcodeTxt;
        private System.Windows.Forms.PictureBox generatedpicture;
        private System.Windows.Forms.Label LvlBarcodeGenerator;
        private System.Windows.Forms.Label label1;
    }
}