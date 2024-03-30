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
    public partial class DateDialog : Form
    {
        public DateTime receivingDate { get; set; }
        bool canClose = false;

        public DateDialog()
        {
            InitializeComponent();
            ReceivingDateDt.MinDate = DateTime.Today;
            receivingDate = ReceivingDateDt.Value;
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            DialogResult = DialogResult.Cancel;
        }

        private void ReceivingDateDt_ValueChanged(object sender, EventArgs e)
        {
            receivingDate = ReceivingDateDt.Value;
        }
    }
}
