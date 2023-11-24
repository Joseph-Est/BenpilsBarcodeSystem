using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class POS : Form
    {
        private User user;
        private SqlConnection connection;

        private string currentDescription; // Added variable to store description
        public POS(User user)
        {
            InitializeComponent();
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
            // TODO: This line of code loads data into the 'benpillMotorcycleCartDatabase.tbl_cart' table. You can move, or remove it, as needed.
            this.tbl_cartTableAdapter1.Fill(this.benpillMotorcycleCartDatabase.tbl_cart);

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

        private void CalculateTotals()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            TotalLbl.Text = total.ToString();

            decimal payment = Convert.ToDecimal(PaymentrichTxt.Text);
            decimal change = payment - total;

            Changelbl.Text = change.ToString();
        }

        private void BarcoderichTxt_TextChanged(object sender, EventArgs e)
        {
            string barcode = BarcoderichTxt.Text;

            // Query to retrieve data from tbl_services
            string queryServices = $"SELECT ServicesName, Price FROM tbl_services WHERE Barcode = '{barcode}'";

            // Query to retrieve data from tbl_itemmasterdata
            string queryItemMasterData = $"SELECT ItemName, UnitPrice, Quantity FROM tbl_itemmasterdata WHERE Barcode = '{barcode}'";

            currentDescription = ""; // Reset description
            decimal price = 0;
            int availableQuantity = 0;

            // Check if the barcode corresponds to a service
            using (SqlCommand command = new SqlCommand(queryServices, connection))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    currentDescription = reader["ServicesName"].ToString();
                    price = Convert.ToDecimal(reader["Price"]);
                }
                reader.Close();
            }

            // If not a service, check if it corresponds to an item
            if (string.IsNullOrEmpty(currentDescription))
            {
                using (SqlCommand command = new SqlCommand(queryItemMasterData, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        currentDescription = reader["ItemName"].ToString();
                        price = Convert.ToDecimal(reader["UnitPrice"]);
                        availableQuantity = Convert.ToInt32(reader["Quantity"]);
                    }
                    reader.Close();
                }
            }

            // Update UI elements based on the retrieved data
            // Show QuantityWindowForm for quantity input
            Quantityform quantityForm = new Quantityform();
            DialogResult result = quantityForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                int quantity = quantityForm.Quantity;

                if (quantity > availableQuantity)
                {
                    MessageBox.Show("Insufficient quantity available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    quantity = availableQuantity;
                }

                decimal subtotal = quantity * price;

                // Update your cart or DataGridView here
                // Example: YourCartDataGridView.Rows.Add(currentDescription, quantity, subtotal);
                dataGridView1.Rows.Add(currentDescription, quantity, subtotal);

                // Calculate and update total and change labels
                CalculateTotals();
            }
        }

        private void Addttocartbtn_Click(object sender, EventArgs e)
        {
            BarcoderichTxt_TextChanged(sender, e);
        }
    }
}
