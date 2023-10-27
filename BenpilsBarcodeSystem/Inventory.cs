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

namespace BenpilsBarcodeSystem
{
    public partial class Inventory : Form
    {
        private User user;
       
        public Inventory(User user)
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
            PointOfSales pos = new PointOfSales(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }
        //Purchasing Button
        private void button5_Click(object sender, EventArgs e)
        {
            Purchasing purchase = new Purchasing(user);
            purchase.Show();
            purchase.StartPosition = FormStartPosition.Manual;
            purchase.Location = this.Location;
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

        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BarcodeTxt.Text)||
                string.IsNullOrEmpty(ItemNameTxt.Text)||
                string.IsNullOrEmpty(ProductIDTxt.Text)||
                string.IsNullOrEmpty(UnitPriceTxt.Text)||
                string.IsNullOrEmpty(QuantityTxt.Text)||
                string.IsNullOrEmpty(CategoryTxt.Text)
                )
            { 
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

            string insertQuery = "INSERT INTO tbl_itemmasterdata (Barcode, ProductID, ItemName, MotorBrand, Brand, UnitPrice, Quantity, Category) " +
                                 "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category)";

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

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

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
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           /*
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                BarcodeTxt.Text = row.Cells["Barcode"].Value.ToString();
                ProductIDTxt.Text = row.Cells["ProductID"].Value.ToString();
                ItemNameTxt.Text = row.Cells["ItemName"].Value.ToString();
                MotorBrandTxt.Text = row.Cells["MotorBrand"].Value.ToString();
                BrandTxt.Text = row.Cells["Brand"].Value.ToString();
                UnitPriceTxt.Text = row.Cells["UnitPrice"].Value.ToString();
                QuantityTxt.Text = row.Cells["Quantity"].Value.ToString();
                CategoryTxt.Text = row.Cells["Category"].Value.ToString();
            }
            */
        }

     
    }
}
