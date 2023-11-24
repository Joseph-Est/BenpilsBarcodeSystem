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
    public partial class Services : Form
    {
        private User user;
        private GenerateBarcode GB;
        public Services(User user)
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
                PointofsalesBtn.Enabled = false;
                reportsBtn.Enabled = false;
                statisticsBtn.Enabled = false;
                usercredentialsbtn.Enabled = false;
                settingsbtn.Enabled = false;
            }
            else if (user.Designation == "Cashier")
            {
                inventoryBtn.Enabled = false;
                purchasingBtn.Enabled = false;
                reportsBtn.Enabled = false;
                statisticsBtn.Enabled = false;
                usercredentialsbtn.Enabled = false;
                settingsbtn.Enabled = false;
            }
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

        private void DashBoardBtn_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

        private void PointofsalesBtn_Click(object sender, EventArgs e)
        {
            POS pos = new POS(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void inventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory(user);
            inv.Show();
            inv.StartPosition = FormStartPosition.CenterScreen;
            inv.Location = this.Location;
            this.Hide();
        }

        private void purchasingBtn_Click(object sender, EventArgs e)
        {
            Purchaserr purchasing = new Purchaserr(user);
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }

        private void reportsBtn_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports(user);
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }

        private void statisticsBtn_Click(object sender, EventArgs e)
        {
            StatisticReport sr = new StatisticReport(user);
            sr.Show();
            sr.StartPosition = FormStartPosition.Manual;
            sr.Location = this.Location;
            this.Hide();
        }

        private void usercredentialsbtn_Click(object sender, EventArgs e)
        {
            Ser UC = new Ser(user);
            UC.Show();
            UC.StartPosition = FormStartPosition.Manual;
            UC.Location = this.Location;
            this.Hide();
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            Settings set = new Settings(user);
            set.Show();
            set.StartPosition = FormStartPosition.Manual;
            set.Location = this.Location;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Services_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleServices.tbl_services' table. You can move, or remove it, as needed.
            this.tbl_servicesTableAdapter1.Fill(this.benpillMotorcycleServices.tbl_services);
        

        }
        private void ClearAllTheTextBoxes()
        {
            ServiceNameTxt.Text = "";
            PriceTxt.Text = "";
            BarcodeTxt.Text  = "";
        }
        private void UpdateDataGridView()
        {
            string selectQuery = "SELECT * FROM tbl_services";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridService.DataSource = dt;
                }
            }
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
           ClearAllTheTextBoxes();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if required textboxes are empty
                if (string.IsNullOrWhiteSpace(ServiceNameTxt.Text) || string.IsNullOrWhiteSpace(PriceTxt.Text) || string.IsNullOrWhiteSpace(BarcodeTxt.Text))
                {
                    MessageBox.Show("Please fill up all the textboxes below.");
                    return;
                }

                // Check if ServiceName already exists
                if (IsServiceNameAlreadyExists(ServiceNameTxt.Text))
                {
                    MessageBox.Show("ServiceName already exists. Please choose a different servicename.");
                    return;
                }
                if (IsBarcodeAlreadyExist(BarcodeTxt.Text))
                {
                    MessageBox.Show("Barcode already exist. Please Choose a different barrcode.");
                    return;
                }

                // Open the connection using the using statement (automatically ensures the connection is closed)
                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();

                    // SQL query to insert data into tbl_services
                    string query = "INSERT INTO tbl_services (ServiceName, Barcode, Price, Quantity) VALUES (@ServiceName, @Barcode, @Price, 1)";

                    // Create a SqlCommand with parameters
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ServiceName", ServiceNameTxt.Text);
                        cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text);
                        cmd.Parameters.AddWithValue("@Price", decimal.Parse(PriceTxt.Text)); // Assuming Price is decimal, adjust if necessary

                        // Execute the query
                        cmd.ExecuteNonQuery();
                    }

                    // Display a success message or perform any additional actions
                    MessageBox.Show("Service added successfully!");

                    // Clear the textboxes or update the display as needed
                    ClearAllTheTextBoxes();
                    UpdateDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }
        private bool IsServiceNameAlreadyExists(string ServiceName)
        {
            string query = "SELECT COUNT(*) FROM tbl_services WHERE servicename = @ServiceName";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@ServiceName", ServiceName);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private bool IsBarcodeAlreadyExist(string Barcode)
        {
            string query = "SELECT COUNT(*) FROM tbl_services WHERE barcode = @Barcode";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Barcode", Barcode);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (dataGridService.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }
            // Check if the "ID" column exists in the DataGridView
            if (dataGridService.Columns.Contains("ServiceID"))
            {
                // Check if the selected row has a cell with the "ID" column
                if (dataGridService.SelectedRows[0].Cells["ServiceID"].Value != null)
                {
                    int selectedRowID;

                    // Attempt to convert the value to an integer
                    if (int.TryParse(dataGridService.SelectedRows[0].Cells["ServiceID"].Value.ToString(), out selectedRowID))
                    {
                        string updateQuery = "UPDATE tbl_services SET ServiceName = @ServiceName, Barcode = @Barcode, Price = @Price WHERE ServiceID = @ServiceID";

                        using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                        {
                            using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@ServiceID", selectedRowID);
                                cmd.Parameters.AddWithValue("@ServiceName", ServiceNameTxt.Text);
                                cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text); // Add this line for the Barcode parameter
                                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(PriceTxt.Text));

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                        UpdateDataGridView();
                        ClearAllTheTextBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Invalid value in the 'ServiceID' column.");
                    }
                }
                else
                {
                    MessageBox.Show("Selected row does not contain a value in the 'ServiceID' column.");
                }
            }
            else
            {
                MessageBox.Show("The 'ID' column does not exist in the DataGridView.");
            }

        }

        private void dataGridService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridService.Rows[e.RowIndex];
                ServiceNameTxt.Text = selectedRow.Cells[2].Value.ToString();
                PriceTxt.Text = selectedRow.Cells[3].Value.ToString();     
                BarcodeTxt.Text = selectedRow.Cells[1].Value.ToString();
                AddBtn.Enabled = false;
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            AddBtn.Enabled = true;
            ClearAllTheTextBoxes() ;
            UpdateDataGridView();
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
    }
}
