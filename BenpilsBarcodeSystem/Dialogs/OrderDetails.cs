using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace BenpilsBarcodeSystem.Dialogs
{
    enum Mode
    {
        OrderConfirmation = 1,
        OrderCompletion = 2,
        OrderView = 3
    }

    public partial class OrderDetails : Form
    {
        private readonly Mode mode;
        private readonly Cart CurrentPurchaseCart;
        private readonly Supplier CurrentSupplier;
        private bool canClose = false;
        private string OrderNo { get; set; }

        internal OrderDetails(Mode mode = Mode.OrderConfirmation, Cart currentPurchaseCart = null, Supplier currentSupplier = null, string orderDate = null,
                            string deliverDate = null, string orderNo = null, string orderedBy = null, string status = null, string dateFulfilled = null,
                            string fulfilledBy = null, string remarks = null, bool isBackOrder = false)
        {
            InitializeComponent();

            this.mode = mode;
            OrderNo = orderNo;

            CurrentSupplier = currentSupplier;
            CurrentPurchaseCart = currentPurchaseCart;
            SupplierLbl.Text = currentSupplier?.ContactName;
            OrderDateLbl.Text = orderDate;
            DeliveryDateLbl.Text = deliverDate;

            switch (mode)
            {
                case Mode.OrderConfirmation:
                    SetOrderConfirmationMode();
                    break;
                case Mode.OrderCompletion:
                    SetOrderCompletionMode(orderedBy, status);
                    break;
                case Mode.OrderView:
                    SetOrderViewMode(fulfilledBy, dateFulfilled, remarks, orderedBy, status);
                    break;
            }
        }

        private void SetOrderConfirmationMode()
        {
            OrderNoPanel.Visible = false;
            OrderedByPanel.Visible = false;
            StatusPanel.Visible = false;
            DateFulfilledPanel.Visible = false;
            FulfilledByPanel.Visible = false;
            RemarksPanel.Visible = false;

            ConfirmBtn.Visible = true;
            CancelBtn.Visible = true;

            TitleLbl.Text = "Order Confirmation";
        }

        private void SetOrderCompletionMode(string orderedBy, string status)
        {
            DateFulfilledPanel.Visible = false;
            FulfilledByPanel.Visible = false;
            RemarksPanel.Visible = false;

            OrderNoLbl.Text = OrderNo;
            OrderedByLbl.Text = orderedBy;
            StatusLbl.Text = status;

            ConfirmBtn.Visible = true;
            CancelBtn.Visible = true;
            ConfirmBtn.Text = "Complete Order";
            CancelBtn.Text = "Close";

            foreach (var item in CurrentPurchaseCart.Items)
            {
                item.ReceivedQuantity = item.Quantity;
            }

            ItemsTbl.Columns["ReceivedQuantity"].Visible = true;

            TitleLbl.Text = "Order Completion";
        }

        private void SetOrderViewMode(string fulfilledBy, string dateFulfilled, string remarks, string orderedBy, string status)
        {
            FulfilledByLbl.Text = fulfilledBy;
            DateFulfilledLbl.Text = dateFulfilled;
            RemarksLbl.Text = remarks;

            OrderNoLbl.Text = OrderNo;
            OrderedByLbl.Text = orderedBy;
            StatusLbl.Text = status;

            PrintBtn.Visible = true;
            CancelBtn.Visible = true;
            ConfirmBtn.Visible = true;
            FulfilledByPanel.Visible = fulfilledBy != null;
            DateFulfilledPanel.Visible = fulfilledBy != null;
            RemarksPanel.Visible = remarks != null;

            CancelBtn.Text = "Close";
            ConfirmBtn.Text = "Cancel Order";
            TitleLbl.Text = "Order Details";
        }

        private void OrderDetails_Load(object sender, EventArgs e)
        {
            ItemsTbl.AutoGenerateColumns = false;
            ItemsTbl.DataSource = CurrentPurchaseCart.Items;
            ItemsTbl.Refresh();

            TotalLbl.Text = CurrentPurchaseCart.GetTotalAmountAsString();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private async void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (mode == Mode.OrderCompletion)
            {
                PurchaseOrderRepository repository = new PurchaseOrderRepository();

                if (CurrentPurchaseCart.AreAllQuantitiesReceived())
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure all the items is delivered exactly, and you want to complete this order?", "Confirm Order Completion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (await repository.CompletePurchaseOrderAsync(InputValidator.ParseToInt(OrderNo), CurrentUser.User.iD, PurchaseOrderRepository.delivered_status, repository.remarks_complete_delivery, CurrentPurchaseCart, null))
                        {
                            MessageBox.Show("The purchase order has been successfully completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            canClose = true;
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("An unexpected error occurred. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Some items are still missing from this order. If you proceed to complete this order now, a new one will automatically be created for the missing items. Are you sure you want to complete this order?", "Confirm Order Completion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DateDialog dateDialog = new DateDialog();

                        if (dateDialog.ShowDialog() == DialogResult.OK)
                        {
                            DateTime receivingDate = dateDialog.receivingDate;
                            if (await repository.CompletePurchaseOrderAsync(InputValidator.ParseToInt(OrderNo), CurrentUser.User.iD, PurchaseOrderRepository.partially_delivered_status, repository.remarks_partially_delivered, CurrentPurchaseCart, receivingDate))
                            {
                                MessageBox.Show("The order has been successfully completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                canClose = true;
                                DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("An unexpected error occurred. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }else if(mode == Mode.OrderView)
            {
                PurchaseOrderRepository repository = new PurchaseOrderRepository();

                ConfirmationWithRemarks confirmation = new ConfirmationWithRemarks("Confirm Order Cancellation", $"Are you sure you want to cancel this order? Once cancelled, this action cannot be undone.");
                DialogResult result = confirmation.ShowDialog();

                if (result == DialogResult.OK)
                {
                    if (await repository.CancelPurchaseOrderAsync(InputValidator.ParseToInt(OrderNoLbl.Text), confirmation.Remarks))
                    {
                        MessageBox.Show("The order has been successfully cancelled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        canClose = true;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("The operation to cancel the order failed. Please try again later.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                canClose = true;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void OrderDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap bitmap = new Bitmap(315, 1000);

            Graphics graphics = e.Graphics;
            string transactionNo = $"Order No. {OrderNo}";
            

            string[] products = CurrentPurchaseCart.GetProductNames();
            decimal[] prices = CurrentPurchaseCart.GetAmounts();

            Util.PrintReceipt(graphics, transactionNo, products, prices, CurrentPurchaseCart.GetTotalAmount(), 0, 0, CurrentSupplier.ContactName, DeliveryDateLbl.Text);

            //bitmap.Save("receipt.png", ImageFormat.Png);
        }

        private void PrintReceipt()
        {
            PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 315, 1000);

            PrintPreview.Document = PrintDocument;
            PrintPreview.ShowDialog();
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            PrintReceipt();
        }

        private void ItemsTbl_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl tb && ItemsTbl.CurrentCell.OwningColumn.Name == "ReceivedQuantity" && ItemsTbl.Columns["ReceivedQuantity"].Visible)
            {
                int maxQuantity = Convert.ToInt32(ItemsTbl.CurrentRow.Cells["Quantity"].Value);
                InputValidator.DGAllowOnlyDigitsMinMax(tb, 0, maxQuantity);
            }
        }

        private void ItemsTbl_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (ItemsTbl.Columns[e.ColumnIndex].Name == "ReceivedQuantity" && ItemsTbl.Columns["ReceivedQuantity"].Visible)
            {
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    e.Cancel = true;
                }
                else
                {
                    ItemsTbl.Rows[e.RowIndex].ErrorText = string.Empty;
                }
            }
        }

        private void ItemsTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ItemsTbl.Columns["ReceivedQuantity"].Visible)
            {
                ItemsTbl.CurrentCell = ItemsTbl.Rows[e.RowIndex].Cells["ReceivedQuantity"];
                ItemsTbl.BeginEdit(true);
            }
        }
    }
}
