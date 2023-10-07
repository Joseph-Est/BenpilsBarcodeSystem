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
    public partial class Inventory : Form
    {
        private bool IsDragging = false;
        private int mouseX,mouseY;
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
            CmbMotorBrand.Items.Add("Rusi");
            CmbMotorBrand.Items.Add("Kawasaki");
            CmbMotorBrand.Items.Add("Suzuki");
            CmbMotorBrand.Items.Add("Honda");
            CmbMotorBrand.Items.Add("Yamaha");
    
            if (user.Designation == "Employee")
            {
                button8.Enabled = false;
                button6.Enabled = false;
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

        private void button2_Click(object sender, EventArgs e)
        {
         
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
            UserCredentials Uc = new UserCredentials(user);
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
            this.WindowState = FormWindowState.Minimized;
        }
        //Close Button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
        }
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {

        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {

        }

        private void AddBtn_Click_1(object sender, EventArgs e)
        {
            txtBarcode.ReadOnly = true;
            string insertQuery = "INSERT INTO tbl_inventory (barcode, [itemname], motorbrand, [brand], pricecode, unitprice, [quantity] , size, [category]) " +
                                "VALUES (@Barcode, @ItemName, @MotorBrand, @Brand, @PriceCode, @UnitPrice, @Quantity ,@Size,@Category)";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@ItemName", TxtItemName.Text);
                    cmd.Parameters.AddWithValue("@MotorBrand", CmbMotorBrand.Text);
                    cmd.Parameters.AddWithValue("@Brand", TxtBrand.Text);
                    cmd.Parameters.AddWithValue("@PriceCode", TxtPriceCode.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", TxtUnityPrice.Text);
                    cmd.Parameters.AddWithValue("@Quantity", TxtQuantity.Text);
                    cmd.Parameters.AddWithValue("@Size", TxtSize.Text);
                    cmd.Parameters.AddWithValue("@Category", txtCategory.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
            ClearAllTextBoxes();
        }


        public void ClearAllTextBoxes()
        {
            txtBarcode.Text =      "";
            TxtItemName.Text =     "";
            CmbMotorBrand.Text =   "";
            TxtBrand.Text =        "";
            TxtPriceCode.Text =    "";
            TxtUnityPrice.Text =   "";
            TxtQuantity.Text =     "";
            TxtSize.Text =         "";
            txtCategory.Text =     "";
        }

        private void UpdateDataGridView()
        {
            string selectQuery = "SELECT * FROM tbl_inventory";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void UpdateBtn_Click_1(object sender, EventArgs e)
        {
            
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }


            int selectedRowID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

            string updateQuery = "UPDATE tbl_inventory SET barcode = @Barcode, [itemname] = @ItemName, motorbrand = @MotorBrand, [brand] = @Brand," +
                              "pricecode = @PriceCode ,[unitprice] = @UnitPrice , size = @Size ,[category] = @Category  WHERE ID = @ID";




            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ID", selectedRowID);
                    cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text);
                    cmd.Parameters.AddWithValue("@ItemName", TxtItemName.Text);
                    cmd.Parameters.AddWithValue("@MotorBrand", CmbMotorBrand.Text);
                    cmd.Parameters.AddWithValue("@Brand", TxtBrand.Text);
                    cmd.Parameters.AddWithValue("@PriceCode", TxtBrand.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", TxtPriceCode.Text);
                    cmd.Parameters.AddWithValue("@Quantity", TxtUnityPrice.Text);
                    cmd.Parameters.AddWithValue("@Size", TxtQuantity.Text);
                    cmd.Parameters.AddWithValue("@Category", TxtSize.Text);
                  
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
            ClearAllTextBoxes();
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillBarcodeDatabaseInventory.tbl_inventory' table. You can move, or remove it, as needed.
            this.tbl_inventoryTableAdapter.Fill(this.benpillBarcodeDatabaseInventory.tbl_inventory);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
        }
    }
}
