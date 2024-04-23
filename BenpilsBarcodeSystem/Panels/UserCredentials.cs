using BenpilsBarcodeSystem.Dialogs;
using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
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
        private bool isAdding = false;
        private bool isUpdating = false;
        private string prevUsername;
        private int selectedID = 0;
        MainForm mainForm;
        public UserCredentials()
        {
            InitializeComponent();
            InputValidator.AllowOnlyPhoneNumber(ContactNoTxt);
        }

        private void UserCredentials_Load(object sender, EventArgs e)
        {
            mainForm = (MainForm)this.ParentForm;
            UpdateDataGridView();
        }

        public void UpdateTable()
        {
            UpdateDataGridView();
        }

        public async void UpdateDataGridView(string searchText = null)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                SearchTxt.Text = "";
            }

            try
            {
                UserCredentialsRepository userCredentialsRepository = new UserCredentialsRepository();
                DataTable userCredentials = await userCredentialsRepository.GetUserCredentialsAsync(true, searchText);

                UserTbl.AutoGenerateColumns = false;
                UserTbl.DataSource = userCredentials;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void UserTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !isAdding && !isUpdating)
            {
                DataGridViewRow selectedRow = UserTbl.Rows[e.RowIndex];
                selectedID = InputValidator.ParseToInt(selectedRow.Cells["id"].Value.ToString());
                FirstNameTxt.Text = selectedRow.Cells["first_name"].Value.ToString();
                LastNameTxt.Text = selectedRow.Cells["last_name"].Value.ToString();
                UsernameTxt.Text = selectedRow.Cells["username"].Value.ToString();
                PasswordTxt.Text = selectedRow.Cells["password"].Value.ToString();
                AddressTxt.Text = selectedRow.Cells["address"].Value.ToString();
                ContactNoTxt.Text = selectedRow.Cells["contact_no"].Value.ToString();
                SetUpDesignation(selectedRow.Cells["designation"].Value.ToString());
            }
        }

        private async void UserTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0 && !isAdding && !isUpdating)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name == "update")
                {
                    string userDesignation = senderGrid.Rows[e.RowIndex].Cells["designation"].Value.ToString();

                    if (CurrentUser.User.Designation == "Admin" && userDesignation == "Super Admin")
                    {
                        MessageBox.Show("You do not have the necessary privileges to update this user.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int id = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value);

                    if ((CurrentUser.User.Designation == "Admin" && userDesignation == "Admin") && CurrentUser.User.ID != id)
                    {
                        MessageBox.Show("You do not have the necessary privileges to update this user.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    isUpdating = true;
                    SetFieldsReadOnly(false);
                    prevUsername = UsernameTxt.Text;
                    SetUpDesignation(userDesignation);
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "archive")
                {
                    int id = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value);
                    string userDesignation = senderGrid.Rows[e.RowIndex].Cells["designation"].Value.ToString();

                    if (CurrentUser.User.ID != id)
                    {
                        if(CurrentUser.User.Designation == "Admin" && userDesignation == "Super Admin")
                        {
                            MessageBox.Show("You do not have the necessary privileges to archive this user.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        Confirmation confirmation = new Confirmation("Are you sure you want to archive", UsernameTxt.Text + "?", "Yes", "Cancel");
                        DialogResult result = confirmation.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            UserCredentialsRepository repository = new UserCredentialsRepository();

                            if (selectedID > 0)
                            {
                                await repository.ArchiveUserAsync(selectedID);
                                UpdateDataGridView();
                                ClearFields();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot archive the currently logged in user. If you need to do this, please log in as a different user.", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            selectedID = 0;
            ClearFields();
            isAdding = true;
            SetFieldsReadOnly(false);
            SetUpDesignation();
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Util.AreTextBoxesNullOrEmpty(FirstNameTxt, LastNameTxt, UsernameTxt, PasswordTxt, AddressTxt, ContactNoTxt) || DesignationCb.SelectedIndex == 0)
            {
                MessageBox.Show("Please ensure all required fields are filled in before proceeding.", "Incomplete Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstName = Util.Capitalize(FirstNameTxt.Text);
            string lastName = Util.Capitalize(LastNameTxt.Text);
            string username = UsernameTxt.Text;
            string password = PasswordTxt.Text;
            string designation = DesignationCb.Text;
            string address = Util.Capitalize(AddressTxt.Text);
            string contactNo = ContactNoTxt.Text;

            UserCredentialsRepository repository = new UserCredentialsRepository();

            if(isAdding || (isUpdating && !(prevUsername.ToLower().Equals(username.ToLower()))))
            {
                if (await repository.IsDataExistsAsync("username", username))
                {
                    MessageBox.Show("The username you've entered is already in use. Please enter a unique username.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (isAdding)
            {
                Confirmation confirmation = new Confirmation("Do you want to save this new user?", null, "Yes", "Cancel");
                DialogResult result = confirmation.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    if (await repository.AddUserAsync(
                        firstName,
                        lastName,
                        username,
                        password,
                        designation,
                        address,
                        contactNo
                    ))
                    {
                        ClearFields();
                        isAdding = false;
                        SetFieldsReadOnly(true);
                        UpdateDataGridView();
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred while attempting to add new user. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                Confirmation confirmation = new Confirmation($"Do you want to save changes to user", $"{prevUsername}?", "Yes", "Cancel");
                DialogResult result = confirmation.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    if (await repository.UpdateUserAsync(
                        selectedID,
                        firstName,
                        lastName,
                        username,
                        password,
                        designation,
                        address,
                        contactNo
                    ))
                    {
                        if (!prevUsername.Equals(username) && (selectedID == CurrentUser.User.ID))
                        {
                            CurrentUser.User.Username = username;
                            mainForm.UpdateUsername();
                        }

                        ClearFields();
                        isUpdating = false;
                        SetFieldsReadOnly(true);
                        UpdateDataGridView();
                    }
                    else
                    {
                        MessageBox.Show($"An error occurred while attempting to update the user. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
            SetFieldsReadOnly(true);
            isAdding = false;
            isUpdating = false;
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView(SearchTxt.Text);
        }

        private void ClearFields()
        {
            Util.ClearTextBoxes(FirstNameTxt, LastNameTxt, UsernameTxt, PasswordTxt, AddressTxt, ContactNoTxt);
            Util.ResetComboBoxes(DesignationCb);
            FirstNameTxt.Text = "";
            LastNameTxt.Text = "";
            UsernameTxt.Text = "";
            PasswordTxt.Text = "";
            AddressTxt.Text = "";
            ContactNoTxt.Text = "";
            DesignationCb.Text = string.Empty;
        }

        private void SetFieldsReadOnly(bool mode)
        {
            Util.SetTextBoxesReadOnly(mode, FirstNameTxt, LastNameTxt, UsernameTxt, PasswordTxt, AddressTxt, ContactNoTxt);

            if(selectedID != CurrentUser.User.ID)
            {
                Util.SetComboBoxesDisabled(mode, DesignationCb);
            }
            
            AddBtn.Enabled = mode;
            SaveBtn.Visible = !mode;
            CancelBtn.Visible = !mode;
            FirstNameTxt.Select();
            mainForm.CanSwitchPanel = mode;
            ShowPasswordEye(!mode);
        }

        private void SetUpDesignation(string selectedRow = null)
        {
            var designations = new List<string> { "-- Select --", "Super Admin", "Admin" };

            if (!isUpdating || selectedRow == null || (!selectedRow.Equals("Admin") && !selectedRow.Equals("Super Admin")))
            {
                designations.AddRange(new[] { "Inventory Manager", "Cashier" });
            }

            DesignationCb.Items.Clear();
            DesignationCb.Items.AddRange(designations.ToArray());

            if ((isAdding || isUpdating) && CurrentUser.User.Designation != "Super Admin")
            {
                DesignationCb.Items.Remove("Super Admin");
            }

            DesignationCb.SelectedItem = selectedRow ?? "-- Select --";
        }

        private void SaveBtn_VisibleChanged(object sender, EventArgs e)
        {
            if (SaveBtn.Visible)
            {
                this.AcceptButton = SaveBtn;
                this.CancelButton = CancelBtn;
            }
            else
            {
                this.AcceptButton = AddBtn;
            }
        }

        private void ShowPasswordCb_Click(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.CheckState == CheckState.Checked)
            {
                checkBox.Image = Properties.Resources.icons8_show_password_15;
                PasswordTxt.PasswordChar = '\0';
            }
            else
            {
                checkBox.Image = Properties.Resources.icons8_hide_15;
                PasswordTxt.PasswordChar = '•';
            }
        }

        private void ShowPasswordEye(bool show)
        {
            ShowPasswordCb.Visible = show;
            ShowPasswordCb.Checked = false;
            ShowPasswordCb.Image = Properties.Resources.icons8_hide_15;
            PasswordTxt.PasswordChar = '•';

        }
    }
}
