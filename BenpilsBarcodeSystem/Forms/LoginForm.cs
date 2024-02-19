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
    public partial class LoginForm : Form
    {
        private bool isDragging = false;
        private int mouseX, mouseY;
        private string usernamePlaceholder = "Username";
        private string passwordPlaceholder = "Password";

        public LoginForm()
        {
            InitializeComponent();
            UsernameTxt.Text = usernamePlaceholder;
            UsernameTxt.ForeColor = System.Drawing.SystemColors.GrayText;
            PasswordTxt.Text = passwordPlaceholder;
            PasswordTxt.ForeColor = System.Drawing.SystemColors.GrayText;
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

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void Showpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (Showpassword.Checked == true)
            {
                PasswordTxt.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordTxt.UseSystemPasswordChar= true;
            }

        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnlogin;
        }

        private void UsernameTxt_Enter(object sender, EventArgs e)
        {
            if (UsernameTxt.Text == usernamePlaceholder)
            {
                UsernameTxt.Text = "";
                UsernameTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void UsernameTxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTxt.Text))
            {
                UsernameTxt.Text = usernamePlaceholder;
                UsernameTxt.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void PasswordTxt_Enter(object sender, EventArgs e)
        {
            if (PasswordTxt.Text == passwordPlaceholder)
            {
                PasswordTxt.Text = "";
                PasswordTxt.ForeColor = System.Drawing.SystemColors.ControlText;
                PasswordTxt.PasswordChar = '•'; 
            }
        }

        private void PasswordTxt_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordTxt.Text))
            {
                PasswordTxt.Text = passwordPlaceholder;
                PasswordTxt.ForeColor = System.Drawing.SystemColors.GrayText;
                PasswordTxt.PasswordChar = '\0'; 
            }
        }

        private void btnlogin_Click_2(object sender, EventArgs e)
        {
            if (UsernameTxt.Text == "")
            {
                MessageBox.Show("Enter the username.");
            }
            else if (PasswordTxt.Text == "")
            {
                MessageBox.Show("Enter the password.");
            }
            else
            {
                try
                {
                    //SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True");
                    SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("select * from tbl_usercredential where username = @username and password = @password", con);
                    cmd.Parameters.AddWithValue("@username", UsernameTxt.Text);
                    cmd.Parameters.AddWithValue("@password", PasswordTxt.Text);
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


                        MainForm dash = new MainForm(user);
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
    }
}
