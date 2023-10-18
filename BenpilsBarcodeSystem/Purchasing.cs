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
    public partial class Purchasing : Form
    {
        private User user;
        private int selectedSupplierID = -1;

        public Purchasing(User user)
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();
            this.user = user;
            label1.Text = "Username: " + user.Username;
            label2.Text = "Designation: " + user.Designation;
            if (user.Designation == "Superadmin")
            {
            }
            else if (user.Designation == "Admin")
            {
            }
            else if (user.Designation == "Inventory Manager")
            {
                PointOfSalesBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticsReportBtn.Enabled = false;
                ServicesBtn.Enabled = false;
                UserCredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
            else if (user.Designation == "Cashier")
            {
                InventoryBtn.Enabled = false;
                PurchasingBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticsReportBtn.Enabled = false;
                UserCredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }
   

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfirmationLogout cl = new ConfirmationLogout();
            if (cl.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }
        private void DashBoardBtn_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

      

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory(user);
            inventory.Show();
            inventory.StartPosition = FormStartPosition.Manual;
            inventory.Location = this.Location;
            this.Hide();
        }

        private void PurchasingBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            Reports rp = new Reports(user);
            rp.Show();
            rp.StartPosition = FormStartPosition.Manual;
            rp.Location = this.Location;
            this.Hide();
        }

        private void StatisticsReportBtn_Click(object sender, EventArgs e)
        {
            StatisticReport SR = new StatisticReport(user);
            SR.Show();
            SR.StartPosition = FormStartPosition.Manual;
            SR.Location = this.Location;
            this.Hide();
        }

        private void UserCredentialsBtn_Click(object sender, EventArgs e)
        {
            Ser UC = new Ser(user);
            UC.Show();
            UC.StartPosition = FormStartPosition.Manual;
            UC.Location = this.Location;
            this.Hide();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Settings set = new Settings(user);
            set.Show();
            set.StartPosition = FormStartPosition.Manual;
            set.Location = this.Location;
            this.Hide();
        }

        private void PointOfSalesBtn_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }

       

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.StartPosition = FormStartPosition.CenterScreen;
            CE.ShowDialog();
        }

        private void LogoutBtn_Click_1(object sender, EventArgs e)
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

        private void ServicesBtn_Click(object sender, EventArgs e)
        {
            Services service = new Services(user);
            service.Show();
            service.StartPosition = FormStartPosition.Manual;
            service.Location = this.Location;
            this.Hide();
        }

        private void addbuton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CompanyNameTxt.Text) ||
                string.IsNullOrWhiteSpace(ContactNametxt.Text) ||
                string.IsNullOrWhiteSpace(AddressTxt.Text) ||
                string.IsNullOrWhiteSpace(ContactNoTxt.Text) ||
                string.IsNullOrWhiteSpace(Emailtxt.Text))
            {
                MessageBox.Show("Please ensure all required fields are filled.");
                return;
            }

            string insertQuery = "INSERT INTO tbl_supplier (CompanyName, ContactName, Address, ContactNo, Email) " +
                               "VALUES (@CompanyName, @ContactName, @Address, @ContactNo, @Email)";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@CompanyName", CompanyNameTxt.Text);
                    cmd.Parameters.AddWithValue("@ContactName", ContactNametxt.Text);
                    cmd.Parameters.AddWithValue("@Address", AddressTxt.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", ContactNoTxt.Text);
                    cmd.Parameters.AddWithValue("@Email", Emailtxt.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
            ClearAllTextBoxes();
        }

        private void UpdateDataGridView()
        {
            string selectQuery = "SELECT * FROM tbl_supplier";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
            }
        }
        private void ClearAllTextBoxes()
        {
            CompanyNameTxt.Text = "";
            ContactNametxt.Text = "";
            AddressTxt.Text = "";
            ContactNoTxt.Text = "";
            Emailtxt.Text = "";
        }

        private void Purchasing_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleSuppier.tbl_supplier' table. You can move, or remove it, as needed.
            this.tbl_supplierTableAdapter1.Fill(this.benpillMotorcycleSuppier.tbl_supplier);
            // TODO: This line of code loads data into the 'benpillMotorcycleDatabaseSupplier.tbl_supplier' table. You can move, or remove it, as needed.
        

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
                selectedSupplierID = int.Parse(selectedRow.Cells["SupplierID"].Value.ToString());
                CompanyNameTxt.Text = selectedRow.Cells["CompanyName"].Value.ToString();
                ContactNametxt.Text = selectedRow.Cells["ContactName"].Value.ToString();
                AddressTxt.Text = selectedRow.Cells["Address"].Value.ToString();
                ContactNoTxt.Text = selectedRow.Cells["ContactNo"].Value.ToString();
                Emailtxt.Text = selectedRow.Cells["Email"].Value.ToString();
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (selectedSupplierID != -1) // Ensure a valid selection
            {
                string companyName = CompanyNameTxt.Text;
                string contactName = ContactNametxt.Text;
                string address = AddressTxt.Text;
                string contactNo = ContactNoTxt.Text;
                string email = Emailtxt.Text;

                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "UPDATE tbl_supplier " +
                                   "SET CompanyName = @CompanyName, " +
                                   "ContactName = @ContactName, " +
                                   "Address = @Address, " +
                                   "ContactNo = @ContactNo, " +
                                   "Email = @Email " +
                                   "WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@CompanyName", companyName);
                    command.Parameters.AddWithValue("@ContactName", contactName);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@ContactNo", contactNo);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID); // Use the selectedSupplierID

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier information updated successfully.");
                        // Refresh the DataGridView to reflect the changes if needed.
                        // You can rebind the data or just update the specific row in the DataGridView.
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row before updating.");
            }
            UpdateDataGridView();
            ClearAllTextBoxes();
        }
        private void SupplierSelectionAdd()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                connection.Open();

                string query = "SELECT SupplierID FROM tbl_supplier";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CmbSelectSupplier.Items.Add(reader["SupplierID"].ToString());
                        }
                    }
                }
            }
        }
        private void CmbSelectSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbSelectSupplier.SelectedIndex != -1)
            {
                int selectedSupplierID = int.Parse(CmbSelectSupplier.SelectedItem.ToString());

                // Declare and initialize the SqlConnection object
                using (SqlConnection connection = new SqlConnection("YourConnectionString"))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("SELECT CompanyName, ContactNo FROM tbl_supplier WHERE SupplierID = @SupplierID", connection))
                    {
                        command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CompanyNameTxt.Text = reader["CompanyName"].ToString();
                                ContactNoTxt.Text = reader["ContactNo"].ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}
