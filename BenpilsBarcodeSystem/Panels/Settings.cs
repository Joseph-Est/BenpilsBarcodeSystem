using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            UpdateInventoryDG();
        }

        private void ArchiveTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            switch (tc.SelectedIndex)
            {
                case 0: // Inventory
                    UpdateInventoryDG();
                    break;
                case 1: // Supplier
                    UpdateSupplierDG();
                    break;
                case 2: // Users
                    UpdateUsersDG();
                    break;
                default:
                    break;
            }
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                switch (textBox.Name)
                {
                    case "InventorySearchTxt":
                        UpdateInventoryDG();
                        break;
                    case "SuppliersSearchTxt":
                        UpdateSupplierDG();
                        break;
                    case "UsersSearchTxt":
                        UpdateUsersDG();
                        break;

                }
            }

        }

        private async void UpdateInventoryDG(string category = "All", string brand = "All")
        {
            if (string.IsNullOrEmpty(InventorySearchTxt.Text))
            {
                InventorySearchTxt.Text = "";
            }

            try
            {
                InventoryRepository inventoryRepository = new InventoryRepository();
                DataTable inventoryDT = await inventoryRepository.GetProductsAsync(false, InventorySearchTxt.Text, category, brand);

                InventoryTbl.AutoGenerateColumns = false;
                InventoryTbl.DataSource = inventoryDT;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public async void UpdateSupplierDG()
        {
            if (string.IsNullOrEmpty(SuppliersSearchTxt.Text))
            {
                SuppliersSearchTxt.Text = "";
            }

            try
            {
                SuppliersRepository repository = new SuppliersRepository();
                DataTable suppliersDT = await repository.GetSupplierAsync(false, SuppliersSearchTxt.Text);

                SupplierTbl.AutoGenerateColumns = false;
                SupplierTbl.DataSource = suppliersDT;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public async void UpdateUsersDG()
        {
            if (string.IsNullOrEmpty(UsersSearchTxt.Text))
            {
                UsersSearchTxt.Text = "";
            }

            try
            {
                UserCredentialsRepository repository = new UserCredentialsRepository();
                DataTable suppliersDT = await repository.GetUserCredentialsAsync(false, UsersSearchTxt.Text);

                UserTbl.AutoGenerateColumns = false;
                UserTbl.DataSource = suppliersDT;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private async void DataGridViewButtonCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView senderGrid)
            {
                if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    if (senderGrid.Columns[e.ColumnIndex].Name.Contains("restore"))
                    {
                        int selectedID;
                        Confirmation confirmation;
                        DialogResult dialogResult;

                        switch (senderGrid.Name)
                        {
                            case "InventoryTbl":
                                selectedID = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["id"].Value);
                                string itemName = senderGrid.Rows[e.RowIndex].Cells["item_name"].Value.ToString();

                                confirmation = new Confirmation("Are you sure you want to restore", itemName + "?", "Yes", "Cancel");
                                dialogResult = confirmation.ShowDialog();

                                if (dialogResult == DialogResult.Yes)
                                {
                                    InventoryRepository repository = new InventoryRepository();

                                    if (selectedID > 0)
                                    {
                                        if (await repository.ArchiveProductAsync(selectedID, true))
                                        {
                                            UpdateInventoryDG();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Failed to restore item");
                                        }

                                    }
                                }
                                break;
                            case "SupplierTbl":
                                selectedID = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["supplier_id"].Value);
                                string contactName = senderGrid.Rows[e.RowIndex].Cells["contact_name"].Value.ToString();

                                confirmation = new Confirmation("Are you sure you want to restore", contactName + "?", "Yes", "Cancel");
                                dialogResult = confirmation.ShowDialog();

                                if (dialogResult == DialogResult.Yes)
                                {
                                    SuppliersRepository repository = new SuppliersRepository();

                                    if (selectedID > 0)
                                    {
                                        if (await repository.ArchiveSupplierAsync(selectedID, true))
                                        {
                                            UpdateSupplierDG();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Failed to restore supplier");
                                        }

                                    }
                                }
                                break;
                            case "UserTbl":
                                selectedID = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["user_id"].Value);
                                string username = senderGrid.Rows[e.RowIndex].Cells["username"].Value.ToString();

                                confirmation = new Confirmation("Are you sure you want to restore", username + "?", "Yes", "Cancel");
                                dialogResult = confirmation.ShowDialog();

                                if (dialogResult == DialogResult.Yes)
                                {
                                    UserCredentialsRepository repository = new UserCredentialsRepository();

                                    if (selectedID > 0)
                                    {
                                        if(await repository.ArchiveUserAsync(selectedID, true))
                                        {
                                            UpdateUsersDG();
                                        }
                                        else
                                        {
                                            MessageBox.Show("Failed to restore user");
                                        }
                                        
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
