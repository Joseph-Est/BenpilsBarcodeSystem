using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Helpers
{
    public class AutoBackupManager
    {
        private System.Timers.Timer autoBackupTimer;

        public void SetupAutoBackup(Form form)
        {
            if (autoBackupTimer != null)
            {
                autoBackupTimer.Stop();
                autoBackupTimer.Dispose();
            }

            BackupSettings settings = LoadBackupSettings();

            autoBackupTimer = new System.Timers.Timer();
            autoBackupTimer.Elapsed += async (sender, e) => await AutoBackup(form);

            bool backup = true;

            switch (settings.Interval)
            {
                case BackupInterval.Hourly:
                    autoBackupTimer.Interval = TimeSpan.FromHours(1).TotalMilliseconds;
                    break;
                case BackupInterval.EveryXHours:
                    autoBackupTimer.Interval = TimeSpan.FromHours(double.Parse(settings.IntervalValue)).TotalMilliseconds;
                    break;
                case BackupInterval.Daily:
                    autoBackupTimer.Interval = TimeSpan.FromMinutes((settings.ActiveEndTime - settings.ActiveStartTime).TotalMinutes).TotalMilliseconds;
                    break;
                case BackupInterval.Weekly:
                    autoBackupTimer.Interval = TimeSpan.FromMinutes((settings.ActiveEndTime - settings.ActiveStartTime).TotalMinutes).TotalMilliseconds;
                    backup = DateTime.Now.DayOfWeek.ToString() == settings.IntervalValue;
                    break;
                case BackupInterval.Monthly:
                    autoBackupTimer.Interval = TimeSpan.FromMinutes((settings.ActiveEndTime - settings.ActiveStartTime).TotalMinutes).TotalMilliseconds;
                    backup = DateTime.Now.Day == int.Parse(settings.IntervalValue);
                    break;
            }

            if (backup)
            {
                autoBackupTimer.Start();
            }
        }

        private async Task AutoBackup(Form form)
        {
            BackupSettings settings = LoadBackupSettings();

            if (IsWithinActiveHours(settings.ActiveStartTime, settings.ActiveEndTime))
            {
                bool backupSucceeded = false;
                int retryCount = 0;
                while (!backupSucceeded && retryCount < 3)
                {
                    try
                    {
                        Dictionary<DataTable, string> dataTableSheetMapping = new Dictionary<DataTable, string>();

                        DateTime fromDate = DateTime.MinValue;
                        DateTime endDate = DateTime.MaxValue;

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

                        switch(Util.ExportData(form, dataTableSheetMapping)){
                            case 0:
                                backupSucceeded = true;
                                Util.Log($"Attempt {retryCount} success");
                                break;
                            case 1:
                                Util.Log($"Attempt {retryCount} failed: No available data to backup.");
                                retryCount++;
                                await Task.Delay(30000);
                                break;
                            case 2:
                                Util.Log($"Attempt {retryCount} failed: Unable to overwrite existing file. Either it's open or you don't have the necessary permissions.");
                                retryCount++;
                                await Task.Delay(30000);
                                break;
                            case 3:
                                Util.Log($"Attempt {retryCount} failed: Unable to export backup.");
                                retryCount++;
                                await Task.Delay(30000);
                                break;
                            default:
                                break;
                        }

                        

                    }
                    catch (Exception ex)
                    {
                        retryCount++;
                        Util.Log($"Attempt {retryCount} failed: {ex.Message}");
                        await Task.Delay(30000);
                    }
                }
            }
        }

        private bool IsWithinActiveHours(TimeSpan startTime, TimeSpan endTime)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;

            return now >= startTime && now <= endTime;
        }

        private BackupSettings LoadBackupSettings()
        {
            return new BackupSettings
            {
                IsEnabled = Properties.Settings.Default.AutomaticBackupEnabled,
                BackupInventory = Properties.Settings.Default.BackupInventory,
                BackupSuppliers = Properties.Settings.Default.BackupSuppliers,
                BackupPurchaseOrder = Properties.Settings.Default.BackupPurchaseOrders,
                BackupSalesTransactions = Properties.Settings.Default.BackupSalesTransactions,
                BackupInventoryReport = Properties.Settings.Default.BackupInventoryReport,
                BackupAuditTrail = Properties.Settings.Default.BackupAuditTrail,

                Interval = (BackupInterval)Enum.Parse(typeof(BackupInterval), Properties.Settings.Default.Interval),
                IntervalValue = Properties.Settings.Default.IntervalValue,

                UseActiveHours = Properties.Settings.Default.UseActiveHours,

                ActiveStartTime = Properties.Settings.Default.ActiveStartTime,
                ActiveEndTime = Properties.Settings.Default.ActiveEndTime,

                SaveLocation = Properties.Settings.Default.SaveLocation,
            };
        }
    }
}
