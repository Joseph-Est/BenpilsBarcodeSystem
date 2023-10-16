
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using ZXing;
using System.Media;

namespace BenpilsBarcodeSystem
{
    public partial class BarcodeGenerator : Form
    {
        private bool isDragging = false;
        private int mouseX, mouseY;
        public BarcodeGenerator()
        {
            InitializeComponent();
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

    

        private void BarcodeGenerator_Load(object sender, EventArgs e)
        {
            this.AcceptButton = EncodeBtn;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DecodeBtn_Click(object sender, EventArgs e)
        {
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode((Bitmap)pictureBox13.Image);

            if (result != null)
            {
                DecodeTxt.Text = result.Text;
                SystemSounds.Beep.Play();
            }
            else
            {
                MessageBox.Show("Unable to decode barcode.");
            }
        }

        private void EncodeBtn_Click(object sender, EventArgs e)
        {
            string textToEncode = Encodetxt.Text;

            if (!string.IsNullOrEmpty(textToEncode))
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.CODE_128; // You can change the format as needed
                Bitmap barcodeBitmap = barcodeWriter.Write(textToEncode);

                pictureBox13.Image = barcodeBitmap;
            }
            else
            {
                MessageBox.Show("Please enter text to encode.");
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
