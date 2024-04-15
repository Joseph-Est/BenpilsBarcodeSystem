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
    public partial class SearchItem : Form
    {
        public string Barcode { get; set; }
        bool canClose = false;

        public SearchItem()
        {
            InitializeComponent();
        }

        private async void UpdateDataGridView(string category = "All", string brand = "All")
        {
            if (string.IsNullOrWhiteSpace(SearchTxt.Text))
            {
                SearchTxt.Text = "";
            }

            try
            {
                InventoryRepository inventoryRepository = new InventoryRepository();
                DataTable inventoryDT = await inventoryRepository.GetProductsAsync(true, SearchTxt.Text, category, brand, false);

                ItemsTbl.AutoGenerateColumns = false;
                ItemsTbl.DataSource = inventoryDT;
                ItemsTbl.ClearSelection();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void SearchItem_Load(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void ItemsTbl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ItemsTbl.Rows[e.RowIndex];
                Barcode = row.Cells["item_barcode"].Value.ToString();
            }
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Barcode))
            {
                canClose = true;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please select an item before proceeding.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
        }

        private void SearchTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void SearchItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void ItemsTbl_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ItemsTbl.Rows[e.RowIndex];
                Barcode = row.Cells["item_barcode"].Value.ToString();

                if (!string.IsNullOrEmpty(Barcode))
                {
                    canClose = true;
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
