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
    public partial class StatisticReport : Form
    {
        private List<SalesData> currentSalesData;
        private List<SalesData> topLowestProfitMargin;
        private List<SalesData> topHighestProfitMargin;

        public StatisticReport()
        {
            InitializeComponent();
        }

        private void StatisticReport_Load(object sender, EventArgs e)
        {
            RefreshData();
            comboBox1.SelectedIndex = 0;
        }

        public async void RefreshData()
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            SuppliersRepository suppliersRepository = new SuppliersRepository();
            PurchaseOrderRepository purchaseOrderRepository = new PurchaseOrderRepository();
            POSRepository posRepository = new POSRepository();

            List<Item> lowStockItems = await inventoryRepository.GetLowStockItemsAsync();
            currentSalesData = await posRepository.GetSalesAsync(DateTime.Now, DateTime.Now, true);

            if (currentSalesData.Any())
            {
                decimal totalSales = currentSalesData.Sum(s => s.TotalSales);
                var uniqueDates = currentSalesData.Select(s => s.Date.Date).Distinct();

                int totalDays = uniqueDates.Count();
                ASalesDailyLbl.Text = (totalSales / totalDays).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");

                int totalWeeks = uniqueDates.GroupBy(d => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(d, CalendarWeekRule.FirstDay, DayOfWeek.Sunday)).Count();
                ASalesWeeklyLbl.Text = (totalSales / totalWeeks).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");

                int totalMonths = uniqueDates.GroupBy(d => d.Year * 100 + d.Month).Count();
                ASalesMonthlyLbl.Text = (totalSales / totalMonths).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");

                int totalYears = uniqueDates.Select(d => d.Year).Distinct().Count();
                ASalesYearlyLbl.Text = (totalSales / totalYears).ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");

                var itemSales = currentSalesData
                    .GroupBy(s => s.DisplayItemName)
                    .Select(g => new { Item = g.Key, TotalSold = g.Sum(s => s.TotalItemSold) })
                    .ToList();

                var bestSellingItems = itemSales.OrderByDescending(s => s.TotalSold).Take(5)
                    .Select((item, index) => new { Rank = index + 1, item.Item, item.TotalSold }).ToList();
                var leastSellingItems = itemSales.OrderBy(s => s.TotalSold).Take(5)
                    .Select((item, index) => new { Rank = index + 1, item.Item, item.TotalSold }).ToList();

                BestSellingTbl.DataBindings.Clear();
                BestSellingTbl.AutoGenerateColumns = false;
                BestSellingTbl.DataSource = bestSellingItems;

                LeastSellingTbl.DataBindings.Clear();
                LeastSellingTbl.AutoGenerateColumns = false;
                LeastSellingTbl.DataSource = leastSellingItems;
            }
            else
            {
                ASalesDailyLbl.Text = "₱ 0.00";
                ASalesWeeklyLbl.Text = "₱ 0.00";
                ASalesMonthlyLbl.Text = "₱ 0.00";
                ASalesYearlyLbl.Text = "₱ 0.00";
            }

            LoadCategoryDistribution(inventoryRepository);
            LoadBrandPopularity(inventoryRepository);
            LoadCategorySalesChart(inventoryRepository);
            LoadBrandSalesChart(inventoryRepository);

            //int activeItemCount = await inventoryRepository.GetItemAcount(false);
            //int categoryCount = await inventoryRepository.GetCategoryCount();
            decimal totalStockValue = await inventoryRepository.GetTotalStockValue(false);
            decimal potentialRevenue = await inventoryRepository.GetTotalStockValue();
            decimal projectedProfit = await inventoryRepository.GetPotentialProfit();

            //TItemsLbl.Text = activeItemCount.ToString();
            //TCategoriesLbl.Text = categoryCount.ToString();
            TStockValueLbl.Text = totalStockValue.ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");
            ProjectedSalesLbl.Text = potentialRevenue.ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");
            ProjectedProfitLbl.Text = projectedProfit.ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ");

            var (TopLowestProfitMarginItems, TopHighestProfitMarginItems) = await inventoryRepository.GetTopProfitMarginItemsAsync();

            topLowestProfitMargin = TopLowestProfitMarginItems;
            topHighestProfitMargin = TopHighestProfitMarginItems;

            updateProfitMarginDG();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateProfitMarginDG();
        }

        private void updateProfitMarginDG()
        {
            ProfitMarginTbl.DataSource = null;
            ProfitMarginTbl.AutoGenerateColumns = false;

            if (topHighestProfitMargin != null && topLowestProfitMargin != null)
            {
                var dataSource = comboBox1.SelectedIndex == 0 ? topHighestProfitMargin : topLowestProfitMargin;

                for (int i = 0; i < dataSource.Count; i++)
                {
                    dataSource[i].Rank = i + 1;
                }

                ProfitMarginTbl.DataSource = dataSource;

                ProfitMarginTbl.Columns["AvgProfitMargin"].DefaultCellStyle.Format = "P2";

                ProfitMarginTbl.Focus();
            }
        }

        private async void LoadCategoryDistribution(InventoryRepository repository)
        {
            try
            {
                DataTable category = await repository.GetItemCountByCategoryAsync();
                CategoryDistributionChart.Series["Series1"].Points.Clear();

                if (category.Rows.Count > 0)
                {
                    CategoryDistributionChart.DataSource = category;

                    CategoryDistributionChart.Series["Series1"].XValueMember = "Category";
                    CategoryDistributionChart.Series["Series1"].YValueMembers = "Count";

                    CategoryDistributionChart.Series["Series1"].IsValueShownAsLabel = true;

                    CategoryDistributionChart.DataBind();
                }
                else
                {
                    CategoryDistributionChart.DataSource = null;
                    CategoryDistributionChart.DataBind();

                    CategoryDistributionChart.Series["Series1"].Points.DataBindXY(new[] { "No Data" }, new[] { 1 });

                    CategoryDistributionChart.Series["Series1"].IsValueShownAsLabel = false;

                    CategoryDistributionChart.ChartAreas[0].AxisY.Minimum = 0;
                    CategoryDistributionChart.ChartAreas[0].AxisY.Maximum = 10;

                    CategoryDistributionChart.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private async void LoadBrandPopularity(InventoryRepository repository)
        {
            try
            {
                DataTable category = await repository.GetBrandPopularityAsync();
                BrandPopularityChart.Series["Series1"].Points.Clear();

                if (category.Rows.Count > 0)
                {
                    BrandPopularityChart.DataSource = category;

                    BrandPopularityChart.Series["Series1"].XValueMember = "Brand";
                    BrandPopularityChart.Series["Series1"].YValueMembers = "ItemCount";

                    BrandPopularityChart.Series["Series1"].IsValueShownAsLabel = true;

                    BrandPopularityChart.DataBind();
                }
                else
                {
                    BrandPopularityChart.DataSource = null;
                    BrandPopularityChart.DataBind();

                    BrandPopularityChart.Series["Series1"].Points.DataBindXY(new[] { "No Data" }, new[] { 1 });

                    BrandPopularityChart.Series["Series1"].IsValueShownAsLabel = false;

                    BrandPopularityChart.ChartAreas[0].AxisY.Minimum = 0;
                    BrandPopularityChart.ChartAreas[0].AxisY.Maximum = 10;

                    BrandPopularityChart.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void BrandPopularityChart_PostPaint(object sender, System.Windows.Forms.DataVisualization.Charting.ChartPaintEventArgs e)
        {
        }

        private async void LoadCategorySalesChart(InventoryRepository repository)
        {
            try
            {
                DataTable dt = await repository.GetSalesByCategoryAsync(currentSalesData);

                if (dt != null && dt.Rows.Count > 0)
                {
                    SalesChart.Series["Series1"].Points.DataBindXY(dt.AsEnumerable().Select(r => r["Category"].ToString()).ToArray(), dt.AsEnumerable().Select(r => r.Field<decimal>("TotalSales")).ToArray());

                    decimal minY = dt.AsEnumerable().Select(r => r.Field<decimal>("TotalSales")).Min();
                    decimal maxY = dt.AsEnumerable().Select(r => r.Field<decimal>("TotalSales")).Max();

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

            SalesChart.DataBind();
        }

        private async void LoadBrandSalesChart(InventoryRepository repository)
        {
            try
            {
                DataTable dt = await repository.GetSalesByBrandAsync(currentSalesData);

                if (dt != null && dt.Rows.Count > 0)
                {
                    BrandChart.Series["Series1"].Points.DataBindXY(dt.AsEnumerable().Select(r => r["Brand"].ToString()).ToArray(), dt.AsEnumerable().Select(r => r.Field<decimal>("TotalSales")).ToArray());

                    decimal minY = dt.AsEnumerable().Select(r => r.Field<decimal>("TotalSales")).Min();
                    decimal maxY = dt.AsEnumerable().Select(r => r.Field<decimal>("TotalSales")).Max();

                    decimal padding = (maxY - minY) * 0.1m;

                    if (minY == maxY)
                    {
                        padding = 10m;
                    }

                    BrandChart.ChartAreas[0].AxisY.Minimum = (double)(minY - padding);
                    BrandChart.ChartAreas[0].AxisY.Maximum = (double)(maxY + padding);
                }
                else
                {
                    BrandChart.Series["Series1"].Points.DataBindXY(new[] { "No Data" }, new[] { 0m });

                    BrandChart.ChartAreas[0].AxisY.Minimum = 0;
                    BrandChart.ChartAreas[0].AxisY.Maximum = 10;
                }
            }
            catch
            {
                BrandChart.Series["Series1"].Points.DataBindXY(new[] { "No Data" }, new[] { 0m });

                BrandChart.ChartAreas[0].AxisY.Minimum = 0;
                BrandChart.ChartAreas[0].AxisY.Maximum = 10;
            }

            BrandChart.DataBind();
        }
    }
}
