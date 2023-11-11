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
    public partial class Print_Invoice : Form
    {
        public Print_Invoice()
        {
            InitializeComponent();
        }
        //Closebutton
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //MinimizedButton
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
