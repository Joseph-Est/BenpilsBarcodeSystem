﻿using System;
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
namespace BenpilsBarcodeSystem
{
    public partial class GenerateBarcode : Form
    {
        private bool isDragging = false;
        private int mouseX, mouseY;
        public GenerateBarcode()
        {
            InitializeComponent();
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
        private void ManualGenerateBtn_Click(object sender, EventArgs e)
        {
            string inputText = ManualRegenratetxt.Text;

            if (!string.IsNullOrWhiteSpace(inputText))
            {
                BarcodeWriter barcodeWriter = new BarcodeWriter();
                barcodeWriter.Format = BarcodeFormat.CODE_128;

                // Generate the barcode image.
                var barcodeBitmap = barcodeWriter.Write(inputText);

                // Display the generated barcode in the PictureBox.
                generatedpicture.Image = barcodeBitmap;
            }
            else
            {
                MessageBox.Show("Please enter data to generate a barcode.");
            }
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            GeneratedBarcodeTxt.Text = "";
            ManualRegenratetxt.Text  = "";
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

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            
        }

        private void ManualRegenratetxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

     
    }
}
