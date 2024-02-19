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

namespace BenpilsBarcodeSystem
{
    public partial class PurchaseOrder : Form
    {
        private User user;
        private int selectedSupplierID = -1;
        private DataTable dtCart;
        private Random random = new Random();
        public DataGridView DataGridView1 => Datelbl;
        public PurchaseOrder()
        {
            InitializeComponent();
            dtCart = new DataTable();
            dtCart.Columns.Add("ProductID", typeof(int));
            dtCart.Columns.Add("ItemName", typeof(string));
            dtCart.Columns.Add("Quantity", typeof(int));
            dtCart.Columns.Add("Subtotal", typeof(decimal));

            dataGridView2.DataSource = dtCart;
        }

        private void UpdateDataGridView()
        {
            string selectQuery = "SELECT * FROM tbl_supplier";
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridSupplier.DataSource = dt;
                }
            }
        }

        public void UpdateDataGridView1(DataTable dataTable)
        {
            Datelbl.DataSource = dataTable;
        }

        private void ClearAllTextBoxes()
        {

            ContactNameTxt.Text = "";
            AddressTxt.Text = "";
            ContactNoTxt.Text = "";

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ContactNameTxt.Text) ||
                string.IsNullOrWhiteSpace(AddressTxt.Text) ||
                string.IsNullOrWhiteSpace(ContactNoTxt.Text) )
            {
                MessageBox.Show("Please ensure all required fields are filled.");
                return;
            }

            string insertQuery = "INSERT INTO tbl_supplier (ContactName, Address, ContactNo) " +
                             "VALUES (@ContactName, @Address, @ContactNo)";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ContactName", ContactNameTxt.Text);
                    cmd.Parameters.AddWithValue("@Address", AddressTxt.Text);
                    cmd.Parameters.AddWithValue("@ContactNo", ContactNoTxt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            UpdateDataGridView();
            ClearAllTextBoxes();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (selectedSupplierID != -1)
            {
                string contactName = ContactNameTxt.Text;
                string address = AddressTxt.Text;
                string contactNo = ContactNoTxt.Text;


                using (SqlConnection connection = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    connection.Open();
                    string query = "UPDATE tbl_supplier " +
                                   "SET ContactName = @ContactName, " +
                                   "Address = @Address, " +
                                   "ContactNo = @ContactNo, " +
                                   "WHERE SupplierID = @SupplierID";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@ContactName", contactName);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@ContactNo", contactNo);
                    command.Parameters.AddWithValue("@SupplierID", selectedSupplierID);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Supplier information updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Update failed.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row before updating.");
            }
            UpdateDataGridView();
            ClearAllTextBoxes();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes();
        }

        private void ContactNoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridSupplier.Rows[e.RowIndex];
                ContactNameTxt.Text = row.Cells[1].Value.ToString();
                AddressTxt.Text = row.Cells[2].Value.ToString();
                ContactNoTxt.Text = row.Cells[3].Value.ToString();
                AddBtn.Enabled = false;
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
            AddBtn.Enabled = true;
        }

        private void AdddBtn_Click(object sender, EventArgs e)
        {
            AddItemSupplier addItemSupplier = new AddItemSupplier(user);
            addItemSupplier.BringToFront();
            addItemSupplier.StartPosition = FormStartPosition.CenterScreen;
            addItemSupplier.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle the "Add Data" button click for DataGridView1
            // Here, you might want to open a QuantityForm to get the quantity input
            // For simplicity, we'll assume the quantity is entered directly here.

            int selectedRowIndex = DataGridView1.SelectedCells[0].RowIndex;

            // Extract data from the selected row in DataGridView1
            int productID = Convert.ToInt32(DataGridView1.Rows[selectedRowIndex].Cells["ProductID"].Value);
            string itemName = DataGridView1.Rows[selectedRowIndex].Cells["ItemName"].Value.ToString();
            decimal unitPrice = Convert.ToDecimal(DataGridView1.Rows[selectedRowIndex].Cells["UnitPrice"].Value);

            // Assume QuantityForm is a form where the user enters the quantity
            QuantityForm quantityForm = new QuantityForm();
            if (quantityForm.ShowDialog() == DialogResult.OK)
            {
                int quantity = quantityForm.Quantity;
                decimal subtotal = unitPrice * quantity;

                // Add data to the DataTable for DataGridView2
                dtCart.Rows.Add(productID, itemName, quantity, subtotal);

                // Update the total label
                UpdateTotalLabel();

                // Regenerate transaction number
                RegenerateTransactionNumber();
            }
        }
        private void UpdateChangeLabel()
        {
            // Calculate change whenever the payment amount changes
            decimal payment;
            if (decimal.TryParse(paymentTxt.Text, out payment))
            {
                decimal total = Convert.ToDecimal(totallbl.Text);

                // Calculate change
                decimal change = payment - total;

                // Update the Change label
                ChangeLbl.Text = change.ToString();
            }
        }
        private void UpdateTotalLabel()
        {
            // Calculate the total from the DataTable
            decimal total = dtCart.AsEnumerable().Sum(row => row.Field<decimal>("Subtotal"));

            // Update the Total label
            totallbl.Text = total.ToString();
        }
        private void RegenerateTransactionNumber()
        {
            string transactionNumber = "PUR" + random.Next(1000000, 9999999).ToString();
            TransactionNumberLbl.Text = transactionNumber;
            Datelbl.Text = DateTime.Now.ToString();
        }
        private void BuyBtn_Click(object sender, EventArgs e)
        {
            decimal payment = Convert.ToDecimal(paymentTxt.Text);
            decimal total = Convert.ToDecimal(totallbl.Text);
            if (dtCart.Rows.Count == 0)
            {
                MessageBox.Show("Please add items to the cart before purchasing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal change = payment - total;
            ChangeLbl.Text = change.ToString();
            if (change >= 0)
            {
                MessageBox.Show("Purchase successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearCart();
                UpdateDataGridView2();
            }
            else
            {
                MessageBox.Show("Purchase failed, insufficient balance", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void paymentTxt_TextChanged(object sender, EventArgs e)
        {
            UpdateChangeLabel();
        }
        private void ClearCart()
        {
            // Clear the DataTable
            dtCart.Clear();

            // Update the total label after clearing
            UpdateTotalLabel();
           UpdateDataGridView2();
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
