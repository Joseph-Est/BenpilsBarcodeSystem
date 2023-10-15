using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                Zen.Barcode.Code128BarcodeDraw brCode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                pictureBox13.Image = brCode.Draw(barCode, 60);
            }
            catch (Exception)
            {

            }
        }

        private void BarcodeGenerator_Load(object sender, EventArgs e)
        {
            this.AcceptButton = GenerateBtn;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
