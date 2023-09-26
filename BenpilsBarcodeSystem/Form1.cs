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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter the username:");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Enter the password");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU\\MSSQLSERVER2022;Initial Catalog=UserLogin;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("select * from tbl_login where username = @username and password = @password", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string username = dt.Rows[0]["username"].ToString();

                        MessageBox.Show("login succesfull");
                        Dashboard dash = new Dashboard();
                        dash.Show();
                        this.Hide();

                    }
                    else
                    {
                        MessageBox.Show("username and password is invalid");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }


            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimiz
        }
    }
}
