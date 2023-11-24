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
    public partial class Purchaserr : Form
    {
        private User user;
        private int selectedSupplierID = -1;
        private DataTable dtCart;
        private Random random = new Random();
        public DataGridView DataGridView1 => dataGridView1;
        public Purchaserr(User user)
        {
            InitializeComponent();
            dtCart = new DataTable();
            dtCart.Columns.Add("ProductID", typeof(int));
            dtCart.Columns.Add("ItemName", typeof(string));
            dtCart.Columns.Add("Quantity", typeof(int));
            dtCart.Columns.Add("Subtotal", typeof(decimal));

            dataGridView2.DataSource = dtCart;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick_1;
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
                StatisticReportBtn.Enabled = false;
                ServicesBtn.Enabled = false;
                UserCredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
            else if (user.Designation == "Cashier")
            {
                InventoryBtn.Enabled = false;
                PurchasingBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticReportBtn.Enabled = false;
                UserCredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
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

        private void PointOfSalesBtn_Click(object sender, EventArgs e)
        {
            POS pos = new POS(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
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

        private void ServicesBtn_Click(object sender, EventArgs e)
        {
            Services service = new Services(user);
            service.Show();
            service.StartPosition = FormStartPosition.Manual;
            service.Location = this.Location;
            this.Hide();
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            Reports rp = new Reports(user);
            rp.Show();
            rp.StartPosition = FormStartPosition.Manual;
            rp.Location = this.Location;
            this.Hide();
        }

        private void StatisticReportBtn_Click(object sender, EventArgs e)
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
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.StartPosition = FormStartPosition.CenterScreen;
            CE.ShowDialog();
        }
        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
                    dataGridSupplier.DataSource = dt;
                }
            }
        }
        public void UpdateDataGridView1(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
        }
        private void ClearAllTextBoxes()
        {

            ContactNameTxt.Text = "";
            AddressTxt.Text = "";
            ContactNoTxt.Text = "";

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContactNameTxt.Text) ||
                string.IsNullOrWhiteSpace(AddressTxt.Text) ||
                string.IsNullOrWhiteSpace(ContactNoTxt.Text) )
            {
                MessageBox.Show("Please ensure all required fields are filled.");
                return;
            }

            string insertQuery = "INSERT INTO tbl_supplier (ContactName, Address, ContactNo) " +
                             "VALUES (@ContactName, @Address, @ContactNo)";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ContactName", ContactNameTxt.Text);
                    cmd.Parameters.AddWithValue("@Address", AddressTxt.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", ContactNoTxt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
            ClearAllTextBoxes();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (selectedSupplierID != -1)
            {
                string contactName = ContactNameTxt.Text;
                string address = AddressTxt.Text;
                string contactNo = ContactNoTxt.Text;


                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "UPDATE tbl_supplier " +
                                   "SET ContactName = @ContactName, " +
                                   "Address = @Address, " +
                                   "ContactNo = @ContactNo, " +
                                   "WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ContactName", contactName);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@ContactNo", contactNo);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier information updated successfully.");
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

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
        }

        private void Purchaserr_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcyclePurchaseOrderList.tbl_purchaseorderlist' table. You can move, or remove it, as needed.
            this.tbl_purchaseorderlistTableAdapter.Fill(this.benpillMotorcyclePurchaseOrderList.tbl_purchaseorderlist);
            // TODO: This line of code loads data into the 'benpillMotorcycleSupplier.tbl_supplier' table. You can move, or remove it, as needed.
            this.tbl_supplierTableAdapter.Fill(this.benpillMotorcycleSupplier.tbl_supplier);

        }

        private void ContactNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridSupplier.Rows[e.RowIndex];
                ContactNameTxt.Text = row.Cells[1].Value.ToString();
                AddressTxt.Text = row.Cells[2].Value.ToString();
                ContactNoTxt.Text = row.Cells[3].Value.ToString();
                AddBtn.Enabled = false;
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
            AddBtn.Enabled = true;
        }

        private void AdddBtn_Click(object sender, EventArgs e)
        {
            AddItemSupplier addItemSupplier = new AddItemSupplier(user);
            addItemSupplier.BringToFront();
            addItemSupplier.StartPosition = FormStartPosition.CenterScreen;
            addItemSupplier.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle the "Add Data" button click for DataGridView1
            // Here, you might want to open a QuantityForm to get the quantity input
            // For simplicity, we'll assume the quantity is entered directly here.

            int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

            // Extract data from the selected row in DataGridView1
            int productID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["ProductID"].Value);
            string itemName = dataGridView1.Rows[selectedRowIndex].Cells["ItemName"].Value.ToString();
            decimal unitPrice = Convert.ToDecimal(dataGridView1.Rows[selectedRowIndex].Cells["UnitPrice"].Value);

            // Assume QuantityForm is a form where the user enters the quantity
            Quantityform quantityForm = new Quantityform();
            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                int quantity = quantityForm.Quantity;
                decimal subtotal = unitPrice * quantity;

                // Add data to the DataTable for DataGridView2
                dtCart.Rows.Add(productID, itemName, quantity, subtotal);

                // Update the total label
                UpdateTotalLabel();
            }
        }
        private void UpdateTotalLabel()
        {
            // Calculate the total from the DataTable
            decimal total = dtCart.AsEnumerable().Sum(row => row.Field<decimal>("Subtotal"));

            // Update the Total label
            totallbl.Text = total.ToString();
        }
    }
}
