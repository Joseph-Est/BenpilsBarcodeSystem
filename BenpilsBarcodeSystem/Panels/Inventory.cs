using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using System.Windows.Forms.VisualStyles;
using BenpilsBarcodeSystem.Utils;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Helpers;

namespace BenpilsBarcodeSystem
{
    public partial class Inventory : Form
    {
        private bool isUpdating = false;
        private int selectedID;
        private string prevItemName, prevBarcode, prevSize, prevBrand, prevMotorBrand;

        public Inventory()
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(QuantityTxt);
            InputValidator.AllowOnlyDigitsAndDecimal(PurchasePriceTxt);
            InputValidator.AllowOnlyDigitsAndDecimal(SellingPriceTxt);
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            PopulateComboBoxes();
            UpdateDataGridView();
        }

        private async void UpdateDataGridView(string searchText = null, string category = "All", string brand = "All")
        {
            if (string.IsNullOrEmpty(searchText))
            {
                SearchTxt.Text = "";
            }

            try
            {
                InventoryRepository inventoryRepository = new InventoryRepository();
                DataTable inventoryDT = await inventoryRepository.GetProductsAsync(searchText, category, brand);

                dataGridItemMasterdata.DataSource = inventoryDT;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private async void AddBtn_Click(object sender, EventArgs e)
        {
            if (isUpdating)
            {
                string itemName = Util.Capitalize(ItemNameTxt.Text);
                string category = CategoryCb.Text.Trim().ToLower() == "none" ? "N/A" : Util.CapitalizeOrNA(CategoryCb.Text);
                string brand = BrandCb.Text.Trim().ToLower() == "none" ? "N/A" : Util.CapitalizeOrNA(BrandCb.Text);
                string motorBrand = MotorBrandTxt.Text.Trim().ToLower() == "none" ? "N/A" : Util.CapitalizeOrNA(MotorBrandTxt.Text);
                string size = Util.CapitalizeOrNA(SizeTxt.Text);

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

                if (!string.IsNullOrEmpty(SellingPriceTxt.Text.Trim()) && !InputValidator.IsValidPrice(SellingPriceTxt.Text))
                {
                    MessageBox.Show("Invalid selling price");
                    return;
                }

                InventoryRepository repository = new InventoryRepository();

                if (isUpdating && (prevSize != SizeTxt.Text || prevItemName != ItemNameTxt.Text) || prevBrand != BrandTxt.Text || prevMotorBrand != MotorBrandTxt.Text)
                {
                    if (await repository.isItemExists(itemName, brand, motorBrand, size))
                    {
                        MessageBox.Show("Item already exists");
                        return;
                    }
                }

                if (prevBarcode != BarcodeTxt.Text)
                {
                    if (await repository.IsDataExistsAsync("barcode", BarcodeTxt.Text))
                    {
                        MessageBox.Show("Barcode already exist.");
                        return;
                    }
                }

                await repository.UpdateProductAsync(
                    selectedID,
                    BarcodeTxt.Text,
                    itemName,
                    category,
                    brand,
                    motorBrand,
                    size,
                    InputValidator.ParseToInt(QuantityTxt.Text),
                    InputValidator.ParseToDecimal(PurchasePriceTxt.Text),
                    InputValidator.ParseToDecimal(SellingPriceTxt.Text)
                );

                isUpdating = false;

                MessageBox.Show($"{Util.Capitalize(ItemNameTxt.Text)} updated succesfully!");

                UpdateDataGridView();
                ClearFields();
                SetFieldsReadOnly(true);

                AddBtn.Text = " Add";
                UpdateBtn.Text = " Update";

                this.CancelButton = null;
            }
            else
            {
                AddItem addItem = new AddItem();
                if (addItem.ShowDialog() == DialogResult.OK)
                {
                    UpdateDataGridView();
                }
            }
            
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if(!isUpdating)
            {
                isUpdating = true;
                SetFieldsReadOnly(false);

                AddBtn.Text = " Save Update";
                UpdateBtn.Text = " Cancel";

                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;

                prevItemName = ItemNameTxt.Text;
                prevBarcode = BarcodeTxt.Text;
                prevBrand = BrandTxt.Text;
                prevMotorBrand = MotorBrandTxt.Text;
                prevSize = SellingPriceTxt.Text;

                this.CancelButton = UpdateBtn;
            }
            else
            {
                isUpdating = false;

                ClearFields();
                SetFieldsReadOnly(true);

                AddBtn.Text = " Add";
                UpdateBtn.Text = " Update";

                BarcodeTxt.Focus();
                this.CancelButton = null;
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private async void ArchiveBtn_Click(object sender, EventArgs e)
        {
            Confirmation confirmation = new Confirmation("Are you sure you want to archive", ItemNameTxt.Text + "?", "Yes", "Cancel");
            DialogResult result = confirmation.ShowDialog();

            if (result == DialogResult.Yes)
            {
                InventoryRepository inventoryRepository = new InventoryRepository();

                if (selectedID > 0)
                {
                    await inventoryRepository.ArchiveProductAsync(selectedID);
                    UpdateDataGridView();
                    ClearFields();
                }
            }
        }

        private async void ReduceStockBtn_Click(object sender, EventArgs e)
        {
            ReduceStockForm reduceStockForm = new ReduceStockForm(selectedID, ItemNameTxt.Text, SizeTxt.Text, InputValidator.ParseToInt(QuantityTxt.Text));
            if (reduceStockForm.ShowDialog() == DialogResult.OK)
            {
                int id = reduceStockForm.SelectedId;
                int amountToDeduct = reduceStockForm.AmountToDeduct;
                string reason = reduceStockForm.Reason;
                string itemName = reduceStockForm.itemName;

                InventoryRepository inventoryRepository = new InventoryRepository();
                if (await inventoryRepository.DeductStockAsync(id, amountToDeduct))
                {
                    MessageBox.Show($"{itemName} quantity reduced succesfully");
                    UpdateDataGridView();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show($"Failed to reduce item quantity");
                }

            }
        }

        private void dataGridItemMasterdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!isUpdating)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridItemMasterdata.Rows[e.RowIndex];
                    selectedID =  InputValidator.ParseToInt(row.Cells["id"].Value.ToString());
                    BarcodeTxt.Text = row.Cells["barcode"].Value.ToString();
                    ItemNameTxt.Text = row.Cells["item_name"].Value.ToString();
                    MotorBrandTxt.Text = row.Cells["motor_brand"].Value.ToString();
                    BrandTxt.Text = row.Cells["brand"].Value.ToString();
                    PurchasePriceTxt.Text = row.Cells["purchase_price"].Value.ToString();
                    SellingPriceTxt.Text = row.Cells["selling_price"].Value.ToString();
                    QuantityTxt.Text = row.Cells["quantity"].Value.ToString();
                    CategoryTxt.Text = row.Cells["category"].Value.ToString();
                    SizeTxt.Text = row.Cells["size"].Value.ToString();
                    UpdateBtn.Enabled = true;

                    if(InputValidator.ParseToInt(row.Cells["quantity"].Value.ToString()) <= 0)
                    {
                        ArchiveBtn.Enabled = true;
                    }
                    
                    ReduceStockBtn.Enabled = true;
                }
            }
        }

        private void BarcodeGeneratorBtn_Click(object sender, EventArgs e)
        {
            GenerateBarcode generateBarcode = new GenerateBarcode();
            if (generateBarcode.ShowDialog() == DialogResult.OK)
            {
                if(BarcodeTxt.ReadOnly == false)
                {
                    BarcodeTxt.Text = Clipboard.GetText();
                }
            }
        }

        private void RefreshPb_Click(object sender, EventArgs e)
        {
            PopulateComboBoxes();
            UpdateDataGridView();
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView(SearchTxt.Text, CategoryCb.SelectedItem?.ToString(), BrandCb.SelectedItem?.ToString());
        }

        private void CategoryCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGridView(SearchTxt.Text, CategoryCb.SelectedItem?.ToString(), BrandCb.SelectedItem?.ToString());
        }

        private void BrandCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataGridView(SearchTxt.Text, CategoryCb.SelectedItem?.ToString(), BrandCb.SelectedItem?.ToString());
        }

        private void ClearFields()
        {
            Util.ClearTextBoxes(BarcodeTxt, ItemNameTxt, CategoryTxt, BrandTxt, QuantityTxt, PurchasePriceTxt, MotorBrandTxt, SizeTxt, SellingPriceTxt);

            if(!isUpdating)
            {
                UpdateBtn.Enabled = false;
                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;
            }
        }

        private void SetFieldsReadOnly(bool mode)
        {
            Util.SetTextBoxesReadOnly(mode, BarcodeTxt, ItemNameTxt, CategoryTxt, BrandTxt, QuantityTxt, MotorBrandTxt, SizeTxt, SellingPriceTxt);
        }

        private void InputFormPanel_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = AddBtn;
        }

        private void InputFormPanel_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }

        private void AddBtn_TextChanged(object sender, EventArgs e)
        {
            if (AddBtn.Text.Contains("Save")){
                AddBtn.ForeColor = Color.FromArgb(80, 180, 80);
                AddBtn.Image = Properties.Resources.icons8_downloading_updates_15;
                UpdateBtn.ForeColor = Color.FromArgb(220, 80, 80);
                UpdateBtn.Image = Properties.Resources.icons8_multiply_15;
            }
            else
            {
                AddBtn.ForeColor = Color.Black;
                UpdateBtn.ForeColor = Color.Black;
                UpdateBtn.Image = Properties.Resources.icons8_update_15;
                AddBtn.Image = Properties.Resources.icons8_add_15;
            }
        }

        private async void PopulateComboBoxes()
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            (List<string> valuesColumn1, List<string> valuesColumn2) = await inventoryRepository.GetCategoryBrandValuesAsync();

            CategoryCb.Items.Clear();
            CategoryCb.Items.AddRange(valuesColumn1.ToArray());

            BrandCb.Items.Clear();
            BrandCb.Items.AddRange(valuesColumn2.ToArray());

            BrandCb.SelectedIndexChanged -= BrandCb_SelectedIndexChanged;
            CategoryCb.SelectedIndexChanged -= CategoryCb_SelectedIndexChanged;

            CategoryCb.SelectedIndex = 0;
            BrandCb.SelectedIndex = 0;

            BrandCb.SelectedIndexChanged += BrandCb_SelectedIndexChanged;
            CategoryCb.SelectedIndexChanged += CategoryCb_SelectedIndexChanged;
        }
    }
}
