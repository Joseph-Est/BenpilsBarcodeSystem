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
    public partial class PointOfSales : Form
    {
        private bool isDragging = false;
        private int mouseX,mouseY;
        public PointOfSales()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory();
            inv.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Purchasing purchasing = new Purchasing();
            purchasing.Show();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport statisticReport = new StatisticReport();
            statisticReport.Show();
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserCredentials credentials = new UserCredentials();
            credentials.Show();
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

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

        private void PointOfSales_MouseDown(object sender, MouseEventArgs e)
        {

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
                this.Left += e.X - mouseY;
                this.Top += e.Y - mouseX;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
