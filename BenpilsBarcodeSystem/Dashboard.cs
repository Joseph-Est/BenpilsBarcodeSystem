using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BenpilsBarcodeSystem
{
    public partial class Dashboard : Form
    {
        private bool IsDragging = false;
        private int mouseX,mouseY;
       
        public Dashboard()
        {
            InitializeComponent();

        
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos =  new PointOfSales();
            pos.Show();
            this.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               IsDragging = true;
                mouseX = e.X;
                mouseY = e.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDragging)
            {
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            IsDragging = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
