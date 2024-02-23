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

namespace BenpilsBarcodeSystem
{
    public partial class Inventory : Form
    {
        private GenerateBarcode GB;
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

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BarcodeTxt.Text) ||
                string.IsNullOrEmpty(ItemNameTxt.Text) ||
                string.IsNullOrEmpty(ProductIDTxt.Text) ||
                string.IsNullOrEmpty(UnitPriceTxt.Text) ||
                string.IsNullOrEmpty(QuantityTxt.Text) ||
                string.IsNullOrEmpty(CategoryTxt.Text))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }


            if (!int.TryParse(ProductIDTxt.Text, out int productId))
            {
                MessageBox.Show("Product ID must be a valid integer.");
                return;
            }

            string size = SizeTxt.Text;

            if (IsSizeAlreadyExists(size))
            {
                MessageBox.Show("Size already exists in the database. Please choose a different Size.");
                return;
            }

            string insertQuery = "INSERT INTO tbl_itemmasterdata (Barcode, ProductID, ItemName, MotorBrand, Brand, UnitPrice, Quantity, Category, Size) " +
                                "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category, @Size)";

            using (SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpilsMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ItemName", ItemNameTxt.Text);
                    cmd.Parameters.AddWithValue("@MotorBrand", MotorBrandTxt.Text);
                    cmd.Parameters.AddWithValue("@Brand", BrandTxt.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", decimal.Parse(UnitPriceTxt.Text));
                    cmd.Parameters.AddWithValue("@Quantity", int.Parse(QuantityTxt.Text));
                    cmd.Parameters.AddWithValue("@Category", CategoryTxt.Text);
                    cmd.Parameters.AddWithValue("@Size", size);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

           
            UpdateDataGridView();
            Util.ClearTextBoxes(BarcodeTxt, ProductIDTxt, ItemNameTxt, MotorBrandTxt, BrandTxt, UnitPriceTxt, QuantityTxt, CategoryTxt, SizeTxt);
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ProductIDTxt.Text, out int productId))
            {
                string updateQuery = "UPDATE tbl_itemmasterdata SET Barcode = @Barcode, ItemName = @ItemName, MotorBrand = @MotorBrand, Brand = @Brand, UnitPrice = @UnitPrice, Quantity = @Quantity, Category = @Category, Size = @Size, ProductID = @ProductID WHERE ProductID = @ProductID";
                using (SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpilsMotorcycleDatabase;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@ItemName", ItemNameTxt.Text);
                        cmd.Parameters.AddWithValue("@MotorBrand", MotorBrandTxt.Text);
                        cmd.Parameters.AddWithValue("@Brand", BrandTxt.Text);

                        if (decimal.TryParse(UnitPriceTxt.Text, out decimal unitPrice))
                        {
                            cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        }
                        else
                        {

                        }

                        if (int.TryParse(QuantityTxt.Text, out int quantity))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                        }
                        else
                        {

                        }

                        cmd.Parameters.AddWithValue("@Category", CategoryTxt.Text);
                        cmd.Parameters.AddWithValue("@Size", SizeTxt.Text);
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("Error: " + ex.Message);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            else
            {

                MessageBox.Show("Please enter a valid Product ID.");
            }
            UpdateDataGridView();
            Util.ClearTextBoxes(BarcodeTxt, ProductIDTxt, ItemNameTxt, MotorBrandTxt, BrandTxt, UnitPriceTxt, QuantityTxt, CategoryTxt, SizeTxt);
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Util.ClearTextBoxes(BarcodeTxt, ProductIDTxt, ItemNameTxt, MotorBrandTxt, BrandTxt, UnitPriceTxt, QuantityTxt, CategoryTxt, SizeTxt);
        }

        private void ArchiveBtn_Click(object sender, EventArgs e)
        {

        }

        private bool IsSizeAlreadyExists(string size)
        {
            using (SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpilsMotorcycleDatabase;Integrated Security=True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_itemmasterdata WHERE Size = @Size", con))
                {
                    cmd.Parameters.AddWithValue("@Size", size);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private void dataGridItemMasterdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridItemMasterdata.Rows[e.RowIndex];
                BarcodeTxt.Text = row.Cells[1].Value.ToString();
                ProductIDTxt.Text = row.Cells[2].Value.ToString();
                ItemNameTxt.Text = row.Cells[3].Value.ToString();
                MotorBrandTxt.Text = row.Cells[4].Value.ToString();
                BrandTxt.Text = row.Cells[5].Value.ToString();
                UnitPriceTxt.Text = row.Cells[6].Value.ToString();
                QuantityTxt.Text = row.Cells[7].Value.ToString();
                CategoryTxt.Text = row.Cells[8].Value.ToString();
                SizeTxt.Text = row.Cells[9].Value.ToString();
                AddBtn.Enabled = false;
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

        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {

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

        private void GenerateProductID()
        {
            using (SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpilsMotorcycleDatabase;Integrated Security=True"))
                con.Open();
            {

                Random rand = new Random();
                int randomProductID = rand.Next(1, 600);

                if (randomProductID > 599)
                {
                    MessageBox.Show("ProductID limit exceeded.");
                    return;
                }

                string productID = randomProductID.ToString("D4");
                ProductIDTxt.Text = productID;
            }
        }

        private DataRow GetProductDetails(int productId)
        {
            DataTable dt = (DataTable)dataGridItemMasterdata.DataSource;
            DataRow[] rows = dt.Select($"ProductID = {productId}");
            return rows.Length > 0 ? rows[0] : null;
        }
    }
}
