﻿using System;
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
    public partial class PointOfSales : Form
    {
        private User user;
        private SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True");
        public PointOfSales(User user)
        {
            InitializeComponent();
            FillComboBox();
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
                button3.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                ServicesBtn.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }
            else if (user.Designation == "Cashier")
            {
                button2.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
            }

        }
        //Dashboard Button
        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }
        //Inventory Button
        private void button2_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory(user);
            inv.Show();
            inv.StartPosition = FormStartPosition.Manual;
            inv.Location = this.Location;
            this.Hide();
        }
        //Purchasing Button
        private void button5_Click(object sender, EventArgs e)
        {
            Purchaserr purchasing = new Purchaserr(user);
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }
        //Reports Button
        private void button6_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports(user);
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }
        //Statistics Report Button
        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport statisticReport = new StatisticReport(user);
            statisticReport.Show();
            statisticReport.StartPosition = FormStartPosition.Manual;
            statisticReport.Location = this.Location;
            this.Hide();
        }
        //UserCredentials Button
        private void button8_Click(object sender, EventArgs e)
        {
            Services credentials = new Services(user);
            credentials.Show();
            credentials.StartPosition = FormStartPosition.Manual;
            credentials.Location = this.Location;
            this.Hide();
        }
        //Settings Button
        private void button9_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(user);
            settings.Show();
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = this.Location;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
        //Close Button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
        }
        //Minimize Button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            ConfirmationExit cl = new ConfirmationExit();
            cl.StartPosition = FormStartPosition.CenterScreen;
            cl.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PointOfSales_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleServicesTransactionsDatabase.tbl_servicestransactions' table. You can move, or remove it, as needed.
            this.tbl_servicestransactionsTableAdapter.Fill(this.benpillMotorcycleServicesTransactionsDatabase.tbl_servicestransactions);
            // TODO: This line of code loads data into the 'benpillMotorcycleServicestransactionDatabase.tbl_servicestransaction' table. You can move, or remove it, as needed.


            this.tbl_voidtableTableAdapter.Fill(this.benpillMotorcycleDatabaseVoidTable.tbl_voidtable);

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
        private void clearAlltextbox()
        {
         
            paymentitemTxt.Text ="";
            changepaymentitemTxt.Text = "";
        }
        private void clearAllTextbox2()
        {
            cmbservices.Items .Clear();
            paymentservicestxt.Text = "";
            changepaymentitemTxt.Text = "";
        }


        private void cmbservices_SelectedIndexChanged(object sender, EventArgs e)
        {
       
        }
        private void FillComboBox()
        {
            try
            {
                connection.Open();

                // Your SQL query to retrieve data from tbl_services
                string query = "SELECT ServiceID, ServiceName, Price FROM tbl_services";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Add a computed column combining ServiceName and Price
                        dataTable.Columns.Add("DisplayMember", typeof(string), "ServiceName + ' - $' + CONVERT(Price, System.String)");

                        // Set the DisplayMember and ValueMember properties
                        cmbservices.DisplayMember = "DisplayMember";
                        cmbservices.ValueMember = "ServiceID";
                        cmbservices.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            clearAlltextbox();
        }

        private void Clear2Btn_Click(object sender, EventArgs e)
        {
            clearAllTextbox2();
        }

        private void Addbtnservices_Click(object sender, EventArgs e)
        {
            // Get the selected ServiceID from the ComboBox
            int selectedServiceID = (int)cmbservices.SelectedValue;

            // Your SQL queries to insert into the three tables
            string queryInsertID = "INSERT INTO ServicesID (ServiceID) VALUES (@ServiceID)";
            string queryInsertName = "INSERT INTO ServiceName (ServiceID, ServiceName) VALUES (@ServiceID, @ServiceName)";
            string queryInsertPrice = "INSERT INTO Price (ServiceID, Price) VALUES (@ServiceID, @Price)";

            try
            {
                connection.Open();

                using (SqlCommand commandID = new SqlCommand(queryInsertID, connection))
                using (SqlCommand commandName = new SqlCommand(queryInsertName, connection))
                using (SqlCommand commandPrice = new SqlCommand(queryInsertPrice, connection))
                {
                    // Add parameters to the commands
                    commandID.Parameters.AddWithValue("@ServiceID", selectedServiceID);
                    commandName.Parameters.AddWithValue("@ServiceID", selectedServiceID);
                    commandName.Parameters.AddWithValue("@ServiceName", cmbservices.Text); // Assuming ServiceName is the text part of the combo box
                    commandPrice.Parameters.AddWithValue("@ServiceID", selectedServiceID);

                    // Get the Price from the selected service in the ComboBox
                    decimal selectedServicePrice = GetSelectedServicePrice(selectedServiceID);
                    commandPrice.Parameters.AddWithValue("@Price", selectedServicePrice);

                    // Execute the commands
                    commandID.ExecuteNonQuery();
                    commandName.ExecuteNonQuery();
                    commandPrice.ExecuteNonQuery();

                    // Display the added data in the DataGridView
                    UpdateDisplayServicesTransactions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
            private decimal GetSelectedServicePrice(int serviceID)
        {
            decimal price = 0;

            try
            {
                connection.Open();

                // Your SQL query to retrieve the price based on the selected ServiceID
                string query = "SELECT Price FROM tbl_services WHERE ServiceID = @ServiceID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ServiceID", serviceID);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        // If the result is not null, convert it to decimal
                        price = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return price;
        }
        private void UpdateDisplayServicesTransactions()
        {
            try
            {
                connection.Open();

                // Your SQL query to retrieve data from the three tables
                string query = "SELECT ServicesID.ServiceID, ServiceName.ServiceName, Price.Price " +
                               "FROM ServicesID " +
                               "INNER JOIN ServiceName ON ServicesID.ServiceID = ServiceName.ServiceID " +
                               "INNER JOIN Price ON ServicesID.ServiceID = Price.ServiceID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Display the data in the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Assuming the columns in the DataGridView are named "ServiceID", "ServiceName", and "Price"
                        // You may need to adjust these column names based on your actual DataGridView setup
                        dataGridView1.Columns["ServiceID"].Visible = false; // Hide ServiceID column if you don't want to display it

                        // Adjust the column names based on your actual DataGridView setup
                        dataGridView1.Columns["ServiceName"].HeaderText = "Service Name";
                        dataGridView1.Columns["Price"].HeaderText = "Price";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    }
    
    


