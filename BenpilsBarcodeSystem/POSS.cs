﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class POS : Form
    {
        private User user;
        private SqlConnection connection;

     
        private decimal total;
        private DataGridView dataGridView1;
        private decimal payment;

        public POS(User user)
        {
            InitializeComponent();
            GenerateTransactionNumber();
            UpdateUI();

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();
            this.user = user;
            label1.Text = "Username: " + user.Username;
            label2.Text = "Designation: " + user.Designation;
            connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True");
            connection.Open();
        }

        private void POSS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleTableCartDatabase.tbl_cart' table. You can move, or remove it, as needed.
            this.tbl_cartTableAdapter2.Fill(this.benpillMotorcycleTableCartDatabase.tbl_cart);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.StartPosition = FormStartPosition.CenterScreen;
            CE.ShowDialog();
        }

        private void MinimizedBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void DashboardBtn_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

        private void PointOfSalesBtn_Click(object sender, EventArgs e)
        {

        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory(user);
            inv.Show();
            inv.StartPosition = FormStartPosition.Manual;
            inv.Location = this.Location;
            this.Hide();
        }

        private void PurchasingBTn_Click(object sender, EventArgs e)
        {
            Purchaserr purchasing = new Purchaserr(user);
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }

        private void ServicesBtn_Click(object sender, EventArgs e)
        {
            Services serv = new Services(user);
            serv.Show();
            serv.StartPosition = FormStartPosition.Manual;
            serv.Location = this.Location;
            this.Hide();
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports(user);
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }

        private void StatisticsBtn_Click(object sender, EventArgs e)
        {
            StatisticReport statisticReport = new StatisticReport(user);
            statisticReport.Show();
            statisticReport.StartPosition = FormStartPosition.Manual;
            statisticReport.Location = this.Location;
            this.Hide();
        }

        private void UsercredentialsBtn_Click(object sender, EventArgs e)
        {
            Ser credentials = new Ser(user);
            credentials.Show();
            credentials.StartPosition = FormStartPosition.Manual;
            credentials.Location = this.Location;
            this.Hide();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(user);
            settings.Show();
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = this.Location;
            this.Hide();
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfirmationLogout cl = new ConfirmationLogout();
            cl.StartPosition = FormStartPosition.CenterScreen;
            if (cl.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
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
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
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
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
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
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
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
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
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
            using (var quantityForm = new Quantityform())
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