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
    public partial class Purchasing : Form
    {
        private bool isDragging = false;
        private int mouseX,mouseY;
        public Purchasing()
        {
            InitializeComponent();
        }

        private void Purchasing_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard();
            dash.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales();
            pos.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.Show();
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
            StatisticReport sreport = new StatisticReport();
            sreport.Show();
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
            Settings set = new Settings();
            set.Show();
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
