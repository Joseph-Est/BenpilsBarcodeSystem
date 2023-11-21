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
        private User user;
        public Dashboard(User user)
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000; 
            timer.Tick += timer1_Tick;
            timer.Start();
            this.user = user;
            label1.Text = "Username: " + user.Username;
            label2.Text = "Designation: " + user.Designation;
            if (user.Designation == "Superadmin")
            {
            }
            else if (user.Designation == "Admin")
            {
            }
            else if (user.Designation == "Inventory Manager")
            {
                PointOfSalesBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticsBtn.Enabled = false;
                ServicesBtn.Enabled = false;
                UsercredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
            else if (user.Designation == "Cashier")
            {
                InventoryBtn.Enabled = false;
                PurchasingBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticsBtn.Enabled = false;
                UsercredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

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
            Inventory inv = new Inventory(user);
            inv.Show();
            inv.StartPosition = FormStartPosition.Manual;
            inv.Location = this.Location;
            this.Hide();
        }
        //purchasing button
        private void button5_Click(object sender, EventArgs e)
        {
            Purchaserr purchasing = new Purchaserr(user);
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }
        //reports button
        private void button6_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports(user);
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }
        //Statistic reports button
        private void button7_Click(object sender, EventArgs e)
        {
            StatisticReport statisticReport = new StatisticReport(user);
            statisticReport.Show();
            statisticReport.StartPosition = FormStartPosition.Manual;
            statisticReport.Location = this.Location;
            this.Hide();
        }
        //Usercredentials button
        private void button8_Click(object sender, EventArgs e)
        {
            Ser credentials = new Ser(user);
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
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfirmationLogout cl = new ConfirmationLogout();
            cl.StartPosition = FormStartPosition.CenterScreen;
            if (cl.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            ConfirmationExit cl = new ConfirmationExit();
            cl.StartPosition = FormStartPosition.CenterScreen;
            cl.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ServicesBtn_Click(object sender, EventArgs e)
        {
            Services service = new Services(user);
            service.Show();
            service.StartPosition = FormStartPosition.Manual;
            service.Location = this.Location;
            this.Hide();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void PointOfSalesBtn_Click(object sender, EventArgs e)
        {
            POS pos = new POS(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.CenterScreen;
            pos.Location = this.Location;
            this.Hide();
        }
    }
}
