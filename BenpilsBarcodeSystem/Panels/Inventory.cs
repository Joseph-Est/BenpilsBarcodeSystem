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
        private bool isAdding = false;
        private bool isUpdating = false;
        private int selectedID;
        private string prevItemName, prevBarcode, prevSize;

        public Inventory()
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(QuantityTxt);
            InputValidator.AllowOnlyDigitsAndDecimal(UnitPriceTxt);
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
            if (!isUpdating && !isAdding)
            {
                isAdding = true;
                ClearFields();
                SetFieldsReadOnly(false);

                AddBtn.Text = " Save Item";
                UpdateBtn.Text = " Cancel";

                GenerateBtn.Enabled = true;
                UpdateBtn.Enabled = true;
                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;

                BarcodeTxt.Focus();
                this.CancelButton = UpdateBtn;
            }
            else
            {
                if (Util.AreTextBoxesNullOrEmpty(BarcodeTxt, ItemNameTxt, ProductIDTxt, UnitPriceTxt, QuantityTxt, CategoryTxt))
                {
                    MessageBox.Show("Please fill in all the required fields.");
                    return;
                }

                if (!InputValidator.IsValidPrice(UnitPriceTxt.Text))
                {
                    MessageBox.Show("Invalid price");
                    return;
                }

                InventoryRepository repository = new InventoryRepository();

                if(isAdding || (isUpdating && (prevSize != SizeTxt.Text || prevItemName != ItemNameTxt.Text)))
                {
                    if (await repository.AreDataExistsAsync("size", SizeTxt.Text, "item_name", ItemNameTxt.Text))
                    {
                        MessageBox.Show("Item already exists. Please choose diffrent item name or different size");
                        return;
                    }
                }

                if (isAdding)
                {
                    await repository.AddProductAsync(
                        BarcodeTxt.Text,
                        InputValidator.ParseToInt(ProductIDTxt.Text),
                        Util.CapitalizeFirstLetter(ItemNameTxt.Text),
                        Util.CapitalizeFirstLetter(MotorBrandTxt.Text),
                        Util.CapitalizeFirstLetter(BrandTxt.Text),
                        InputValidator.ParseToDecimal(UnitPriceTxt.Text),
                        InputValidator.ParseToInt(QuantityTxt.Text),
                        Util.CapitalizeFirstLetter(CategoryTxt.Text),
                        Util.CapitalizeFirstLetter(SizeTxt.Text)
                    );

                    isAdding = false;
                    GenerateBtn.Enabled = false;

                    MessageBox.Show("New item added succesfully!");
                }
                else
                {
                    if(prevBarcode != BarcodeTxt.Text)
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
                        Util.CapitalizeFirstLetter(ItemNameTxt.Text),
                        Util.CapitalizeFirstLetter(MotorBrandTxt.Text),
                        Util.CapitalizeFirstLetter(BrandTxt.Text),
                        InputValidator.ParseToDecimal(UnitPriceTxt.Text),
                        InputValidator.ParseToInt(QuantityTxt.Text),
                        Util.CapitalizeFirstLetter(CategoryTxt.Text),
                        Util.CapitalizeFirstLetter(SizeTxt.Text)
                    );
                    isUpdating = false;

                    MessageBox.Show($"{Util.CapitalizeFirstLetter(ItemNameTxt.Text)} updated succesfully!");
                }
                
                UpdateDataGridView();
                ClearFields();
                SetFieldsReadOnly(true);

                AddBtn.Text = " Add";
                UpdateBtn.Text = " Update";

                AddBtn.ForeColor = Color.White;
                UpdateBtn.ForeColor = Color.White;

                this.CancelButton = null;
            }
            
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if(!isUpdating && !isAdding)
            {
                isUpdating = true;
                SetFieldsReadOnly(false);

                AddBtn.Text = " Save Update";
                UpdateBtn.Text = " Cancel";

                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;

                prevItemName = ItemNameTxt.Text;
                prevBarcode = BarcodeTxt.Text;
                prevSize = SizeTxt.Text;

                this.CancelButton = UpdateBtn;
            }
            else
            {
                isAdding = false;
                isUpdating = false;

                ClearFields();
                SetFieldsReadOnly(true);

                GenerateBtn.Enabled = false;

                AddBtn.Text = " Add";
                UpdateBtn.Text = " Update";

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
            if(!isAdding && !isUpdating)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridItemMasterdata.Rows[e.RowIndex];
                    selectedID =  InputValidator.ParseToInt(row.Cells["id"].Value.ToString());
                    BarcodeTxt.Text = row.Cells["barcode"].Value.ToString();
                    ProductIDTxt.Text = row.Cells["product_id"].Value.ToString();
                    ItemNameTxt.Text = row.Cells["item_name"].Value.ToString();
                    MotorBrandTxt.Text = row.Cells["motor_brand"].Value.ToString();
                    BrandTxt.Text = row.Cells["brand"].Value.ToString();
                    UnitPriceTxt.Text = row.Cells["unit_price"].Value.ToString();
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

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(10000000, 99999999);
            ProductIDTxt.Text = randomNumber.ToString();
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
            Util.ClearTextBoxes(BarcodeTxt, ProductIDTxt, ItemNameTxt, MotorBrandTxt, BrandTxt, UnitPriceTxt, QuantityTxt, CategoryTxt, SizeTxt);

            if(!isAdding && !isUpdating)
            {
                UpdateBtn.Enabled = false;
                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;
            }
        }

        private void SetFieldsReadOnly(bool mode)
        {
            Util.SetTextBoxesReadOnly(mode, BarcodeTxt, ItemNameTxt, MotorBrandTxt, BrandTxt, UnitPriceTxt, QuantityTxt, CategoryTxt, SizeTxt);
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
                AddBtn.ForeColor = Color.Empty;
                UpdateBtn.ForeColor = Color.Empty;
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
