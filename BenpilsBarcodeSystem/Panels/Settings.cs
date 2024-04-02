using BenpilsBarcodeSystem.Dialogs;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            Util.SetDateTimePickerFormat("MMM dd, yyyy", StartDt, EndDt);
            SetUpCB();
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
                                        if (await repository.ArchiveProductAsync(selectedID, false))
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


        // BACKUP

        private void SwitchCb_CheckedChanged(object sender, EventArgs e)
        {
            if (SwitchCb.Checked)
            {
                SwitchCb.Image = Properties.Resources.icons8_toggle_on_30;
                DisabledLbl.ForeColor = Color.FromArgb(255, 255, 255);
                DisabledLbl.Text = "Enabled";
                AutomaticBackupPanel.Enabled = true;
            }
            else
            {
                SwitchCb.Image = Properties.Resources.icons8_toggle_off_30;
                DisabledLbl.ForeColor = Color.FromArgb(240, 62, 62);
                DisabledLbl.Text = "Disabled";
                AutomaticBackupPanel.Enabled = false;
            }
        }

        private async void ExportBtn_Click(object sender, EventArgs e)
        {
            //LoadingDialog loadingDialog = null;

            //Thread thread = new Thread(() =>
            //{
            //    loadingDialog = new LoadingDialog("Backing up data, please wait.");

            //    loadingDialog.StartPosition = FormStartPosition.Manual;
            //    loadingDialog.Location = new Point(this.Location.X + this.Width / 2 - loadingDialog.Width / 2,
            //                                       this.Location.Y + this.Height / 2 - loadingDialog.Height / 2);

            //    loadingDialog.ShowDialog();
            //});

            Action<string, string, MessageBoxIcon> showMessage = (message, title, MessageBoxIcon) =>
            {
                //if (loadingDialog != null && loadingDialog.InvokeRequired)
                //{
                //    loadingDialog.Invoke(new Action(() => loadingDialog.Close()));
                //}
                MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon);
                this.Focus();
            };

            if (Util.IsAnyCheckboxChecked(InventoryCb, SuppliersCb, SalesTransactionsCb, InventoryReportCb, AuditTrailCb))
            {
                Dictionary<DataTable, string> dataTableSheetMapping = new Dictionary<DataTable, string>();

                DateTime fromDate = StartDt.Value;
                DateTime endDate = EndDt.Value;

                if (InventoryCb.Checked)
                {
                    DataTable dt = await GetInventoryDT(fromDate, endDate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataTableSheetMapping.Add(dt, "Inventory");
                    }
                }

                if (SuppliersCb.Checked)
                {
                    DataTable dt = await GetSuppliersDT(fromDate, endDate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataTableSheetMapping.Add(dt, "Suppliers");
                    }
                }

                if (PurchaseOrdersCb.Checked)
                {
                    //DataTable dt = await getpurc(fromDate, endDate);
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    dataTableSheetMapping.Add(dt, "Suppliers");
                    //}
                }

                if (SalesTransactionsCb.Checked)
                {
                    DataTable dt = await GetSalesTransactionsDT(fromDate, endDate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataTableSheetMapping.Add(dt, "Sales Transactions");
                    }
                }

                if (InventoryReportCb.Checked)
                {
                    DataTable dt = await GetInventoryReportDT(fromDate, endDate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataTableSheetMapping.Add(dt, "Inventory Report");
                    }
                }

                if (AuditTrailCb.Checked)
                {
                    DataTable dt = await GetAuditTrailDT(fromDate, endDate);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dataTableSheetMapping.Add(dt, "Audit Trail");
                    }
                }

                if (dataTableSheetMapping != null && dataTableSheetMapping.Count > 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog
                    {
                        Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                        FileName = "Backup.xlsx",
                        Title = "Save Excel File",
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                    };
                    try
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            //thread.Start();

                            string filePath = Path.GetDirectoryName(saveFileDialog.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                            switch(Util.ExportToExcel(filePath, fileName, dataTableSheetMapping))
                            {
                                case 0:
                                    showMessage("Backup Exported Successfully", "Backup", MessageBoxIcon.Information);
                                    break;
                                case 1:
                                    showMessage("Unable to overwrite existing file. Either it's open or you don't have the necessary permissions.", "File Overwrite Error", MessageBoxIcon.Error);
                                    break;
                                case 2:
                                    showMessage("Unable to export backup", "Export Error", MessageBoxIcon.Error);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    finally
                    {
                        //if (loadingDialog != null && loadingDialog.InvokeRequired)
                        //{
                        //    loadingDialog.Invoke(new Action(() => loadingDialog.Close()));
                        //}
                    }
                }
                else
                {
                    MessageBox.Show("No available data to backup.", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please check a data to export", "Export Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task<DataTable> GetInventoryDT(DateTime dateFom, DateTime dateTo)
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            DataTable dt = await inventoryRepository.GetProductsAsync(true);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove("formatted_purchase_price");
            dt.Columns.Remove("formatted_selling_price");
            dt.Columns.Remove("status");

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {InventoryRepository.col_id, "Product ID"},
                    {InventoryRepository.col_barcode, "Barcode"},
                    {InventoryRepository.col_item_name, "Item"},
                    {InventoryRepository.col_brand, "Brand"},
                    {InventoryRepository.col_motor_brand, "Motor Brand"},
                    {InventoryRepository.col_category, "Category"},
                    {InventoryRepository.col_size, "Size"},
                    {InventoryRepository.col_date_created, "Date Created"},
                    {InventoryRepository.col_purchase_price, "Purchase Price"},
                    {InventoryRepository.col_selling_price, "Selling Price"},
                    {InventoryRepository.col_quantity, "Quantity"}
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Date Created",
                    "Product ID",
                    "Barcode",
                    "Category",
                    "Item",
                    "Brand",
                    "Motor Brand",
                    "Size",
                    "Purchase Price",
                    "Selling Price",
                    "Quantity"
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

        private async Task<DataTable> GetSuppliersDT(DateTime dateFom, DateTime dateTo)
        {
            SuppliersRepository repository = new SuppliersRepository();
            DataTable dt = await repository.GetSupplierAsync(true);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
            {
                {SuppliersRepository.col_id, "Supplier ID"},
                {SuppliersRepository.col_contact_name, "Contact Name"},
                {SuppliersRepository.col_contact_no, "Contact No"},
                {SuppliersRepository.col_address, "Address"},
                {SuppliersRepository.col_date_created, "Date Created"},
            };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Date Created",
                    "Supplier ID",
                    "Contact Name",
                    "Contact No",
                    "Address",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

        private async Task<DataTable> GetSalesTransactionsDT(DateTime dateFrom, DateTime dateTo)
        {
            ReportsRepository repository = new ReportsRepository();
            DataTable dt = await repository.GetSalesAsync(dateFrom, dateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove("formatted_transaction_date");

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {POSRepository.col_transaction_id, "Transaction ID"},
                    {UserCredentialsRepository.col_username, "Salesperson"},
                    {POSRepository.col_transaction_date, "Transaction Date"},
                    {"item_name", "Item"},
                    {POSRepository.col_quantity, "Quantity"},
                    {POSRepository.col_total, "Total"},
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Transaction Date",
                    "Transaction ID",
                    "Salesperson",
                    "Item",
                    "Quantity",
                    "Total",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

        private async Task<DataTable> GetInventoryReportDT(DateTime dateFrom, DateTime dateTo)
        {
            ReportsRepository repository = new ReportsRepository();
            DataTable dt = await repository.GetInventoryReportsAsync(dateFrom, dateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove(ReportsRepository.col_item_id);
            dt.Columns.Remove(ReportsRepository.col_purchase_order_id);

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {InventoryRepository.col_barcode, "Barcode"},
                    {ReportsRepository.col_date, "Date"},
                    {ReportsRepository.col_action, "Action"},
                    {ReportsRepository.col_quantity, "Quantity"},
                    {ReportsRepository.col_modified_by, "Operated By"},
                    {ReportsRepository.col_old_stock, "Old Stock"},
                    {ReportsRepository.col_new_stock, "New Stock"},
                    {ReportsRepository.col_remarks, "Remarks"},
                    {"item", "Item"},
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Date",
                    "Operated By",
                    "Action",
                    "Barcode",
                    "Item",
                    "Quantity",
                    "Old Stock",
                    "New Stock",
                    "Remarks",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

        private async Task<DataTable> GetAuditTrailDT(DateTime dateFrom, DateTime dateTo)
        {
            ReportsRepository repository = new ReportsRepository();
            DataTable dt = await repository.GetAuditTrailAsync(dateFrom, dateTo);

            if (dt == null || dt.Rows.Count == 0)
            {
                return dt;
            }

            dt.Columns.Remove("name");
            dt.Columns.Remove("formatted_date");

            Dictionary<string, string> columnRenameDict = new Dictionary<string, string>
                {
                    {UserCredentialsRepository.col_username, "Username"},
                    {UserCredentialsRepository.col_first_name, "First Name"},
                    {UserCredentialsRepository.col_last_name, "Last Name"},
                    {ReportsRepository.col_action, "Action"},
                    {ReportsRepository.col_date, "Date"},
                    {ReportsRepository.col_details, "Details"},
                };

            foreach (var item in columnRenameDict)
            {
                dt.Columns[item.Key].ColumnName = item.Value;
            }

            List<string> columnOrder = new List<string>
                {
                    "Date",
                    "Username",
                    "First Name",
                    "Last Name",
                    "Action",
                    "Details",
                };

            return Util.ReorderColumns(dt, columnOrder);
        }

        private void StartDt_ValueChanged(object sender, EventArgs e)
        {
            EndDt.MinDate = StartDt.Value;
        }

        private void CB_CheckedChanged(object sender, EventArgs e)
        {
            bool check = Util.IsAnyCheckboxChecked(PurchaseOrdersCb, SalesTransactionsCb, InventoryReportCb, AuditTrailCb);
            DateRangePanel.Enabled = check;;

            if (!check)
            {
                StartDt.Value = DateTime.Today;
                EndDt.Value = DateTime.Today;
            }
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null && radioButton.Checked)
            {
                string radioButtonName = radioButton.Name;

                bool activeHours = false;
                HoursCb.Enabled = false;
                WeeklyCb.Enabled = false;
                MonthlyCb.Enabled = false;

                switch (radioButtonName)
                {
                    case "HourlyRb":
                        break;
                    case "EveryHoursRb":
                        HoursCb.Enabled = true;
                        break;
                    case "DailyRb":
                        activeHours = true;
                        break;
                    case "WeeklyRb":
                        activeHours = true;
                        WeeklyCb.Enabled = true;
                        break;
                    case "MonthlyRb":
                        MonthlyCb.Enabled = true;
                        activeHours = true;
                        break;
                    default:
                        break;
                }

                ActiveHoursPanel.Enabled = activeHours;
                HoursPanel.Enabled = activeHours;
            }
        }

        private void SetUpCB(bool isReset = false)
        {
            if (isReset)
            {
                HoursCb.SelectedIndex = 0;
                WeeklyCb.SelectedIndex = 0;
                MonthlyCb.SelectedIndex = 0;
            }
            else
            {
                for (int i = 1; i <= 23; i++)
                {
                    HoursCb.Items.Add(i.ToString());
                }

                HoursCb.SelectedIndex = 0;

                string[] daysOfWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
                WeeklyCb.Items.AddRange(daysOfWeek);

                WeeklyCb.SelectedIndex = 0;

                for (int i = 1; i <= 31; i++)
                {
                    MonthlyCb.Items.Add(i.ToString());
                }

                MonthlyCb.SelectedIndex = 0;
            }
        }
    }
}
