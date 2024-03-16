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
        public int itemId { get; set; }
        public bool isExistingItem;

        public AddPurchaseItem(Supplier supplier = null, bool isModify = false, int itemId = 0, string itemName = null)
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigitsMinMax(QuantityTxt, 1, 999999999);

            this.isModify = isModify;

            if (!isModify && supplier != null)
            {
                CurrentSupplier = supplier;
            }
            else if(isModify && itemId > 0)
            {
                this.itemId = itemId;
                ItemsCb.Items.Add(itemName);
                ItemsCb.SelectedItem = itemName;
                ItemsCb.Enabled = false;
            }
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
            ItemsCb.Items.Add("-- Select --");
            ItemsCb.Items.AddRange(items.ToArray());
            ItemsCb.Items.Add("-- Add an existing item --");
            ItemsCb.Items.Add("-- Add new item --");
            ItemsCb.SelectedItem = "-- Select --";
        }

        private async void PopulateExistingItem()
        {
            InventoryRepository repository = new InventoryRepository();
            List<Item> items = await repository.GetNonSupplierItems(CurrentSupplier.SupplierID);

            comboBox1.Items.Clear();
            comboBox1.Items.Add("-- Select an existing item --");
            comboBox1.Items.AddRange(items.ToArray());
            comboBox1.SelectedItem = "-- Select an existing item --";
        }

        private void ItemsCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExistingItemPanel.Visible = false;

            if (ItemsCb.SelectedItem != null && ItemsCb.SelectedItem.ToString() == "-- Add new item --")
            {
                AddItem addItem = new AddItem(CurrentSupplier);
                addItem.ShowDialog();
                PopulateItem();
            }else if (ItemsCb.SelectedItem != null && ItemsCb.SelectedItem.ToString() == "-- Add an existing item --")
            {
                PopulateExistingItem();
                ExistingItemPanel.Visible = true;
            }
            else if(ItemsCb.SelectedIndex != 0 && ItemsCb.SelectedIndex != ItemsCb.Items.Count - 1 && ItemsCb.SelectedIndex != ItemsCb.Items.Count - 2)
            {
                if (ItemsCb.SelectedItem is Item selectedItem)
                {
                    isExistingItem = false;
                    SelectedItem = selectedItem;
                }
            }
            else
            {
                SelectedItem = null;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                if (comboBox1.SelectedItem is Item selectedItem)
                {
                    isExistingItem = true;
                    SelectedItem = selectedItem;
                }
            }
            else
            {
                SelectedItem = null;
            }
        }

        private async void AcceptBtn_Click(object sender, EventArgs e)
        {
            int quantity = InputValidator.ParseToInt(QuantityTxt.Text);

            if (quantity <= 0)
            {
                MessageBox.Show("Enter quantity");
                return;
            }

            this.quantity = quantity;

            if (isModify)
            {
                canClose = true;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                if (ItemsCb.SelectedIndex == 0 || ItemsCb.SelectedIndex == ItemsCb.Items.Count - 1 || SelectedItem == null)
                {
                    MessageBox.Show("Select an item");
                    return;
                }

                

                if (isExistingItem)
                {
                    InventoryRepository repository = new InventoryRepository();
                    if (await repository.AddProductToSuppier(CurrentSupplier.SupplierID, SelectedItem.Id))
                    {
                        canClose = true;
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    canClose = true;
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
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
