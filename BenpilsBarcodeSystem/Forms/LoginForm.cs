using BenpilsBarcodeSystem.Repository;
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
using BenpilsBarcodeSystem.Utils;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using BenpilsBarcodeSystem.Database;

namespace BenpilsBarcodeSystem
{
    public partial class LoginForm : Form
    {
        private readonly string usernamePlaceholder = "Username";
        private readonly string passwordPlaceholder = "Password";
        private bool isLoggingIn = false;

        public LoginForm()
        {
            InitializeComponent();
            UsernameTxt.Text = usernamePlaceholder;
            UsernameTxt.ForeColor = System.Drawing.SystemColors.GrayText;
            PasswordTxt.Text = passwordPlaceholder;
            PasswordTxt.ForeColor = System.Drawing.SystemColors.GrayText;
            passwordModified = false;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            DatabaseInitializer dbInitializer = new DatabaseInitializer();

            if(await dbInitializer.InitializeDatabaseAsync())
            {
                btnLogin.Enabled = true;
            }
            else
            {
                MessageBox.Show("The application was unable to initialize the database and will now exit. Please check your database configuration and try again.", "Database Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            UsernameTxt.Select();
            this.AcceptButton = btnLogin;
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            if (!isLoggingIn)
            {
                isLoggingIn = true;

                if (UsernameTxt.Text == "")
                {
                    MessageBox.Show("Please enter your username to proceed.", "Username Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (PasswordTxt.Text == "")
                {
                    MessageBox.Show("Please enter your password to proceed.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    btnLogin.Text = "LOGGING IN....";

                    try
                    {
                        UserCredentialsRepository repository = new UserCredentialsRepository();

                        switch(await repository.LoginAsync(UsernameTxt.Text, PasswordTxt.Text))
                        {
                            case 0:
                                MainForm dash = new MainForm();
                                dash.Show();
                                dash.StartPosition = FormStartPosition.WindowsDefaultLocation;
                                    this.Hide();
                                break;
                            case 1:
                                MessageBox.Show("The username you entered does not match any account. Please check your username and try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                UsernameTxt.Select();
                                break;
                            case 2:
                                MessageBox.Show("The password you entered is incorrect. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                PasswordTxt.Select();
                                break;
                            case 3:
                                MessageBox.Show("Unable to connect to the server. Please try again later.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            case 4:
                                MessageBox.Show("An unexpected error occurred. Please try again later.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            case 5:
                                MessageBox.Show("This account is currently inactive. If you believe this is an error, please contact administrator.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                UsernameTxt.Select();
                                break;
                        }

                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    }
                    finally
                    {
                        btnLogin.Text = "LOGIN";
                    }
                }
            }

            isLoggingIn = false;
        }

        private bool isDragging = false;
        private int mouseX, mouseY;

        private void PanelHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void PanelHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void PanelHeader_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
        
        private void UsernameTxt_Enter(object sender, EventArgs e)
        {
            UsernameBorder.BackColor = Color.FromArgb(193, 57, 57);
            if (UsernameTxt.Text == usernamePlaceholder)
            {
                UsernameTxt.Text = "";
                UsernameTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            }
        }

        private void UsernameTxt_Leave(object sender, EventArgs e)
        {
            UsernameBorder.BackColor = Color.FromArgb(40, 40, 40);
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
            PasswordBorder.BackColor = Color.FromArgb(193, 57, 57);
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
            PasswordBorder.BackColor = Color.FromArgb(40, 40, 40);
            if (!passwordModified)
            {
                PasswordTxt.Text = passwordPlaceholder;
                PasswordTxt.UseSystemPasswordChar = false;
                PasswordTxt.ForeColor = System.Drawing.SystemColors.GrayText;
                passwordModified = false;
            }
        }

        private void PasswordTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(PasswordTxt.Text))
            //{
            //    PasswordTxt.ForeColor = System.Drawing.SystemColors.ControlText;
            //    //MessageBox.Show("modified1");
            //    passwordModified = true;
            //}
        }

        private void PasswordTxt_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(PasswordTxt.Text.Trim()))
            {
                //MessageBox.Show("modified2:" + PasswordTxt.Text.Trim());
                PasswordTxt.ForeColor = System.Drawing.SystemColors.ControlText;
                passwordModified = true;
            }
            else
            {
                //MessageBox.Show("not modified");
                passwordModified = false;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UsernamePanel_Click(object sender, EventArgs e)
        {
            UsernameTxt.Select();
        }

        private void PasswordPanel_Click(object sender, EventArgs e)
        {
            PasswordTxt.Select();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
