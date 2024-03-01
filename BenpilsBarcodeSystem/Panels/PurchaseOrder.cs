using BenpilsBarcodeSystem.Helpers;
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
                PurchaseOrderRepository purchaseOrderRepository = new PurchaseOrderRepository();
                DataTable suppliersDT = await purchaseOrderRepository.GetSupplierAsync(searchText);

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

                PurchaseOrderRepository repository = new PurchaseOrderRepository();

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
                        Util.CapitalizeFirstLetter(ContactNameTxt.Text),
                        ContactNoTxt.Text,
                        Util.CapitalizeFirstLetter(AddressTxt.Text)
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
                        Util.CapitalizeFirstLetter(ContactNameTxt.Text),
                        ContactNoTxt.Text,
                        Util.CapitalizeFirstLetter(AddressTxt.Text)
                    );
                    isUpdating = false;

                    MessageBox.Show("Supplier updated succesfully!");
                }

                UpdateSupplierDG();
                ClearSupplierFields();
                SetSupplierFieldsReadOnly(true);

                AddBtn.Text = " Add";
                UpdateBtn.Text = " Update";

                AddBtn.ForeColor = Color.White;
                UpdateBtn.ForeColor = Color.White;

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
                PurchaseOrderRepository repository = new PurchaseOrderRepository();

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
                AddBtn.ForeColor = Color.Empty;
                UpdateBtn.ForeColor = Color.Empty;
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






        private void RefreshBtn_Click(object sender, EventArgs e)
        {

        }

        private void AdddBtn_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void UpdateChangeLabel()
        {
            //// Calculate change whenever the payment amount changes
            //decimal payment;
            //if (decimal.TryParse(paymentTxt.Text, out payment))
            //{
            //    decimal total = Convert.ToDecimal(totallbl.Text);

            //    // Calculate change
            //    decimal change = payment - total;

            //    // Update the Change label
            //    PurchasePage.Text = change.ToString();
            //}
        }
        private void UpdateTotalLabel()
        {
            //// Calculate the total from the DataTable
            //decimal total = dtCart.AsEnumerable().Sum(row => row.Field<decimal>("Subtotal"));

            //// Update the Total label
            //totallbl.Text = total.ToString();
        }

        private void BuyBtn_Click(object sender, EventArgs e)
        {
            //decimal payment = Convert.ToDecimal(paymentTxt.Text);
            //decimal total = Convert.ToDecimal(totallbl.Text);
            //if (dtCart.Rows.Count == 0)
            //{
            //    MessageBox.Show("Please add items to the cart before purchasing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //decimal change = payment - total;
            //ChangeLbl.Text = change.ToString();
            //if (change >= 0)
            //{
            //    MessageBox.Show("Purchase successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    ClearCart();
            //    UpdateDataGridView2();
            //}
            //else
            //{
            //    MessageBox.Show("Purchase failed, insufficient balance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void paymentTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateChangeLabel();
        }
        private void ClearCart()
        {
           // // Clear the DataTable
           // dtCart.Clear();

           // // Update the total label after clearing
           // UpdateTotalLabel();
           //UpdateDataGridView2();
        }

        private void UpdateDataGridView2()
        {
            string selectQuery = "SELECT * FROM tbl_cartpurchasing";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
            }
        }
    }
}
