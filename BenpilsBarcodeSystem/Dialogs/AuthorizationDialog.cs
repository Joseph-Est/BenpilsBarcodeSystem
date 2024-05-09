using BenpilsBarcodeSystem.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Dialogs
{
    public partial class AuthorizationDialog : Form
    {
        private bool canClose = false;
        public AuthorizationDialog()
        {
            InitializeComponent();
        }

        private async void AcceptBtn_Click(object sender, EventArgs e)
        {
            if (UsernameTxt.Text == "")
            {
                MessageBox.Show("Please enter username to proceed.", "Username Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (PasswordTxt.Text == "")
            {
                MessageBox.Show("Please enter password to proceed.", "Password Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                UserCredentialsRepository repository = new UserCredentialsRepository();
                switch (await repository.AuthorizeAsync(UsernameTxt.Text, PasswordTxt.Text))
                {
                    case 0:
                        canClose = true;
                        DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case 1:
                        MessageBox.Show("The username you entered does not match any account. Please check your username and try again.", "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UsernameTxt.Select();
                        break;
                    case 2:
                        MessageBox.Show("This account is currently inactive.", "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UsernameTxt.Select();
                        break;
                    case 3:
                        MessageBox.Show("Invalid account.", "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        UsernameTxt.Select();
                        break;
                    case 4:
                        MessageBox.Show("The password you entered is incorrect. Please try again.", "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        PasswordTxt.Select();
                        break;
                    case 5:
                        MessageBox.Show("Unable to connect to the server. Please try again later.", "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case 6:
                        MessageBox.Show("An unexpected error occurred. Please try again later.", "Authorization Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }  
        }

        private void AuthorizationDialog_Load(object sender, EventArgs e)
        {

        }

        private void AuthorizationDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
