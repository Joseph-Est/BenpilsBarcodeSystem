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

namespace BenpilsBarcodeSystem.Dialogs
{
    public partial class QuantityDialog : Form
    {
        public int Quantity { get; set; }
        bool canClose = false;
        private readonly int defaultQuantity;

        public QuantityDialog(int stock, string itemName, string itemSize, string itemBrand, string price, int defaultQuantity = 1)
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigitsMinMax(QuantityTxt, 1, stock);
            StockLbl.Text = stock.ToString();
            ItemLbl.Text = $"{itemName}" + (itemBrand != "N/A" ? $", {itemBrand}" : "");
            SizeLbl.Text = itemSize;
            PriceLblTxt.Text = price;

            this.defaultQuantity = defaultQuantity;
        }

        private void QuantityDialog_Load(object sender, EventArgs e)
        {
            QuantityTxt.Text = defaultQuantity.ToString();
            QuantityTxt.Select();
            QuantityTxt.TextChanged += QuantityTxt_TextChanged;
        }

        private void QuantityTxt_TextChanged(object sender, EventArgs e)
        {
            this.AcceptButton = ConfirmBtn;
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            int quantity = InputValidator.ParseToInt(QuantityTxt.Text);
            if (quantity < 1)
            {
                MessageBox.Show("Please enter a valid Quantity.", "Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(quantity > InputValidator.ParseToInt(StockLbl.Text))
            {
                MessageBox.Show("Item Quantity exceeds the available stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                QuantityTxt.Select();
                return;
            }

            Quantity = InputValidator.ParseToInt(QuantityTxt.Text);
            canClose = true;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void QuantityDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void QuantityTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter;
        }
    }
}
