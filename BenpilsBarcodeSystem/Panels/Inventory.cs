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
        private GenerateBarcode GB;
        private bool isAdding = false;
        private bool isUpdating = false;
        private int selectedID;

        public Inventory()
        {
            InitializeComponent();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private async void UpdateDataGridView()
        {
            try
            {
                InventoryRepository inventoryRepository = new InventoryRepository();
                DataTable userCredentials = await inventoryRepository.GetProductsAsync();

                dataGridItemMasterdata.DataSource = userCredentials;
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
                AddBtn.Text = "Save";
                GenerateBtn.Enabled = true;
                UpdateBtn.Enabled = true;
                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;
                UpdateBtn.Text = "Cancel";
            }
            else
            {
                if (isAdding)
                {
                    if (Util.AreTextBoxesNullOrEmpty(BarcodeTxt, ItemNameTxt, ProductIDTxt, UnitPriceTxt, QuantityTxt, CategoryTxt))
                    {
                        MessageBox.Show("Please fill in all the required fields.");
                        return;
                    }

                    InventoryRepository repository = new InventoryRepository();

                    if (!InputValidator.IsValidPrice(UnitPriceTxt.Text))
                    {
                        MessageBox.Show("Invalid price");
                        return;
                    }

                    if (!InputValidator.IsValidInt(ProductIDTxt.Text))
                    {
                        MessageBox.Show("Product ID must be valid number");
                        return;
                    }

                    if (await repository.AreDataExistsAsync("Size", SizeTxt.Text, "ItemName", ItemNameTxt.Text))
                    {
                        MessageBox.Show("Size already exists in the database. Please choose a different Size.");
                        return;
                    }

                    await repository.AddProductAsync(
                        BarcodeTxt.Text,
                        InputValidator.ParseToInt(ProductIDTxt.Text),
                        ItemNameTxt.Text,
                        MotorBrandTxt.Text,
                        BrandTxt.Text,
                        InputValidator.ParseToDecimal(UnitPriceTxt.Text),
                        InputValidator.ParseToInt(QuantityTxt.Text),
                        CategoryTxt.Text,
                        SizeTxt.Text
                    );

                    isAdding = false;
                    GenerateBtn.Enabled = false;
                }
                else
                {
                    isUpdating = false;
                }
                
                UpdateDataGridView();
                ClearFields();
                SetFieldsReadOnly(true);
                AddBtn.Text = "Add";
                UpdateBtn.Text = "Update";
            }
            
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if(!isUpdating && !isAdding)
            {
                isUpdating = true;
                SetFieldsReadOnly(false);
                AddBtn.Text = "Save";
                UpdateBtn.Text = "Cancel";
                ArchiveBtn.Enabled = false;
                ReduceStockBtn.Enabled = false;
            }
            else
            {
                isAdding = false;
                isUpdating = false;
                ClearFields();
                SetFieldsReadOnly(true);
                AddBtn.Text = "Add";
                GenerateBtn.Enabled = false;
                UpdateBtn.Text = "Update";
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

        private void dataGridItemMasterdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!isAdding && !isUpdating)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridItemMasterdata.Rows[e.RowIndex];
                    selectedID =  InputValidator.ParseToInt(row.Cells["ID"].Value.ToString());
                    BarcodeTxt.Text = row.Cells["Barcode"].Value.ToString();
                    ProductIDTxt.Text = row.Cells["ProductID"].Value.ToString();
                    ItemNameTxt.Text = row.Cells["ItemName"].Value.ToString();
                    MotorBrandTxt.Text = row.Cells["MotorBrand"].Value.ToString();
                    BrandTxt.Text = row.Cells["Brand"].Value.ToString();
                    UnitPriceTxt.Text = row.Cells["UnitPrice"].Value.ToString();
                    QuantityTxt.Text = row.Cells["Quantity"].Value.ToString();
                    CategoryTxt.Text = row.Cells["Category"].Value.ToString();
                    SizeTxt.Text = row.Cells["Size"].Value.ToString();
                    UpdateBtn.Enabled = true;
                    ArchiveBtn.Enabled = true;
                    ReduceStockBtn.Enabled = true;
                }
            }
        }

        private void BarcodeGeneratorBtn_Click(object sender, EventArgs e)
        {
            if (GB == null || GB.IsDisposed)
            {     
                GB = new GenerateBarcode();
            }
            GB.Show();
            GB.StartPosition = FormStartPosition.CenterScreen;
            GB.BringToFront();
        }

        private void RefreshPb_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(10000000, 99999999);
            ProductIDTxt.Text = randomNumber.ToString();
        }

        private void QuantityTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ProductIDTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UnitPriceTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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
    }
}
