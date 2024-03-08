using BenpilsBarcodeSystem.Entities;
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
    public partial class OrderDetails : Form
    {
        bool isConfirmation;
        private Cart CurrentPurchaseCart;
        private bool canClose = false;

        internal OrderDetails(bool isConfirmation = false, Cart currentPurchaseCart = null, Supplier currentSupplier = null, string orderDate = null, string deliverDate = null)
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
                PrintBtn.Visible = true;
                CancelBtn.Visible = true;
                CancelBtn.Text = "Close";
            }

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
    }
}
