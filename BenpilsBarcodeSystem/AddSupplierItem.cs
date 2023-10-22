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

namespace BenpilsBarcodeSystem
{
    public partial class AddSupplierItem : Form
    {
        private bool isDragging = false;
        private int mouseX, mouseY;
        private string connectionString = "Data Source=DESKTOP-GM16NRU;Initial Catalog=BenpillMotorcycleDatabase;Integrated Security=True";

        public AddSupplierItem()
        {
            InitializeComponent();
            AddSupplierItem ASI = new AddSupplierItem();
            DataTable dataTable = ASI.GetSupplierData();
            comboBox1.DataSource = dataTable;
            comboBox1.DisplayMember = "ContactName"; // Display ContactName in the ComboBox
            comboBox1.ValueMember = "SupplierID";
        }
        public DataTable GetSupplierData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Modify this query according to your database structure and relationships.
                string query = "SELECT SupplierID, ContactName FROM Suppliers";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSupplierID = comboBox1.SelectedValue.ToString();
        }
    }
}
