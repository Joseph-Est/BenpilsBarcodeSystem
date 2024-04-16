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
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                        break;
                    case "PurchaseCancelDateCb":
                        PurchaseTodayCb.Checked = true;
                        break;
                    case "SalesCancelDateCb":
                        SalesTodayCb.Checked = true;
                        break;
                    case "AuditCancelDateCb":
                        AuditTodayCb.Checked = true;
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
                Console.WriteLine("dumaan naman");
                ReportsRepository repository = new ReportsRepository();
                DataTable ir = await repository.GetInventoryReportsAsync(InventoryStartDateDt.Value, InventoryEndDateDt.Value, InventorySearchTxt.Text, inventoryPageNumber, inventoryPageSize);

                foreach (DataRow row in ir.Rows)
                {
                    Console.WriteLine(string.Join(", ", row.ItemArray));
                }

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

                int totalRecords = await repository.GetPurchaseOrderTransactionsCountAsync(SalesStartDateDt.Value, SalesEndDateDt.Value, SalesSearchTxt.Text);
                int totalPages = (int)Math.Ceiling((double)totalRecords / srPageSize);

                SrPageLbl.Text = $"{srPageNumber} | {totalPages}";

                SRNxtBtn.Enabled = srPageNumber < totalPages;
                SRPrevBtn.Enabled = srPageNumber > 1;

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

        private void SRNxtBtn_Click(object sender, EventArgs e)
        {
            srPageNumber++;
            UpdateAuditTrailDG();
        }

        private void SRPrevBtn_Click(object sender, EventArgs e)
        {
            if (srPageNumber > 1)
            {
                srPageNumber--;
                UpdateAuditTrailDG();
            }
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
    }
}
