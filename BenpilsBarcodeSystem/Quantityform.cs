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
    public partial class Quantityform : Form
    {
        public int Quantity { get; private set; }
        public Quantityform()
        {
            InitializeComponent();
        }

        private void Btnokay_Click(object sender, EventArgs e)
        {
            if (int.TryParse(quanitityTxt.Text, out int quantity))
            {
                Quantity = quantity;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
