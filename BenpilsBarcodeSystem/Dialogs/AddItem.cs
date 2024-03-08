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
        public bool isSupplierItem { get; set; }
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
                isSupplierItem = true;
                CurrentSupplier = supplier;
            }
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            if (isSupplierItem) {
                string supplier = $"{CurrentSupplier.ContactName} ({CurrentSupplier.ContactNo})";
                SupplierCb.Enabled = false;
                SupplierCb.Items.Clear();
                SupplierCb.Items.Add(supplier);
                SupplierCb.SelectedItem = supplier;
            }
            else
            {
                PopulateSupplier();
            }
        }

        private async void AcceptBtn_Click(object sender, EventArgs e)
        {
            AcceptBtn.Text = "Saving.....";

            string barcode = BarcodeTxt.Text.Trim();
            string itemName = Util.Capitalize(ItemNameTxt.Text);
            string category = CategoryCb.Text.Trim().ToLower() == "none" ? "N/A" : Util.CapitalizeOrNA(CategoryCb.Text);
            string brand = BrandCb.Text.Trim().ToLower() == "none" ? "N/A" : Util.CapitalizeOrNA(BrandCb.Text);
            string motorBrand = MotorBrandCb.Text.Trim().ToLower() == "none" ? "N/A" : Util.CapitalizeOrNA(MotorBrandCb.Text);
            string size = Util.CapitalizeOrNA(SizeTxt.Text);
            int quantity = InputValidator.ParseToInt(QuantityTxt.Text);

            if (Util.AreTextBoxesNullOrEmpty(BarcodeTxt, ItemNameTxt))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            if (!string.IsNullOrEmpty(PurchasePriceTxt.Text.Trim()) && !InputValidator.IsValidPrice(PurchasePriceTxt.Text))
            {
                MessageBox.Show("Invalid purchase price");
                return;
            }

            decimal purchasePrice = InputValidator.ParseToDecimal(PurchasePriceTxt.Text);

            if (!string.IsNullOrEmpty(SellingPriceTxt.Text.Trim()) && !InputValidator.IsValidPrice(SellingPriceTxt.Text))
            {
                MessageBox.Show("Invalid selling price");
                return;
            }

            decimal sellingPrice = InputValidator.ParseToDecimal(SellingPriceTxt.Text);

            InventoryRepository repository = new InventoryRepository();

            if(await repository.isItemExists(itemName, brand, motorBrand, size))
            {
                MessageBox.Show("Item already exists");
                return;
            }

            if (await repository.IsDataExistsAsync("barcode", BarcodeTxt.Text))
            {
                MessageBox.Show("Barcode already exist.");
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
                MessageBox.Show("Item added succesfully");
                canClose = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Failed to save item, please try again");
            }

            AcceptBtn.Text = "Save";
        }

        private async void PopulateSupplier()
        {
            SuppliersRepository repository = new SuppliersRepository();
            List<Supplier> suppliers = await repository.GetSuppliersAsync();

            SupplierCb.Items.Clear();
            SupplierCb.Items.Add("Select a supplier (optional)");
            SupplierCb.Items.AddRange(suppliers.ToArray());

            SupplierCb.SelectedItem = "Select a supplier (optional)";
        }

        private async void CategoryCb_Enter(object sender, EventArgs e)
        {
            if (CategoryCb.Items.Count == 0)
            {
                InventoryRepository repository = new InventoryRepository();
                List<string> categories = await repository.GetDistinctValuesAsync("category", "N/A");
                CategoryCb.Items.AddRange(categories.ToArray());
            }
        }

        private async void BrandCb_Enter(object sender, EventArgs e)
        {
            if (BrandCb.Items.Count == 0)
            {
                InventoryRepository repository = new InventoryRepository();
                List<string> brands = await repository.GetDistinctValuesAsync("brand", "N/A");
                BrandCb.Items.AddRange(brands.ToArray());
            }
        }

        private async void MotorBrandCb_Enter(object sender, EventArgs e)
        {
            if (MotorBrandCb.Items.Count == 0)
            {
                InventoryRepository repository = new InventoryRepository();
                List<string> motorBrands = await repository.GetDistinctValuesAsync("motor_brand", "N/A");
                MotorBrandCb.Items.AddRange(motorBrands.ToArray());
            }
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
            if (!isSupplierItem)
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
            DialogResult result = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                canClose = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void BarcodeBtn_Click(object sender, EventArgs e)
        {
            GenerateBarcode generateBarcode = new GenerateBarcode();
            generateBarcode.StartPosition = FormStartPosition.Manual;
            generateBarcode.Location = new Point(this.Location.X + this.Width + 10, this.Location.Y);
            if (generateBarcode.ShowDialog() == DialogResult.OK)
            {
                BarcodeTxt.Text = Clipboard.GetText();
            }
        }

        private void BarcodeTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
            {
            }
        }
    }
}