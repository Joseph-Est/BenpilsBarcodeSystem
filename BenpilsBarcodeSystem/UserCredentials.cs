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
    public partial class UserCredentials : Form
    {
        private bool isDragging = false;
        private int mouseX,mouseY;
        private User user;

        public UserCredentials(User user)
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();
            ComboDesignation.Items.Add("Admin");
            ComboDesignation.Items.Add("SuperAdmin");
            ComboDesignation.Items.Add("Employee");
            this.user = user;
            label1.Text = "Username: " + user.Username;
            label2.Text = "Designation: " + user.Designation;
            if (user.Designation == "Employee")
            {
                button6.Enabled = false;
                button8.Enabled = false;
                dataGridView1.Columns["FirstName"].ReadOnly = true;
                dataGridView1.Columns["LastName"].ReadOnly = true;
                dataGridView1.Columns["UserName"].ReadOnly = true;
                dataGridView1.Columns["Password"].ReadOnly = true;
                dataGridView1.Columns["Designation"].ReadOnly = true;
                dataGridView1.Columns["Address"].ReadOnly = true;
                dataGridView1.Columns["ContactNo"].ReadOnly = true;
            }
            else if (user.Designation == "Admin")
            {
                dataGridView1.Columns["FirstName"].ReadOnly = true;
                dataGridView1.Columns["LastName"].ReadOnly = true;
                dataGridView1.Columns["UserName"].ReadOnly = true;
                dataGridView1.Columns["Password"].ReadOnly = true;
                dataGridView1.Columns["Designation"].ReadOnly = true;
                dataGridView1.Columns["Address"].ReadOnly = true;
                dataGridView1.Columns["ContactNo"].ReadOnly = true;
            }
            else if (user.Designation == "SuperAdmin")
            {
                dataGridView1.Columns["FirstName"].ReadOnly = true;
                dataGridView1.Columns["LastName"].ReadOnly = true;
                dataGridView1.Columns["UserName"].ReadOnly = true;
                dataGridView1.Columns["Password"].ReadOnly = true;
                dataGridView1.Columns["Designation"].ReadOnly = true;
                dataGridView1.Columns["Address"].ReadOnly = true;
                dataGridView1.Columns["ContactNo"].ReadOnly = true;
            }
           
        }

        //DashBoard Button
        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }
        //Point of sales button
        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
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
            Purchasing purchasing = new Purchasing(user);
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
            StatisticReport sreport = new StatisticReport(user);
            sreport.Show();
            sreport.StartPosition = FormStartPosition.Manual;
            sreport.Location = this.Location;
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
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
        //Close Button
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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void UserCredentials_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleDatabaseDataSet1.tbl_usercredential' table. You can move, or remove it, as needed.
            this.tbl_usercredentialTableAdapter1.Fill(this.benpillMotorcycleDatabaseDataSet1.tbl_usercredential);
            // TODO: This line of code loads data into the 'benpillMotorcycleDatabaseDataSet.tbl_usercredential' table. You can move, or remove it, as needed.
            this.tbl_usercredentialTableAdapter.Fill(this.benpillMotorcycleDatabaseDataSet.tbl_usercredential);
            // TODO: This line of code loads data into the 'userCredentialsDataSet1.tbl_login' table. You can move, or remove it, as needed.
            this.tbl_loginTableAdapter1.Fill(this.userCredentialsDataSet1.tbl_login);

            this.tbl_loginTableAdapter.Fill(this.userCredentialsDataSet.tbl_login);

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }



        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
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

        private void ComboDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TxtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void TxtSearchBar_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
