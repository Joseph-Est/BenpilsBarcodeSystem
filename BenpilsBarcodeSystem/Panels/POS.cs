using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class POS : Form
    {
        private decimal total;
        private DataGridView dataGridView1;
        private decimal payment;
        private string transactionNumber;

        public POS()
        {
            InitializeComponent();
            GenerateTransactionNumber();
            UpdateUI();
        }

        private void BarcoderichTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateDateLabel();
        }

        private void UpdateDateLabel()
        {
            lbldate.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void Addttocartbtn_Click(object sender, EventArgs e)
        {
            string barcode = BarcoderichTxt.Text.Trim();
            int quantity = GetQuantityFromUser();

            if (quantity > 0)
            {
                Item item = GetItemDetails(barcode);

                if (item != null && item.Quantity >= quantity)
                {
                    UpdateItemQuantity(barcode, item.Quantity - quantity);

                    // Insert the cart item into the tbl_cart table
                    InsertCartItem(item.ItemName, item.MotorBrand, item.Brand, item.UnitPrice * quantity, quantity);

                    total = CalculateTotal(); // Recalculate total
                    GenerateTransactionNumber();
                    UpdateUI();
                }
                else
                {
                    MessageBox.Show("Insufficient stock for the selected quantity.");
                }
            }
        }
        private void InsertCartItem( string itemName, string motorBrand, string brand, decimal subtotal,int  quantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "INSERT INTO tbl_cart (ItemName, MotorBrand, Brand, Subtotal, Quantity) VALUES (@ItemName, @MotorBrand, @Brand, @Subtotal, @Quantity)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemName", itemName);
                        command.Parameters.AddWithValue("@MotorBrand", motorBrand);
                        command.Parameters.AddWithValue("@Brand", brand);
                        command.Parameters.AddWithValue("@Subtotal", subtotal);
                        command.Parameters.AddWithValue("@Quantity", quantity);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inserting cart item: {ex.Message}");
            }
        }

        private void UpdateChangeLabel()
        {
            decimal change = payment - total;

            if (change < 0)
            {
                Changelbl.Text = "Insufficient balance";
            }
            else
            {
                Changelbl.Text = change.ToString();
            }
        }

        private void UpdateItemQuantity(string barcode, int newQuantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "UPDATE tbl_itemmasterdata SET Quantity = @NewQuantity WHERE Barcode = @Barcode";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewQuantity", newQuantity);
                        command.Parameters.AddWithValue("@Barcode", barcode);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating item quantity: {ex.Message}");
            }
        }

        private Item GetItemDetails(string barcode)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();

                    string query = "SELECT ItemName, MotorBrand, Brand, UnitPrice, Quantity FROM tbl_itemmasterdata WHERE Barcode = @Barcode";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Barcode", barcode);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Item
                                {
                                    ItemName = reader["ItemName"].ToString(),
                                    MotorBrand = reader["MotorBrand"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching item details: {ex.Message}");
            }

            return null;
        }

        private void UpdateUI()
        {
            TotalLbl.Text = total.ToString();
            TransactionNumberlbl.Text = transactionNumber;

            // Retrieve cart items from the tbl_cart table
            dataGridView1.DataSource = GetCartItems();

            UpdateChangeLabel();
        }

        private DataTable GetCartItems()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "SELECT ItemName, MotorBrand, Brand, Subtotal FROM tbl_cart WHERE TransactionNumber = @TransactionNumber";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TransactionNumber", transactionNumber);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving cart items: {ex.Message}");
            }

            return dt;
        }

        private void GenerateTransactionNumber()
        {
            Random random = new Random();
            int randomDigits = random.Next(100000000, 999999999);
            transactionNumber = "BEN - " + randomDigits.ToString();
        }

        private int GetQuantityFromUser()
        {
            using (var quantityForm = new QuantityForm())
            {
                if (quantityForm.ShowDialog() == DialogResult.OK)
                {
                    return quantityForm.Quantity;
                }
            }
            return 0;
        }

        private decimal CalculateTotal()
        {
            decimal total = 0;

            // Retrieve cart items from the tbl_cart table
            DataTable cartItems = GetCartItems();

            foreach (DataRow row in cartItems.Rows)
            {
                total += Convert.ToDecimal(row["Subtotal"]);
            }

            return total;
        }

        private class Item
        {
            public string ItemName { get; set; }
            public string MotorBrand { get; set; }
            public string Brand { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
        }
    }
}