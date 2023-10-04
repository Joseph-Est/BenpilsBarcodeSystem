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
    public partial class StatisticReport : Form
    {
        private bool isDragging = false;
        private int mouseX,mouseY;
        private User user;
        public StatisticReport(User user)
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer1_Tick;
            timer.Start();
            this.user = user;
            label1.Text = "Username: " + user.Username;
            label2.Text = "Designation: " + user.Designation;
            if (user.Designation == "Employee")
            {
                button8.Enabled = false;
            }
        }
        //Dashboard Button
        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }
        //Point of sales button
        private void button3_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }
        //Inventory Button
        private void button2_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory(user);
            inventory.Show();
            inventory.StartPosition = FormStartPosition.Manual;
            inventory.Location = this.Location;
            this.Hide();
        }
        //Purchasing button
        private void button5_Click(object sender, EventArgs e)
        {
            Purchasing purchasing = new Purchasing(user);
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }
        //Reports Button
        private void button6_Click(object sender, EventArgs e)
        {
            Reports rep = new Reports(user);
            rep.Show();
            rep.StartPosition = FormStartPosition.Manual;
            rep.Location = this.Location;
            this.Hide();
        }
        //Usercredentials button
        private void button8_Click(object sender, EventArgs e)
        {
            UserCredentials credentials = new UserCredentials(user);    
            credentials.Show();
            credentials.StartPosition = FormStartPosition.Manual;
            credentials.Location = this.Location;
            this.Hide();
        }
        //Settings Button
        private void button9_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(user);
            settings.Show();
            settings.StartPosition = FormStartPosition.Manual;
            settings.Location = this.Location;
            this.Hide();
        }

        private void StatisticReport_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        //Close Button
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit ce = new ConfirmationExit();
            ce.StartPosition = FormStartPosition.CenterScreen;
            ce.ShowDialog();
        }
        //Minimize Button
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
                this.Left += e.X - mouseX;
                this.Top += e.Y - mouseY;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "TIME: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "DATE: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
