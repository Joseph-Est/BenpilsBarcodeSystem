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

namespace BenpilsBarcodeSystem
{
    public partial class LoginForm : Form
    {
        private string usernamePlaceholder = "Username";
        private string passwordPlaceholder = "Password";
        private bool isLoggingIn = false;

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
            if (!isLoggingIn)
            {
                isLoggingIn = true;

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
                                MessageBox.Show("User does not exist.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            case 2:
                                MessageBox.Show("Incorrect password!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            case 3:
                                MessageBox.Show("Connection failed!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            case 4:
                                MessageBox.Show("An error has occured, please try again later!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            case 5:
                                MessageBox.Show("User is not active.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                        }

                        UsernameTxt.Select();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        btnLogin.Text = "LOGIN";
                    }
                }
            }

            isLoggingIn = false;
        }

        private void testPrint_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
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
            Application.Exit();
        }

        private void minimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
