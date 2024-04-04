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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            CategoryTbl.MouseWheel += CategoryTbl_MouseWheel;
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
            LoadCategorySalesChart(inventoryRepository, categorySalesChartCurrentPage);
            LoadBrandSalesChart(inventoryRepository, brandSalesChartCurrentPage);

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
                    if (category.Rows.Count > 8)
                    {
                        CategoryTblPanel.Visible = true;
                        category.Columns.Add("Rank", typeof(int));
                        int rank = 1;

                        foreach (DataRow row in category.Rows)
                        {
                            row["Rank"] = rank;
                            int indexOfSpace = row["Category"].ToString().IndexOf(' ');
                            if (indexOfSpace > -1)
                            {
                                row["Category"] = row["Category"].ToString().Substring(indexOfSpace + 1);
                            }
                            rank++;
                        }

                        CategoryTbl.DataBindings.Clear();
                        CategoryTbl.AutoGenerateColumns = false;
                        CategoryTbl.DataSource = category;
                    }
                    else
                    {
                        CategoryTblPanel.Visible = false;
                        CategoryDistributionChart.DataSource = category;

                        CategoryDistributionChart.Series["Series1"].XValueMember = "Category";
                        CategoryDistributionChart.Series["Series1"].YValueMembers = "Count";

                        CategoryDistributionChart.Series["Series1"].IsValueShownAsLabel = true;

                        CategoryDistributionChart.DataBind();
                    }
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

        private void CategoryTbl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (CategoryTbl.RowCount == 0) return;

            if (e.Delta > 0 && CategoryTbl.FirstDisplayedScrollingRowIndex > 0)
            {
                CategoryTbl.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0 && CategoryTbl.FirstDisplayedScrollingRowIndex < CategoryTbl.RowCount - 1)
            {
                CategoryTbl.FirstDisplayedScrollingRowIndex++;
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

        private int categorySalesChartCurrentPage = 0;
        private int categorySalesChartItemPerPage = 5;

        private async void LoadCategorySalesChart(InventoryRepository repository, int page)
        {
            try
            {
                DataTable dt = await repository.GetSalesByCategoryAsync(currentSalesData);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var pagedData = dt.AsEnumerable().Skip(page * categorySalesChartItemPerPage).Take(categorySalesChartItemPerPage);

                    SalesChart.Series["Series1"].Points.DataBindXY(pagedData.Select(r => r["Category"].ToString()).ToArray(), pagedData.Select(r => r.Field<decimal>("TotalSales")).ToArray());

                    decimal minY = pagedData.Min(r => r.Field<decimal>("TotalSales"));
                    decimal maxY = pagedData.Max(r => r.Field<decimal>("TotalSales"));

                    decimal padding = (maxY - minY) * 0.1m;

                    if (minY == maxY)
                    {
                        padding = 10m;
                    }

                    SalesChartNextBtn.Enabled = dt.Rows.Count > (page + 1) * categorySalesChartItemPerPage;
                    SalesChartPrevBtn.Enabled = page > 0;

                    SPrevNextPanel.Visible = SalesChartNextBtn.Enabled || SalesChartPrevBtn.Enabled;
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

        private void SalesChartPrevBtn_Click(object sender, EventArgs e)
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            categorySalesChartCurrentPage--;
            LoadCategorySalesChart(inventoryRepository, categorySalesChartCurrentPage);
        }

        private void SalesChartNextBtn_Click(object sender, EventArgs e)
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            categorySalesChartCurrentPage++;
            LoadCategorySalesChart(inventoryRepository, categorySalesChartCurrentPage);
        }


        private int brandSalesChartCurrentPage = 0;
        private int brandSalesChartItemPerPage = 5;

        private async void LoadBrandSalesChart(InventoryRepository repository, int page)
        {
            try
            {
                DataTable dt = await repository.GetSalesByBrandAsync(currentSalesData);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var pagedData = dt.AsEnumerable().Skip(page * brandSalesChartItemPerPage).Take(brandSalesChartItemPerPage);

                    BrandChart.Series["Series1"].Points.DataBindXY(pagedData.Select(r => r["Brand"].ToString()).ToArray(), pagedData.Select(r => r.Field<decimal>("TotalSales")).ToArray());


                    decimal minY = pagedData.Min(r => r.Field<decimal>("TotalSales"));
                    decimal maxY = pagedData.Max(r => r.Field<decimal>("TotalSales"));

                    decimal padding = (maxY - minY) * 0.1m;

                    if (minY == maxY)
                    {
                        padding = 10m;
                    }

                    BrandChart.ChartAreas[0].AxisY.Minimum = (double)(minY - padding);
                    BrandChart.ChartAreas[0].AxisY.Maximum = (double)(maxY + padding);

                    BrandChartNextBtn.Enabled = dt.Rows.Count > (page + 1) * brandSalesChartItemPerPage;
                    BrandChartPrevBtn.Enabled = page > 0;

                    BPrevNextPanel.Visible = BrandChartNextBtn.Enabled || BrandChartPrevBtn.Enabled;
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

        private void BrandChartPrevBtn_Click(object sender, EventArgs e)
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            brandSalesChartCurrentPage--;
            LoadBrandSalesChart(inventoryRepository, brandSalesChartCurrentPage);
            
        }

        private void BrandChartNextBtn_Click(object sender, EventArgs e)
        {
            InventoryRepository inventoryRepository = new InventoryRepository();
            brandSalesChartCurrentPage++;
            LoadBrandSalesChart(inventoryRepository, brandSalesChartCurrentPage);
        }
    }
}
