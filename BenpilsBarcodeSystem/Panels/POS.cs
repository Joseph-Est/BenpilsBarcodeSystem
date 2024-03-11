﻿using BenpilsBarcodeSystem.Dialogs;
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
using System.Text;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class POS : Form
    {
        InventoryRepository inventory;
        Cart CurrentCart;
        private string TransactionNo { get; set; }
        private StringBuilder _barcode;
        MainForm mainForm;

        public POS()
        {
            InitializeComponent();
            _barcode = new StringBuilder();
        }

        private void BarcodeTimer_Tick(object sender, EventArgs e)
        {
            BarcodeTimer.Stop();
            BarcodeTxt.Text = _barcode.ToString();
            _barcode.Clear();
        }

        private void POS_KeyPress(object sender, KeyPressEventArgs e)
        {
            _barcode.Append(e.KeyChar);

            BarcodeTimer.Stop();
            BarcodeTimer.Start();
        }

        private void POS_Load(object sender, EventArgs e)
        {
            mainForm = (MainForm)this.ParentForm;
            CurrentCart = new Cart();
            InputValidator.AllowOnlyDigitsAndDecimal(PaymentTxt);
        }

        private async void BarcodeTxt_TextChanged(object sender, EventArgs e)
        {
            string barcode = BarcodeTxt.Text.Trim();
            bool isExistingItem = false;

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

                isExistingItem = existingItem != null;

                int stock = CurrentItem.Quantity - (isExistingItem ? existingItem.Quantity : 0);

                if (stock <= 0)
                {
                    MessageBox.Show("Out of stock");
                    BarcodeTxt.Clear();
                    return;
                }

                QuantityDialog quantityDialog = new QuantityDialog(stock, CurrentItem.ItemName, CurrentItem.Size, CurrentItem.Brand);
                
                if(quantityDialog.ShowDialog() == DialogResult.OK){
                    int quantity = quantityDialog.quantity;

                    if (isExistingItem)
                    {
                        if (CurrentItem.Quantity - existingItem.Quantity - quantity < 0)
                        {
                            MessageBox.Show("Not enough stock");
                            BarcodeTxt.Clear();
                            return;

                        }

                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        if (CurrentItem.Quantity - quantity < 0)
                        {
                            MessageBox.Show("Not enough stock");
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
                            Quantity = quantity,
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
            }else if(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var selectedItem = (PurchaseItem)senderGrid.Rows[e.RowIndex].DataBoundItem;

                if (senderGrid.Columns[e.ColumnIndex].Name == "Void")
                {
                    var result = MessageBox.Show("Are you sure you want to remove this item from the cart?", "Warning", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        CurrentCart.Items.Remove(selectedItem);
                        CartTbl.Refresh();
                        CartCheck();
                    }
                }
            }
        }

        private void VoidCartBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove all items from the cart?", "Warning", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                ClearCart();
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
            if (CurrentCart == null)
            {
                CheckoutBtn.Enabled = false;
                VoidCartBtn.Enabled = false;
                mainForm.CanSwitchPanel = true;
            }
            else
            {
                CheckoutBtn.Enabled = CurrentCart.HasItems();
                VoidCartBtn.Enabled = CurrentCart.HasItems();
                mainForm.CanSwitchPanel = !CurrentCart.HasItems();
                TotalLbl.Text = CurrentCart.GetTotalPrice().ToString();
            }
            BarcodeTxt.Select();
        }

        private void ClearCart()
        {
            CurrentCart = null;
            CartTbl.DataSource = null;
            CartTbl.Rows.Clear();
            ChangeLbl.Text = "0.00";
            TotalLbl.Text = "0.00";
            PaymentTxt.Text = null;
            BarcodeTxt.Text = null;
            CartCheck();
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

        private void POS_ParentChanged(object sender, EventArgs e)
        {
            BarcodeTxt.Select();
        }

        private void BarcodeTxt_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }


        private void BarcodeTxt_Leave(object sender, EventArgs e)
        {
            if (PaymentTxt.Focused)
            {
                this.AcceptButton = CheckoutBtn;
            }
            else
            {
                BarcodeTxt.Select();
            }
        }

        private void PaymentTxt_Leave(object sender, EventArgs e)
        {
            BarcodeTxt.Select();
        }

        private void PaymentTxt_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}