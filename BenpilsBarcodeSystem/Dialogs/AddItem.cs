﻿using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace BenpilsBarcodeSystem
{
    public partial class AddItem : Form
    {
        public Item NewItem = new Item();
        public Supplier CurrentSupplier = new Supplier();  
        public bool IsSupplierItem { get; set; }
        bool canClose = false;

        public AddItem(Supplier supplier = null)
        {
            InitializeComponent();

            InputValidator.AllowOnlyDigits(QuantityTxt);
            InputValidator.AllowOnlyDigits(BarcodeTxt);
            InputValidator.AllowOnlyDigitsAndDecimal(PurchasePriceTxt);
            InputValidator.AllowOnlyDigitsAndDecimal(SellingPriceTxt);

            if (supplier != null)
            {
                IsSupplierItem = true;
                CurrentSupplier = supplier;
            }
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            if (IsSupplierItem) {
                string supplier = $"{CurrentSupplier.ContactName} ({CurrentSupplier.ContactNo})";
                SupplierCb.Enabled = false;
                SupplierCb.Items.Clear();
                SupplierCb.Items.Add(supplier);
                SupplierCb.SelectedItem = supplier;
                QuantityTxt.ReadOnly = true;
                QuantityTxt.Text = "0";
            }
            else
            {
                PopulateSupplier();
            }
        }

        private async void AcceptBtn_Click(object sender, EventArgs e)
        {
            string barcode = BarcodeTxt.Text.Trim();
            string itemName = Util.Capitalize(ItemNameTxt.Text);
            string category = CategoryCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(CategoryCb.Text);
            string brand = BrandCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(BrandCb.Text);
            string motorBrand = MotorBrandCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(MotorBrandCb.Text);
            string size = SizeCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(SizeCb.Text);
            int quantity = InputValidator.ParseToInt(QuantityTxt.Text);



            if (Util.AreTextBoxesNullOrEmpty(BarcodeTxt ,ItemNameTxt))

            {
                MessageBox.Show("Please ensure all required fields are filled in before proceeding.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.IsNullOrEmpty(PurchasePriceTxt.Text.Trim()) && !InputValidator.IsValidPrice(PurchasePriceTxt.Text))
            {
                MessageBox.Show("The purchase price entered is not valid. Please enter a valid number.", "Invalid Purchase Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal purchasePrice = InputValidator.ParseToDecimal(PurchasePriceTxt.Text);

            if (!string.IsNullOrEmpty(SellingPriceTxt.Text.Trim()) && !InputValidator.IsValidPrice(SellingPriceTxt.Text))
            {
                MessageBox.Show("The selling price entered is not valid. Please enter a valid number.", "Invalid Selling Price", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal sellingPrice = InputValidator.ParseToDecimal(SellingPriceTxt.Text);

            InventoryRepository repository = new InventoryRepository();

            if(await repository.IsItemExists(itemName, brand, motorBrand, size))
            {
                MessageBox.Show("The item already exists. Please check the item details.", "Item Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (await repository.IsDataExistsAsync("barcode", BarcodeTxt.Text))
            {
                MessageBox.Show("The barcode you've entered is already in use. Please enter a unique barcode.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (purchasePrice > sellingPrice)
            {
                MessageBox.Show("Invalid input: The purchase price must not be higher than the selling price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (await repository.AddProductAsync(
                barcode,
                itemName,
                category,
                brand,
                motorBrand,
                size,
                quantity,
                purchasePrice,
                sellingPrice,
                CurrentSupplier?.SupplierID
            ))
            {
                MessageBox.Show("The item has been successfully added!", "Addition Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                canClose = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("An error occurred while attempting to save the item. Please try again.", "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void PopulateSupplier()
        {
            SuppliersRepository repository = new SuppliersRepository();
            List<Supplier> suppliers = await repository.GetSuppliersAsync();

            SupplierCb.Items.Clear();
            SupplierCb.Items.Add("-- Optional --");
            SupplierCb.Items.AddRange(suppliers.ToArray());

            SupplierCb.SelectedItem = "-- Optional --";
        }

        private void AddItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose) 
            {
                e.Cancel = true;
            }
        }

        private void SupplierCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsSupplierItem)
            {
                if (SupplierCb.SelectedIndex != 0)
                {
                    if (SupplierCb.SelectedItem is Supplier selectedSupplier)
                    {
                        CurrentSupplier = selectedSupplier;
                    }
                }
                else
                {
                    CurrentSupplier = null;
                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                canClose = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void BarcodeBtn_Click(object sender, EventArgs e)
        {
            GenerateBarcode generateBarcode = new GenerateBarcode
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(this.Location.X + this.Width + 10, this.Location.Y)
            };

            if (generateBarcode.ShowDialog() == DialogResult.OK)
            {
                BarcodeTxt.Text = Clipboard.GetText();
            }
        }

        private void BarcodeTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void BarcodeTxt_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void BarcodeTxt_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = AcceptBtn;
        }

        private async void ComboBox_Enter(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox cb && cb.Items.Count == 0)
            {
                InventoryRepository repository = new InventoryRepository();
                string column = "";
                switch (cb.Name)
                {
                    case "CategoryCb":
                        column = "category";
                        break;
                    case "BrandCb":
                        column = "brand";
                        break;
                    case "MotorBrandCb":
                        column = "motor_brand";
                        break;
                    case "SizeCb":
                        column = "size";
                        break;
                }
                List<string> values = await repository.GetDistinctValuesAsync(column, "N/A");
                cb.Items.AddRange(values.ToArray());
            }
        }

        private void ComboBox_Leave(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox cb && string.IsNullOrEmpty(cb.Text.Trim()))
            {
                cb.SelectedIndex = 0;
            }
        }

        private void NumberTextBox_Leave(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.TextBox tb && string.IsNullOrEmpty(tb.Text.Trim()))
            {
                tb.Text = "0";
            }
        }

        private void PurchasePriceTxt_TextChanged(object sender, EventArgs e)
        {
           
        }
    }
}
