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

        private void Form1_Load(object sender, EventArgs e)
        {
            UsernameTxt.Select();
            this.AcceptButton = btnLogin;
        }

        private async void btnlogin_Click(object sender, EventArgs e)
        {
            if (UsernameTxt.Text == "")
            {
                MessageBox.Show("Enter your username!");
            }
            else if (PasswordTxt.Text == "")
            {
                MessageBox.Show("Enter your password!");
            }
            else
            {
                btnLogin.Text = "Logging in....";
                btnLogin.Enabled = false;

                try
                {
                    DatabaseHelper dbHelper = new DatabaseHelper();

                    DataTable dt = await dbHelper.GetUserCredentials(UsernameTxt.Text, PasswordTxt.Text);

                    if (dt != null && dt.Rows.Count > 0)
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
                        MessageBox.Show("Invalid username or password!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    btnLogin.Text = "Login";
                    btnLogin.Enabled = true;
                }
            }
        }

        private bool isDragging = false;
        private int mouseX, mouseY;

        private void panelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void panelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void panelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
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

        private bool passwordModified = false;

        private void Showpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (passwordModified == true)
            {
                if (ShowPassword.Checked == true)
                {
                    PasswordTxt.UseSystemPasswordChar = false;
                }
                else
                {
                    PasswordTxt.UseSystemPasswordChar = true;
                }
            }
        }

        private void PasswordTxt_Enter(object sender, EventArgs e)
        {
            if (!passwordModified)
            {
                if (!ShowPassword.Checked)
                {
                    PasswordTxt.UseSystemPasswordChar = true;
                }

                PasswordTxt.Text = "";
            }
        }

        private void PasswordTxt_Leave(object sender, EventArgs e)
        {
            if (!passwordModified)
            {
                PasswordTxt.ForeColor = System.Drawing.SystemColors.GrayText;
                PasswordTxt.Text = passwordPlaceholder;
                PasswordTxt.UseSystemPasswordChar = false;
            }
        }

        private void PasswordTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PasswordTxt.Text))
            {
                PasswordTxt.ForeColor = System.Drawing.SystemColors.ControlText;
                passwordModified = true;
            }
        }

        private void PasswordTxt_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(PasswordTxt.Text))
            {
                passwordModified = false;
            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
