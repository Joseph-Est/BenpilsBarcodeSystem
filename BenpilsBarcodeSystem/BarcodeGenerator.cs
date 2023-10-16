using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem
{
    public partial class BarcodeGenerator : Form
    {
        private bool isDragging = false;
        private int mouseX, mouseY;
        
        public BarcodeGenerator()
        {
            InitializeComponent();
            BarcodeWriter barcodeWriter =
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

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            string barCode = txtBarcodefiller.Text;

            try
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.CODE_128; // You can change the barcode format if needed
                barcodeWriter.Options = new ZXing.Common.EncodingOptions
                {
                    Width = 300, // Adjust the width and height as needed
                    Height = 100
                };

                pictureBox13.Image = barcodeWriter.Write(barCode);
            }
            catch (Exception)
            {
                // Handle exceptions here
            }
        }

        private void BarcodeGenerator_Load(object sender, EventArgs e)
        {
            this.AcceptButton = GenerateBtn;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
