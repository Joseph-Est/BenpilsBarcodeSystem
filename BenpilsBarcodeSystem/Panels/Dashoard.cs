using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
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

namespace BenpilsBarcodeSystem
{
    public partial class Dashboard : Form
    {
        private DateTime dateFrom;
        private DateTime dateTo;
        private string selectedCb = "today";

        public Dashboard()
        {
            InitializeComponent();

            toolTip1.SetToolTip(PendingOrdersLbl, "Total pending orders");
            toolTip2.SetToolTip(OverdueOrdersLbl, "Overdue orders");
            toolTip3.SetToolTip(LowStockLbl, "Low stock tems");
            toolTip4.SetToolTip(NoStockLbl, "Out of stock items");
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            TodayCb.Checked = true;
            dateFrom = DateTime.Today;
            dateTo = DateTime.Today;

            RefreshData(dateFrom, dateTo);
            LowStockTbl.MouseWheel += LowStockTbl_MouseWheel;
            OverdueTbl.MouseWheel += OverdueTbl_MouseWheel;
        }

        private async void RefreshData(DateTime dateFrom, DateTime dateTo)
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

            List<SalesData> salesData = await posRepository.GetSalesAsync(dateFrom, dateTo);

            ItemsSoldLbl.Text = salesData.Sum(s => s.TotalItemSold).ToString();
            SalesRevenueLbl.Text = salesData.Sum(s => s.TotalSales).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");
            TotalProfitLbl.Text = salesData.Sum(s => s.TotalProfit).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");

            LoadSalesChart(posRepository, salesData);
            LoadTopSellingItems(posRepository, salesData);

            List<Item> lowStockItems = await inventoryRepository.GetLowStockItemsAsync();

            LowStockTbl.AutoGenerateColumns = false;
            LowStockTbl.DataSource = lowStockItems;

            int noStockCount = lowStockItems.Count(item => item.Quantity == 0);
            int lowStockCount = lowStockItems.Count(item => item.Quantity > 0);

            LowStockLbl.Text = lowStockCount.ToString();
            NoStockLbl.Text = noStockCount.ToString();

            List<PurchaseOrderEntity> overduePurchaseOrder = await purchaseOrderRepository.GetPendingOrdersAsync();

            OverdueTbl.AutoGenerateColumns = false;
            OverdueTbl.DataSource = overduePurchaseOrder;

            int activeItemCount = await inventoryRepository.GetActiveItemsCount();
            int activeSupplierCount = await suppliersRepository.GetActiveSuppliersCount();

            TotalItemsLbl.Text = activeItemCount.ToString();
            TotalSuppliersLbl.Text = activeSupplierCount.ToString();

            var (TotalPending, TotalOverdue) = await purchaseOrderRepository.GetPendingStatusCountsAsync();
            OverdueOrdersLbl.Text = TotalOverdue.ToString();
            PendingOrdersLbl.Text = TotalPending.ToString();
        }

        private async void LoadSalesChart(POSRepository posRepository, List<SalesData> salesData)
        {
            try
            {
                DataTable dt = await posRepository.GetSalesChartDataAsync(salesData, selectedCb);

                if (dt != null && dt.Rows.Count > 0)
                {
                    SalesChart.Series["Series1"].Points.DataBindXY(dt.AsEnumerable().Select(r => r["Date"].ToString()).ToArray(), dt.AsEnumerable().Select(r => r.Field<decimal>("SalesAmount")).ToArray());

                    decimal minY = dt.AsEnumerable().Select(r => r.Field<decimal>("SalesAmount")).Min();
                    decimal maxY = dt.AsEnumerable().Select(r => r.Field<decimal>("SalesAmount")).Max();

                    decimal padding = (maxY - minY) * 0.1m;

                    if (minY == maxY)
                    {
                        padding = 10m;
                    }

                    SalesChart.ChartAreas[0].AxisY.Minimum = (double)(minY - padding);
                    SalesChart.ChartAreas[0].AxisY.Maximum = (double)(maxY + padding);
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
                }

                RefreshData(dateFrom, dateTo);
                UpdateCheckboxStyles();
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

        }

        private void Sales_Click(object sender, EventArgs e)
        {

        }

        private void Profit_Click(object sender, EventArgs e)
        {

        }

        private void Supplier_Click(object sender, EventArgs e)
        {

        }

        private void ItemsClick(object sender, EventArgs e)
        {

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
