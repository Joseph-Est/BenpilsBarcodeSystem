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
     
        public AddItemSupplier(User user)
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
        //close button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            string randomBarcode = rand.Next(1000000, 9999999).ToString();
            BarcodeWriter barcodeWriter = new BarcodeWriter();
            barcodeWriter.Format = BarcodeFormat.CODE_128;
            generatedpicture.Image = barcodeWriter.Write(randomBarcode);
            GeneratedBarcodeTxt.Text = randomBarcode;
        }
        private void ManualRegeeBtn_Click(object sender, EventArgs e)
        {
            string inputText = ManualRegenratetxt.Text;

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.CODE_128;
                var barcodeBitmap = barcodeWriter.Write(inputText);             
                generatedpicture.Image = barcodeBitmap;
            }
            else
            {
                MessageBox.Show("Please enter data to generate a barcode.");
            }
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
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void CmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSupplierID = CmbSupplier.SelectedValue.ToString();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CmbSupplier.Text) ||
                 string.IsNullOrWhiteSpace(BarcodeTxt.Text) ||
                 string.IsNullOrWhiteSpace(ItemNameTxt.Text) ||
                 string.IsNullOrWhiteSpace(Brandtxt.Text) ||
                 string.IsNullOrWhiteSpace(UnitPriceTxt.Text) ||
                 string.IsNullOrWhiteSpace(CategoryTxt.Text) ||
                 string.IsNullOrWhiteSpace(productIDtxt.Text))
            {
                MessageBox.Show("Please fill up all the required fields.");
                return;
            }

            string supplierID = CmbSupplier.SelectedValue.ToString();
            decimal unitPrice;

            if (!decimal.TryParse(UnitPriceTxt.Text, out unitPrice))
            {
                MessageBox.Show("Invalid Unit Price. Please enter a valid decimal value.");
                return;
            }

            string checkProductIDQuery = "SELECT COUNT(*) FROM tbl_purchaseorderlist WHERE ProductID = @ProductID";

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                con.Open();

                using (SqlCommand checkCmd = new SqlCommand(checkProductIDQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@ProductID", productIDtxt.Text);
                    int existingRecordsCount = (int)checkCmd.ExecuteScalar();

                    if (existingRecordsCount > 0)
                    {
                        MessageBox.Show("Product with the same ProductID already exists in the database. Please choose a different ProductID.");
                        return;
                    }
                }


                string insertQuery = "INSERT INTO tbl_purchaseorderlist (supplierID, contactName, barcode, itemName, motorBrand, brand, unitPrice, category, ProductID) " +
                                    "VALUES (@SupplierID, @ContactName, @Barcode, @ItemName, @MotorBrand, @Brand, @UnitPrice, @Category, @ProductID)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);        
                    cmd.Parameters.AddWithValue("@ContactName", CmbSupplier.GetItemText(CmbSupplier.SelectedItem));
                    cmd.Parameters.AddWithValue("@Barcode", BarcodeTxt.Text);
                    cmd.Parameters.AddWithValue("@ItemName", ItemNameTxt.Text);
                    cmd.Parameters.AddWithValue("@MotorBrand", MotorbrandTxt.Text);
                    cmd.Parameters.AddWithValue("@Brand", Brandtxt.Text);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@Category", CategoryTxt.Text);
                    cmd.Parameters.AddWithValue("@ProductID", productIDtxt.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            CLearAllTextBoxes();
            this.Close();
        }

        private void CLearAllTextBoxes()
        {
            CmbSupplier.Text         = "";
            BarcodeTxt.Text          = "";
            ItemNameTxt.Text         = "";
            MotorbrandTxt.Text       = "";
            Brandtxt.Text            = "";
            UnitPriceTxt.Text        = "";
            CategoryTxt.Text         = "";
            productIDtxt.Text        = "";
            GeneratedBarcodeTxt.Text = "";
            ManualRegenratetxt.Text  = "";
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            CLearAllTextBoxes();
        }

        private void productIDtxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UnitPriceTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void ManualRegenratetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }

        private void GenerateProductID()
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True"))
            {
                con.Open();

                Random rand = new Random();
                int randomProductID = rand.Next(1, 600);

                if (randomProductID > 599)
                {
                    MessageBox.Show("ProductID limit exceeded.");
                    return;
                }

                string productID =randomProductID.ToString("D4");
                productIDtxt.Text = productID;
            }
        }
        private void GenerateproductidBtn_Click(object sender, EventArgs e)
        {
            GenerateProductID();
        }
        private void UpdateDataGridview()
        {
            
        }
    }
}
