using BenpilsBarcodeSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class ReduceStockForm : Form
    {
        private bool canClose = false;
        public int SelectedId { get; private set; }
        public int AmountToDeduct { get; private set; }
        public string Reason { get; private set; }
        private int StockQuantity { get; set; }
        public string itemName { get; set; }

        public ReduceStockForm(int selectedId, string itemName, string size, int quantity)
        {
            InitializeComponent();

            ItemNameLbl.Text = itemName;
            SizeLbl.Text = size;
            StockLbl.Text = quantity.ToString();

            SelectedId = selectedId;
            StockQuantity = quantity;
            this.itemName = itemName;

            InputValidator.AllowOnlyDigits(ReduceTxt);
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ReduceTxt.Text, out int amount) && !string.IsNullOrWhiteSpace(ReduceTxt.Text) && !string.IsNullOrWhiteSpace(ReasonCb.Text))
            {
                if (amount <= StockQuantity) 
                {
                    AmountToDeduct = amount;
                    Reason = ReasonCb.Text;
                    DialogResult = DialogResult.OK;
                    canClose = true;
                    Close();
                }
                else
                {
                    ErrorTxt.Text = "The amount to deduct cannot exceed the available stock quantity";
                    ErrorTxt.Visible = true;
                }
            }
            else
            {
                ErrorTxt.Text = "Please enter a valid amount and select a reason";
                ErrorTxt.Visible = true;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ReduceTxt_Enter(object sender, EventArgs e)
        {
            ErrorTxt.Visible = false;
        }

        private void ReasonCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ErrorTxt.Visible = false;
        }

        private void ReduceStockForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose) // If it's not OK to close the form, cancel the closing operation
            {
                e.Cancel = true;
            }
        }
    }
}
