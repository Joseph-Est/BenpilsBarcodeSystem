using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repositories;
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
    public partial class AddPurchaseItem : Form
    {
        public Supplier CurrentSupplier = new Supplier();
        public Item SelectedItem = new Item();
        private bool canClose = false;
        public int quantity {get; set;}
        public bool isModify { get; set; }

        public AddPurchaseItem(Supplier supplier, bool isModify = false)
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(QuantityTxt);
            CurrentSupplier = supplier;
            this.isModify = isModify;
        }

        private void AddPurchaseItem_Load(object sender, EventArgs e)
        {
            if (isModify)
            {
                AcceptBtn.Text = "Done";
                TitleLbl.Text = "Modify Supplier Item";
            }
            else
            {
                PopulateItem();
            }
        }

        private async void PopulateItem()
        {
            InventoryRepository repository = new InventoryRepository();
            List<Item> items = await repository.GetSupplierItems(CurrentSupplier.SupplierID);

            ItemsCb.Items.Clear();
            ItemsCb.Items.Add("Select an item");
            ItemsCb.Items.AddRange(items.ToArray());
            ItemsCb.Items.Add("Add new item");
            ItemsCb.SelectedItem = "Select an item";
        }

        private void ItemsCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ItemsCb.SelectedItem != null && ItemsCb.SelectedItem.ToString() == "Add new item")
            {
                AddItem addItem = new AddItem(CurrentSupplier);
                addItem.ShowDialog();
                PopulateItem();
            }else if(ItemsCb.SelectedIndex != 0 && ItemsCb.SelectedIndex != ItemsCb.Items.Count - 1)
            {
                if (ItemsCb.SelectedItem is Item selectedItem)
                {
                    SelectedItem = selectedItem;
                }
            }
            else
            {
                SelectedItem = null;
            }
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            if (ItemsCb.SelectedIndex != 0 && ItemsCb.SelectedIndex != ItemsCb.Items.Count - 1 && SelectedItem != null)
            {
                int quantity = InputValidator.ParseToInt(QuantityTxt.Text);

                if (quantity <= 0)
                {
                    MessageBox.Show("Enter quantity");
                    return;
                }

                this.quantity = quantity;
                canClose = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Select an item");
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void AddPurchaseItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }
    }
}
