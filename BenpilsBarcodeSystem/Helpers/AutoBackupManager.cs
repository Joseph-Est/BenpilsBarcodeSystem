using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Helpers
{
    public class AutoBackupManager
    {
        public System.Timers.Timer autoBackupTimer;
        private string saveLocation;
        private bool useActiveTime = false;
        private bool backingUp = false;

        public TimeSpan TimeRemaining
        {
            get
            {
                if (autoBackupTimer != null)
                {
                    DateTime nextBackupTime = DateTime.Now.AddMilliseconds(autoBackupTimer.Interval);
                    TimeSpan timeRemaining = nextBackupTime - DateTime.Now;

                    return timeRemaining;
                }
                else
                {
                    return TimeSpan.Zero;
                }
            }
        }

        public async void SetupAutoBackup(Form form)
        {
            if (autoBackupTimer != null)
            {
                autoBackupTimer.Stop();
                autoBackupTimer.Dispose();
            }

            autoBackupTimer = new System.Timers.Timer();
            autoBackupTimer.Elapsed += async (sender, e) => await AutoBackup();

            BackupSettings settings = LoadBackupSettings();

            bool backup = true;

            saveLocation = settings.SaveLocation;

            if (string.IsNullOrEmpty(saveLocation.Trim()) || !Directory.Exists(saveLocation))
            {
                Util.Log($"Backup failed: Invalid Save Location.");
                return;
            }

            switch (settings.Interval)
            {
                case BackupInterval.Hourly:
                    autoBackupTimer.Interval = TimeSpan.FromHours(1).TotalMilliseconds;
                    break;
                case BackupInterval.EveryXHours:
                    if (double.TryParse(settings.IntervalValue, out double intervalInHours))
                    {
                        autoBackupTimer.Interval = TimeSpan.FromHours(intervalInHours).TotalMilliseconds;
                    }
                    else
                    {
                        Util.Log($"Backup failed: Invalid interval value.");
                        return;
                    }
                    break;
                case BackupInterval.Daily:
                    useActiveTime = true;
                    autoBackupTimer.Interval = TimeSpan.FromMinutes((settings.ActiveStartTime).TotalMinutes).TotalMilliseconds;
                    break;
                case BackupInterval.Weekly:
                    useActiveTime = true;
                    autoBackupTimer.Interval = TimeSpan.FromMinutes((settings.ActiveStartTime).TotalMinutes).TotalMilliseconds;
                    backup = DateTime.Now.DayOfWeek.ToString() == settings.IntervalValue;
                    
                    break;
                case BackupInterval.Monthly:
                    useActiveTime = true;
                    autoBackupTimer.Interval = TimeSpan.FromMinutes((settings.ActiveStartTime).TotalMinutes).TotalMilliseconds;
                    //MessageBox.Show($"{settings.ActiveEndTime} : {settings.ActiveStartTime} {settings.ActiveEndTime - settings.ActiveStartTime}");
                    if (int.TryParse(settings.IntervalValue, out int intervalInDays))
                    {
                        backup = DateTime.Now.Day == int.Parse(settings.IntervalValue);
                    }
                    else
                    {
                        Util.Log($"Backup failed: Invalid interval value.");
                        return;
                    }
                    
                    break;
            }

            if (backup)
            {
                if (useActiveTime)
                {
                    if (!Util.IsBackupSuccessfulToday())
                    {
                        DateTime activeStartTimeToday = DateTime.Today.Add(settings.ActiveStartTime);
                        DateTime activeEndTimeToday = DateTime.Today.Add(settings.ActiveEndTime);

                        if (DateTime.Now > activeStartTimeToday && DateTime.Now < activeEndTimeToday)
                        {
                            autoBackupTimer.Interval = 10000;
                        }
                        else
                        {
                            autoBackupTimer.Interval = (activeStartTimeToday - DateTime.Now).TotalMilliseconds;
                        }

                        autoBackupTimer.AutoReset = false;
                        autoBackupTimer.Start();
                    }
                }
                else
                {
                    autoBackupTimer.Start();
                }
               
                // Show the next backup time
                DateTime nextBackupTime = DateTime.Now.AddMilliseconds(autoBackupTimer.Interval);
                //MessageBox.Show($"Next backup will be at {nextBackupTime}", "Backup Scheduled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public async Task<bool> AutoBackup(bool force = false)
        {
            BackupSettings settings = LoadBackupSettings();

            if (!backingUp)
            {
                if ((useActiveTime && IsWithinActiveHours(settings.ActiveStartTime, settings.ActiveEndTime)) || !useActiveTime || force)
                {
                    backingUp = true;
                    bool backupSucceeded = false;
                    int retryCount = 0;
                    while (!backupSucceeded && retryCount < 3)
                    {
                        try
                        {
                            Dictionary<DataTable, string> dataTableSheetMapping = new Dictionary<DataTable, string>();

                            DateTime fromDate = new DateTime(1753, 1, 1, 0, 0, 0);
                            DateTime endDate = new DateTime(9999, 12, 31, 23, 59, 59);

                            if (settings.BackupInventory)
                            {
                                InventoryRepository repository = new InventoryRepository();
                                DataTable dt = await repository.GetInventoryExportDT(fromDate, endDate);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    dataTableSheetMapping.Add(dt, "Inventory");
                                }
                            }

                            if (settings.BackupSuppliers)
                            {
                                SuppliersRepository repository = new SuppliersRepository();
                                DataTable dt = await repository.GetSuppliersDT(fromDate, endDate);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    dataTableSheetMapping.Add(dt, "Suppliers");
                                }
                            }

                            if (settings.BackupPurchaseOrder)
                            {
                                ReportsRepository repository = new ReportsRepository();
                                DataTable dt = await repository.GetPurchaseOrderExportDT(fromDate, endDate);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    dataTableSheetMapping.Add(dt, "Purchase Order");
                                }
                            }

                            if (settings.BackupSalesTransactions)
                            {
                                ReportsRepository repository = new ReportsRepository();
                                DataTable dt = await repository.GetSalesTransactionsExportDT(fromDate, endDate);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    dataTableSheetMapping.Add(dt, "Sales Transactions");
                                }
                            }

                            if (settings.BackupInventoryReport)
                            {
                                ReportsRepository repository = new ReportsRepository();
                                DataTable dt = await repository.GetInventoryReportExportDT(fromDate, endDate);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    dataTableSheetMapping.Add(dt, "Inventory Report");
                                }
                            }

                            if (settings.BackupAuditTrail)
                            {
                                ReportsRepository repository = new ReportsRepository();
                                DataTable dt = await repository.GetAuditTrailExportDT(fromDate, endDate);
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    dataTableSheetMapping.Add(dt, "Audit Trail");
                                }
                            }

                            switch (Util.ExportData(dataTableSheetMapping, saveLocation, true))
                            {
                                case 0:
                                    retryCount++;
                                    backupSucceeded = true;
                                    Util.Log($"Attempt {retryCount} success. Backup saved to {saveLocation}.");
                                    break;
                                case 1:
                                    retryCount++;
                                    Util.Log($"Attempt {retryCount} failed: No available Data to backup.");
                                    await Task.Delay(600000);
                                    break;
                                case 2:
                                    retryCount++;
                                    Util.Log($"Attempt {retryCount} failed: Unable to overwrite existing file. Either it's open or you don't have the necessary permissions.");
                                    await Task.Delay(600000);
                                    break;
                                case 3:
                                    retryCount++;
                                    Util.Log($"Attempt {retryCount} failed: Unable to export backup.");
                                    await Task.Delay(600000);
                                    break;
                                default:
                                    break;
                            }



                        }
                        catch (Exception ex)
                        {
                            retryCount++;
                            Util.Log($"Attempt {retryCount} failed: {ex.Message}");
                            await Task.Delay(600000);
                        }

                        if (force)
                        {
                            break;
                        }
                    }
                    backingUp = false;
                    return backupSucceeded;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Util.Log($"Backup failed: Currently backing up.");
                return false;
            }
        }

        private bool IsWithinActiveHours(TimeSpan startTime, TimeSpan endTime)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            return now >= startTime && now <= endTime;
        }

        private BackupSettings LoadBackupSettings()
        {
            BackupInterval interval;
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Interval))
            {
                interval = (BackupInterval)Enum.Parse(typeof(BackupInterval), Properties.Settings.Default.Interval);
            }
            else
            {
                interval = default;
            }

            return new BackupSettings
            {
                IsEnabled = Properties.Settings.Default.AutomaticBackupEnabled,
                BackupInventory = Properties.Settings.Default.BackupInventory,
                BackupSuppliers = Properties.Settings.Default.BackupSuppliers,
                BackupPurchaseOrder = Properties.Settings.Default.BackupPurchaseOrders,
                BackupSalesTransactions = Properties.Settings.Default.BackupSalesTransactions,
                BackupInventoryReport = Properties.Settings.Default.BackupInventoryReport,
                BackupAuditTrail = Properties.Settings.Default.BackupAuditTrail,

                Interval = interval,
                IntervalValue = Properties.Settings.Default.IntervalValue,

                UseActiveHours = Properties.Settings.Default.UseActiveHours,

                ActiveStartTime = Properties.Settings.Default.ActiveStartTime,
                ActiveEndTime = Properties.Settings.Default.ActiveEndTime,

                SaveLocation = Properties.Settings.Default.SaveLocation,
            };
        }
    }
}
