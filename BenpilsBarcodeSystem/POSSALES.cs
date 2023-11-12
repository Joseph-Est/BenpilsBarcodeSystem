using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BenpilsBarcodeSystem
{
    public partial class POSSALES : Form
    {
        private User user;
        private SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True");
        private string connectionString = "Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True";


        public POSSALES(User user)
        {
            InitializeComponent();
            FillComboBox();
            //cmbservice.SelectedIndexChanged += Cmbservice_SelectedIndexChanged;
            //paymentservicesTxt.TextChanged += Paymentservicestxt_TextChanged
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

            }
            else if (user.Designation == "Cashier")
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }
        //Close Button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit confirmationExit = new ConfirmationExit();
            confirmationExit.StartPosition = FormStartPosition.CenterParent;
            confirmationExit.ShowDialog();
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
            Services service = new Services(user);
            service.Show();
            service.StartPosition = FormStartPosition.Manual;
            service.Location = this.Location;
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

        private void POSSALES_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleServiceTransactions.tbl_servicestransactions' table. You can move, or remove it, as needed.
            this.tbl_servicestransactionsTableAdapter.Fill(this.benpillMotorcycleServiceTransactions.tbl_servicestransactions);
            // TODO: This line of code loads data into the 'benpillMotorcycleItemmmasterdataforPOS.tbl_itemmasterdata' table. You can move, or remove it, as needed.
            this.tbl_itemmasterdataTableAdapter.Fill(this.benpillMotorcycleItemmmasterdataforPOS.tbl_itemmasterdata);
            // TODO: This line of code loads data into the 'benpillMotorcycleCartDatabasefinal.tbl_Cart' table. You can move, or remove it, as needed.
            this.tbl_CartTableAdapter.Fill(this.benpillMotorcycleCartDatabasefinal.tbl_Cart);

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
                        cmbservice.DisplayMember = "ServiceName";
                        cmbservice.ValueMember = "ServiceID";
                        cmbservice.DataSource = dataTable;
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

        private void UpdateTotalAmountServices()
        {
            try
            {
                // Step 1: Calculate Total Amount
                decimal totalAmount = CalculateTotalAmountService();

                // Step 2: Display Total Amount
                TotalAmuntServiceTxt.Text = totalAmount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
        private decimal CalculateTotalAmountService()
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

        private void addservicesBtn_Click(object sender, EventArgs e)
        {
            // Get the selected ServiceID from the ComboBox
            int selectedServiceID = (int)cmbservice.SelectedValue;

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
                    commandTransaction.Parameters.AddWithValue("@ServiceName", cmbservice.Text); // Assuming ServiceName is the text part of the combo box
                    commandTransaction.Parameters.AddWithValue("@Price", selectedServicePrice);

                    // Execute the command to insert into tbl_servicestransactions
                    commandTransaction.ExecuteNonQuery();

                    // Display the added data in the DataGridView
                    UpdateDisplayServicesTransactions();
                }

                // Update total amount when a service is added
                UpdateTotalAmountServices();
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
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the button column is clicked
            if (e.ColumnIndex == dataGridView3.Columns["Remove"].Index && e.RowIndex >= 0)
            {
                // Get the values from the selected row

                int serviceID = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells["ServiceID"].Value);
                string serviceName = dataGridView3.Rows[e.RowIndex].Cells["ServiceName"].Value.ToString();
                decimal price = Convert.ToDecimal(dataGridView3.Rows[e.RowIndex].Cells["Price"].Value);

                // Remove the row from the DataGridView
                dataGridView3.Rows.RemoveAt(e.RowIndex);

                // Delete the corresponding row from the database
                DeleteRowFromDatabase(serviceID, serviceName, price);
            }
        }
        private void DeleteRowFromDatabase(int serviceID, string serviceName, decimal price)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM tbl_servicestransactions " +
                                     "WHERE ServiceID = @ServiceID " +
                                     "AND ServiceName = @ServiceName " +
                                     "AND Price = @Price";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {

                    command.Parameters.AddWithValue("@ServiceID", serviceID);
                    command.Parameters.AddWithValue("@ServiceName", serviceName);
                    command.Parameters.AddWithValue("@Price", price);

                    command.ExecuteNonQuery();
                }
            }
        }
        private void ClearServicesBtn_Click(object sender, EventArgs e)
        {
            clearAllTextbox2();
        }
        private void clearAllTextbox2()
        {
            TotalAmmontItemTxt.Clear();
            paymentservicesTxt.Text = "";
            changeservicesTxt.Clear();
        }
        private void cmbservice_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTotalAmountServices();
        }

        private void paymentservicesTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalAmountServices();
        }

        private void PayservicesBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Step 3: Input Payment Amount
                decimal paymentAmount = decimal.Parse(paymentservicesTxt.Text);

                // Step 4: Calculate Change
                if (paymentAmount >= Convert.ToDecimal(TotalAmuntServiceTxt.Text))
                {
                    decimal change = paymentAmount - Convert.ToDecimal(TotalAmuntServiceTxt.Text);

                    // Display Change
                    changeservicesTxt.Text = change.ToString();

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
    
    //POS


    private void ClearPOSBtn_Click(object sender, EventArgs e)
        {
            cleartableCart();
            UpdateDataCartview();
        }

        private void BuyBtn_Click(object sender, EventArgs e)
        {
            cleartableCart();

            // Calculate and display the total amount
            CalculateTotalAmountPOS();

            // Handle payment and change calculation
            CalculateChangePOS();
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {

        }
        private void cleartableCart()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Clear the cart table
                string clearCartTableQuery = "DELETE FROM tbl_Cart";
                SqlCommand clearCartTableCommand = new SqlCommand(clearCartTableQuery, connection);
                clearCartTableCommand.ExecuteNonQuery();


            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Add"].Index && e.RowIndex != -1)
            {
                // Show the QuantityInputForm
                using (Quantityform quantityForm = new Quantityform())
                {
                    if (quantityForm.ShowDialog() == DialogResult.OK)
                    {
                        int quantity = quantityForm.Quantity;
                        int productid;

                        // Retrieve data from the clicked row
                        string barcode = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                        string itemName = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                        string motorBrand = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                        string brand = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                        string size = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();
                        decimal unitPrice = Convert.ToDecimal(dataGridView2.Rows[e.RowIndex].Cells[6].Value);
                        string category = dataGridView2.Rows[e.RowIndex].Cells[9].Value.ToString();

                        // Convert the value to an integer
                        if (int.TryParse(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString(), out productid))
                        {
                            // Calculate the subtotal
                            decimal subTotal = unitPrice * quantity;

                            // Add the data to the database
                            InsertDataIntoDatabase(productid, barcode, itemName, motorBrand, brand, size, unitPrice, quantity, category, subTotal);
                        }
                        else
                        {
                            // Handle the case where conversion to int fails
                            // For example, show an error message or set a default value
                        }
                    }
                    else
                    {
                        MessageBox.Show("Must input an item first");
                    }
                }
            }
        }
        private void InsertDataIntoDatabase(int productid, string barcode, string itemName, string motorBrand, string brand, string size, decimal unitPrice, int quantity, string category, decimal subTotal)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();

                    // Check if the item with the given barcode already exists in the cart database
                    string checkIfExistsQuery = "SELECT COUNT(*) FROM tbl_Cart WHERE Barcode = @Barcode";
                    using (SqlCommand checkIfExistsCommand = new SqlCommand(checkIfExistsQuery, connection, transaction))
                    {
                        checkIfExistsCommand.Parameters.AddWithValue("@Barcode", barcode);
                        int existingItemCount = (int)checkIfExistsCommand.ExecuteScalar();

                        if (existingItemCount > 0)
                        {
                            // If the item exists in the cart, update the quantity and subtotal
                            string updateQuantityQuery = "UPDATE tbl_Cart SET Quantity = Quantity + @Quantity, SubTotal = SubTotal + @SubTotal WHERE Barcode = @Barcode";
                            using (SqlCommand updateQuantityCommand = new SqlCommand(updateQuantityQuery, connection, transaction))
                            {
                                updateQuantityCommand.Parameters.AddWithValue("@Barcode", barcode);
                                updateQuantityCommand.Parameters.AddWithValue("@Quantity", quantity);
                                updateQuantityCommand.Parameters.AddWithValue("@SubTotal", subTotal);
                                updateQuantityCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // If the item does not exist in the cart, insert a new row
                            string insertQuery = "INSERT INTO tbl_Cart (Barcode, ItemName, MotorBrand, Brand, Size, UnitPrice, Quantity, Category, SubTotal) " +
                                                 "VALUES (@Barcode, @ItemName, @MotorBrand, @Brand, @Size, @UnitPrice, @Quantity, @Category, @SubTotal)";
                            using (SqlCommand command = new SqlCommand(insertQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Barcode", barcode);
                                command.Parameters.AddWithValue("@ItemName", itemName);
                                command.Parameters.AddWithValue("@MotorBrand", motorBrand);
                                command.Parameters.AddWithValue("@Brand", brand);
                                command.Parameters.AddWithValue("@Size", size);
                                command.Parameters.AddWithValue("@UnitPrice", unitPrice);
                                command.Parameters.AddWithValue("@Quantity", quantity);
                                command.Parameters.AddWithValue("@Category", category);
                                command.Parameters.AddWithValue("@SubTotal", subTotal);
                                command.ExecuteNonQuery();
                            }
                        }

                        // Update quantity in tbl_itemmasterdata
                        string updateQuantityItemMasterDataQuery = "UPDATE tbl_itemmasterdata SET Quantity = Quantity - @Quantity WHERE Barcode = @Barcode";
                        using (SqlCommand updateQuantityItemMasterDataCommand = new SqlCommand(updateQuantityItemMasterDataQuery, connection, transaction))
                        {
                            updateQuantityItemMasterDataCommand.Parameters.AddWithValue("@Barcode", barcode);
                            updateQuantityItemMasterDataCommand.Parameters.AddWithValue("@Quantity", quantity);
                            updateQuantityItemMasterDataCommand.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                        UpdateDataCartview();
                        UpdateDataItemmasterdataview();
                    }
                }
                catch (Exception ex)
                {
                    // Roll back the transaction in case of an error
                    transaction?.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void ClearCartBtn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Clear the cart table
                string clearCartTableQuery = "DELETE FROM tbl_Cart";
                SqlCommand clearCartTableCommand = new SqlCommand(clearCartTableQuery, connection);
                clearCartTableCommand.ExecuteNonQuery();
            }
        }
        private void UpdateDataItemmasterdataview()
        {
            string selectQuery = "SELECT * FROM tbl_itemmasterdata";
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
        private void UpdateDataCartview()
        {
            string selectQuery = "SELECT * FROM tbl_Cart";
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
        private void CalculateTotalAmountPOS()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Retrieve all Subtotal values from tbl_Cart
                string getTotalAmountQuery = "SELECT SUM(Subtotal) FROM tbl_Cart";
                using (SqlCommand getTotalAmountCommand = new SqlCommand(getTotalAmountQuery, connection))
                {
                    object totalAmountObj = getTotalAmountCommand.ExecuteScalar();

                    if (totalAmountObj != DBNull.Value)
                    {
                        decimal totalAmount = Convert.ToDecimal(totalAmountObj);
                        TotalAmmontItemTxt.Text = totalAmount.ToString(); // Display total amount as currency
                    }
                }
            }
        }
        private void GenerateTransactionNumberPOS()
        {
            // Generate a random 5-digit transaction number
            Random random = new Random();
            int transactionNumber = random.Next(10000, 99999);

            // Display the generated transaction number
            lbltransaction.Text = "BP" + transactionNumber.ToString();
        }
        private void DisplayDatePOS()
        {
            lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm");
        }
        private void CalculateChangePOS()
        {

            if (!string.IsNullOrEmpty(PaymentitemTxt.Text))
            {
                decimal payment;
                if (decimal.TryParse(PaymentitemTxt.Text, out payment))
                {

                    decimal totalAmount;
                    if (decimal.TryParse(TotalAmmontItemTxt.Text, out totalAmount))
                    {

                        if (payment < totalAmount)
                        {
                            MessageBox.Show("Insufficient funds. Please enter a sufficient payment amount.");
                        }
                        else
                        {

                            decimal change = payment - totalAmount;
                            ChangeitemTxt.Text = change.ToString();
                            GenerateTransactionNumberPOS();
                            DisplayDatePOS();
                            clearAlltextbox();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid total amount.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid payment amount.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a payment amount.");
            }
        }
        private void clearAlltextbox()
        {
            TotalAmmontItemTxt.Clear();
            PaymentitemTxt.Text = "";
            ChangeitemTxt.Clear();
        }

        private void TotalAmmontItemTxt_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalAmountPOS();
        }


    }
}
