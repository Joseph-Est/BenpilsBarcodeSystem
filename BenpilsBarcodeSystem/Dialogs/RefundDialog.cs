using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Dialogs
{
    public partial class RefundDialog : Form
    {
        bool canClose = false;
        string selectedID;

        public RefundDialog()
        {
            InitializeComponent();
        }

        private void RefundDialog_Load(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private async void UpdateDataGridView()
        {
            if (string.IsNullOrWhiteSpace(SearchTxt.Text))
            {
                SearchTxt.Text = "";
            }

            try
            {
                POSRepository repository = new POSRepository();
                DataTable transactionsDT = await repository.GetTransactionsAsync(SearchTxt.Text);

                TransactionsTbl.AutoGenerateColumns = false;
                TransactionsTbl.DataSource = transactionsDT;
                TransactionsTbl.ClearSelection();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void RefundDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
        }

        private async void TransactionsTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name == "refund")
                {
                    selectedID = senderGrid.Rows[e.RowIndex].Cells["transaction_id"].Value.ToString();
                    POSRepository repository = new POSRepository();
                    (Cart cart, decimal paymentReceived, string salesPerson, string transactionDate) = await repository.GetSalesDetailsAsync(selectedID);

                    TransactionDetails td = new TransactionDetails(transactionDate, selectedID, salesPerson, cart);

                    if (td.ShowDialog() == DialogResult.OK)
                    {
                        UpdateDataGridView();
                    }
                }
            }
        }
    }
}
