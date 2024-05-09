using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Repository;
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
    public partial class TransactionDetails : Form
    {
        private readonly Cart CurrentCart;
        private bool canClose = false;
        private readonly string transactionId;

        internal TransactionDetails(string transactionDate, string transactionId, string salesperson, Cart CurrentCart)
        {
            InitializeComponent();
            this.CurrentCart = CurrentCart;
            this.transactionId = transactionId;
            TransactionIdLbl.Text = transactionId;
            TransactionDateLbl.Text = transactionDate;
            SalespersonLbl.Text = salesperson;

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TransactionDetails_Load(object sender, EventArgs e)
        {
            ItemsTbl.AutoGenerateColumns = false;
            ItemsTbl.DataSource = CurrentCart.Items;
            ItemsTbl.Refresh();

            TotalLbl.Text = CurrentCart.GetTotalPriceAsString();
        }

        private void TransactionDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private async void ConfirmBtn_Click(object sender, EventArgs e)
        {
            string designation = CurrentUser.User.Designation.ToLower();
            bool allowConfirm = true;
            if (designation != "admin" && designation != "super admin")
            {
                AuthorizationDialog ad = new AuthorizationDialog();
                if (ad.ShowDialog() == DialogResult.OK)
                {
                    allowConfirm = true;
                }
                else
                {
                    allowConfirm = false;
                }
            }

            if (allowConfirm)
            {
                POSRepository repository = new POSRepository();

                switch (await repository.RefundTransactionAsync(transactionId, CurrentCart))
                {
                    case 0:
                        MessageBox.Show("The refund process has been successfully completed!.", "Refund Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        canClose = true;
                        DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case 1:
                        MessageBox.Show("Refund operation could not be completed because some items are currently archived.", "Refund Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        MessageBox.Show("Something went wrong, please contact administrator.", "Refund Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Refund operation could not be completed. Authorization failed.", "Refund Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
