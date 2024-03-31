﻿using BenpilsBarcodeSystem.Dialogs;
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
    public partial class Ser : Form
    {
        private bool isAdding = false;
        private bool isUpdating = false;
        private string prevUsername;
        private int selectedID = 0;

        public Ser()
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(ContactNoTxt);
        }

        private void UserCredentials_Load(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        public void updateTable()
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
                        MessageBox.Show("Action not allowed");
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

                    if (CurrentUser.User.iD != id)
                    {
                        if(CurrentUser.User.Designation == "Admin" && userDesignation == "Super Admin")
                        {
                            MessageBox.Show("Insufficient privileges", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        MessageBox.Show("Unable to archive logged in user", "Action Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
            isAdding = true;
            SetFieldsReadOnly(false);
            SetUpDesignation();
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (Util.AreTextBoxesNullOrEmpty(FirstNameTxt, LastNameTxt, UsernameTxt, PasswordTxt, AddressTxt, ContactNoTxt) || DesignationCb.SelectedIndex == 0)
            {
                MessageBox.Show("Please fill in all the required fields.");
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
                    MessageBox.Show("Username already exist.");
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
                        MessageBox.Show($"Failed to add new user!");
                    }
                }
            }
            else
            {
                Confirmation confirmation = new Confirmation($"Do you want to save changes to user {prevUsername}?", null, "Yes", "Cancel");
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
                        ClearFields();
                        isUpdating = false;
                        SetFieldsReadOnly(true);
                        UpdateDataGridView();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to update user!");
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
            Util.SetComboBoxesDisabled(mode, DesignationCb);

            AddBtn.Visible = mode;
            SaveBtn.Visible = !mode;
            CancelBtn.Visible = !mode;
            FirstNameTxt.Select();
        }

        private void SetUpDesignation(string selectedRow = null)
        {
            DesignationCb.Items.Clear();
            DesignationCb.Items.Add("-- Select --");

            DesignationCb.Items.Add("Super Admin");
            DesignationCb.Items.Add("Admin");
            DesignationCb.Items.Add("Inventory Manager");
            DesignationCb.Items.Add("Cashier");

            if(isAdding || isUpdating)
            {
                if (CurrentUser.User.Designation != "Super Admin")
                {
                    DesignationCb.Items.Remove("Super Admin");
                }
            }

            if (selectedRow == null)
            {
                DesignationCb.SelectedItem = "-- Select --";
            }
            else
            {
                DesignationCb.SelectedItem = selectedRow;
            }
           
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
    }
}
