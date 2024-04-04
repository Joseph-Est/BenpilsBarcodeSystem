using BenpilsBarcodeSystem.Dialogs;
using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BenpilsBarcodeSystem
{
    public partial class Dashboard : Form
    {
        private DateTime dateFrom;
        private DateTime dateTo;
        private string selectedCb = "today";
        private List<SalesData> currentSalesData;
        List<Item> lowStockItems;
        List<PurchaseOrderEntity> purchaseOrder;

        public bool IsLoadCalled { get; set; } = false;

        public Dashboard()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            toolTip1.SetToolTip(PendingOrdersLbl, "Total pending orders");
            toolTip2.SetToolTip(OverdueOrdersLbl, "Overdue orders");
            toolTip3.SetToolTip(LowStockLbl, "Low stock tems");
            toolTip4.SetToolTip(NoStockLbl, "Out of stock items");
            toolTip4.SetToolTip(PendingTodayLbl, "Due today");
        }

        public void Dashboard_Load(object sender, EventArgs e)
        {
            if (!IsLoadCalled)
            {
                TodayCb.Checked = true;
                dateFrom = DateTime.Today;
                dateTo = DateTime.Today;

                RefreshData(dateFrom, dateTo);
                LowStockTbl.MouseWheel += LowStockTbl_MouseWheel;
                OverdueTbl.MouseWheel += OverdueTbl_MouseWheel;

                IsLoadCalled = true;
            }
        }

        public async void RefreshData(DateTime dateFrom, DateTime dateTo)
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            SuppliersRepository suppliersRepository = new SuppliersRepository();
            PurchaseOrderRepository purchaseOrderRepository = new PurchaseOrderRepository();
            POSRepository posRepository = new POSRepository();

            // Monthly Test
            //DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            //DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Tomorrow Test
            //DateTime tomorrow = DateTime.Today.AddDays(1);
            //DateTime tomorrowEnd = tomorrow.AddDays(1).AddTicks(-1);  // End of tomorrow

            currentSalesData = await posRepository.GetSalesAsync(dateFrom, dateTo);

            //foreach (var sale in currentSalesData)
            //{
            //    Console.WriteLine($"Date: {sale.Date}, Item: {sale.DisplayItemName}, Total Item Sold: {sale.TotalItemSold}, Total Sales: {sale.TotalSales}, Total Profit: {sale.TotalProfit}");
            //}
            salesChartCurrentPage = -1;

            ItemsSoldLbl.Text = currentSalesData.Sum(s => s.TotalItemSold).ToString();
            TotalSalesLbl.Text = currentSalesData.Sum(s => s.TotalSales).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");
            TotalProfitLbl.Text = currentSalesData.Sum(s => s.TotalProfit).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");

            LoadSalesChart(posRepository);
            LoadTopSellingItems(posRepository, currentSalesData);

            lowStockItems = await inventoryRepository.GetLowStockItemsAsync();

            LowStockTbl.AutoGenerateColumns = false;
            LowStockTbl.DataSource = lowStockItems;

            int noStockCount = lowStockItems.Count(item => item.Quantity == 0);
            int lowStockCount = lowStockItems.Count(item => item.Quantity > 0);

            LowStockLbl.Text = lowStockCount.ToString();
            NoStockLbl.Text = noStockCount.ToString();

            purchaseOrder = await purchaseOrderRepository.GetPendingOrdersAsync();

            OverdueTbl.AutoGenerateColumns = false;
            OverdueTbl.DataSource = purchaseOrder;

            int activeItemCount = await inventoryRepository.GetItemAcount();
            int activeSupplierCount = await suppliersRepository.GetActiveSuppliersCount();

            TotalItemsLbl.Text = activeItemCount.ToString();
            TotalSuppliersLbl.Text = activeSupplierCount.ToString();

            var (delivered, pending, overdue, pendingToday) = await purchaseOrderRepository.GetOrderStatusCountsAsync();
            OverdueOrdersLbl.Text = overdue.ToString();
            PendingOrdersLbl.Text = pending.ToString();
            PendingTodayLbl.Text = pendingToday.ToString();

            IsLoadCalled = false;
        }

        private int salesChartCurrentPage = -1;
        private int salesChartItemPerPage = 10;

        private void SalesChartNextBtn_Click(object sender, EventArgs e)
        {
            POSRepository repository = new POSRepository();
            salesChartCurrentPage++;
            LoadSalesChart(repository);
        }

        private void SalesChartPrevBtn_Click(object sender, EventArgs e)
        {
            POSRepository repository = new POSRepository();
            salesChartCurrentPage--;
            LoadSalesChart(repository);
        }

        private async void LoadSalesChart(POSRepository posRepository)
        {
            try
            {
                DataTable dt = selectedCb == "year" ? await posRepository.GetSalesChartDataAsync(currentSalesData, selectedCb, DailyRb.Checked ? "day" : "month") : await posRepository.GetSalesChartDataAsync(currentSalesData, selectedCb);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int totalItems = dt.Rows.Count;
                    int totalPages = (totalItems + salesChartItemPerPage - 1) / salesChartItemPerPage; // Calculate total pages

                    if (salesChartCurrentPage == -1) // Initialize currentPage to the last page
                    {
                        salesChartCurrentPage = totalPages - 1;
                    }


                    var pagedData = dt.AsEnumerable().Skip(salesChartCurrentPage * salesChartItemPerPage).Take(salesChartItemPerPage);

                    SalesChart.Series["Series1"].Points.DataBindXY(pagedData.Select(r => r["Date"].ToString()).ToArray(), pagedData.Select(r => r.Field<decimal>("SalesAmount")).ToArray());

                    //SalesChart.Series["Series1"].Points.DataBindXY(dt.AsEnumerable().Select(r => r["Date"].ToString()).ToArray(), dt.AsEnumerable().Select(r => r.Field<decimal>("SalesAmount")).ToArray());

                    decimal minY = pagedData.Min(r => r.Field<decimal>("SalesAmount"));
                    decimal maxY = pagedData.Max(r => r.Field<decimal>("SalesAmount"));

                    decimal padding = (maxY - minY) * 0.1m;

                    if (minY == maxY)
                    {
                        padding = 10m;
                    }

                    SalesChart.ChartAreas[0].AxisY.Minimum = (double)(minY - padding);
                    SalesChart.ChartAreas[0].AxisY.Maximum = (double)(maxY + padding);

                    SalesChartNextBtn.Enabled = salesChartCurrentPage < totalPages - 1; // "Next" button is visible if there are next pages
                    SalesChartPrevBtn.Enabled = salesChartCurrentPage > 0; // "Prev" button is visible if there are previous pages

                    PrevNextPanel.Visible = SalesChartNextBtn.Enabled || SalesChartPrevBtn.Enabled;
                }
                else
                {
                    SalesChart.Series["Series1"].Points.DataBindXY(new[] { "No Data" }, new[] { 0m });

                    SalesChart.ChartAreas[0].AxisY.Minimum = 0;
                    SalesChart.ChartAreas[0].AxisY.Maximum = 10;
                }
            }
            catch
            {
                SalesChart.Series["Series1"].Points.DataBindXY(new[] { "No Data" }, new[] { 0m });

                SalesChart.ChartAreas[0].AxisY.Minimum = 0;
                SalesChart.ChartAreas[0].AxisY.Maximum = 10;
            }

            SalesChart.ChartAreas[0].AxisX.Title = "Date";
            SalesChart.ChartAreas[0].AxisY.Title = "Sales Amount";

            SalesChart.DataBind();
        }

        private async void LoadTopSellingItems(POSRepository posRepository, List<SalesData> salesData)
        {
            try
            {
                DataTable dt = await posRepository.GetTopSellingItems(salesData);


                SellingItemsChart.Series["Series1"].Points.Clear();

                if (dt.Rows.Count > 0)
                {

                    SellingItemsChart.DataSource = dt;

                    SellingItemsChart.Series["Series1"].XValueMember = "DisplayItemName"; 
                    SellingItemsChart.Series["Series1"].YValueMembers = "TotalItemSold";   

                    SellingItemsChart.Series["Series1"].IsValueShownAsLabel = true;

                    SellingItemsChart.DataBind();
                }
                else
                {
                    SellingItemsChart.DataSource = null;
                    SellingItemsChart.DataBind();

                    SellingItemsChart.Series["Series1"].Points.DataBindXY(new[] { "No Data" }, new[] { 1 });

                    SellingItemsChart.Series["Series1"].IsValueShownAsLabel = false;

                    SellingItemsChart.ChartAreas[0].AxisY.Minimum = 0;
                    SellingItemsChart.ChartAreas[0].AxisY.Maximum = 10; 

                    SellingItemsChart.DataBind();
                }
            }
            catch
            {

            }
        }

        private void DateCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox senderCb && senderCb.Checked)
            {
                foreach (var checkbox in new[] { TodayCb, WeeklyCb, MonthlyCb, YearlyCb })
                {
                    if (checkbox != senderCb)
                    {
                        checkbox.Checked = false;
                    }
                }

               RbPanel.Visible = false;

                if (senderCb == TodayCb)
                {
                    SalesTitleLbl.Text = "Sales Today";
                    selectedCb = "today";
                    dateFrom = DateTime.Today;
                    dateTo = DateTime.Today;
                }
                else if (senderCb == WeeklyCb)
                {
                    SalesTitleLbl.Text = "Sales This Week";
                    selectedCb = "week";
                    DateTime today = DateTime.Today;
                    dateFrom = today.AddDays(-(int)today.DayOfWeek).Date;
                    dateTo = today;
                }
                else if (senderCb == MonthlyCb)
                {
                    SalesTitleLbl.Text = "Sales This Month";
                    selectedCb = "month";
                    DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                    dateFrom = firstDayOfMonth;
                    dateTo = lastDayOfMonth;
                }
                else if (senderCb == YearlyCb)
                {
                    SalesTitleLbl.Text = "Sales This Year";
                    selectedCb = "year";
                    DateTime firstDayOfYear = new DateTime(DateTime.Today.Year, 1, 1);
                    DateTime lastDayOfYear = new DateTime(DateTime.Today.Year, 12, 31);
                    dateFrom = firstDayOfYear;
                    dateTo = lastDayOfYear;
                    RbPanel.Visible = true;
                }

                RefreshData(dateFrom, dateTo);
                UpdateCheckboxStyles();
            }
        }

        private void RB_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null && radioButton.Checked)
            {
                string radioButtonName = radioButton.Name;
                if(selectedCb == "year")
                {
                    POSRepository repository = new POSRepository();
                    salesChartCurrentPage = -1;
                    switch (radioButtonName)
                    {
                        case "DailyRb":
                            LoadSalesChart(repository);
                            break;
                        case "MonthlyRb":
                            LoadSalesChart(repository);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void UpdateCheckboxStyles()
        {
            foreach (var checkbox in new[] { TodayCb, WeeklyCb, MonthlyCb, YearlyCb })
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

        private void LowStockTbl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (LowStockTbl.RowCount == 0) return; 

            if (e.Delta > 0 && LowStockTbl.FirstDisplayedScrollingRowIndex > 0)
            {
                LowStockTbl.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0 && LowStockTbl.FirstDisplayedScrollingRowIndex < LowStockTbl.RowCount - 1)
            {
                LowStockTbl.FirstDisplayedScrollingRowIndex++;
            }
        }

        private void OverdueTbl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (OverdueTbl.RowCount == 0) return;

            if (e.Delta > 0 && OverdueTbl.FirstDisplayedScrollingRowIndex > 0)
            {
                OverdueTbl.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0 && OverdueTbl.FirstDisplayedScrollingRowIndex < OverdueTbl.RowCount - 1)
            {
                OverdueTbl.FirstDisplayedScrollingRowIndex++;
            }
        }

        private void ItemsSold_Click(object sender, EventArgs e)
        {

            List<object[]> data = new List<object[]>();

            if (!currentSalesData.Any())
            {
                return;
            }

            var groupedData = currentSalesData.GroupBy(s => new { s.DisplayItemName, s.Date })
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
            int[] middleRightColumns = {};

            ShowCustomReport("Total Items Sold", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns);
        }

        private void Sales_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!currentSalesData.Any())
            {
                return;
            }

            var groupedData = currentSalesData.GroupBy(s => new { s.DisplayItemName, s.Date })
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

            string[] headers = {"Date", "Item", "Qty", "Total Sales" };
            int fillColumnIndex = 1;
            int[] middleCenterColumns = {2};
            int[] middleRightColumns = {3};

            ShowCustomReport("Total Sales", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns);
        }

        private void Profit_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!currentSalesData.Any())
            {
                return;
            }

            var groupedData = currentSalesData.GroupBy(s => new { s.DisplayItemName, s.Date })
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

            string[] headers = {"Date", "Item", "Qty", "Total Profit" };
            int fillColumnIndex = 1;
            int[] middleCenterColumns = {2};
            int[] middleRightColumns = {3};

            ShowCustomReport("Total Profit", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns);
        }

        private async void Supplier_Click(object sender, EventArgs e)
        {
            SuppliersRepository repository = new SuppliersRepository();
            DataTable dt = await repository.GetSupplierAsync(true);

            if (dt.Rows.Count == 0)
            {
                return;
            }

            List<object[]> data = new List<object[]>();

            foreach (DataRow row in dt.Rows)
            {
                data.Add(new object[]
                {
                    Util.ConvertDateLongWithTime(DateTime.Parse(row["date_created"].ToString())),
                    row["contact_name"],
                    row["contact_no"],
                });
            }

            string[] headers = { "Date Added", "Name", "Contact #" };
            int fillColumnIndex = 1;
            int[] middleCenterColumns = { 2 };
            int[] middleRightColumns = { };

            ShowCustomReport("Available Suppliers", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }

        private async void ItemsClick(object sender, EventArgs e)
        {
            InventoryRepository repository = new InventoryRepository();
            DataTable dt = await repository.GetProductsAsync(true);

            if (dt.Rows.Count == 0)
            {
                return;
            }

            List<object[]> data = new List<object[]>();

            foreach (DataRow row in dt.Rows)
            {
                data.Add(new object[]
                {
                    Util.ConvertDateLongWithTime(DateTime.Parse(row["date_created"].ToString())),
                    row["item_name"],
                    row["brand"],
                    row["size"],
                });
            }

            string[] headers = { "Date Added", "Item", "Brand", "Size"};
            int fillColumnIndex = 1;
            int[] middleCenterColumns = {3};
            int[] middleRightColumns = { };

            ShowCustomReport("Available Items", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }

        private void LowStockLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!lowStockItems.Any())
            {
                return;
            }

            foreach (var item in lowStockItems)
            {
                if (item.Quantity > 0)
                {
                    data.Add(new object[] { item.DisplayItemName, item.Quantity });
                }
            }

            string[] headers = { "Item", "Qty" };
            int fillColumnIndex = 0;

            int[] middleCenterColumns = {};
            int[] middleRightColumns = {1};

            ShowCustomReport("Low Stock Items", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }

        private void NoStockLbl_Click(object sender, EventArgs e)
        {
            List<object[]> data = new List<object[]>();

            if (!lowStockItems.Any())
            {
                return;
            }

            foreach (var item in lowStockItems)
            {
                if (item.Quantity == 0)
                {
                    data.Add(new object[] { item.DisplayItemName, item.Quantity });
                }
            }

            string[] headers = { "Item", "Qty" };
            int fillColumnIndex = 0;

            int[] middleCenterColumns = { };
            int[] middleRightColumns = { 1 };

            ShowCustomReport("Out of Stock Items", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }

        private void PendingTodayLbl_Click(object sender, EventArgs e)
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

            int[] middleCenterColumns = {0};
            int[] middleRightColumns = {};

            ShowCustomReport("Due Today", data, headers, fillColumnIndex, middleCenterColumns, middleRightColumns, false);
        }

        private void PendingOrdersLbl_Click(object sender, EventArgs e)
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

        private void OverdueOrdersLbl_Click(object sender, EventArgs e)
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
                    else if(label.Parent.BackColor == Color.FromArgb(242, 92, 92))
                    {
                        label.Parent.BackColor = Color.FromArgb(193, 57, 57);
                    }
                }
            }
        }
    }
}
