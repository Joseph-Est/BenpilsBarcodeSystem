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
    public partial class ConfirmationWithRemarks : Form
    {
        bool canClose = false;
        public string Remarks { get; set; }

        public ConfirmationWithRemarks(string title, string message)
        {
            InitializeComponent();
            TitleLbl.Text = title;
            MessageLbl.Text = message;
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            Remarks = RemarksTxt.Text.Trim();
            DialogResult = DialogResult.OK;
            canClose = true;
            Close();
        }

        private void ConfrimationWithRemarks_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            Close();
        }
    }
}
