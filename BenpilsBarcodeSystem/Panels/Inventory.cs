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
        MainForm mainForm;

        public Inventory()
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(QuantityTxt);
            InputValidator.AllowOnlyDigitsAndDecimal(PurchasePriceTxt);
            InputValidator.AllowOnlyDigitsAndDecimal(SellingPriceTxt);
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            mainForm = (MainForm)this.ParentForm;
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
                DataTable inventoryDT = await inventoryRepository.GetProductsAsync(true, searchText, category, brand);

                InventoryTbl.AutoGenerateColumns = false;
                InventoryTbl.DataSource = inventoryDT;

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
                string category = CategoryInputCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(CategoryInputCb.Text);
                string brand = BrandInputCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(BrandInputCb.Text);
                string motorBrand = MotorBrandInputCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(MotorBrandInputCb.Text);
                string size = SizeCb.Text.Trim().ToLower() == "n/a" ? "N/A" : Util.CapitalizeOrNA(SizeCb.Text);

                if (Util.AreTextBoxesNullOrEmpty(BarcodeTxt, ItemNameTxt))
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

                if (isUpdating && (prevSize != SizeCb.Text || prevItemName != ItemNameTxt.Text) || prevBrand != BrandInputCb.Text || prevMotorBrand != MotorBrandInputCb.Text)
                {
                    if (await repository.IsItemExists(itemName, brand, motorBrand, size))
                    {
                        MessageBox.Show("The item you're trying to update already exists. Please check the item details.", "Item Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (prevBarcode != BarcodeTxt.Text)
                {
                    if (await repository.IsDataExistsAsync("barcode", BarcodeTxt.Text))
                    {
                        MessageBox.Show("The barcode you've entered is already in use. Please enter a unique barcode.", "Duplicate Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (purchasePrice > sellingPrice)
                {
                    MessageBox.Show("Invalid input: The purchase price must not be higher than the selling price.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (await repository.UpdateProductAsync(
                    selectedID,
                    BarcodeTxt.Text,
                    itemName,
                    category,
                    brand,
                    motorBrand,
                    size,
                    purchasePrice,
                    sellingPrice
                ))
                {
                    isUpdating = false;

                    MessageBox.Show($"The item '{Util.Capitalize(ItemNameTxt.Text)}' has been successfully updated!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    UpdateDataGridView();
                    ClearFields();
                    SetFieldsReadOnly(true);

                    AddBtn.Text = " Add";
                    UpdateBtn.Text = " Update";

                    this.CancelButton = null;
                }
                else
                {
                    MessageBox.Show($"Unfortunately, the update operation for '{prevItemName}' failed. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                prevBrand = BrandInputCb.Text;
                prevMotorBrand = MotorBrandInputCb.Text;
                prevSize = SizeCb.Text;

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
            if (InputValidator.ParseToInt(QuantityTxt.Text) > 0)
            {
                MessageBox.Show("The item cannot be archived because it is still in stock. Please ensure the item is out of stock before attempting to archive it.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Confirmation confirmation = new Confirmation("Are you sure you want to archive", ItemNameTxt.Text + "?", "Yes", "Cancel");
            DialogResult result = confirmation.ShowDialog();

            if (result == DialogResult.Yes)
            {
                InventoryRepository inventoryRepository = new InventoryRepository();

                if (selectedID > 0)
                {
                    if(await inventoryRepository.HasPendingOrdersAsync(selectedID))
                    {
                        MessageBox.Show("The item cannot be archived because there are pending orders associated with it. Please complete or cancel all pending orders before attempting to archive the item.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if(await inventoryRepository.ArchiveProductAsync(selectedID))
                    {
                        UpdateDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while attempting to archive the item. Please try again.", "Archive Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void ReduceStockBtn_Click(object sender, EventArgs e)
        {
            ReduceStockForm reduceStockForm = new ReduceStockForm(selectedID, ItemNameTxt.Text, SizeCb.Text, InputValidator.ParseToInt(QuantityTxt.Text));
            if (reduceStockForm.ShowDialog() == DialogResult.OK)
            {
                int id = reduceStockForm.SelectedId;
                int amountToDeduct = reduceStockForm.AmountToDeduct;
                string reason = reduceStockForm.Reason;
                string itemName = reduceStockForm.ItemName;

                InventoryRepository inventoryRepository = new InventoryRepository();
                if (await inventoryRepository.DeductStockAsync(id, amountToDeduct, reason))
                {
                    MessageBox.Show($"The Quantity of '{itemName}' has been successfully reduced.", "Quantity Reduced", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateDataGridView();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("An error occurred while attempting to reduce the item Quantity. Please try again.", "Quantity Reduction Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void InventoryTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!isUpdating)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = InventoryTbl.Rows[e.RowIndex];
                    selectedID =  InputValidator.ParseToInt(row.Cells["id"].Value.ToString());
                    BarcodeTxt.Text = row.Cells["barcode"].Value.ToString();
                    ItemNameTxt.Text = row.Cells["item_name"].Value.ToString();
                    MotorBrandInputCb.Text = row.Cells["motor_brand"].Value.ToString();
                    BrandInputCb.Text = row.Cells["brand"].Value.ToString();
                    PurchasePriceTxt.Text = row.Cells["purchase_price"].Value.ToString();
                    SellingPriceTxt.Text = row.Cells["selling_price"].Value.ToString();
                    QuantityTxt.Text = row.Cells["Quantity"].Value.ToString();
                    CategoryInputCb.Text = row.Cells["category"].Value.ToString();
                    SizeCb.Text = row.Cells["size"].Value.ToString();
                    UpdateBtn.Enabled = true;
                    ArchiveBtn.Enabled = InputValidator.ParseToInt(row.Cells["Quantity"].Value.ToString()) <= 0;
                    ReduceStockBtn.Enabled = true;
                }
            }
        }

        private void RefreshPb_Click(object sender, EventArgs e)
        {
            PopulateComboBoxes();
            UpdateDataGridView();
            CancelFilterCb.Visible = false;
        }

        private void UpdateDataGridView_Event(object sender, EventArgs e)
        {
            UpdateDataGridView(SearchTxt.Text, CategoryCb.SelectedItem?.ToString(), BrandCb.SelectedItem?.ToString());
            CancelFilterCb.Visible = (!string.IsNullOrEmpty(SearchTxt.Text) || CategoryCb.SelectedIndex != 0 || BrandCb.SelectedIndex != 0);
        }

        private void ClearFields()
        {
            Util.ClearTextBoxes(BarcodeTxt, ItemNameTxt, QuantityTxt, PurchasePriceTxt, SellingPriceTxt);
            Util.ResetComboBoxes(CategoryInputCb, BrandInputCb, MotorBrandInputCb, SizeCb);

            if(!isUpdating)
            {
                UpdateBtn.Enabled = false;
                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;
            }
        }

        private void SetFieldsReadOnly(bool mode)
        {
            Util.SetTextBoxesReadOnly(mode, BarcodeTxt, ItemNameTxt, PurchasePriceTxt,SellingPriceTxt);
            Util.SetComboBoxesDisabled(mode, CategoryInputCb, BrandInputCb, MotorBrandInputCb, SizeCb);
            mainForm.CanSwitchPanel = mode;
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
                AddBtn.ForeColor = Color.FromArgb(62, 146, 62);
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

            BrandCb.SelectedIndexChanged -= UpdateDataGridView_Event;
            CategoryCb.SelectedIndexChanged -= UpdateDataGridView_Event;

            CategoryCb.SelectedIndex = 0;
            BrandCb.SelectedIndex = 0;

            BrandCb.SelectedIndexChanged += UpdateDataGridView_Event;
            CategoryCb.SelectedIndexChanged += UpdateDataGridView_Event;
        }

        private void CancelFilterCb_Click(object sender, EventArgs e)
        {
            PopulateComboBoxes();
            UpdateDataGridView();
            CancelFilterCb.Visible = false;
        }

        private void SearchTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter;
        }

        private void CB_DropDownClosed(object sender, EventArgs e)
        {
            InventoryTbl.Focus();

        }

        private async void ComboBox_Enter(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox cb && cb.Enabled == true && cb.Items.Count == 0)
            {
                string selectedItem = cb.Text;
                InventoryRepository repository = new InventoryRepository();
                string column = "";
                switch (cb.Name)
                {
                    case "CategoryInputCb":
                        column = "category";
                        break;
                    case "BrandInputCb":
                        column = "brand";
                        break;
                    case "MotorBrandInputCb":
                        column = "motor_brand";
                        break;
                    case "SizeCb":
                        column = "size";
                        break;
                }
                List<string> values = await repository.GetDistinctValuesAsync(column, "N/A");
                cb.Items.AddRange(values.ToArray());
                cb.SelectedItem = selectedItem;
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
            if (sender is TextBox tb && tb.ReadOnly == false && string.IsNullOrEmpty(tb.Text.Trim()))
            {
                tb.Text = "0";
            }
        }

        public void UpdateTable()
        {
            UpdateDataGridView();
        }
    }
}
