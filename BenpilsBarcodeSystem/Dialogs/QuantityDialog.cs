using BenpilsBarcodeSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Dialogs
{
    public partial class QuantityDialog : Form
    {
        public int quantity { get; set; }
        bool canClose = false;
        public QuantityDialog()
        {
            InitializeComponent();
            InputValidator.AllowOnlyDigits(QuantityTxt);
        }

        private void QuantityDialog_Load(object sender, EventArgs e)
        {
            QuantityTxt.Text = "1";
            QuantityTxt.Select();
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            if (InputValidator.ParseToInt(QuantityTxt.Text) > 0)
            {
                quantity = InputValidator.ParseToInt(QuantityTxt.Text);
                canClose = true;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid quantity");
            }
        }

        private void QuantityDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }
    }
}
