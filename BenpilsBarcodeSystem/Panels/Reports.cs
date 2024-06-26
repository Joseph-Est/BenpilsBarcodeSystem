﻿using BenpilsBarcodeSystem.Dialogs;
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
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
            Util.SetDateTimePickerFormat("MMM dd, yyyy", InventoryStartDateDt, InventoryEndDateDt, PurchaseEndDateDt, PurchaseStartDateDt, SalesStartDateDt, SalesEndDateDt, AuditEndDateDt, AuditStartDateDt);
            CheckToday();
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            //UpdateInventoryReportsDG();
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

        public void UpdateCurrentTable()
        {
            TabPage selectedTab = ReportsTabControl.SelectedTab;

            if (selectedTab != null)
            {
                switch (selectedTab.Name)
                {
                    case "InventoryReportTab":
                        UpdateInventoryReportsDG();
                        break;
                    case "PurchaseReportTab":
                        UpdatePurchaseReportDG();
                        break;
                    case "SalesReportTab":
                        UpdateSalesReportDG();
                        break;
                    case "AuditTrailTab":
                        UpdateAuditTrailDG();
                        break;
                    default:
                        break;
                }
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
                        inventoryPageNumber = 1;
                        break;
                    case "PurchaseStartDateDt":
                        PurchaseEndDateDt.MinDate = PurchaseStartDateDt.Value;
                        UpdatePurchaseReportDG();
                        PurchaseCancelDateCb.Visible = PurchaseStartDateDt.Value != DateTime.Today || PurchaseEndDateDt.Value != DateTime.Today;
                        prPageNumber = 1;
                        break;
                    case "SalesStartDateDt":
                        SalesEndDateDt.MinDate = SalesStartDateDt.Value;
                        UpdateSalesReportDG();
                        SalesCancelDateCb.Visible = SalesStartDateDt.Value != DateTime.Today || SalesEndDateDt.Value != DateTime.Today;
                        srPageNumber = 1;
                        break;
                    case "AuditStartDateDt":
                        AuditEndDateDt.MinDate = AuditStartDateDt.Value;
                        UpdateAuditTrailDG();
                        AuditCancelDateCb.Visible = AuditStartDateDt.Value != DateTime.Today || AuditEndDateDt.Value != DateTime.Today;
                        auditTrailPageNumber = 1;
                        break;

                }

                //UncheckActiveCb(dateTimePicker.Name);
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
                        inventoryPageNumber = 1;
                        break;
                    case "PurchaseEndDateDt":
                        UpdatePurchaseReportDG();
                        PurchaseCancelDateCb.Visible = PurchaseStartDateDt.Value != DateTime.Today || PurchaseEndDateDt.Value != DateTime.Today;
                        prPageNumber = 1;
                        break;
                    case "SalesEndDateDt":
                        UpdateSalesReportDG();
                        SalesCancelDateCb.Visible = SalesStartDateDt.Value != DateTime.Today || SalesEndDateDt.Value != DateTime.Today;
                        srPageNumber = 1;
                        break;
                    case "AuditEndDateDt":
                        UpdateAuditTrailDG();
                        AuditCancelDateCb.Visible = AuditStartDateDt.Value != DateTime.Today || AuditEndDateDt.Value != DateTime.Today;
                        auditTrailPageNumber = 1;
                        break;

                }

                //UncheckActiveCb(dateTimePicker.Name);
            }
        }

        private void DateDt_DropDown(object sender, EventArgs e)
        {
            if (sender is DateTimePicker dateTimePicker)
            {
                string senderName = dateTimePicker.Name.ToLower();
                var sets = new[]
                {
                    new { Name = "inventory", TodayCb = InventoryTodayCb, WeekCb = InventoryWeekCb, MonthCb = InventoryMonthCb, YearCb = InventoryYearCb },
                    new { Name = "purchase", TodayCb = PurchaseTodayCb, WeekCb = PurchaseWeekCb, MonthCb = PurchaseMonthCb, YearCb = PurchaseYearCb },
                    new { Name = "sales", TodayCb = SalesTodayCb, WeekCb = SalesWeekCb, MonthCb = SalesMonthCb, YearCb = SalesYearCb },
                    new { Name = "audit", TodayCb = AuditTodayCb, WeekCb = AuditWeekCb, MonthCb = AuditMonthCb, YearCb = AuditYearCb },
                };

                foreach (var set in sets)
                {
                    if (senderName.Contains(set.Name))
                    {
                        var checkboxes = new[] { set.TodayCb, set.WeekCb, set.MonthCb, set.YearCb };
                        foreach (var checkbox in checkboxes)
                        {
                            checkbox.Checked = false;
                        }
                        UpdateCheckboxStyles(set.TodayCb, set.WeekCb, set.MonthCb, set.YearCb);
                    }
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
                        InventoryTodayCb.Checked = true;
                        InventoryStartDateDt.Value = DateTime.Today;
                        InventoryEndDateDt.Value = DateTime.Today;
                        break;
                    case "PurchaseCancelDateCb":
                        PurchaseTodayCb.Checked = true;
                        PurchaseStartDateDt.Value = DateTime.Today;
                        PurchaseEndDateDt.Value = DateTime.Today;
                        break;
                    case "SalesCancelDateCb":
                        SalesTodayCb.Checked = true;
                        SalesStartDateDt.Value = DateTime.Today;
                        SalesEndDateDt.Value = DateTime.Today;
                        break;
                    case "AuditCancelDateCb":
                        AuditTodayCb.Checked = true;
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

        private void DateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox senderCb && senderCb.Checked)
            {
                var sets = new[]
                {
                    new { TodayCb = InventoryTodayCb, WeekCb = InventoryWeekCb, MonthCb = InventoryMonthCb, YearCb = InventoryYearCb, StartDateDt = InventoryStartDateDt, EndDateDt = InventoryEndDateDt, PageNumber = inventoryPageNumber },
                    new { TodayCb = PurchaseTodayCb, WeekCb = PurchaseWeekCb, MonthCb = PurchaseMonthCb, YearCb = PurchaseYearCb, StartDateDt = PurchaseStartDateDt, EndDateDt = PurchaseEndDateDt, PageNumber = prPageNumber  },
                    new { TodayCb = SalesTodayCb, WeekCb = SalesWeekCb, MonthCb = SalesMonthCb, YearCb = SalesYearCb, StartDateDt = SalesStartDateDt, EndDateDt = SalesEndDateDt , PageNumber = srPageNumber },
                    new { TodayCb = AuditTodayCb, WeekCb = AuditWeekCb, MonthCb = AuditMonthCb, YearCb = AuditYearCb, StartDateDt = AuditStartDateDt, EndDateDt = AuditEndDateDt, PageNumber = auditTrailPageNumber  },
                };

                foreach (var set in sets)
                {
                    var checkboxes = new[] { set.TodayCb, set.WeekCb, set.MonthCb, set.YearCb };
                    if (checkboxes.Contains(senderCb))
                    {
                        foreach (var checkbox in checkboxes)
                        {
                            if (checkbox != senderCb)
                            {
                                checkbox.Checked = false;
                            }
                        }

                        if (senderCb == set.TodayCb)
                        {
                            if (set.StartDateDt.Value.Date != DateTime.Today)
                            {
                                set.StartDateDt.Value = DateTime.Today;
                            }
                            if (set.EndDateDt.Value.Date != DateTime.Today)
                            {
                                set.EndDateDt.Value = DateTime.Today;
                            }
                        }
                        else if (senderCb == set.WeekCb)
                        {
                            DateTime today = DateTime.Today;
                            set.StartDateDt.Value = today.AddDays(-(int)today.DayOfWeek).Date;
                            set.EndDateDt.Value = today;
                        }
                        else if (senderCb == set.MonthCb)
                        {
                            DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                            set.StartDateDt.Value = firstDayOfMonth;
                            set.EndDateDt.Value = lastDayOfMonth;
                        }
                        else if (senderCb == set.YearCb)
                        {
                            DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                            DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);
                            set.StartDateDt.Value = firstDayOfYear;
                            set.EndDateDt.Value = lastDayOfYear;
                        }

                        UpdateCheckboxStyles(set.TodayCb, set.WeekCb, set.MonthCb, set.YearCb);
                    }
                }
            }
        }

        private void UpdateCheckboxStyles(params CheckBox[] checkboxes)
        {
            foreach (var checkbox in checkboxes)
            {
                if (checkbox.Checked)
                {
                    checkbox.BackColor = Color.FromArgb(40, 40, 40);
                    checkbox.ForeColor = Color.White;
                }
                else
                {
                    checkbox.BackColor = SystemColors.Control;
                    checkbox.ForeColor = Color.Black;
                }
            }
        }

        private void CheckToday()
        {
            InventoryTodayCb.Checked = true;
            PurchaseTodayCb.Checked = true;
            SalesTodayCb.Checked = true;
            AuditTodayCb.Checked = true;
        }
        public void ShowCustomReport(string title, List<object[]> data, string[] headers, int fillColumnIndex, int[] middleCenterColumns, int[] middleRightColumns, bool countTotal = true)
        {
            TableDialog dialog = new TableDialog(title, data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, countTotal);
            dialog.ShowDialog();
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                if (panel.Parent != null)
                {
                    panel.Parent.BackColor = Color.FromArgb(60, 60, 60);
                }
            }
            else if (sender is Label label)
            {
                if (label.Parent != null)
                {
                    if (label.Parent.BackColor == Color.White)
                    {
                        label.Parent.BackColor = Color.LightGray;
                    }
                    else if (label.Parent.BackColor == Color.FromArgb(40, 40, 40))
                    {
                        label.Parent.BackColor = Color.FromArgb(60, 60, 60);
                    }
                    else if (label.Parent.BackColor == Color.FromArgb(193, 57, 57))
                    {
                        label.Parent.BackColor = Color.FromArgb(242, 92, 92);
                    }
                }
            }
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                if (panel.Parent != null)
                {
                    panel.Parent.BackColor = Color.FromArgb(40, 40, 40);
                }
            }
            else if (sender is Label label)
            {
                if (label.Parent != null)
                {
                    if (label.Parent.BackColor == Color.LightGray)
                    {
                        label.Parent.BackColor = Color.White;
                    }
                    else if (label.Parent.BackColor == Color.FromArgb(60, 60, 60))
                    {
                        label.Parent.BackColor = Color.FromArgb(40, 40, 40);
                    }
                    else if (label.Parent.BackColor == Color.FromArgb(242, 92, 92))
                    {
                        label.Parent.BackColor = Color.FromArgb(193, 57, 57);
                    }
                }
            }
        }


        //INVENTORY REPORT

        private int inventoryPageNumber = 1;
        private readonly int inventoryPageSize = 30;

        public async void UpdateInventoryReportsDG()
        {
            if (string.IsNullOrWhiteSpace(InventorySearchTxt.Text))
            {
                InventorySearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable ir = await repository.GetInventoryReportsAsync(InventoryStartDateDt.Value, InventoryEndDateDt.Value, InventorySearchTxt.Text, inventoryPageNumber, inventoryPageSize);

                InventoryTbl.AutoGenerateColumns = false;
                InventoryTbl.DataSource = ir;

                int totalRecords = await repository.GetInventoryReportCountAsync(InventoryStartDateDt.Value, InventoryEndDateDt.Value, InventorySearchTxt.Text);
                int totalPages = (int)Math.Ceiling((double)totalRecords / inventoryPageSize);

                InventoryPageLbl.Text = $"{inventoryPageNumber} | {totalPages}";

                InventoryNextBtn.Enabled = inventoryPageNumber < totalPages;
                InventoryPrevBtn.Enabled = inventoryPageNumber > 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

       
        private void InventoryNextBtn_Click(object sender, EventArgs e)
        {
            inventoryPageNumber++;
            UpdateInventoryReportsDG();
        }

        private void InventoryPrevBtn_Click(object sender, EventArgs e)
        {
            if (inventoryPageNumber > 1)
            {
                inventoryPageNumber--;
                UpdateInventoryReportsDG();
            }
        }

        //PURCHASE REPORT

        private int prPageNumber = 1;
        private readonly int prPageSize = 30;
        private List<PurchaseOrderEntity> purchaseOrder;
        private List<PurchaseOrderEntity> deliveredPurchaseOrder;

        public async void UpdatePurchaseReportDG()
        {
            if (string.IsNullOrWhiteSpace(PurchaseSearchTxt.Text))
            {
                PurchaseSearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable pr = await repository.GetPurchaseOrderTransactionsAsync(PurchaseStartDateDt.Value, PurchaseEndDateDt.Value, PurchaseSearchTxt.Text, prPageNumber, prPageSize);
                OrdersTbl.AutoGenerateColumns = false;
                OrdersTbl.DataSource = pr;

                int totalRecords = await repository.GetPurchaseOrderTransactionsCountAsync(PurchaseStartDateDt.Value, PurchaseEndDateDt.Value, PurchaseSearchTxt.Text);
                int totalPages = (int)Math.Ceiling((double)totalRecords / prPageSize);

                PRPageLbl.Text = $"{prPageNumber} | {totalPages}";

                PRNextBtn.Enabled = prPageNumber < totalPages;
                PRPrevBtn.Enabled = prPageNumber > 1;

                PurchaseOrderRepository purchaseOrderRepository = new PurchaseOrderRepository();
                purchaseOrder = await purchaseOrderRepository.GetPendingOrdersAsync();
                deliveredPurchaseOrder = await purchaseOrderRepository.GetDeliveredOrdersAsync();
                (int delivered, int pending, int overdue, int pendingToday) = await purchaseOrderRepository.GetOrderStatusCountsAsync();
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
        private void DueToday_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!purchaseOrder.Any())
            {
                return;
            }

            foreach (var order in purchaseOrder)
            {
                if (order.DeliveryStatus.ToLower().Equals("today"))
                {
                    data.Add(new object[] { order.OrderId, order.SupplierName, order.OrderDate, order.DeliveryDate });
                }
            }

            if (!data.Any())
            {
                return;
            }

            string[] headers = { "Order ID", "Supplier", "Order Date", "Delivery Date" };
            int fillColumnIndex = 1;

            int[] middleCenterColumns = { 0 };
            int[] middleRightColumns = { };

            ShowCustomReport("Due Today", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }
        private void TotalPendingLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!purchaseOrder.Any())
            {
                return;
            }

            foreach (var order in purchaseOrder)
            {
                data.Add(new object[] { order.OrderId, order.SupplierName, order.OrderDate, order.DeliveryDate });
            }

            if (!data.Any())
            {
                return;
            }

            string[] headers = { "Order ID", "Supplier", "Order Date", "Delivery Date" };
            int fillColumnIndex = 1;

            int[] middleCenterColumns = { 0 };
            int[] middleRightColumns = { };

            ShowCustomReport("Pending Orders", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }
        private void TotalOverdueLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!purchaseOrder.Any())
            {
                return;
            }

            foreach (var order in purchaseOrder)
            {
                if (order.DeliveryStatus.ToLower().Contains("overdue") || order.DeliveryStatus.ToLower().Contains("yesterday"))
                {
                    data.Add(new object[] { order.OrderId, order.SupplierName, order.OrderDate, order.DeliveryDate });
                }
            }

            if (!data.Any())
            {
                return;
            }

            string[] headers = { "Order ID", "Supplier", "Order Date", "Delivery Date" };
            int fillColumnIndex = 1;

            int[] middleCenterColumns = { 0 };
            int[] middleRightColumns = { };

            ShowCustomReport("Overdue Orders", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }

        private void TotalDeliveredLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!deliveredPurchaseOrder.Any())
            {
                return;
            }

            foreach (var order in deliveredPurchaseOrder)
            {
                data.Add(new object[] { order.OrderId, order.SupplierName, order.OrderDate, order.DeliveryDate });              
            }

            if (!data.Any())
            {
                return;
            }

            string[] headers = { "Order ID", "Supplier", "Order Date", "Delivery Date" };
            int fillColumnIndex = 1;

            int[] middleCenterColumns = { 0 };
            int[] middleRightColumns = { };

            ShowCustomReport("Total Delivered", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }

        private void PRNextBtn_Click(object sender, EventArgs e)
        {
            prPageNumber++;
            UpdatePurchaseReportDG();
        }

        private void PRPrevBtn_Click(object sender, EventArgs e)
        {
            if (prPageNumber > 1)
            {
                prPageNumber--;
                UpdatePurchaseReportDG();
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
                        OrderDetails orderDetails = new OrderDetails(Mode.OrderView, cart, supplier, orderDate, deliveryDate, orderId.ToString(), orderedBy, status, dateFulfilled, fulfilledBy, remarks, false, false);
                        orderDetails.ShowDialog();
                    }
                }
            }
        }

        //SALES REPORT

        private int srPageNumber = 1;
        private readonly int srPageSize = 30;
        private List<SalesData> salesData;

        public async void UpdateSalesReportDG()
        {
            if (string.IsNullOrWhiteSpace(SalesSearchTxt.Text))
            {
                SalesSearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable pr = await repository.GetSalesAsync(SalesStartDateDt.Value, SalesEndDateDt.Value, SalesSearchTxt.Text, srPageNumber, srPageSize);
                SalesTbl.AutoGenerateColumns = false;
                SalesTbl.DataSource = pr;
                SalesTbl.ClearSelection();

                int totalRecords = await repository.GetSalesCountAsync(SalesStartDateDt.Value, SalesEndDateDt.Value, SalesSearchTxt.Text);
                int totalPages = (int)Math.Ceiling((double)totalRecords / srPageSize);

                SrPageLbl.Text = $"{srPageNumber} | {totalPages}";

                SRNxtBtn.Enabled = srPageNumber < totalPages;
                SRPrevBtn.Enabled = srPageNumber > 1;

                POSRepository posRepository = new POSRepository();

                salesData = await posRepository.GetSalesAsync(SalesStartDateDt.Value, SalesEndDateDt.Value);

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

        private void ItemsSoldLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!salesData.Any())
            {
                return;
            }

            var groupedData = salesData.GroupBy(s => new { s.DisplayItemName, s.Date })
                                              .Select(g => new
                                              {
                                                  Date = Util.ConvertDateLongWithTime(g.Key.Date),
                                                  Item = g.Key.DisplayItemName,
                                                  Quantity = g.Sum(s => s.TotalItemSold)
                                              })
                                              .OrderByDescending(g => g.Date);

            foreach (var item in groupedData)
            {
                data.Add(new object[] { item.Date, item.Item, item.Quantity });
            }

            string[] headers = { "Date", "Item", "Qty" };
            int fillColumnIndex = 1;
            int[] middleCenterColumns = { 2 };
            int[] middleRightColumns = { };

            ShowCustomReport("Total Items Sold", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns);
        }

        private void SalesRevenueLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!salesData.Any())
            {
                return;
            }

            var groupedData = salesData.GroupBy(s => new { s.DisplayItemName, s.Date })
                                              .Select(g => new
                                              {
                                                  Date = Util.ConvertDateLongWithTime(g.Key.Date),
                                                  Item = g.Key.DisplayItemName,
                                                  Amount = g.Sum(s => s.TotalSales),
                                                  Quantity = g.Sum(s => s.TotalItemSold)
                                              })
                                              .OrderByDescending(g => g.Date);

            foreach (var item in groupedData)
            {
                data.Add(new object[] { item.Date, item.Item, item.Quantity, item.Amount });
            }

            string[] headers = { "Date", "Item", "Qty", "Total Sales" };
            int fillColumnIndex = 1;
            int[] middleCenterColumns = { 2 };
            int[] middleRightColumns = { 3 };

            ShowCustomReport("Total Sales", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns);
        }
        private void ProfitLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!salesData.Any())
            {
                return;
            }

            var groupedData = salesData.GroupBy(s => new { s.DisplayItemName, s.Date })
                                              .Select(g => new
                                              {
                                                  Date = Util.ConvertDateLongWithTime(g.Key.Date),
                                                  Item = g.Key.DisplayItemName,
                                                  Profit = g.Sum(s => s.TotalProfit),
                                                  Quantity = g.Sum(s => s.TotalItemSold)
                                              })
                                              .OrderByDescending(g => g.Date);
            foreach (var item in groupedData)
            {
                data.Add(new object[] { item.Date, item.Item, item.Quantity, item.Profit });
            }

            string[] headers = { "Date", "Item", "Qty", "Total Profit" };
            int fillColumnIndex = 1;
            int[] middleCenterColumns = { 2 };
            int[] middleRightColumns = { 3 };

            ShowCustomReport("Total Profit", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns);
        }
        private void SRNxtBtn_Click(object sender, EventArgs e)
        {
            srPageNumber++;
            UpdateSalesReportDG();
        }

        private void SRPrevBtn_Click(object sender, EventArgs e)
        {
            if (srPageNumber > 1)
            {
                srPageNumber--;
                UpdateSalesReportDG();
            }
        }

        private async void SalesTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                DataGridViewRow row = senderGrid.Rows[e.RowIndex];

                if (row.Cells != null && row.Cells.Count > 0)
                {
                    if (senderGrid.Columns[e.ColumnIndex].Name == "generate_receipt")
                    {
                        string transactionId = row.Cells["transaction_id"].Value.ToString();

                        POSRepository repository = new POSRepository();
                        (Cart cart, decimal paymentReceived, decimal discount, string salesPerson, string transactionDate) = await repository.GetSalesDetailsAsync(transactionId);

                        GenerateReceipt(transactionId, cart, salesPerson, paymentReceived, discount, transactionDate);
                        
                    }
                }
            }
        }

        private void GenerateReceipt(string transactionNo, Cart currentCart, string salesPerson, decimal paymentReceived, decimal discount, string transactionDate)
        {
            if (currentCart != null)
            {
                int itemRowCount = currentCart.Items.Count;
                int paperHeight = 1000;

                if (itemRowCount > 10)
                {
                    paperHeight += (itemRowCount - 10) * 20;
                }

                PrintDocument pd = new PrintDocument();

                pd.PrintPage += (sender, e) => PrintDocument_PrintPage(sender, e, transactionNo, currentCart, salesPerson, paymentReceived, discount, transactionDate);

                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 315, paperHeight);

                PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
                ppd.ShowDialog();
            }
        }

        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e, string transactionNo, Cart currentCart, string salesPerson, decimal paymentReceived, decimal discount, string transactionDate)
        {
            //Bitmap bitmap = new Bitmap(315, 1000);

            Graphics graphics = e.Graphics;

            string TransactionNo = $"Trx No. {transactionNo}";

            string[] products = currentCart.GetProductNames();
            decimal[] prices = currentCart.GetPrices();

            decimal change = paymentReceived - (currentCart.GetTotalPrice() - discount);

            Util.PrintReceipt(graphics, TransactionNo, products, prices, currentCart.GetTotalPrice() - discount, paymentReceived, change, discount, salesPerson, null, null, null, transactionDate);

            //bitmap.Save("receipt.png", ImageFormat.Png);
        }

        //AUDIT TRAIL

        private int auditTrailPageNumber = 1;
        private readonly int auditTrailPageSize = 30;

        public async void UpdateAuditTrailDG()
        {
            if (string.IsNullOrWhiteSpace(AuditSearchTxt.Text))
            {
                AuditSearchTxt.Text = "";
            }

            try
            {
                ReportsRepository repository = new ReportsRepository();
                DataTable at = await repository.GetAuditTrailAsync(AuditStartDateDt.Value, AuditEndDateDt.Value, AuditSearchTxt.Text, auditTrailPageNumber, auditTrailPageSize);
                AuditTrailTbl.AutoGenerateColumns = false;
                AuditTrailTbl.DataSource = at;

                int totalRecords = await repository.GetAuditTrailCountAsync(AuditStartDateDt.Value, AuditEndDateDt.Value, AuditSearchTxt.Text);
                int totalPages = (int)Math.Ceiling((double)totalRecords / auditTrailPageSize);

                AuditTrailPageNumberLbl.Text = $"{auditTrailPageNumber} | {totalPages}";

                AuditTrailNextPage.Enabled = auditTrailPageNumber < totalPages;
                AuditTrailPrevPage.Enabled = auditTrailPageNumber > 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void SearchTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter;
        }

        private void AuditTrailNextPage_Click(object sender, EventArgs e)
        {
            auditTrailPageNumber++;
            UpdateAuditTrailDG();
        }   

        private void AuditTrailPrevPage_Click(object sender, EventArgs e)
        {
            if (auditTrailPageNumber > 1)
            {
                auditTrailPageNumber--;
                UpdateAuditTrailDG();
            }
        }

        private class CustomFlatButton : Button
        {
            protected override void OnPaint(PaintEventArgs pevent)
            {
                if (!this.Enabled)
                {
                    TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, Color.Red); // Change Color.Red to your desired color
                }
                else
                {
                    base.OnPaint(pevent);
                }
            }
        }

        private void InventoryCancelDateCb_CheckedChanged(object sender, EventArgs e)
        {

        }

       
    }
}
