using BenpilsBarcodeSystem.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        bool canClose = false;
        public GenerateBarcode()
        {
            InitializeComponent();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            string randomBarcode = rand.Next(1000000, 9999999).ToString();
            GeneratedBarcodePb.Image = Util.GenerateBarcode(randomBarcode);
            PrintBtn.Enabled = true;
            BarcodeTxt.Text = randomBarcode;
            CopyBtn.Enabled = true;
        }
        private void ManualGenerateBtn_Click(object sender, EventArgs e)
        {
            
            if (ManualGenerateBtn.Text.Contains("Manual"))
            {
                BarcodeTxt.ReadOnly = false;
                GenerateBtn.Enabled = false;
                ManualGenerateBtn.Text = " Generate";
                PrintBtn.Enabled = false;
                GeneratedBarcodePb.Image = null;
                BarcodeTxt.Text = "";
                BarcodeTxt.Focus();
            }
            else
            {
                string inputText = BarcodeTxt.Text;

                if (!string.IsNullOrWhiteSpace(inputText))
                {
                    GeneratedBarcodePb.Image = Util.GenerateBarcode(inputText);
                    PrintBtn.Enabled = true;
                    ManualGenerateBtn.Text = " Manual";
                    BarcodeTxt.ReadOnly = true;
                    GenerateBtn.Enabled = true;
                    CopyBtn.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please enter the Data for which you want to generate a barcode.", "No Data Entered", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            if (GeneratedBarcodePb.Image != null)
            {
                Util.PrintBarcode(GeneratedBarcodePb, 1.46f, 0.2f);
            }
            else
            {
                MessageBox.Show("No barcode to print.", "No Barcode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ManualGenerateBtn.Enabled = false;
            GenerateBtn.Enabled = true;
            BarcodeTxt.ReadOnly = true;
            PrintBtn.Enabled = false;
            BarcodeTxt.Text  = "";
            GeneratedBarcodePb.Image = null;
        }
        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }
        private void Panel1_MouseUp(object sender, MouseEventArgs e)
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

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(BarcodeTxt.Text))
            {
                canClose = true;
                Clipboard.SetText(BarcodeTxt.Text);
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void GenerateBarcode_Load(object sender, EventArgs e)
        {
            BarcodeTxt.Focus();
        }

        private void GenerateBarcode_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void ManualRegenratetxt_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BarcodeTxt.Text.Trim()))
            {
                CopyBtn.Enabled = false;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            this.Close();
        }
    }
}
