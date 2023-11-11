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
using USB_Barcode_Scanner;

namespace BenpilsBarcodeSystem
{
    public partial class PointOfSales : Form
    {
        private User user;
        private SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True");
        private string connectionString = "Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True";
        private BarcodeScanner scanner;

        public PointOfSales(User user)
        {
            InitializeComponent();
            //reportsreference = reports;
            
          
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
            if (e.ColumnIndex == dataGridView1.Columns["Add"].Index && e.RowIndex != -1)
            {
                // Show the QuantityInputForm
                using (Quantityform quantityForm = new Quantityform())
                {
                    if (quantityForm.ShowDialog() == DialogResult.OK)
                    {
                        int quantity = quantityForm.Quantity;

                        // Retrieve data from the clicked row
                        string barcode = dataGridView1.Rows[e.RowIndex].Cells["Barcode"].Value.ToString();
                        string itemName = dataGridView1.Rows[e.RowIndex].Cells["ItemName"].Value.ToString();
                        string motorBrand = dataGridView1.Rows[e.RowIndex].Cells["MotorBrand"].Value.ToString();
                        string brand = dataGridView1.Rows[e.RowIndex].Cells["Brand"].Value.ToString();
                        string size = dataGridView1.Rows[e.RowIndex].Cells["Size"].Value.ToString();
                        decimal unitPrice = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex].Cells["UnitPrice"].Value);
                        string category = dataGridView1.Rows[e.RowIndex].Cells["Category"].Value.ToString();

                        // Add the data to the database
                        InsertDataIntoDatabase(barcode, itemName, motorBrand, brand, size, unitPrice, quantity, category);
                    }
                }
            }
        }
        private void InsertDataIntoDatabase(string barcode, string itemName, string motorBrand, string brand, string size, decimal unitPrice, int quantity, string category)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Check if the item with the given barcode already exists in the database
                    string checkIfExistsQuery = "SELECT COUNT(*) FROM tbl_Cart WHERE Barcode = @Barcode";
                    using (SqlCommand checkIfExistsCommand = new SqlCommand(checkIfExistsQuery, connection))
                    {
                        checkIfExistsCommand.Parameters.AddWithValue("@Barcode", barcode);
                        int existingItemCount = (int)checkIfExistsCommand.ExecuteScalar();

                        if (existingItemCount > 0)
                        {
                            // If the item exists, update the quantity in the database
                            string updateQuantityQuery = "UPDATE tbl_Cart SET Quantity = Quantity + @Quantity WHERE Barcode = @Barcode";
                            using (SqlCommand updateQuantityCommand = new SqlCommand(updateQuantityQuery, connection))
                            {
                                updateQuantityCommand.Parameters.AddWithValue("@Barcode", barcode);
                                updateQuantityCommand.Parameters.AddWithValue("@Quantity", quantity);
                                updateQuantityCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // If the item does not exist, insert a new row into the database
                            string insertQuery = "INSERT INTO tbl_Cart (Barcode, ItemName, MotorBrand, Brand, Size, UnitPrice, Quantity, Category) " +
                                                 "VALUES (@Barcode, @ItemName, @MotorBrand, @Brand, @Size, @UnitPrice, @Quantity, @Category)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@Barcode", barcode);
                                command.Parameters.AddWithValue("@ItemName", itemName);
                                command.Parameters.AddWithValue("@MotorBrand", motorBrand);
                                command.Parameters.AddWithValue("@Brand", brand);
                                command.Parameters.AddWithValue("@Size", size);
                                command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Category", category);

                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void PointOfSales_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleCartDatabaseFinal.tbl_Cart' table. You can move, or remove it, as needed.
            this.tbl_CartTableAdapter2.Fill(this.benpillMotorcycleCartDatabaseFinal.tbl_Cart);
            // TODO: This line of code loads data into the 'benpillMotorcycleCartDatabase.tbl_Cart' table. You can move, or remove it, as needed.
            this.tbl_CartTableAdapter1.Fill(this.benpillMotorcycleCartDatabase.tbl_Cart);
            // TODO: This line of code loads data into the 'benpillMotorcycleDatabaseDataSet2.tbl_Cart' table. You can move, or remove it, as needed.
            this.tbl_CartTableAdapter.Fill(this.benpillMotorcycleDatabaseDataSet2.tbl_Cart);
            // TODO: This line of code loads data into the 'benpillMotorcycleItemMasterDataOnPOS.tbl_itemmasterdata' table. You can move, or remove it, as needed.
            this.tbl_itemmasterdataTableAdapter.Fill(this.benpillMotorcycleItemMasterDataOnPOS.tbl_itemmasterdata);
            // TODO: This line of code loads data into the 'benpillMotorcycleServicesTransactionsDatabase.tbl_servicestransactions' table. You can move, or remove it, as needed.
            this.tbl_servicestransactionsTableAdapter.Fill(this.benpillMotorcycleServicesTransactionsDatabase.tbl_servicestransactions);
            // TODO: This line of code loads data into the 'benpillMotorcycleServicestransactionDatabase.tbl_servicestransaction' table. You can move, or remove it, as needed.


         

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

                        // Set the DisplayMember and ValueMember properties
                        cmbservices.DisplayMember = "ServiceName";
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
        {// Get the selected ServiceID from the ComboBox
            int selectedServiceID = (int)cmbservices.SelectedValue;

            // Your SQL queries to insert into tbl_servicestransactions
            string queryInsertTransaction = "INSERT INTO tbl_servicestransactions (ServiceID, ServiceName, Price) VALUES (@ServiceID, @ServiceName, @Price)";

            try
            {
                connection.Open();

                // Get the Price from the selected service in the ComboBox
                decimal selectedServicePrice = GetSelectedServicePrice(selectedServiceID);

                using (SqlCommand commandTransaction = new SqlCommand(queryInsertTransaction, connection))
                {
                    // Add parameters to the command
                    commandTransaction.Parameters.AddWithValue("@ServiceID", selectedServiceID);
                    commandTransaction.Parameters.AddWithValue("@ServiceName", cmbservices.Text); // Assuming ServiceName is the text part of the combo box
                    commandTransaction.Parameters.AddWithValue("@Price", selectedServicePrice);

                    // Execute the command to insert into tbl_servicestransactions
                    commandTransaction.ExecuteNonQuery();

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
                string query = "SELECT Price FROM tbl_services WHERE ServiceID = @ServiceID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ServiceID", serviceID);
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
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
            }

            return price;
        }
        private void UpdateDisplayServicesTransactions()
        {
            string selectQuery = "SELECT * FROM tbl_servicestransactions";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView3.DataSource = dt;
                }
            }
        }

        private void PayServiceBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 3: Input Payment Amount
                decimal paymentAmount = decimal.Parse(paymentservicestxt.Text);

                // Step 4: Calculate Change
                if (paymentAmount >= Convert.ToDecimal(TotalAmountServiceTxt.Text))
                {
                    decimal change = paymentAmount - Convert.ToDecimal(TotalAmountServiceTxt.Text);

                    // Display Change
                    changepaymentservicestxt.Text = change.ToString();

                    // Step 5: Clear Table and Reset Seed
                    ClearTableAndResetSeedServicesTransactions();
                    UpdateDisplayServicesTransactions();
                    


                    MessageBox.Show("Services Payment Succesful");
                }
                else
                {
                    MessageBox.Show("Insufficient funds. Please provide a sufficient payment amount.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 1: Calculate Total Amount
                decimal totalAmount = CalculateTotalAmount();

                // Step 2: Display Total Amount
                TotalAmountServiceTxt.Text = totalAmount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
         private decimal CalculateTotalAmount()
            {
        decimal totalAmount = 0;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT SUM(Price) AS TotalAmount FROM tbl_servicestransactions";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                if (reader["TotalAmount"] != DBNull.Value)
                {
                    totalAmount = Convert.ToDecimal(reader["TotalAmount"]);
                }
            }

            reader.Close();
        }

        return totalAmount;
         }
        private void ClearTableAndResetSeedServicesTransactions()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string clearTableQuery = "DELETE FROM tbl_servicestransactions";
                SqlCommand clearTableCommand = new SqlCommand(clearTableQuery, connection);
                clearTableCommand.ExecuteNonQuery();
                string resetSeedQuery = "DBCC CHECKIDENT('tbl_servicestransactions', RESEED, 0)";
                SqlCommand resetSeedCommand = new SqlCommand(resetSeedQuery, connection);
                resetSeedCommand.ExecuteNonQuery();
            }
        }

   

        private void ClearTableBtn_Click_1(object sender, EventArgs e)
        {
            ClearTableAndResetSeedServicesTransactions();
            UpdateDisplayServicesTransactions();
        }

        /* private void RecordTransactionInReportsService()
         {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string recordsServicesTransactionQuery = "INSERT INTO tbl_servicetransactions_reports (ServiceName, Price, PaymentAmount, ChangeAmount, TransactionDateTime) " +
                                                        "VALUES (@ServiceName, @Price, @PaymentAmount, @ChangeAmount, @TransactionDateTime)";
                connection.Open();

                using (SqlCommand recordTransactionCommand = new SqlCommand(recordsServicesTransactionQuery, connection))
                {
                    recordTransactionCommand.Parameters.AddWithValue("@ServiceName", row.Cells[0].Value.ToString());
                    recordTransactionCommand.Parameters.AddWithValue("@Price", Convert.ToDecimal(row.Cells[1].Value));
                    recordTransactionCommand.Parameters.AddWithValue("@PaymentAmount", Convert.ToDecimal(row.Cells[2].Value));
                    recordTransactionCommand.Parameters.AddWithValue("@ChangeAmount", Convert.ToDecimal(row.Cells[3].Value));
                    recordTransactionCommand.Parameters.AddWithValue("@TransactionDateTime", DateTime.Now);

                    recordTransactionCommand.ExecuteNonQuery();
                }
            }
        }*/
        
        private void BarcodeScanner_OnScan(string scannedData)
        {
            // Handle the scanned data here
            MessageBox.Show($"Scanned: {scannedData}");
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the button column is clicked
            if (e.ColumnIndex == dataGridView3.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                // Get the values from the selected row
                string transactionNumber = dataGridView3.Rows[e.RowIndex].Cells["TransactionNumber"].Value.ToString();
                int serviceID = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells["ServiceID"].Value);
                string serviceName = dataGridView3.Rows[e.RowIndex].Cells["ServiceName"].Value.ToString();
                decimal price = Convert.ToDecimal(dataGridView3.Rows[e.RowIndex].Cells["Price"].Value);

                // Remove the row from the DataGridView
                dataGridView3.Rows.RemoveAt(e.RowIndex);

                // Delete the corresponding row from the database
                DeleteRowFromDatabase(transactionNumber, serviceID, serviceName, price);
            }
        }
        private void DeleteRowFromDatabase(string transactionNumber, int serviceID, string serviceName, decimal price)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM tbl_servicestransactions " +
                                     "WHERE TransactionNumber = @TransactionNumber " +
                                     "AND ServiceID = @ServiceID " +
                                     "AND ServiceName = @ServiceName " +
                                     "AND Price = @Price";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@TransactionNumber", transactionNumber);
                    command.Parameters.AddWithValue("@ServiceID", serviceID);
                    command.Parameters.AddWithValue("@ServiceName", serviceName);
                    command.Parameters.AddWithValue("@Price", price);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            Print_Invoice printinv = new Print_Invoice();
            printinv.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printPreviewDialog1.ShowDialog();
        }
    }
    }
    
    


