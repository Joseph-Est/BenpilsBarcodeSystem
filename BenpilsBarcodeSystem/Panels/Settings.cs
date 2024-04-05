using BenpilsBarcodeSystem.Dialogs;
using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Properties;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class Settings : Form
    {
        private BackupInterval selectedInterval;
        MainForm mainForm;
        bool backingUp = false;

        public Settings()
        {
            InitializeComponent();
            Util.SetDateTimePickerFormat("MMM dd, yyyy", StartDt, EndDt);
            InputValidator.AllowOnlyDigitsMinMax(Hour1Txt, 1, 12);
            InputValidator.AllowOnlyDigitsMinMax(Hour2Txt, 1, 12);
            InputValidator.AllowOnlyDigitsMinMax(Minute1Txt, 1, 59);
            InputValidator.AllowOnlyDigitsMinMax(Minute2Txt, 1, 59);
            SetUpCB();
            SetUpAutomaticBackup();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            mainForm = (MainForm)this.ParentForm;
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
                                            MessageBox.Show("Failed to restore item due to an error. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                            MessageBox.Show("Failed to restore supplier due to an error. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                            MessageBox.Show("Failed to restore user due to an error. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void SearchTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter;
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
                EnableAutomaticBackup(true);
            }
            else
            {
                Confirmation confirmation = new Confirmation("Are you sure you want to disable automatic backup?", "", "Yes", "No");
                DialogResult result = confirmation.ShowDialog();

                if (result == DialogResult.Yes)
                {
                    SwitchCb.Image = Properties.Resources.icons8_toggle_off_30;
                    DisabledLbl.ForeColor = Color.FromArgb(240, 62, 62);
                    DisabledLbl.Text = "Disabled";
                    AutomaticBackupPanel.Enabled = false;
                    EnableAutomaticBackup(false);
                }
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (Util.IsAnyCheckboxChecked(InventoryCb, SuppliersCb, PurchaseOrdersCb, SalesTransactionsCb, InventoryReportCb, AuditTrailCb))
            {
                DateTime fromDate = StartDt.Value;
                DateTime toDate = EndDt.Value;

                List<string> checkedCB = new List<string>();

                if (InventoryCb.Checked) checkedCB.Add("inventory");
                if (SuppliersCb.Checked) checkedCB.Add("suppliers");
                if (PurchaseOrdersCb.Checked) checkedCB.Add("purchase order");
                if (SalesTransactionsCb.Checked) checkedCB.Add("sales");
                if (InventoryReportCb.Checked) checkedCB.Add("inventory report");
                if (AuditTrailCb.Checked) checkedCB.Add("audit trail");

                LoadingDialog loading = new LoadingDialog("Backing up Data, please wait ...", LoadingFor.ManualBackup, fromDate, toDate, checkedCB);
                loading.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please check a Data to export", "Export Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                        selectedInterval = BackupInterval.Hourly;
                        break;
                    case "EveryHoursRb":
                        selectedInterval = BackupInterval.EveryXHours;
                        HoursCb.Enabled = true;
                        break;
                    case "DailyRb":
                        selectedInterval = BackupInterval.Daily;
                        activeHours = true;
                        break;
                    case "WeeklyRb":
                        selectedInterval = BackupInterval.Weekly;
                        activeHours = true;
                        WeeklyCb.Enabled = true;
                        break;
                    case "MonthlyRb":
                        selectedInterval = BackupInterval.Monthly;
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

                string[] tt = { "AM", "PM"};
                Ampm1Cb.Items.AddRange(tt);
                Ampm1Cb.SelectedIndex = 0;

                Ampm2Cb.Items.AddRange(tt);
                Ampm2Cb.SelectedIndex = 0;
            }
        }

        private void SaveBackupSettings_Click(object sender, EventArgs e)
        {
            if (Util.IsAnyCheckboxChecked(AInventoryCb, ASuppliersCb, ASalesTransactionsCb, AInventoryReportCb, AAuditTrailCb))
            {
                BackupSettings settings = new BackupSettings();

                settings.IsEnabled = true;
                settings.BackupInventory = AInventoryCb.Checked;
                settings.BackupSuppliers = ASuppliersCb.Checked;
                settings.BackupPurchaseOrder = APurchaseOrdersCb.Checked;
                settings.BackupSalesTransactions = ASalesTransactionsCb.Checked;
                settings.BackupInventoryReport = AInventoryReportCb.Checked;
                settings.BackupAuditTrail = AAuditTrailCb.Checked;

                if (!Util.IsAnyRadioButtonChecked(HourlyRb, EveryHoursRb, DailyRb, WeeklyRb, MonthlyRb))
                {
                    MessageBox.Show("Please select backup interval.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                settings.Interval = selectedInterval;
                settings.IntervalValue = selectedInterval == BackupInterval.EveryXHours ? HoursCb.Text :
                                         selectedInterval == BackupInterval.Weekly ? WeeklyCb.Text : MonthlyCb.Text;

                bool activeHoursEnabled = selectedInterval == BackupInterval.Daily || selectedInterval == BackupInterval.Weekly || selectedInterval == BackupInterval.Monthly;
                settings.UseActiveHours = activeHoursEnabled;

                if (ActiveHoursPanel.Enabled && Util.AreTextBoxesNullOrEmpty(Hour1Txt, Minute1Txt, Hour2Txt, Minute2Txt))
                {
                    MessageBox.Show("Please select valid active hours.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                string hour1 = Hour1Txt.Text;
                string minute1 = Minute1Txt.Text;
                string ampm1 = Ampm1Cb.SelectedItem.ToString();

                string hour2 = Hour2Txt.Text;
                string minute2 = Minute2Txt.Text;
                string ampm2 = Ampm2Cb.SelectedItem.ToString();

                DateTime time1 = DateTime.Parse($"{hour1}:{minute1} {ampm1}", System.Globalization.CultureInfo.InvariantCulture);
                DateTime time2 = DateTime.Parse($"{hour2}:{minute2} {ampm2}", System.Globalization.CultureInfo.InvariantCulture);

                if (ActiveHoursPanel.Enabled)
                {
                    if (time1 > time2)
                    {
                        MessageBox.Show("Start time should be earlier than or equal to end time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (time1.TimeOfDay > time2.TimeOfDay)
                    {
                        MessageBox.Show("Start time and end time should not overlap to the next day.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                settings.ActiveStartTime = time1.TimeOfDay;
                settings.ActiveEndTime = time2.TimeOfDay;

                if (Util.AreTextBoxesNullOrEmpty(SaveLocationTxt))
                {
                    MessageBox.Show("Please select save location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (!Directory.Exists(SaveLocationTxt.Text))
                {
                    MessageBox.Show("Please enter a valid directory for the backup location.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                settings.SaveLocation = SaveLocationTxt.Text;

                SaveAutomaticBackupSettings(settings);
            }
            else
            {
                MessageBox.Show("Please check a Data to backup", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private BackupSettings LoadBackupSettings()
        {
            BackupSettings settings = new BackupSettings
            {
                IsEnabled = Properties.Settings.Default.AutomaticBackupEnabled,
                BackupInventory = Properties.Settings.Default.BackupInventory,
                BackupSuppliers = Properties.Settings.Default.BackupSuppliers,
                BackupPurchaseOrder = Properties.Settings.Default.BackupPurchaseOrders,
                BackupSalesTransactions = Properties.Settings.Default.BackupSalesTransactions,
                BackupInventoryReport = Properties.Settings.Default.BackupInventoryReport,
                BackupAuditTrail = Properties.Settings.Default.BackupAuditTrail,

                IntervalValue = Properties.Settings.Default.IntervalValue,

                UseActiveHours = Properties.Settings.Default.UseActiveHours,

                ActiveStartTime = Properties.Settings.Default.ActiveStartTime,
                ActiveEndTime = Properties.Settings.Default.ActiveEndTime,
                SaveLocation = Properties.Settings.Default.SaveLocation
            };

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Interval))
            {
                if (Enum.TryParse(Properties.Settings.Default.Interval, out BackupInterval interval))
                {
                    settings.Interval = interval;
                }
                else
                {
                    settings.Interval = default;
                }
            }

            return settings;
        }

        private void SetUpAutomaticBackup()
        {
            BackupSettings settings = LoadBackupSettings();

            SwitchCb.Checked = settings.IsEnabled;
            
            AInventoryCb.Checked = settings.BackupInventory;
            ASuppliersCb.Checked = settings.BackupSuppliers;
            APurchaseOrdersCb.Checked = settings.BackupPurchaseOrder;
            AInventoryReportCb.Checked = settings.BackupInventoryReport;
            ASalesTransactionsCb.Checked = settings.BackupSalesTransactions;
            AAuditTrailCb.Checked = settings.BackupAuditTrail;

            switch (settings.Interval)
            {
                case BackupInterval.Hourly:
                    HourlyRb.Checked = true;
                    break;
                case BackupInterval.EveryXHours:
                    EveryHoursRb.Checked = true;
                    HoursCb.SelectedItem = settings.IntervalValue;
                    break;
                case BackupInterval.Daily:
                    DailyRb.Checked = true;
                    break;
                case BackupInterval.Weekly:
                    WeeklyRb.Checked = true;
                    WeeklyCb.SelectedItem = settings.IntervalValue;
                    break;
                case BackupInterval.Monthly:
                    MonthlyRb.Checked = true;
                    MonthlyCb.SelectedItem = settings.IntervalValue;
                    break;
                default:
                    break;
            }

            switch (settings.Interval)
            {
                case BackupInterval.Daily:
                case BackupInterval.Weekly:
                case BackupInterval.Monthly:
                    if (settings.UseActiveHours)
                    {
                        (string hour1, string minute1, string ampm1, string hour2, string minute2, string ampm2) = DisectTime(settings.ActiveStartTime, settings.ActiveEndTime);
                        Hour1Txt.Text = hour1;
                        Minute1Txt.Text = minute1;
                        Ampm1Cb.SelectedItem = ampm1;
                        Hour2Txt.Text = hour2;
                        Minute2Txt.Text = minute2;
                        Ampm2Cb.SelectedItem = ampm2;
                    }
                    else
                    {
                        Hour1Txt.Text = "08";
                        Minute1Txt.Text = "00";
                        Ampm1Cb.SelectedItem = "AM";
                        Hour2Txt.Text = "05";
                        Minute2Txt.Text = "00";
                        Ampm2Cb.SelectedItem = "PM";
                    }
                    break;
                default:
                    Hour1Txt.Text = "08";
                    Minute1Txt.Text = "00";
                    Ampm1Cb.SelectedItem = "AM";
                    Hour2Txt.Text = "05";
                    Minute2Txt.Text = "00";
                    Ampm2Cb.SelectedItem = "PM";
                    break;
            }

            SaveLocationTxt.Text = settings.SaveLocation;
        }

        private (string hour1, string minute1, string ampm1, string hour2, string minute2, string ampm2) DisectTime(TimeSpan activeStartTime, TimeSpan activeEndTime)
        {
            // Format TimeSpan to DateTime to get AM/PM time
            DateTime time1 = DateTime.Today.Add(activeStartTime);
            DateTime time2 = DateTime.Today.Add(activeEndTime);

            // Convert DateTime to formatted strings
            string hour1 = time1.ToString("hh");
            string minute1 = time1.ToString("mm");
            string ampm1 = time1.ToString("tt");

            string hour2 = time2.ToString("hh");
            string minute2 = time2.ToString("mm");
            string ampm2 = time2.ToString("tt");

            return (hour1, minute1, ampm1, hour2, minute2, ampm2);
        }

        private void SaveAutomaticBackupSettings(BackupSettings settings)
        {
            Properties.Settings.Default.AutomaticBackupEnabled = settings.IsEnabled;
            Properties.Settings.Default.BackupInventory = settings.BackupInventory;
            Properties.Settings.Default.BackupSuppliers = settings.BackupSuppliers;
            Properties.Settings.Default.BackupPurchaseOrders = settings.BackupPurchaseOrder;
            Properties.Settings.Default.BackupSalesTransactions = settings.BackupSalesTransactions;
            Properties.Settings.Default.BackupInventoryReport = settings.BackupInventoryReport;
            Properties.Settings.Default.BackupAuditTrail = settings.BackupAuditTrail;

            Properties.Settings.Default.Interval = settings.Interval.ToString();
            Properties.Settings.Default.IntervalValue = settings.IntervalValue;

            Properties.Settings.Default.UseActiveHours = settings.UseActiveHours;
            Properties.Settings.Default.ActiveStartTime = settings.ActiveStartTime;
            Properties.Settings.Default.ActiveEndTime = settings.ActiveEndTime;
            Properties.Settings.Default.SaveLocation = settings.SaveLocation;

            Properties.Settings.Default.Save();

            MessageBox.Show("Settings saved succesfully");

            mainForm.AutoBackupManagerInstance.SetupAutoBackup(mainForm);
        }

        private void EnableAutomaticBackup(bool enable)
        {
            Properties.Settings.Default.AutomaticBackupEnabled = enable;
            Properties.Settings.Default.Save();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    SaveLocationTxt.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private static readonly string logFilePath = @"autobackup_log.txt";

        private void BackupLogsBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string[] logLines = System.IO.File.ReadAllLines(logFilePath);

                List<object[]> data = new List<object[]>();
                foreach (string line in logLines)
                {
                    int firstSeparatorIndex = line.IndexOf(':');
                    int secondSeparatorIndex = line.IndexOf(':', firstSeparatorIndex + 1);
                    int thirdSeparatorIndex = line.IndexOf(':', secondSeparatorIndex + 1);

                    if (thirdSeparatorIndex > 0)
                    {
                        string datePart = line.Substring(0, thirdSeparatorIndex).Trim();
                        string messagePart = line.Substring(thirdSeparatorIndex + 1).Trim();

                        object[] row = new object[] { datePart, messagePart };
                        data.Add(row);
                    }
                }
                LogsDialog dialog = new LogsDialog(data);
                dialog.ShowDialog();
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("No available logs to show");
                return;
            }
        }
        private async void BackupNowBtn_Click(object sender, EventArgs e)
        {
            if (!backingUp)
            {
                backingUp = true;
                if(await mainForm.AutoBackupManagerInstance.AutoBackup(true))
                {
                    MessageBox.Show("Backup Success");
                }
                else
                {
                    MessageBox.Show("Backup failed");

                }
            }
            backingUp = false;
        }
    }
}
