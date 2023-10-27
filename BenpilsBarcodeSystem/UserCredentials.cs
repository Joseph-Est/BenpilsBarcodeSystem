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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BenpilsBarcodeSystem
{
    public partial class Ser : Form
    {
        private User user;
        private string searchValue = "";

        public Ser(User user)
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start(); 
            this.user = user;
            ComboDesignation.Items.Clear();
            ComboDesignation.Items.Add("SuperAdmin");
            ComboDesignation.Items.Add("Admin");
            ComboDesignation.Items.Add("Inventory Manager");
            ComboDesignation.Items.Add("Cashier");
            label1.Text = "Username: " + user.Username;
            label2.Text = "Designation: " + user.Designation;
            if (user.Designation == "Superadmin")
            {

            }
            else if (user.Designation == "Admin")
            {
                ComboDesignation.Items.Remove("SuperAdmin");


            }
            else if (user.Designation == "Inventory Manager")
            {
                PointOfSalesBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticsBtn.Enabled = false;
                button8.Enabled = false;
                ServicesBtn.Enabled = false;
                SettingsBtn.Enabled = false;


            }
            else if (user.Designation == "Cashier")
            {
                InventoryBtn.Enabled = false;
                PurchasingBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticsBtn.Enabled = false;
                button8.Enabled = false;
                SettingsBtn.Enabled = false;
    
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
        }
        //Minimize button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void UserCredentials_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleUsercredentialmain.tbl_usercredential' table. You can move, or remove it, as needed.
            this.tbl_usercredentialTableAdapter2.Fill(this.benpillMotorcycleUsercredentialmain.tbl_usercredential);
            // TODO: This line of code loads data into the 'benpillMotorcycleDatabaseDataSet1.tbl_usercredential' table. You can move, or remove it, as needed.
            this.tbl_usercredentialTableAdapter1.Fill(this.benpillMotorcycleDatabaseDataSet1.tbl_usercredential);
            // TODO: This line of code loads data into the 'benpillMotorcycleDatabaseDataSet.tbl_usercredential' table. You can move, or remove it, as needed.
            this.tbl_usercredentialTableAdapter.Fill(this.benpillMotorcycleDatabaseDataSet.tbl_usercredential);
            // TODO: This line of code loads data into the 'userCredentialsDataSet1.tbl_login' table. You can move, or remove it, as needed.
    

        
          

        }
        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtFirstName.Text) ||
                string.IsNullOrWhiteSpace(TxtLastName.Text) ||
                string.IsNullOrWhiteSpace(TxtUsername.Text) ||
                string.IsNullOrWhiteSpace(TxtPassword.Text) ||
                string.IsNullOrWhiteSpace(TxtAddress.Text) ||
                string.IsNullOrWhiteSpace(TxtContactNo.Text) ||
                string.IsNullOrWhiteSpace(ComboDesignation.Text))
            {
                MessageBox.Show("Please fill up all the textboxes below.");
                return;
            }
            if (IsUsernameAlreadyExists(TxtUsername.Text))
            {
                MessageBox.Show("Username already exists. Please choose a different username.");
                return;
            }
            string insertQuery = "INSERT INTO tbl_usercredential (firstname, [lastname], username, [password], designation, address, [contactno]) " +
                                 "VALUES (@FirstName, @LastName, @UserName, @Password, @Designation, @Address, @ContactNo)";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@FirstName", TxtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", TxtLastName.Text);
                    cmd.Parameters.AddWithValue("@UserName", TxtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", TxtPassword.Text);
                    cmd.Parameters.AddWithValue("@Address", TxtAddress.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", TxtContactNo.Text);
                    cmd.Parameters.AddWithValue("@Designation", ComboDesignation.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
            ClearAllTextBoxes();
        }
        private bool IsUsernameAlreadyExists(string username)
        {
            string query = "SELECT COUNT(*) FROM tbl_usercredential WHERE username = @Username";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Username", username);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }
        private void ClearAllTextBoxes()
        {
            TxtFirstName.Text = "";
            TxtLastName.Text = "";
            TxtUsername.Text = "";
            TxtPassword.Text = "";
            TxtAddress.Text = "";
            TxtContactNo.Text = "";
            ComboDesignation.Text = "";
        }
        private void UpdateBtn_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }


            int selectedRowID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

            string updateQuery = "UPDATE tbl_usercredential SET firstname = @FirstName, [lastname] = @LastName, username = @UserName, [password] = @Password, " +
                                 "designation = @Designation, address = @Address, [contactno] = @ContactNo WHERE ID = @ID";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ID", selectedRowID);
                    cmd.Parameters.AddWithValue("@FirstName", TxtFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", TxtLastName.Text);
                    cmd.Parameters.AddWithValue("@UserName", TxtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", TxtPassword.Text);
                    cmd.Parameters.AddWithValue("@Address", TxtAddress.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", TxtContactNo.Text);
                    cmd.Parameters.AddWithValue("@Designation", ComboDesignation.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
            ClearAllTextBoxes();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            int selectedRowID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

            string deleteQuery = "DELETE FROM tbl_usercredential WHERE ID = @ID";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ID", selectedRowID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            string selectQuery = "SELECT * FROM tbl_usercredential";
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

           
                TxtFirstName.Text = selectedRow.Cells["firstname"].Value.ToString();
                TxtLastName.Text = selectedRow.Cells["lastname"].Value.ToString();
                TxtUsername.Text = selectedRow.Cells["username"].Value.ToString();
                TxtPassword.Text = selectedRow.Cells["password"].Value.ToString();
                TxtAddress.Text = selectedRow.Cells["address"].Value.ToString();
                TxtContactNo.Text = selectedRow.Cells["contactno"].Value.ToString();
                ComboDesignation.Text = selectedRow.Cells["designation"].Value.ToString();
                AddBtn.Enabled = false;
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.StartPosition = FormStartPosition.CenterScreen;
            CE.ShowDialog();
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

        private void PointOfSalesBtn_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
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

        private void PurchasingBtn_Click(object sender, EventArgs e)
        {
            Purchasing pur = new Purchasing(user);
            pur.Show();
            pur.StartPosition = FormStartPosition.Manual;
            pur.Location = this.Location;
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
            StatisticReport SR = new StatisticReport(user);
            SR.Show();
            SR.StartPosition = FormStartPosition.Manual;
            SR.Location = this.Location;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            UpdateDataGridView();
            dataGridView1.ClearSelection();
            AddBtn.Enabled = true;
            ClearAllTextBoxes();
        }

        private void ServicesBtn_Click(object sender, EventArgs e)
        {
            Services service = new Services(user);
            service.Show();
            service.StartPosition = FormStartPosition.Manual;
            service.Location = this.Location;
            this.Hide();
        }

        private void TxtSearchBar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("Cannot input symbols");
            }
        }

        private void TxtSearchBar_TextChanged(object sender, EventArgs e)
        {

            searchValue = TxtSearchBar.Text; 
            FilterDataGridView(searchValue);
        }
        private void FilterDataGridView(string searchValue)
        {
            string filterQuery = "SELECT * FROM tbl_usercredential WHERE FirstName LIKE @Search OR LastName LIKE @Search OR UserName LIKE @Search OR Designation LIKE @Search";
            DataTable filteredTable = new DataTable();
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(filterQuery, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Search", "%" + searchValue + "%"); 

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(filteredTable); 
                    }
                }
            }
            dataGridView1.DataSource = filteredTable;
        }

        
    }
}
