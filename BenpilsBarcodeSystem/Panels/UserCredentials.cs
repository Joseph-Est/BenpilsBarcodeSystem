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
        private string searchValue = "";

        public Ser()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
            ComboDesignation.Items.Clear();
            ComboDesignation.Items.Add("SuperAdmin");
            ComboDesignation.Items.Add("Admin");
            ComboDesignation.Items.Add("Inventory Manager");
            ComboDesignation.Items.Add("Cashier");
        }



        private void UserCredentials_Load(object sender, EventArgs e)
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
            ComboDesignation.Text = string.Empty;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];


                    TxtFirstName.Text = selectedRow.Cells[1].Value.ToString();
                    TxtLastName.Text = selectedRow.Cells[2].Value.ToString();
                    TxtUsername.Text = selectedRow.Cells[3].Value.ToString();
                    TxtPassword.Text = selectedRow.Cells[4].Value.ToString();
                    TxtAddress.Text = selectedRow.Cells[6].Value.ToString();
                    TxtContactNo.Text = selectedRow.Cells[7].Value.ToString();
                    ComboDesignation.Text = selectedRow.Cells[5].Value.ToString();
                    AddBtn.Enabled = false;
                }
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            UpdateDataGridView();
            dataGridView1.ClearSelection();
            AddBtn.Enabled = true;
            ClearAllTextBoxes();
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

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
        }

        private void ArchiveBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
