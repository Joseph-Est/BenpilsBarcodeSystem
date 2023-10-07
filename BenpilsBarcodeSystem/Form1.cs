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
        private bool isDragging = false;
        private int mouseX, mouseY;
        public Form1()
        {
            InitializeComponent();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
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
        private void Showpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (Showpassword.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar= true;
            }

        }
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter the username.");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Enter the password.");
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("select * from tbl_usercredential where username = @username and password = @password", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string username = dt.Rows[0]["username"].ToString();
                        string designation = dt.Rows[0]["designation"].ToString();

                        MessageBox.Show("Login successful");


                        User user = new User
                        {
                            Username = username,
                            Designation = designation
                        };

                     
                        Dashboard dash = new Dashboard(user);
                        dash.Show();
                        dash.StartPosition = FormStartPosition.WindowsDefaultBounds;
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username and password!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = LoginBtn;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
