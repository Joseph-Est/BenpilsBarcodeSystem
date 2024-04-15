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
using System.Text;
using System.Windows.Forms;
using ZXing.QrCode.Internal;

namespace BenpilsBarcodeSystem
{
    public partial class POS : Form
    {
        InventoryRepository inventory;
        Cart CurrentCart;
        private string TransactionNo { get; set; }
        //private StringBuilder _barcode;
        MainForm mainForm;

        public POS()
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(BarcodeTxt);
            //_barcode = new StringBuilder();
        }

        private void BarcodeTimer_Tick(object sender, EventArgs e)
        {
            //BarcodeTimer.Stop();
            //BarcodeTxt.Text = _barcode.ToString();
            //_barcode.Clear();
        }

        private void POS_KeyPress(object sender, KeyPressEventArgs e)
        {
            //_barcode.Append(e.KeyChar);

            //BarcodeTimer.Stop();
            //BarcodeTimer.Start();
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
                    if (isExistingItem)
                    {
                        MessageBox.Show("Item Quantity exceeds the available stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Item is out of stock.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    
                    BarcodeTxt.Clear();
                    return;
                }

                QuantityDialog quantityDialog = new QuantityDialog(stock, CurrentItem.ItemName, CurrentItem.Size, CurrentItem.Brand, InputValidator.DecimalToFormattedStringPrice(CurrentItem.SellingPrice));
                
                if(quantityDialog.ShowDialog() == DialogResult.OK){
                    int quantity = quantityDialog.Quantity;

                    if (isExistingItem)
                    {
                        if (CurrentItem.Quantity - existingItem.Quantity - quantity < 0)
                        {
                            MessageBox.Show("Item Quantity exceeds the available stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            BarcodeTxt.Clear();
                            return;

                        }

                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        if (CurrentItem.Quantity - quantity < 0)
                        {
                            MessageBox.Show("Item Quantity exceeds the available stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            BarcodeTxt.Clear();
                            return;
                        }

                        if (CurrentItem.SellingPrice < 1)
                        {
                            MessageBox.Show("The item you're trying to add to the cart has a selling price of 0. Please update the selling price in the inventory before adding it to the cart.", "Cart Addition Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                }

                BarcodeTxt.Clear();
                CartCheck();
            }
        }

        private async void CartTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {

                var selectedItem = (PurchaseItem)senderGrid.Rows[e.RowIndex].DataBoundItem;

                if (senderGrid.Columns[e.ColumnIndex].Name == "increase")
                {
                    if (selectedItem.Stock > selectedItem.Quantity)
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
                        var result = MessageBox.Show("Are you sure you want to remove this item from the cart?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                var selectedItem = (PurchaseItem)senderGrid.Rows[e.RowIndex].DataBoundItem;

                if (senderGrid.Columns[e.ColumnIndex].Name == "Void")
                {
                    var result = MessageBox.Show("Are you sure you want to remove this item from the cart?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        CurrentCart.Items.Remove(selectedItem);
                        CartTbl.Refresh();
                        CartCheck();
                    }
                }
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn && e.RowIndex >= 0)
            {
                var selectedItem = (PurchaseItem)senderGrid.Rows[e.RowIndex].DataBoundItem;

                if (senderGrid.Columns[e.ColumnIndex].Name == "Quantity")
                {
                    if (inventory == null)
                    {
                        inventory = new InventoryRepository();
                    }

                    Item CurrentItem = await inventory.GetItemByIDAsync(selectedItem.Id);

                    if (CurrentItem != null)
                    {
                        var existingItem = CurrentCart.Items.FirstOrDefault(item => item.Id == CurrentItem.Id);

                        if (existingItem != null)
                        {
                            QuantityDialog quantityDialog = new QuantityDialog(CurrentItem.Quantity, CurrentItem.ItemName, CurrentItem.Size, CurrentItem.Brand, InputValidator.DecimalToFormattedStringPrice(CurrentItem.SellingPrice), existingItem.Quantity);

                            if (quantityDialog.ShowDialog() == DialogResult.OK)
                            {
                                int quantity = quantityDialog.Quantity;

                                if (CurrentItem.Quantity - quantity < 0)
                                {
                                    MessageBox.Show("Item Quantity exceeds the available stock.", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    BarcodeTxt.Clear();
                                    return;

                                }

                                existingItem.Quantity = quantity;
                                CartTbl.Refresh();
                                CartCheck();
                            }
                        }
                    }
                }
            }
        }

        private void CartTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void VoidCartBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to remove all items from the cart?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                    MessageBox.Show("Please enter the amount received from the customer.", "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PaymentTxt.Focus();
                    return;
                }


                if(InputValidator.ParseToDecimal(PaymentTxt.Text.Trim()) < CurrentCart.GetTotalPrice())
                {
                    MessageBox.Show("The payment amount entered is not valid. Please enter a valid number.", "Invalid Payment Amount", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PaymentTxt.Select();
                    return;
                }

                var result = MessageBox.Show("Checkout transaction?", "Confirm Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string transactionNo = Util.GenerateRandomNumberWithLetter(100000, 999999, "TRX");
                    POSRepository repository = new POSRepository();
                    if (await repository.InsertTransactionAsync(transactionNo, CurrentCart, InputValidator.ParseToDecimal(PaymentTxt.Text)))
                    {
                        TransactionNo = transactionNo;
                        PrintReceipt();
                        ClearCart();
                        mainForm.UpdateInventoryTable = true;
                    }
                    else
                    {
                        MessageBox.Show("The transaction failed due to an error. Please try again later.", "Transaction Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Cart is currently empty. Please add items to the cart before proceeding.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            BarcodeTxt.Select();
        }

        private void SearchItemBtn_Click(object sender, EventArgs e)
        {
            SearchItem searchItemDialog = new SearchItem();

            if (searchItemDialog.ShowDialog() == DialogResult.OK)
            {
                BarcodeTxt.Text = searchItemDialog.Barcode;
            }
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
                TotalLbl.Text = InputValidator.DecimalToFormattedStringPrice(CurrentCart.GetTotalPrice());
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
            //if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return)
            //{
            //    e.Handled = true;
            //    MessageBox.Show("The barcode you entered does not match any existing items. Please check the barcode and try again.", "Barcode Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    BarcodeTxt.Clear();
            //    BarcodeTxt.Select();
            //}
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
                FocusBarcode();
            }
        }

        private void PaymentTxt_Leave(object sender, EventArgs e)
        {
            FocusBarcode();
        }

        public void SelectBarcode()
        {
            BarcodeTxt.Select();
        }

        private void FocusBarcode()
        {
            if (this.ContainsFocus)
            {
                BarcodeTxt.Select();
            }
        }
    }
}