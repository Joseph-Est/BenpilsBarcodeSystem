using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void RetrievedItemBtn_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridArchived.SelectedRows.Count > 0)
            {
                // Get the selected ProductID from the DataGridView
                int productId = Convert.ToInt32(dataGridArchived.SelectedRows[0].Cells["ProductID"].Value);

                // Call the retrieve function
                RetrieveProduct(productId);

                // Refresh the DataGridView in the Archive window
                UpdateArchivedDataGridView();
            }
            else
            {
                MessageBox.Show("Please select a row to retrieve.");
            }
        }

        private void RetrieveProduct(int productId)
        {
            // Fetch the product details based on ProductID
            DataRow selectedRow = GetArchivedProductDetails(productId);

            if (selectedRow != null)
            {
                // Retrieve the product by inserting it back into the main table
                string retrieveQuery = "INSERT INTO tbl_itemmasterdata (Barcode, ProductID, ItemName, MotorBrand, Brand, UnitPrice, Quantity, Category, Size) " +
                                       "VALUES (@Barcode, @ProductID, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Quantity, @Category, @Size)";

                using (SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
                {
                    using (SqlCommand cmd = new SqlCommand(retrieveQuery, con))
                    {
                        // Set parameters from the selected row in the ArchivedItems table
                        cmd.Parameters.AddWithValue("@Barcode", selectedRow["Barcode"]);
                        cmd.Parameters.AddWithValue("@ProductID", selectedRow["ProductID"]);
                        cmd.Parameters.AddWithValue("@ItemName", selectedRow["ItemName"]);
                        cmd.Parameters.AddWithValue("@MotorBrand", selectedRow["MotorBrand"]);
                        cmd.Parameters.AddWithValue("@Brand", selectedRow["Brand"]);
                        cmd.Parameters.AddWithValue("@UnitPrice", selectedRow["UnitPrice"]);
                        cmd.Parameters.AddWithValue("@Quantity", selectedRow["Quantity"]);
                        cmd.Parameters.AddWithValue("@Category", selectedRow["Category"]);
                        cmd.Parameters.AddWithValue("@Size", selectedRow["Size"]);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Remove the retrieved product from the ArchivedItems table
                DeleteArchivedProduct(productId);
            }
        }
        private DataRow GetArchivedProductDetails(int productId)
        {
            DataTable dt = (DataTable)dataGridArchived.DataSource;
            DataRow[] rows = dt.Select($"ProductID = {productId}");
            return rows.Length > 0 ? rows[0] : null;
        }
        private void UpdateArchivedDataGridView()
        {
            // Fetch data from the ArchivedItems table and update the DataGridView
            string selectQuery = "SELECT * FROM ArchivedItems";
            using (SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, con))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridArchived.DataSource = dt;
                }
            }
        }
        private void DeleteArchivedProduct(int productId)
        {
            string deleteQuery = "DELETE FROM ArchivedItems WHERE ProductID = @ProductID";

            using (SqlConnection con = new SqlConnection("Data Source=SKLERBIDI;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'benpillMotorcycleArchivedItemMaster.ArchivedItems' table. You can move, or remove it, as needed.
            //this.archivedItemsTableAdapter.Fill(this.benpillMotorcycleArchivedItemMaster.ArchivedItems);

        }
    }
}
