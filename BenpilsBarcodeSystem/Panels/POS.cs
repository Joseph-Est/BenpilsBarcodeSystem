using BenpilsBarcodeSystem.Dialogs;
using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class POS : Form
    {
        InventoryRepository inventory;
        Cart CurrentCart;
        private string TransactionNo { get; set; }

        public POS()
        {
            InitializeComponent();
        }

        private void POS_Load(object sender, EventArgs e)
        {
            CurrentCart = new Cart();
            InputValidator.AllowOnlyDigitsAndDecimal(PaymentTxt);
        }

        private async void BarcodeTxt_TextChanged(object sender, EventArgs e)
        {
            string barcode = BarcodeTxt.Text.Trim();

            if (inventory == null)
            {
                inventory = new InventoryRepository();
            }

            if(CurrentCart == null)
            {
                CurrentCart = new Cart();
            }

            Item CurrentItem = await inventory.GetItemByBarcodeAsync(barcode);

            
            if (CurrentItem != null)
            {
                
                var existingItem = CurrentCart.Items.FirstOrDefault(item => item.Id == CurrentItem.Id);
                if (existingItem != null)
                {
                    if (CurrentItem.Quantity - existingItem.Quantity <= 0)
                    {
                        MessageBox.Show("Out of stock");
                        BarcodeTxt.Clear();
                        return;

                    }

                    existingItem.Quantity += 1;
                }
                else
                {
                    if (CurrentItem.Quantity <= 0)
                    {
                        MessageBox.Show("Out of stock");
                        BarcodeTxt.Clear();
                        return;

                    }

                    var purchaseItem = new PurchaseItem
                    {
                        Id = CurrentItem.Id,
                        ItemName = CurrentItem.ItemName,
                        Brand = CurrentItem.Brand,
                        Size = CurrentItem.Size,
                        Stock = CurrentItem.Quantity,
                        Quantity = 1,
                        SellingPrice = CurrentItem.SellingPrice
                    };

                    CurrentCart.Items.Add(purchaseItem);
                }

                CartTbl.AutoGenerateColumns = false;
                CartTbl.DataSource = CurrentCart.Items;
                CartTbl.Refresh();
                BarcodeTxt.Clear();
                CartCheck();
            }
        }

        private void CartTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {

                var selectedItem = (PurchaseItem)senderGrid.Rows[e.RowIndex].DataBoundItem;

                if (senderGrid.Columns[e.ColumnIndex].Name == "increase")
                {
                    if(selectedItem.Stock > selectedItem.Quantity)
                    {
                        selectedItem.Quantity += 1;
                    }
                    else
                    {
                        MessageBox.Show("Out of stock");
                    }
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "decrease")
                {
                    selectedItem.Quantity -= 1;

                    if (selectedItem.Quantity == 0)
                    {
                        var result = MessageBox.Show("Are you sure you want to remove this item from the cart?", "Warning", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            CurrentCart.Items.Remove(selectedItem);
                        }
                        else
                        {
                            selectedItem.Quantity = 1;
                        }
                    }
                }

                CartTbl.Refresh();
                CartCheck();
            }
        }

        private void PaymentTxt_TextChanged(object sender, EventArgs e)
        {
            decimal change = InputValidator.ParseToDecimal(PaymentTxt.Text) - InputValidator.ParseToDecimal(TotalLbl.Text);

            if(change > 0)
            {
                ChangeLbl.Text = InputValidator.DecimalToFormattedStringPrice(change);
            }
            else
            {
                ChangeLbl.Text = "0.00";
            }
            
        }

        private async void CheckoutBtn_Click(object sender, EventArgs e)
        {
            if (CurrentCart.HasItems())
            {
                if (string.IsNullOrEmpty(PaymentTxt.Text.Trim())){
                    MessageBox.Show("Please enter received payment");
                    PaymentTxt.Select();
                    return;
                }


                if(InputValidator.ParseToDecimal(PaymentTxt.Text.Trim()) < CurrentCart.GetTotalPrice())
                {
                    MessageBox.Show("Invalid payment amount");
                    PaymentTxt.Select();
                    return;
                }

                var result = MessageBox.Show("Checkout?", "Warning", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string transactionNo = Util.GenerateRandomNumberWithLetter(100000, 999999, "TRX");
                    POSRepository repository = new POSRepository();
                    if (await repository.InsertTransactionAsync(transactionNo, CurrentCart, InputValidator.ParseToDecimal(PaymentTxt.Text)))
                    {
                        TransactionNo = transactionNo;
                        PrintReceipt();
                        ClearCart();
                    }
                    else
                    {
                        MessageBox.Show("Transaction failed, please try again later.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Cart is empty");
            }
            BarcodeTxt.Select();
        }

        private void CartCheck()
        {
            CheckoutBtn.Enabled = CurrentCart.HasItems();
            TotalLbl.Text = CurrentCart.GetTotalPriceAsString();
        }

        private void ClearCart()
        {
            CurrentCart = null;
            CartTbl.DataSource = null;
            CartTbl.Rows.Clear();
            TotalLbl.Text = "0.00";
            ChangeLbl.Text = "0.00";
            PaymentTxt.Text = null;
            BarcodeTxt.Text = null;
        }

        private void BarcodeTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
            }
        }

        private void POS_Enter(object sender, EventArgs e)
        {
            BarcodeTxt.Select();
        }

        private void PrintReceipt()
        {
            PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 315, 1000);

            PrintPreview.Document = PrintDocument;
            PrintPreview.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Bitmap bitmap = new Bitmap(315, 1000);

            Graphics graphics = e.Graphics;

            string transactionNo = $"Trx No. {TransactionNo}";

            string[] products = CurrentCart.GetProductNames();
            decimal[] prices = CurrentCart.GetPrices();

            Util.PrintReceipt(graphics, transactionNo, products, prices, CurrentCart.GetTotalPrice(), InputValidator.ParseToDecimal(PaymentTxt.Text), InputValidator.ParseToDecimal(ChangeLbl.Text));

            //bitmap.Save("receipt.png", ImageFormat.Png);
        }
    }
}