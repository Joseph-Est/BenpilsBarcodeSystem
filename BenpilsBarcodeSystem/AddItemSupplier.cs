using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BenpilsBarcodeSystem
{
    public partial class AddItemSupplier : Form
    {
        private bool isDragging = false;
        private int mouseX, mouseY;
        public AddItemSupplier()
        {
            InitializeComponent();

            DatabaseHelper dbHelper = new DatabaseHelper();
            DataTable dataTable = dbHelper.GetSupplierData();


            CmbSupplier.DataSource = dataTable;
            CmbSupplier.DisplayMember = "ContactName"; // Display ContactName in the ComboBox
            CmbSupplier.ValueMember = "SupplierID";
            CmbSupplier.DisplayMember = "CompanyName";
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.ShowDialog();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            string randomBarcode = rand.Next(1000000, 9999999).ToString(); // Adjust the range as needed

            // Create a BarcodeWriter instance
            BarcodeWriter barcodeWriter = new BarcodeWriter();

            // Set the barcode format (you can change it to other formats like QR_CODE, etc.)
            barcodeWriter.Format = BarcodeFormat.CODE_128;

            generatedpicture.Image = barcodeWriter.Write(randomBarcode);
            GeneratedBarcodeTxt.Text = randomBarcode;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSupplierID = CmbSupplier.SelectedValue.ToString();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CmbSupplier.Text) || // Make sure to adjust the ComboBox names
                string.IsNullOrWhiteSpace(BarcodeTxt.Text) ||
                string.IsNullOrWhiteSpace(ItemNameTxt.Text) ||
                string.IsNullOrWhiteSpace(MotorbrandTxt.Text) ||
                string.IsNullOrWhiteSpace(Brandtxt.Text) ||
                string.IsNullOrWhiteSpace(UnitPriceTxt.Text) ||
                string.IsNullOrWhiteSpace(CategoryTxt.Text))
            {
                MessageBox.Show("Please fill up all the required fields.");
                return;
            }

            string supplierID = CmbSupplier.SelectedValue.ToString(); // Get Supplier ID from ComboBox
            string companyName = CmbSupplier.Text; // Get Company Name from ComboBox
            string contactName = CmbSupplier.GetItemText(CmbSupplier.SelectedItem); // Get Contact Name from ComboBox

            string insertQuery = "INSERT INTO tbl_purchaseorderlist (supplierID, companyName, contactName, barcode, itemName, motorBrand, brand, unitPrice, category) " +
                               "VALUES (@SupplierID, @CompanyName, @ContactName, @Barcode, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Category)";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    cmd.Parameters.AddWithValue("@CompanyName", companyName);
                    cmd.Parameters.AddWithValue("@ContactName", contactName);
                    cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text);
                    cmd.Parameters.AddWithValue("@ItemName", ItemNameTxt.Text);
                    cmd.Parameters.AddWithValue("@MotorBrand", MotorbrandTxt.Text);
                    cmd.Parameters.AddWithValue("@Brand", Brandtxt.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPriceTxt.Text);
                    cmd.Parameters.AddWithValue("@Category", CategoryTxt.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }


            CLearAllTextBoxes();
            this.Close();
        }

        private void CLearAllTextBoxes()
        {
            CmbSupplier.Text= "";
            BarcodeTxt.Text = "";
            ItemNameTxt.Text = "";
            MotorbrandTxt.Text = "";
            Brandtxt.Text = "";
            UnitPriceTxt.Text = "";
            CategoryTxt.Text = "";
        }

   

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
