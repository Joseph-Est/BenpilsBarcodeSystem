using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using System.Windows.Forms.VisualStyles;

namespace BenpilsBarcodeSystem
{
    public partial class Inventory : Form
    {
        private User user;
        private GenerateBarcode GB;
        public Inventory(User user)
        {
            InitializeComponent();
            numericUpDown1.Minimum = -500;
            numericUpDown1.Maximum = 0;
            numericUpDown1.Value = -1;
            PopulateComboBoxStatus();
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
        //Point Of Sales Button
        private void button3_Click(object sender, EventArgs e)
        {
            POS pos = new POS(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
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
            Reports rp = new Reports(user);
            rp.Show();
            rp.StartPosition = FormStartPosition.Manual;
            rp.Location = this.Location;
            this.Hide();
        }
        //StatisticReports Button
        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport sreport = new StatisticReport(user);
            sreport.Show();
            sreport.StartPosition = FormStartPosition.Manual;
            sreport.Location = this.Location;
            this.Hide();
        }
        //Usercredentials Button
        private void button8_Click(object sender, EventArgs e)
        {
            Ser Uc = new Ser(user);
            Uc.Show();
            Uc.StartPosition = FormStartPosition.Manual;
            Uc.Location = this.Location;
            this.Hide();
        }
        //Settings Button
        private void button9_Click(object sender, EventArgs e)
        {
            Settings set = new Settings(user);
            set.Show();
            set.StartPosition = FormStartPosition.Manual;
            set.Location = this.Location;
            this.Hide();
        }
        //Minimize Button
        private void label6_Click(object sender, EventArgs e)
        {
           
        }
        //Close Button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
        }
        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }
     

       
        private void Inventory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleItemmasterdata.tbl_itemmasterdata' table. You can move, or remove it, as needed.
            this.tbl_itemmasterdataTableAdapter3.Fill(this.benpillMotorcycleItemmasterdata.tbl_itemmasterdata);
            // TODO: This line of code loads data into the 'benpillMotorcycleStockManagementDatabase.tbl_itemmasterdata' table. You can move, or remove it, as needed.
            this.tbl_itemmasterdataTableAdapter2.Fill(this.benpillMotorcycleStockManagementDatabase.tbl_itemmasterdata);
            // TODO: This line of code loads data into the 'benpillMotorcycleItemmasterDataMain.tbl_itemmasterdata' table. You can move, or remove it, as needed.
            this.tbl_itemmasterdataTableAdapter1.Fill(this.benpillMotorcycleItemmasterDataMain.tbl_itemmasterdata);
            // TODO: This line of code loads data into the 'benpillMotorcycleMasterData.tbl_itemmasterdata' table. You can move, or remove it, as needed.
            this.tbl_itemmasterdataTableAdapter.Fill(this.benpillMotorcycleMasterData.tbl_itemmasterdata);

        }
        private void Archive_Click(object sender, EventArgs e)
        {

        }
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfirmationLogout cl = new ConfirmationLogout();
            cl.StartPosition = FormStartPosition.CenterParent;
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
        private void UpdateDataGridView()
        {
            string selectQuery = "SELECT * FROM tbl_itemmasterdata";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridItemMasterdata.DataSource = dt;
                }
            }
        }
        private void ClearAllTextBoxes()
        {
            BarcodeTxt.Text = "";
            ProductIDTxt.Text = "";
            ItemNameTxt.Text = "";
            MotorBrandTxt.Text = "";
            BrandTxt.Text = "";
            UnitPriceTxt.Text = "";
            QuantityTxt.Text = "";
            CategoryTxt.Text = "";
            SizeTxt.Text = "";
        }
        private void InventoryTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void QuantityTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void ProductIDTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void UnitPriceTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BarcodeTxt.Text) ||
                string.IsNullOrEmpty(ItemNameTxt.Text) ||
                string.IsNullOrEmpty(ProductIDTxt.Text) ||
                string.IsNullOrEmpty(UnitPriceTxt.Text) ||
                string.IsNullOrEmpty(QuantityTxt.Text) ||
                string.IsNullOrEmpty(CategoryTxt.Text))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return;
            }

            int productId;

            if (!int.TryParse(ProductIDTxt.Text, out productId))
            {
                MessageBox.Show("Product ID must be a valid integer.");
                return;
            }

            if (IsProductIDAlreadyExists(productId))
            {
                MessageBox.Show("Product ID already exists in the database. Please choose a different Product ID.");
                return;
            }

            string size = SizeTxt.Text;

            if (IsSizeAlreadyExists(size))
            {
                MessageBox.Show("Size already exists in the database. Please choose a different Size.");
                return;
            }

            string insertQuery = "INSERT INTO tbl_itemmasterdata (Barcode, ProductID, ItemName, MotorBrand, Brand, UnitPrice, Quantity, Category, Size) " +
                                "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category, @Size)";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ItemName", ItemNameTxt.Text);
                    cmd.Parameters.AddWithValue("@MotorBrand", MotorBrandTxt.Text);
                    cmd.Parameters.AddWithValue("@Brand", BrandTxt.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", decimal.Parse(UnitPriceTxt.Text));
                    cmd.Parameters.AddWithValue("@Quantity", int.Parse(QuantityTxt.Text));
                    cmd.Parameters.AddWithValue("@Category", CategoryTxt.Text);
                    cmd.Parameters.AddWithValue("@Size", size);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateStockStatus(productId);  // Call the method to update stock status
            UpdateDataGridView();
            ClearAllTextBoxes();
        }
        private bool IsProductIDAlreadyExists(int productId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_itemmasterdata WHERE ProductID = @ProductID", con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private bool IsSizeAlreadyExists(string size)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbl_itemmasterdata WHERE Size = @Size", con))
                {
                    cmd.Parameters.AddWithValue("@Size", size);
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ProductIDTxt.Text, out int productId))
            {
                string updateQuery = "UPDATE tbl_itemmasterdata SET Barcode = @Barcode, ItemName = @ItemName, MotorBrand = @MotorBrand, Brand = @Brand, UnitPrice = @UnitPrice, Quantity = @Quantity, Category = @Category, Size = @Size, ProductID = @ProductID WHERE ProductID = @ProductID";
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@ItemName", ItemNameTxt.Text);
                        cmd.Parameters.AddWithValue("@MotorBrand", MotorBrandTxt.Text);
                        cmd.Parameters.AddWithValue("@Brand", BrandTxt.Text);

                        if (decimal.TryParse(UnitPriceTxt.Text, out decimal unitPrice))
                        {
                            cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                        }
                        else
                        {
           
                        }

                        if (int.TryParse(QuantityTxt.Text, out int quantity))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                        }
                        else
                        {
                
                        }

                        cmd.Parameters.AddWithValue("@Category", CategoryTxt.Text);
                        cmd.Parameters.AddWithValue("@Size", SizeTxt.Text);
                        try
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
               
                            MessageBox.Show("Error: " + ex.Message);
                        }
                        finally
                        {
                            con.Close();
                        }
                    }
                }
            }
            else
            {
                
                MessageBox.Show("Please enter a valid Product ID.");
            }
            UpdateDataGridView();
            ClearAllTextBoxes();
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
        }

     

        private void dataGridItemMasterdata_SelectionChanged(object sender, EventArgs e)
        {
         
        }

        private void dataGridItemMasterdata_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridItemMasterdata.Rows[e.RowIndex];
                BarcodeTxt.Text = row.Cells[1].Value.ToString();
                ProductIDTxt.Text = row.Cells[2].Value.ToString();
                ItemNameTxt.Text = row.Cells[3].Value.ToString();
                MotorBrandTxt.Text = row.Cells[4].Value.ToString();
                BrandTxt.Text = row.Cells[5].Value.ToString();
                UnitPriceTxt.Text = row.Cells[6].Value.ToString();
                QuantityTxt.Text = row.Cells[7].Value.ToString();
                CategoryTxt.Text = row.Cells[8].Value.ToString();
                SizeTxt.Text = row.Cells[9].Value.ToString();
                AddBtn.Enabled = false;
            }
        }
        //refresh button
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            string selectQuery = "SELECT * FROM tbl_itemmasterdata";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridItemMasterdata . DataSource = dt;
                }
            }
            AddBtn.Enabled = true;
            ClearAllTextBoxes();
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
        private void GenerateProductID()
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                con.Open();
            {

                Random rand = new Random();
                int randomProductID = rand.Next(1, 600);

                if (randomProductID > 599)
                {
                    MessageBox.Show("ProductID limit exceeded.");
                    return;
                }

                string productID = randomProductID.ToString("D4");
                ProductIDTxt.Text = productID;
            }
        }

        private void RandomGenerateProductidBtn_Click(object sender, EventArgs e)
        {
            GenerateProductID();
        }
        private void PopulateComboBoxStatus()
        {
            // Populate the ComboBox with status options (e.g., Lost Item, Broken Item, Wrong Item).
            comboBox2.Items.AddRange(new string[] { "Lost Item", "Broken Item", "Wrong Item" });
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void UpdateStockStatus(int productId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                con.Open();

                // Assuming the table name is StockStatus and it has a column named Quantity
                string updateStockStatusQuery = "UPDATE StockStatus SET Status = " +
                                                "CASE " +
                                                    "WHEN Quantity = 0 THEN 'Out of stock' " +
                                                    "WHEN Quantity <= 20 THEN 'Low stock' " +
                                                    "ELSE 'High stock' " +
                                                "END " +
                                                "WHERE ProductID = @ProductID";

                using (SqlCommand cmd = new SqlCommand(updateStockStatusQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void ArchiveBtn_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridItemMasterdata.SelectedRows.Count > 0)
            {
                // Get the selected ProductID from the DataGridView
                int productId = Convert.ToInt32(dataGridItemMasterdata.SelectedRows[0].Cells["ProductID"].Value);

                // Call the archive function
                ArchiveProduct(productId);

                // Refresh the DataGridView
                UpdateDataGridView();
            }
            else
            {
                MessageBox.Show("Please select a row to archive.");
            }    // Check if a row is selected
          
        }
        private void ArchiveProduct(int productId)
        {
            // Fetch the product details based on ProductID
            DataRow selectedRow = GetProductDetails(productId);

            if (selectedRow != null)
            {
                // Archive the product by inserting it into the ArchivedItems table
                string archiveQuery = "INSERT INTO ArchivedItems (Barcode, ProductID, ItemName, MotorBrand, Brand, UnitPrice, Quantity, Category, Size) " +
                                      "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category, @Size)";

                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(archiveQuery, con))
                    {
                        // Set parameters from the selected row
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

                // Remove the archived product from the main table
                DeleteProduct(productId);
            }
        }
        private void DeleteProduct(int productId)
        {
            string deleteQuery = "DELETE FROM tbl_itemmasterdata WHERE ProductID = @ProductID";

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
        private DataRow GetProductDetails(int productId)
        {
            DataTable dt = (DataTable)dataGridItemMasterdata.DataSource;
            DataRow[] rows = dt.Select($"ProductID = {productId}");
            return rows.Length > 0 ? rows[0] : null;
        }
    }
}
