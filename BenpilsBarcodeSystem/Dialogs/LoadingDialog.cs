using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
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
    enum LoadingFor
    {
        ManualBackup = 1,
    }


    public partial class LoadingDialog : Form
    {
        readonly LoadingFor LoadingFor;
        readonly List<string> Data;
        private readonly Timer hideLoadingTimer;
        private string Message;
        private bool canClose = false;
        readonly DateTime FromDate;
        readonly DateTime ToDate;

        internal LoadingDialog(string title, LoadingFor loadingFor, DateTime fromDate, DateTime toDate, List<string> data = null)
        {
            InitializeComponent();
            TitleLbl.Text = title;
            LoadingFor = loadingFor;
            Data = data;
            FromDate = fromDate;
            ToDate = toDate;

            hideLoadingTimer = new Timer
            {
                Interval = 2000
            };

            hideLoadingTimer.Tick += HideLoadingTimer_Tick;
        }

        private void LoadingDialog_Load(object sender, EventArgs e)
        {
           
            if (LoadingFor == LoadingFor.ManualBackup)
            {
                ManualBackup();
            }
        }

        private async void ManualBackup()
        {
            Dictionary<DataTable, string> dataTableSheetMapping = new Dictionary<DataTable, string>();

            bool inventoryExists = Data != null && Data.Any(item => item.ToLower() == "inventory");
            bool supplierExists = Data != null && Data.Any(item => item.ToLower() == "suppliers");
            bool purchaseOrderExists = Data != null && Data.Any(item => item.ToLower() == "purchase order");
            bool salesExists = Data != null && Data.Any(item => item.ToLower() == "sales");
            bool inventoryReportExists = Data != null && Data.Any(item => item.ToLower() == "inventory report");
            bool auditTrailExists = Data != null && Data.Any(item => item.ToLower() == "audit trail");

            if (inventoryExists)
            {
                InventoryRepository repository = new InventoryRepository();
                DataTable dt = await repository.GetInventoryExportDT(FromDate, ToDate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataTableSheetMapping.Add(dt, "Inventory");
                }
            }

            if (supplierExists)
            {
                SuppliersRepository repository = new SuppliersRepository();
                DataTable dt = await repository.GetSuppliersDT(FromDate, ToDate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataTableSheetMapping.Add(dt, "Suppliers");
                }
            }

            if (purchaseOrderExists)
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable dt = await repository.GetPurchaseOrderExportDT(FromDate, ToDate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataTableSheetMapping.Add(dt, "Purchase Order");
                }
            }

            if (salesExists)
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable dt = await repository.GetSalesTransactionsExportDT(FromDate, ToDate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataTableSheetMapping.Add(dt, "Sales Transactions");
                }
            }

            if (inventoryReportExists)
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable dt = await repository.GetInventoryReportExportDT(FromDate, ToDate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataTableSheetMapping.Add(dt, "Inventory Report");
                }
            }

            if (auditTrailExists)
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable dt = await repository.GetAuditTrailExportDT(FromDate, ToDate);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dataTableSheetMapping.Add(dt, "Audit Trail");
                }
            }

            switch (Util.ExportData(dataTableSheetMapping))
            {
                case 0:
                    Message = "Backup completed successfully.";
                    break;
                case 1:
                    Message = "Backup failed: No Data available.";
                    break;
                case 2:
                    Message = "Backup failed: Unable to overwrite existing file.";
                    break;
                case 3:
                    Message = "Backup failed: Unable to export backup.";
                    break;
                case 5:
                    canClose = true;
                    this.Close();
                    break;
                default:
                    break;
            }

            hideLoadingTimer.Start();
        }

        private void HideLoadingTimer_Tick(object sender, EventArgs e)
        {
            TitleLbl.Text = Message;
            AcceptBtn.Visible = true;

            hideLoadingTimer.Stop();
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            this.Close();
        }

        private void LoadingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
            else
            {
                hideLoadingTimer.Stop();
            }
        }
    }
}
