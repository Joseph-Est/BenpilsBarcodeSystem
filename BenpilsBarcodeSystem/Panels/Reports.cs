using BenpilsBarcodeSystem.Dialogs;
using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace BenpilsBarcodeSystem
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
            Util.SetDateTimePickerFormat("MMM dd, yyyy", InventoryStartDateDt, InventoryEndDateDt, PurchaseEndDateDt, PurchaseStartDateDt, SalesStartDateDt, SalesEndDateDt, AuditEndDateDt, AuditStartDateDt);
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            UpdateInventoryReportsDG();
        }

        private void ReportsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tc = (TabControl)sender;
            switch (tc.SelectedIndex)
            {
                case 0: // Inventory Report Tab
                    UpdateInventoryReportsDG();
                    break;
                case 1: // Purchase Report Tab
                    UpdatePurchaseReportDG();
                    break;
                case 2: // Sales Report Tab
                    UpdateSalesReportDG();
                    break;
                case 3: // Audit Trail Tab
                    UpdateAuditTrailDG();
                    break;
                default:
                    break;
            }
        }

        private void StartDateDt_ValueChanged(object sender, EventArgs e)
        {

            if (sender is DateTimePicker dateTimePicker)
            {
                switch (dateTimePicker.Name)
                {
                    case "InventoryStartDateDt":
                        InventoryEndDateDt.MinDate = InventoryStartDateDt.Value;
                        UpdateInventoryReportsDG();
                        InventoryCancelDateCb.Visible = InventoryStartDateDt.Value != DateTime.Today || InventoryEndDateDt.Value != DateTime.Today;
                        break;
                    case "PurchaseStartDateDt":
                        PurchaseEndDateDt.MinDate = PurchaseStartDateDt.Value;
                        UpdatePurchaseReportDG();
                        PurchaseCancelDateCb.Visible = PurchaseStartDateDt.Value != DateTime.Today || PurchaseEndDateDt.Value != DateTime.Today;
                        break;
                    case "SalesStartDateDt":
                        SalesEndDateDt.MinDate = SalesStartDateDt.Value;
                        UpdateSalesReportDG();
                        SalesCancelDateCb.Visible = SalesStartDateDt.Value != DateTime.Today || SalesEndDateDt.Value != DateTime.Today;
                        break;
                    case "AuditStartDateDt":
                        AuditEndDateDt.MinDate = AuditStartDateDt.Value;
                        UpdateAuditTrailDG();
                        AuditCancelDateCb.Visible = AuditStartDateDt.Value != DateTime.Today || AuditEndDateDt.Value != DateTime.Today;
                        break;

                }
            }
        }

        private void EndDateDt_ValueChanged(object sender, EventArgs e)
        {

            if (sender is DateTimePicker dateTimePicker)
            {
                switch (dateTimePicker.Name)
                {
                    case "InventoryEndDateDt":
                        UpdateInventoryReportsDG();
                        InventoryCancelDateCb.Visible = InventoryStartDateDt.Value != DateTime.Today || InventoryEndDateDt.Value != DateTime.Today;
                        break;
                    case "PurchaseEndDateDt":
                        UpdatePurchaseReportDG();
                        PurchaseCancelDateCb.Visible = PurchaseStartDateDt.Value != DateTime.Today || PurchaseEndDateDt.Value != DateTime.Today;
                        break;
                    case "SalesEndDateDt":
                        UpdateSalesReportDG();
                        SalesCancelDateCb.Visible = SalesStartDateDt.Value != DateTime.Today || SalesEndDateDt.Value != DateTime.Today;
                        break;
                    case "AuditEndDateDt":
                        UpdateAuditTrailDG();
                        AuditCancelDateCb.Visible = AuditStartDateDt.Value != DateTime.Today || AuditEndDateDt.Value != DateTime.Today;
                        break;

                }
            }
        }

        private void CancelDateCb_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                switch (checkBox.Name)
                {
                    case "InventoryCancelDateCb":
                        InventoryStartDateDt.Value = DateTime.Today;
                        InventoryEndDateDt.Value = DateTime.Today;
                        break;
                    case "PurchaseCancelDateCb":
                        PurchaseStartDateDt.Value = DateTime.Today;
                        PurchaseEndDateDt.Value = DateTime.Today;
                        break;
                    case "SalesCancelDateCb":
                        SalesStartDateDt.Value = DateTime.Today;
                        SalesEndDateDt.Value = DateTime.Today;
                        break;
                    case "AuditCancelDateCb":
                        AuditStartDateDt.Value = DateTime.Today;
                        AuditEndDateDt.Value = DateTime.Today;
                        break;

                }
                checkBox.Visible = false;
            }
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                switch (textBox.Name)
                {
                    case "InventorySearchTxt":
                        UpdateInventoryReportsDG();
                        InventoryCancelSearchCb.Visible = !string.IsNullOrWhiteSpace(InventorySearchTxt.Text);
                        break;
                    case "PurchaseSearchTxt":
                        UpdatePurchaseReportDG();
                        PurchaseCancelSearchCb.Visible = !string.IsNullOrWhiteSpace(PurchaseSearchTxt.Text);
                        break;
                    case "SalesSearchTxt":
                        UpdateSalesReportDG();
                        SalesCancelSearchCb.Visible = !string.IsNullOrWhiteSpace(SalesSearchTxt.Text);
                        break;
                    case "AuditSearchTxt":
                        UpdateAuditTrailDG();
                        AuditCancelSearchCb.Visible = !string.IsNullOrWhiteSpace(AuditSearchTxt.Text);
                        break;

                }
            }
        }

        private void CancelSearchCb_Click(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                switch (checkBox.Name)
                {
                    case "InventoryCancelSearchCb":
                        InventorySearchTxt.Clear();
                        break;
                    case "PurchaseCancelSearchCb":
                        PurchaseSearchTxt.Clear();
                        break;
                    case "SalesCancelSearchCb":
                        SalesSearchTxt.Clear();
                        break;
                    case "AuditCancelSearchCb":
                        AuditSearchTxt.Clear();
                        break;

                }
                checkBox.Visible = false;
            }
        }

        //INVENTORY REPORT

        public async void UpdateInventoryReportsDG()
        {
            if (string.IsNullOrWhiteSpace(InventorySearchTxt.Text))
            {
                InventorySearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable ir = await repository.GetInventoryReportsAsync(InventoryStartDateDt.Value, InventoryEndDateDt.Value, InventorySearchTxt.Text);
                InventoryTbl.AutoGenerateColumns = false;
                InventoryTbl.DataSource = ir;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        //PURCHASE REPORT

        public async void UpdatePurchaseReportDG()
        {
            if (string.IsNullOrWhiteSpace(PurchaseSearchTxt.Text))
            {
                PurchaseSearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable pr = await repository.GetPurchaseOrderTransactionsAsync(PurchaseStartDateDt.Value, PurchaseEndDateDt.Value, PurchaseSearchTxt.Text);
                OrdersTbl.AutoGenerateColumns = false;
                OrdersTbl.DataSource = pr;

                PurchaseOrderRepository posRepository = new PurchaseOrderRepository();
                (int delivered, int pending, int overdue, int pendingToday) = await posRepository.GetOrderStatusCountsAsync();
                DueToday.Text = pendingToday.ToString();
                TotalPendingLbl.Text = pending.ToString();
                TotalOverdueLbl.Text = overdue.ToString();
                TotalDeliveredLbl.Text = delivered.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private async void OrdersTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                DataGridViewRow row = senderGrid.Rows[e.RowIndex];

                if (row.Cells != null && row.Cells.Count > 0)
                {
                    int orderId = InputValidator.ParseToInt(row.Cells["order_id"].Value.ToString());
                    string orderDate = row.Cells["formatted_order_date"].Value.ToString();
                    string deliveryDate = row.Cells["formatted_receiving_date"].Value.ToString();
                    string remarks = row.Cells["purchase_remarks"]?.Value?.ToString();

                    PurchaseOrderRepository repository = new PurchaseOrderRepository();
                    (Supplier supplier, Cart cart, Dictionary<string, object> details) = await repository.GetOrderDetails(orderId);

                    string orderedBy = (string)details["OrderedBy"];
                    string status = (string)details["Status"];
                    string dateFulfilled = (string)details["DateFulfilled"];
                    string fulfilledBy = (string)details["FulfilledBy"];

                    if (senderGrid.Columns[e.ColumnIndex].Name == "view_details")
                    {
                        OrderDetails orderDetails = new OrderDetails(Mode.OrderView, cart, supplier, orderDate, deliveryDate, orderId.ToString(), orderedBy, status, dateFulfilled, fulfilledBy, remarks);
                        orderDetails.ShowDialog();
                    }
                }
            }
        }

        //SALES REPORT

        public async void UpdateSalesReportDG()
        {
            if (string.IsNullOrWhiteSpace(SalesSearchTxt.Text))
            {
                SalesSearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable pr = await repository.GetSalesAsync(SalesStartDateDt.Value, SalesEndDateDt.Value, SalesSearchTxt.Text);
                SalesTbl.AutoGenerateColumns = false;
                SalesTbl.DataSource = pr;
                SalesTbl.ClearSelection();

                POSRepository posRepository = new POSRepository();

                List<SalesData> salesData = await posRepository.GetSalesAsync(SalesStartDateDt.Value, SalesEndDateDt.Value);

                if (SalesStartDateDt.Value.Date == DateTime.Now.Date && SalesEndDateDt.Value.Date == DateTime.Now.Date)
                {
                    SummaryLbl.Text = $"SALES REPORT FOR TODAY";
                }
                else if (SalesStartDateDt.Value == SalesEndDateDt.Value)
                {
                    SummaryLbl.Text = $"SALES REPORT FOR {Util.ConvertDateLong(SalesStartDateDt.Value).ToUpper()}";
                }
                else
                {
                    SummaryLbl.Text = $"SALES REPORT FOR {Util.ConvertDateLong(SalesStartDateDt.Value).ToUpper()} - {Util.ConvertDateLong(SalesEndDateDt.Value).ToUpper()}";
                }
               
                ItemsSoldLbl.Text = salesData.Sum(s => s.TotalItemSold).ToString();
                SalesRevenueLbl.Text = salesData.Sum(s => s.TotalSales).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");
                ProfitLbl.Text = salesData.Sum(s => s.TotalProfit).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        //AUDIT TRAIL

        public async void UpdateAuditTrailDG()
        {
            if (string.IsNullOrWhiteSpace(AuditSearchTxt.Text))
            {
                AuditSearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable at = await repository.GetAuditTrailAsync(AuditStartDateDt.Value, AuditEndDateDt.Value, AuditSearchTxt.Text);
                AuditTrailTbl.AutoGenerateColumns = false;
                AuditTrailTbl.DataSource = at;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
