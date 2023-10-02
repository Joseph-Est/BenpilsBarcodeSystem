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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BenpilsBarcodeSystem
{
    public partial class UserCredentials : Form
    {
        private bool isDragging = false;
        private int mouseX,mouseY;
        

        public UserCredentials()
        {
            InitializeComponent();
            ComboDesignation.Items.Add("Admin");
            ComboDesignation.Items.Add("SuperAdmin");
            ComboDesignation.Items.Add("Employee");
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales();
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory();
            inv.Show(); 
            inv.StartPosition = FormStartPosition.Manual;
            inv.Location = this.Location;
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Purchasing purchasing = new Purchasing();
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport sreport = new StatisticReport();
            sreport.Show();
            sreport.StartPosition = FormStartPosition.Manual;
            sreport.Location = this.Location;
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = this.Location;
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.Show();
        }

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

        private void button10_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
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

        private void button11_Click(object sender, EventArgs e)
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
        }
   
        private void button10_Click_1(object sender, EventArgs e)
        {
          
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
