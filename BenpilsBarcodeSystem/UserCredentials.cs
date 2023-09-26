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
    public partial class UserCredentials : Form
    {
        private bool isDragging = false;
        private int mouseX,mouseY;

        public UserCredentials()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales();
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory();
            inv.Show(); 
            inv.StartPosition = FormStartPosition.Manual;
            inv.Location = this.Location;
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Purchasing purchasing = new Purchasing();
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport sreport = new StatisticReport();
            sreport.Show();
            sreport.StartPosition = FormStartPosition.Manual;
            sreport.Location = this.Location;
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
          
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = this.Location;
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
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
    }
}
