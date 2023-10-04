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
            Timer timer = new Timer();
            timer.Interval = 1000; 
            timer.Tick += timer1_Tick;
            timer.Start();
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        //To point of sales
        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos =  new PointOfSales();
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }
        //close button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
        }
        //minimize button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //inventory button
        private void button2_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory();
            inv.Show();
            inv.StartPosition = FormStartPosition.Manual;
            inv.Location = this.Location;
            this.Hide();
        }
        //purchasing button
        private void button5_Click(object sender, EventArgs e)
        {
            Purchasing purchasing = new Purchasing();
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }
        //reports button
        private void button6_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }
        //Statistic reports button
        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport statisticReport = new StatisticReport();
            statisticReport.Show();
            statisticReport.StartPosition = FormStartPosition.Manual;
            statisticReport.Location = this.Location;
            this.Hide();
        }
        //Usercredentials button
        private void button8_Click(object sender, EventArgs e)
        {
            UserCredentials credentials = new UserCredentials();
            credentials.Show();
            credentials.StartPosition = FormStartPosition.Manual;
            credentials.Location = this.Location;
            this.Hide();
        }
        //Settings Button
        private void button9_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = this.Location;
            this.Hide();
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
    
        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "TIME: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "DATE: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
