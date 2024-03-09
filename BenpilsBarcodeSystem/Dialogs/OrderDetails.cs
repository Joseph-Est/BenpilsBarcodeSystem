using BenpilsBarcodeSystem.Entities;
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

namespace BenpilsBarcodeSystem.Dialogs
{
    public partial class OrderDetails : Form
    {
        bool isConfirmation;
        private Cart CurrentPurchaseCart;
        private Supplier CurrentSupplier;
        private bool canClose = false;
        private string OrderNo {get;set;}

        internal OrderDetails(bool isConfirmation = false, Cart currentPurchaseCart = null, Supplier currentSupplier = null, string orderDate = null, string deliverDate = null, string orderNo = null, string orderedBy = null)
        {
            InitializeComponent();
            this.isConfirmation = isConfirmation;

            if (isConfirmation)
            {
                OrderNoPanel.Visible = false;
                OrderedByPanel.Visible = false;
                TitleLbl.Text = "Order Confirmation";
                ConfirmBtn.Visible = true;
                CancelBtn.Visible = true;
                CancelBtn.Text = "Cancel";
            }
            else
            {
                OrderNo = orderNo;
                OrderNoLbl.Text = orderNo;
                OrdereByLbl.Text = orderedBy;
                PrintBtn.Visible = true;
                CancelBtn.Visible = true;
                CancelBtn.Text = "Close";
            }

            CurrentSupplier = currentSupplier;
            CurrentPurchaseCart = currentPurchaseCart;
            SupplierLbl.Text = currentSupplier.ContactName;
            OrderDateLbl.Text = orderDate;
            DeliveryDateLbl.Text = deliverDate;
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

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.OK;
            this.Close();
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
    }
}
