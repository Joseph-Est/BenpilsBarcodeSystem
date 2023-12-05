using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace BenpilsBarcodeSystem
{
    public partial class POS : Form
    {
        private User user;
        private SqlConnection connection;
        private ArrayList cart = new ArrayList();
        private string transactionNumber;
        private decimal total;
        private DataGridView dataGridView1;
        private decimal payment;

        public POS(User user)
        {
            InitializeComponent();
            GenerateTransactionNumber();
            UpdateUI();
            InitializeDataGridView();
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

                    cart.Add(new CartItem
                    {
                        ItemName = item.ItemName,
                        MotorBrand = item.MotorBrand,
                        Brand = item.Brand,
                        Subtotal = item.UnitPrice * quantity
                    });

                    GenerateTransactionNumber();
                    UpdateUI();
                }
                else
                {
                    MessageBox.Show("Insufficient stock for the selected quantity.");
                }
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
                Changelbl.Text = change.ToString("C");
            }
        }
        private void InitializeDataGridView()
        {
            dataGridView1 = new DataGridView();
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Dock = DockStyle.Fill;
            Controls.Add(dataGridView1);
            dataGridView1.DataSource = cart;
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
            TotalLbl.Text = total.ToString("C");
            TransactionNumberlbl.Text = transactionNumber;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = cart;
            PaymentrichTxt.Text = "";
            Changelbl.Text = "";
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

            foreach (CartItem item in cart)
            {
                total += item.Subtotal;
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

        private class CartItem
        {
            public string ItemName { get; set; }
            public string MotorBrand { get; set; }
            public string Brand { get; set; }
            public decimal Subtotal { get; set; }
        }

        private void PaymentrichTxt_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(PaymentrichTxt.Text, out payment))
            {
                UpdateChangeLabel();
            }
            else
            {
                Changelbl.Text = "Invalid payment amount";
            }
        }
    }
}
