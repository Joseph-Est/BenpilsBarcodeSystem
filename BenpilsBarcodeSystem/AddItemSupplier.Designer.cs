﻿namespace BenpilsBarcodeSystem
{
    partial class AddItemSupplier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddItemSupplier));
            this.label1 = new System.Windows.Forms.Label();
            this.LvlBarcodeGenerator = new System.Windows.Forms.Label();
            this.generatedpicture = new System.Windows.Forms.PictureBox();
            this.GeneratedBarcodeTxt = new System.Windows.Forms.TextBox();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.CategoryTxt = new System.Windows.Forms.TextBox();
            this.lblcategory = new System.Windows.Forms.Label();
            this.lblunitprice = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lvlmotorbrand = new System.Windows.Forms.Label();
            this.UnitPriceTxt = new System.Windows.Forms.TextBox();
            this.Brandtxt = new System.Windows.Forms.TextBox();
            this.MotorbrandTxt = new System.Windows.Forms.TextBox();
            this.ItemNameTxt = new System.Windows.Forms.TextBox();
            this.lblitemname = new System.Windows.Forms.Label();
            this.lvlbarcode = new System.Windows.Forms.Label();
            this.BarcodeTxt = new System.Windows.Forms.TextBox();
            this.SupplierLabel = new System.Windows.Forms.Label();
            this.CmbSupplier = new System.Windows.Forms.ComboBox();
            this.MinimizeBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AddBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.productIDtxt = new System.Windows.Forms.TextBox();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.GenerateproductidBtn = new System.Windows.Forms.Button();
            this.ManualRegenratetxt = new System.Windows.Forms.TextBox();
            this.RandomRegenLvl = new System.Windows.Forms.Label();
            this.ManualRegenLbl = new System.Windows.Forms.Label();
            this.ManualRegeeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 24);
            this.label1.TabIndex = 84;
            this.label1.Text = "Add Item Supplier";
            // 
            // LvlBarcodeGenerator
            // 
            this.LvlBarcodeGenerator.AutoSize = true;
            this.LvlBarcodeGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LvlBarcodeGenerator.ForeColor = System.Drawing.Color.Black;
            this.LvlBarcodeGenerator.Location = new System.Drawing.Point(478, 112);
            this.LvlBarcodeGenerator.Name = "LvlBarcodeGenerator";
            this.LvlBarcodeGenerator.Size = new System.Drawing.Size(135, 18);
            this.LvlBarcodeGenerator.TabIndex = 106;
            this.LvlBarcodeGenerator.Text = "Barcode Generator";
            this.LvlBarcodeGenerator.UseWaitCursor = true;
            // 
            // generatedpicture
            // 
            this.generatedpicture.Location = new System.Drawing.Point(434, 149);
            this.generatedpicture.Name = "generatedpicture";
            this.generatedpicture.Size = new System.Drawing.Size(199, 72);
            this.generatedpicture.TabIndex = 105;
            this.generatedpicture.TabStop = false;
            this.generatedpicture.UseWaitCursor = true;
            // 
            // GeneratedBarcodeTxt
            // 
            this.GeneratedBarcodeTxt.Location = new System.Drawing.Point(434, 262);
            this.GeneratedBarcodeTxt.Name = "GeneratedBarcodeTxt";
            this.GeneratedBarcodeTxt.ReadOnly = true;
            this.GeneratedBarcodeTxt.Size = new System.Drawing.Size(199, 22);
            this.GeneratedBarcodeTxt.TabIndex = 104;
            this.GeneratedBarcodeTxt.UseWaitCursor = true;
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(647, 259);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(75, 23);
            this.GenerateBtn.TabIndex = 103;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // CategoryTxt
            // 
            this.CategoryTxt.Location = new System.Drawing.Point(136, 324);
            this.CategoryTxt.Name = "CategoryTxt";
            this.CategoryTxt.Size = new System.Drawing.Size(141, 22);
            this.CategoryTxt.TabIndex = 102;
            // 
            // lblcategory
            // 
            this.lblcategory.AutoSize = true;
            this.lblcategory.Location = new System.Drawing.Point(77, 329);
            this.lblcategory.Name = "lblcategory";
            this.lblcategory.Size = new System.Drawing.Size(65, 16);
            this.lblcategory.TabIndex = 100;
            this.lblcategory.Text = "Category:";
            // 
            // lblunitprice
            // 
            this.lblunitprice.AutoSize = true;
            this.lblunitprice.Location = new System.Drawing.Point(77, 286);
            this.lblunitprice.Name = "lblunitprice";
            this.lblunitprice.Size = new System.Drawing.Size(64, 16);
            this.lblunitprice.TabIndex = 98;
            this.lblunitprice.Text = "UnitPrice:";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(91, 247);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(46, 16);
            this.lblBrand.TabIndex = 96;
            this.lblBrand.Text = "Brand:";
            // 
            // lvlmotorbrand
            // 
            this.lvlmotorbrand.AutoSize = true;
            this.lvlmotorbrand.Location = new System.Drawing.Point(61, 208);
            this.lvlmotorbrand.Name = "lvlmotorbrand";
            this.lvlmotorbrand.Size = new System.Drawing.Size(83, 16);
            this.lvlmotorbrand.TabIndex = 95;
            this.lvlmotorbrand.Text = "Motor Brand:";
            // 
            // UnitPriceTxt
            // 
            this.UnitPriceTxt.Location = new System.Drawing.Point(136, 283);
            this.UnitPriceTxt.Name = "UnitPriceTxt";
            this.UnitPriceTxt.Size = new System.Drawing.Size(141, 22);
            this.UnitPriceTxt.TabIndex = 94;
            this.UnitPriceTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UnitPriceTxt_KeyPress);
            // 
            // Brandtxt
            // 
            this.Brandtxt.Location = new System.Drawing.Point(136, 244);
            this.Brandtxt.Name = "Brandtxt";
            this.Brandtxt.Size = new System.Drawing.Size(141, 22);
            this.Brandtxt.TabIndex = 92;
            // 
            // MotorbrandTxt
            // 
            this.MotorbrandTxt.Location = new System.Drawing.Point(136, 208);
            this.MotorbrandTxt.Name = "MotorbrandTxt";
            this.MotorbrandTxt.Size = new System.Drawing.Size(141, 22);
            this.MotorbrandTxt.TabIndex = 91;
            // 
            // ItemNameTxt
            // 
            this.ItemNameTxt.Location = new System.Drawing.Point(136, 171);
            this.ItemNameTxt.Name = "ItemNameTxt";
            this.ItemNameTxt.Size = new System.Drawing.Size(141, 22);
            this.ItemNameTxt.TabIndex = 90;
            // 
            // lblitemname
            // 
            this.lblitemname.AutoSize = true;
            this.lblitemname.Location = new System.Drawing.Point(68, 171);
            this.lblitemname.Name = "lblitemname";
            this.lblitemname.Size = new System.Drawing.Size(75, 16);
            this.lblitemname.TabIndex = 89;
            this.lblitemname.Text = "Item Name:";
            // 
            // lvlbarcode
            // 
            this.lvlbarcode.AutoSize = true;
            this.lvlbarcode.Location = new System.Drawing.Point(79, 132);
            this.lvlbarcode.Name = "lvlbarcode";
            this.lvlbarcode.Size = new System.Drawing.Size(62, 16);
            this.lvlbarcode.TabIndex = 88;
            this.lvlbarcode.Text = "Barcode:";
            // 
            // BarcodeTxt
            // 
            this.BarcodeTxt.Location = new System.Drawing.Point(135, 129);
            this.BarcodeTxt.Name = "BarcodeTxt";
            this.BarcodeTxt.Size = new System.Drawing.Size(142, 22);
            this.BarcodeTxt.TabIndex = 87;
            // 
            // SupplierLabel
            // 
            this.SupplierLabel.AutoSize = true;
            this.SupplierLabel.Location = new System.Drawing.Point(42, 91);
            this.SupplierLabel.Name = "SupplierLabel";
            this.SupplierLabel.Size = new System.Drawing.Size(110, 16);
            this.SupplierLabel.TabIndex = 86;
            this.SupplierLabel.Text = "Choose Supplier:";
            // 
            // CmbSupplier
            // 
            this.CmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbSupplier.FormattingEnabled = true;
            this.CmbSupplier.Location = new System.Drawing.Point(135, 88);
            this.CmbSupplier.Name = "CmbSupplier";
            this.CmbSupplier.Size = new System.Drawing.Size(142, 24);
            this.CmbSupplier.TabIndex = 85;
            this.CmbSupplier.SelectedIndexChanged += new System.EventHandler(this.CmbSupplier_SelectedIndexChanged);
            // 
            // MinimizeBtn
            // 
            this.MinimizeBtn.Image = ((System.Drawing.Image)(resources.GetObject("MinimizeBtn.Image")));
            this.MinimizeBtn.Location = new System.Drawing.Point(684, 0);
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.Size = new System.Drawing.Size(38, 32);
            this.MinimizeBtn.TabIndex = 8;
            this.MinimizeBtn.TabStop = false;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(728, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 28);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.MinimizeBtn);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 38);
            this.panel1.TabIndex = 84;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // AddBtn
            // 
            this.AddBtn.Location = new System.Drawing.Point(94, 437);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(75, 23);
            this.AddBtn.TabIndex = 107;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 361);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 108;
            this.label2.Text = "Product ID:";
            // 
            // productIDtxt
            // 
            this.productIDtxt.Location = new System.Drawing.Point(136, 361);
            this.productIDtxt.Name = "productIDtxt";
            this.productIDtxt.Size = new System.Drawing.Size(141, 22);
            this.productIDtxt.TabIndex = 109;
            this.productIDtxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.productIDtxt_KeyPress);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(202, 437);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(75, 23);
            this.ClearBtn.TabIndex = 110;
            this.ClearBtn.Text = "Clear";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // GenerateproductidBtn
            // 
            this.GenerateproductidBtn.Location = new System.Drawing.Point(292, 361);
            this.GenerateproductidBtn.Name = "GenerateproductidBtn";
            this.GenerateproductidBtn.Size = new System.Drawing.Size(123, 23);
            this.GenerateproductidBtn.TabIndex = 111;
            this.GenerateproductidBtn.Text = "Generate Product ID";
            this.GenerateproductidBtn.UseVisualStyleBackColor = true;
            this.GenerateproductidBtn.Click += new System.EventHandler(this.GenerateproductidBtn_Click);
            // 
            // ManualRegenratetxt
            // 
            this.ManualRegenratetxt.Location = new System.Drawing.Point(434, 236);
            this.ManualRegenratetxt.Name = "ManualRegenratetxt";
            this.ManualRegenratetxt.Size = new System.Drawing.Size(199, 22);
            this.ManualRegenratetxt.TabIndex = 112;
            this.ManualRegenratetxt.UseWaitCursor = true;
            this.ManualRegenratetxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ManualRegenratetxt_KeyPress);
            // 
            // RandomRegenLvl
            // 
            this.RandomRegenLvl.AutoSize = true;
            this.RandomRegenLvl.Location = new System.Drawing.Point(319, 264);
            this.RandomRegenLvl.Name = "RandomRegenLvl";
            this.RandomRegenLvl.Size = new System.Drawing.Size(137, 16);
            this.RandomRegenLvl.TabIndex = 113;
            this.RandomRegenLvl.Text = "Random Regenerate:";
            // 
            // ManualRegenLbl
            // 
            this.ManualRegenLbl.AutoSize = true;
            this.ManualRegenLbl.Location = new System.Drawing.Point(324, 239);
            this.ManualRegenLbl.Name = "ManualRegenLbl";
            this.ManualRegenLbl.Size = new System.Drawing.Size(129, 16);
            this.ManualRegenLbl.TabIndex = 114;
            this.ManualRegenLbl.Text = "Manual Regenerate:";
            // 
            // ManualRegeeBtn
            // 
            this.ManualRegeeBtn.Location = new System.Drawing.Point(647, 234);
            this.ManualRegeeBtn.Name = "ManualRegeeBtn";
            this.ManualRegeeBtn.Size = new System.Drawing.Size(75, 23);
            this.ManualRegeeBtn.TabIndex = 115;
            this.ManualRegeeBtn.Text = "Generate";
            this.ManualRegeeBtn.UseVisualStyleBackColor = true;
            this.ManualRegeeBtn.Click += new System.EventHandler(this.ManualRegeeBtn_Click);
            // 
            // AddItemSupplier
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(770, 491);
            this.Controls.Add(this.ManualRegeeBtn);
            this.Controls.Add(this.ManualRegenLbl);
            this.Controls.Add(this.RandomRegenLvl);
            this.Controls.Add(this.ManualRegenratetxt);
            this.Controls.Add(this.GenerateproductidBtn);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.productIDtxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.LvlBarcodeGenerator);
            this.Controls.Add(this.generatedpicture);
            this.Controls.Add(this.GeneratedBarcodeTxt);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.CategoryTxt);
            this.Controls.Add(this.lblcategory);
            this.Controls.Add(this.lblunitprice);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.lvlmotorbrand);
            this.Controls.Add(this.UnitPriceTxt);
            this.Controls.Add(this.Brandtxt);
            this.Controls.Add(this.MotorbrandTxt);
            this.Controls.Add(this.ItemNameTxt);
            this.Controls.Add(this.lblitemname);
            this.Controls.Add(this.lvlbarcode);
            this.Controls.Add(this.BarcodeTxt);
            this.Controls.Add(this.SupplierLabel);
            this.Controls.Add(this.CmbSupplier);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddItemSupplier";
            this.Text = "AddItemSupplier";
            ((System.ComponentModel.ISupportInitialize)(this.generatedpicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MinimizeBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LvlBarcodeGenerator;
        private System.Windows.Forms.PictureBox generatedpicture;
        private System.Windows.Forms.TextBox GeneratedBarcodeTxt;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.TextBox CategoryTxt;
        private System.Windows.Forms.Label lblcategory;
        private System.Windows.Forms.Label lblunitprice;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lvlmotorbrand;
        private System.Windows.Forms.TextBox UnitPriceTxt;
        private System.Windows.Forms.TextBox Brandtxt;
        private System.Windows.Forms.TextBox MotorbrandTxt;
        private System.Windows.Forms.TextBox ItemNameTxt;
        private System.Windows.Forms.Label lblitemname;
        private System.Windows.Forms.Label lvlbarcode;
        private System.Windows.Forms.TextBox BarcodeTxt;
        private System.Windows.Forms.Label SupplierLabel;
        private System.Windows.Forms.ComboBox CmbSupplier;
        private System.Windows.Forms.PictureBox MinimizeBtn;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox productIDtxt;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.Button GenerateproductidBtn;
        private System.Windows.Forms.TextBox ManualRegenratetxt;
        private System.Windows.Forms.Label RandomRegenLvl;
        private System.Windows.Forms.Label ManualRegenLbl;
        private System.Windows.Forms.Button ManualRegeeBtn;
    }
}