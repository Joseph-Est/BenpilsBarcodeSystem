using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class Settings : Form
    {
        private User user;
        public Settings(User user)
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
               ServicesBtn.Enabled = false;
               StatisticreportsBtn.Enabled = false;
               UserCredentialsBtn.Enabled = false;
               
            }
            else if (user.Designation == "Cashier")
            {
               InventoryBtn.Enabled = false;
               PurchasingBtn.Enabled = false;
               ReportsBtn.Enabled = false;
               StatisticreportsBtn.Enabled = false;
               UserCredentialsBtn.Enabled = false;
            }
        }

        private void ClosedBtn_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.StartPosition = FormStartPosition.CenterScreen;
            CE.ShowDialog();
        }

        private void MinimizedBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PointOfSalesBtn_Click(object sender, EventArgs e)
        {
            POS pos = new POS(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory(user);
            inv.Show();
            inv.StartPosition = FormStartPosition.Manual;
            inv.Location = this.Location;
            this.Hide();
        }

        private void PurchasingBtn_Click(object sender, EventArgs e)
        {
            Purchaserr purchasing = new Purchaserr(user);
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
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

        private void StatisticreportsBtn_Click(object sender, EventArgs e)
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

        private void DashboardBtn_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

        private void ServicesBtn_Click(object sender, EventArgs e)
        {
            Services service = new Services(user);
            service.Show();
            service.StartPosition = FormStartPosition.Manual;
            service.Location = this.Location;
            this.Hide();
        }

        private void RetrievedItemBtn_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridArchived.SelectedRows.Count > 0)
            {
                // Get the selected ProductID from the DataGridView
                int productId = Convert.ToInt32(dataGridArchived.SelectedRows[0].Cells["ProductID"].Value);

                // Call the retrieve function
                RetrieveProduct(productId);

                // Refresh the DataGridView in the Archive window
                UpdateArchivedDataGridView();
            }
            else
            {
                MessageBox.Show("Please select a row to retrieve.");
            }
        }
        private void RetrieveProduct(int productId)
        {
            // Fetch the product details based on ProductID
            DataRow selectedRow = GetArchivedProductDetails(productId);

            if (selectedRow != null)
            {
                // Retrieve the product by inserting it back into the main table
                string retrieveQuery = "INSERT INTO tbl_itemmasterdata (Barcode, ProductID, ItemName, MotorBrand, Brand, UnitPrice, Quantity, Category, Size) " +
                                       "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category, @Size)";

                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(retrieveQuery, con))
                    {
                        // Set parameters from the selected row in the ArchivedItems table
                        cmd.Parameters.AddWithValue("@Barcode", selectedRow["Barcode"]);
                        cmd.Parameters.AddWithValue("@ProductID", selectedRow["ProductID"]);
                        cmd.Parameters.AddWithValue("@ItemName", selectedRow["ItemName"]);
                        cmd.Parameters.AddWithValue("@MotorBrand", selectedRow["MotorBrand"]);
                        cmd.Parameters.AddWithValue("@Brand", selectedRow["Brand"]);
                        cmd.Parameters.AddWithValue("@UnitPrice", selectedRow["UnitPrice"]);
                        cmd.Parameters.AddWithValue("@Quantity", selectedRow["Quantity"]);
                        cmd.Parameters.AddWithValue("@Category", selectedRow["Category"]);
                        cmd.Parameters.AddWithValue("@Size", selectedRow["Size"]);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Remove the retrieved product from the ArchivedItems table
                DeleteArchivedProduct(productId);
            }
        }
        private DataRow GetArchivedProductDetails(int productId)
        {
            DataTable dt = (DataTable)dataGridArchived.DataSource;
            DataRow[] rows = dt.Select($"ProductID = {productId}");
            return rows.Length > 0 ? rows[0] : null;
        }
        private void UpdateArchivedDataGridView()
        {
            // Fetch data from the ArchivedItems table and update the DataGridView
            string selectQuery = "SELECT * FROM ArchivedItems";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridArchived.DataSource = dt;
                }
            }
        }
        private void DeleteArchivedProduct(int productId)
        {
            string deleteQuery = "DELETE FROM ArchivedItems WHERE ProductID = @ProductID";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleArchivedItemMaster.ArchivedItems' table. You can move, or remove it, as needed.
            this.archivedItemsTableAdapter.Fill(this.benpillMotorcycleArchivedItemMaster.ArchivedItems);

        }
    }
}
