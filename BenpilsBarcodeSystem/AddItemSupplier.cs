using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            string supplierID = CmbSupplier.SelectedValue.ToString();
            string contactName = CmbSupplier.Text;
            string CompanyName = CmbSupplier.Text;
            string barcode = BarcodeTxt.Text;
            string itemName = ItemNameTxt.Text;
            string motorBrand = MotorbrandTxt.Text;
            string brand = Brandtxt.Text;
            string unitPrice = UnitPriceTxt.Text;
            string category = CategoryTxt.Text;

    

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
