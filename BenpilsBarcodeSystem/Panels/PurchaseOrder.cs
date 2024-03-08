using BenpilsBarcodeSystem.Dialogs;
using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repositories;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace BenpilsBarcodeSystem
{
    public partial class PurchaseOrder : Form
    {
        private bool isAdding = false;
        private bool isUpdating = false;
        private bool isPurchasing = false;
        private int selectedID;
        private string prevContactName, prevContactNumber;

        public PurchaseOrder()
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(ContactNoTxt);
        }

        private void SupplierPage_Enter(object sender, EventArgs e)
        {
            UpdateSupplierDG();
        }

        public async void UpdateSupplierDG(string searchText = null)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                SearchTxt.Text = "";
            }

            try
            {
                SuppliersRepository repository = new SuppliersRepository();
                DataTable suppliersDT = await repository.GetSupplierAsync(searchText);

                SupplierTbl.DataSource = suppliersDT;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private async void AddBtn_Click(object sender, EventArgs e)
        {
            if (!isUpdating && !isAdding)
            {
                isAdding = true;
                ClearSupplierFields();
                SetSupplierFieldsReadOnly(false);

                AddBtn.Text = " Save Supplier";
                UpdateBtn.Text = " Cancel";

                UpdateBtn.Enabled = true;
                ArchiveBtn.Enabled = false;

                ContactNameTxt.Focus();
                this.CancelButton = UpdateBtn;
            }
            else
            {
                if (Util.AreTextBoxesNullOrEmpty(ContactNameTxt, ContactNoTxt, AddressTxt))
                {
                    MessageBox.Show("Please fill in all the required fields.");
                    return;
                }

                SuppliersRepository repository = new SuppliersRepository();

                if (isAdding || (isUpdating && (prevContactName != ContactNameTxt.Text || prevContactNumber != ContactNoTxt.Text)))
                {
                    if (await repository.AreDataExistsAsync("contact_name", ContactNameTxt.Text, "contact_no", ContactNoTxt.Text))
                    {
                        MessageBox.Show("Supplier already exists.");
                        return;
                    }
                }

                if (isAdding)
                {
                    await repository.AddSupplierAsync(
                        Util.Capitalize(ContactNameTxt.Text),
                        ContactNoTxt.Text,
                        Util.Capitalize(AddressTxt.Text)
                    );

                    isAdding = false;

                    MessageBox.Show("New supplier added succesfully!");
                }
                else
                {
                    if (prevContactName != ContactNameTxt.Text || prevContactNumber != ContactNoTxt.Text)
                    {
                        if (await repository.AreDataExistsAsync("contact_name", ContactNameTxt.Text, "contact_no", ContactNoTxt.Text))
                        {
                            MessageBox.Show("Supplier already exists.");
                            return;
                        }
                    }

                    await repository.UpdateSupplierAsync(
                        selectedID,
                        Util.Capitalize(ContactNameTxt.Text),
                        ContactNoTxt.Text,
                        Util.Capitalize(AddressTxt.Text)
                    );
                    isUpdating = false;

                    MessageBox.Show("Supplier updated succesfully!");
                }

                UpdateSupplierDG();
                ClearSupplierFields();
                SetSupplierFieldsReadOnly(true);

                AddBtn.Text = " Add";
                UpdateBtn.Text = " Update";

                this.CancelButton = null;
            }
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (!isUpdating && !isAdding)
            {
                isUpdating = true;
                SetSupplierFieldsReadOnly(false);

                AddBtn.Text = " Save Update";
                UpdateBtn.Text = " Cancel";

                ArchiveBtn.Enabled = false;

                prevContactName = ContactNameTxt.Text;
                prevContactNumber = ContactNoTxt.Text;

                this.CancelButton = UpdateBtn;
            }
            else
            {
                isAdding = false;
                isUpdating = false;

                ClearSupplierFields();
                SetSupplierFieldsReadOnly(true);

                AddBtn.Text = " Add";
                UpdateBtn.Text = " Update";

                this.CancelButton = null;
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearSupplierFields();
        }

        private async void ArchiveBtn_Click(object sender, EventArgs e)
        {
            Confirmation confirmation = new Confirmation("Are you sure you want to archive", "\"" + ContactNameTxt.Text + "\"?" + "?", "Yes", "Cancel");
            DialogResult result = confirmation.ShowDialog();

            if (result == DialogResult.Yes)
            {
                SuppliersRepository repository = new SuppliersRepository();

                if (selectedID > 0)
                {
                    if (await repository.ArchiveSupplierAsync(selectedID))
                    {
                        UpdateSupplierDG();
                        ClearSupplierFields();
                        MessageBox.Show("Supplier archived succesfully!");
                    }
                   
                }
            }
        }

        private void ClearSupplierFields()
        {
            Util.ClearTextBoxes(ContactNameTxt, ContactNoTxt, AddressTxt);

            if (!isAdding && !isUpdating)
            {
                UpdateBtn.Enabled = false;
                ArchiveBtn.Enabled = false;
            }
        }

        private void SetSupplierFieldsReadOnly(bool mode)
        {
            Util.SetTextBoxesReadOnly(mode, ContactNameTxt, ContactNoTxt, AddressTxt);
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateSupplierDG(SearchTxt.Text);
        }

        private void SupplierTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isAdding && !isUpdating)
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = SupplierTbl.Rows[e.RowIndex];
                    selectedID = InputValidator.ParseToInt(row.Cells["supplier_id"].Value.ToString());
                    ContactNameTxt.Text = row.Cells["contact_name"].Value.ToString();
                    ContactNoTxt.Text = row.Cells["contact_no"].Value.ToString();
                    AddressTxt.Text = row.Cells["address"].Value.ToString();
                    UpdateBtn.Enabled = true;
                    ArchiveBtn.Enabled = true;
                }
            }
        }

        private void AddBtn_TextChanged(object sender, EventArgs e)
        {
            if (AddBtn.Text.Contains("Save"))
            {
                AddBtn.ForeColor = Color.FromArgb(80, 180, 80);
                AddBtn.Image = Properties.Resources.icons8_downloading_updates_15;
                UpdateBtn.ForeColor = Color.FromArgb(220, 80, 80);
                UpdateBtn.Image = Properties.Resources.icons8_multiply_15;
            }
            else
            {
                AddBtn.ForeColor = Color.Black;
                UpdateBtn.ForeColor = Color.Black;
                UpdateBtn.Image = Properties.Resources.icons8_update_15;
                AddBtn.Image = Properties.Resources.icons8_add_15;
            }
        }

        private void InputFormPanel_Enter(object sender, EventArgs e)
        {
            this.AcceptButton = AddBtn;
        }

        private void InputFormPanel_Leave(object sender, EventArgs e)
        {
            this.AcceptButton = null;
        }



        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

        private Cart CurrentPurchaseCart;
        private Supplier SelectedSupplier;

        private void PurchasePage_Enter(object sender, EventArgs e)
        {
            UpdatePurchaseOrdersDG();
            OrderDt.MinDate = DateTime.Now;
            DeliveryDt.MinDate = DateTime.Now;
        }

        public async void UpdatePurchaseOrdersDG()
        {
            try
            {
                PurchaseOrderRepository repository = new PurchaseOrderRepository();
                DataTable ordersDT = await repository.GetPurchaseOrderTransactionsAsync();

                OrdersTbl.AutoGenerateColumns = false;
                OrdersTbl.DataSource = ordersDT;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void NewPurchaseBtn_Click(object sender, EventArgs e)
        {
            SelectedSupplier = new Supplier();
            CurrentPurchaseCart = new Cart();
            EnablePurchasePanel(true);
            PopulateSupplier();
        }

        private void AddItemCb_Click(object sender, EventArgs e)
        {
            if(SelectedSupplier != null)
            {
                AddPurchaseItem addPurchaseItem = new AddPurchaseItem(SelectedSupplier);
                if (addPurchaseItem.ShowDialog() == DialogResult.OK)
                {
                    int id = addPurchaseItem.SelectedItem.Id;
                    string itemName = addPurchaseItem.SelectedItem.ItemName;
                    string brand = addPurchaseItem.SelectedItem.Brand;
                    string size = addPurchaseItem.SelectedItem.Size;
                    decimal purchasePrice = addPurchaseItem.SelectedItem.PurchasePrice;
                    int quantity = addPurchaseItem.quantity;

                    var existingItem = CurrentPurchaseCart.Items.FirstOrDefault(item => item.Id == id);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        CurrentPurchaseCart.Items.Add(new PurchaseItem { Id = id, ItemName = itemName, Brand = brand, Size = size, Quantity = quantity, PurchasePrice = purchasePrice });
                    }

                    ItemsTbl.AutoGenerateColumns = false;
                    ItemsTbl.DataSource = CurrentPurchaseCart.Items;
                    ItemsTbl.Refresh();
                    ItemsTableCheck();
                }
            }
        }

        private async void CompleteBtn_Click(object sender, EventArgs e)
        {
            if (CurrentPurchaseCart.HasItems() && SelectedSupplier != null)
            {
                DateTime orderDate = OrderDt.Value;
                DateTime deliveryDate = DeliveryDt.Value;
                int orderNo = Util.GenerateRandomNumber(10000000, 99999999);

                OrderDetails orderDetails = new OrderDetails(true, CurrentPurchaseCart, SelectedSupplier, Util.ConvertDate(orderDate), Util.ConvertDate(deliveryDate));
                if(orderDetails.ShowDialog() == DialogResult.OK)
                {
                    PurchaseOrderRepository repository = new PurchaseOrderRepository();
                    if (await repository.InsertPurchaseOrderAsync(orderNo, CurrentPurchaseCart, SelectedSupplier, orderDate, deliveryDate))
                    {
                        MessageBox.Show("Purchase success, order is pending.");
                        //await Util.SavePurchaseOrderReceiptAsync(orderNo.ToString(), CurrentPurchaseCart, "Harigato");
                        ClearPurchase();
                        UpdatePurchaseOrdersDG();
                    }
                    else
                    {
                        MessageBox.Show("Purchase failed, please try again later.");
                    }
                }
            }
            else
            {

            }
        }

        private void ItemsTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {

                var selectedItem = (PurchaseItem)senderGrid.Rows[e.RowIndex].DataBoundItem;

                if (senderGrid.Columns[e.ColumnIndex].Name == "increase")
                {
                    selectedItem.Quantity += 1;
                }
                else if (senderGrid.Columns[e.ColumnIndex].Name == "decrease")
                {
                    selectedItem.Quantity -= 1;

                    if (selectedItem.Quantity == 0)
                    {
                        var result = MessageBox.Show("Are you sure you want to remove this item from the cart?", "Warning", MessageBoxButtons.YesNo);

                        if (result == DialogResult.Yes)
                        {
                            CurrentPurchaseCart.Items.Remove(selectedItem);
                        }
                        else
                        {
                            selectedItem.Quantity = 1;
                        }
                    }
                }

                ItemsTbl.Refresh();
                ItemsTableCheck();
            }
        }

        private void OrdersTbl_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (senderGrid.Columns[e.ColumnIndex].Name == "view_details")
                {
                    //OrderDetails orderDetails = new OrderDetails();
                    //orderDetails.ShowDialog();
                }

                else if (senderGrid.Columns[e.ColumnIndex].Name == "complete_order")
                {
                   
                }
            }
        }

        private void SupplierCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isPurchasing)
            {
                if (SupplierCb.SelectedIndex != 0)
                {
                    AddItemPanel.Enabled = true;

                    if (SupplierCb.SelectedItem is Supplier selectedSupplier)
                    {
                        SelectedSupplier = selectedSupplier;
                    }
                }
                else
                {
                    AddItemPanel.Enabled = false;
                    SelectedSupplier = null;
                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Cancel Purchase", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ClearPurchase();
            }
        }

        private void ClearPurchase()
        {
            SelectedSupplier = null;
            CurrentPurchaseCart = null;
            ItemsTbl.DataSource = null;
            ItemsTbl.Rows.Clear();
            TotalAmountLbl.Text = "0.00";
            SupplierCb.SelectedIndex = -1;
            AddItemPanel.Enabled = false;
            CompleteBtn.Enabled = false;
            OrderDt.Value = DateTime.Now;
            DeliveryDt.Value = DateTime.Now;
            EnablePurchasePanel(false);
        }

        private async void PopulateSupplier()
        {
            SuppliersRepository repository = new SuppliersRepository();
            List<Supplier> suppliers = await repository.GetSuppliersAsync();

            SupplierCb.Items.Clear();
            SupplierCb.Items.Add("Select supplier");
            SupplierCb.Items.AddRange(suppliers.ToArray());

            SupplierCb.SelectedItem = "Select supplier";
        }

        private void ItemsTableCheck()
        {
            TotalAmountLbl.Text = CurrentPurchaseCart.GetTotalAmount();
            CompleteBtn.Enabled = CurrentPurchaseCart.HasItems();
            SupplierCb.Enabled = !CurrentPurchaseCart.HasItems();
        }

        private void OrderDt_ValueChanged(object sender, EventArgs e)
        {
            DeliveryDt.MinDate = OrderDt.Value;
        }

        private void EnablePurchasePanel(bool isEnable)
        {
            NewPurchaseBtn.Enabled = !isEnable;
            AddBtn.Enabled = isEnable;
            isPurchasing = isEnable;
            SupplierPanel.Enabled = isEnable;
            DatePanel.Enabled = isEnable;
            ItemsTbl.Visible = isEnable;
            SummaryPanel.Visible = isEnable;
            CompleteBtn.Visible = isEnable;
            CancelBtn.Visible = isEnable;
        }
    }
}
