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
        private User user;
        public Purchasing(User user)
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
                StatisticsReportBtn.Enabled = false;
                ServicesBtn.Enabled = false;
                UserCredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
            else if (user.Designation == "Cashier")
            {
                InventoryBtn.Enabled = false;
                PurchasingBtn.Enabled = false;
                ReportsBtn.Enabled = false;
                StatisticsReportBtn.Enabled = false;
                UserCredentialsBtn.Enabled = false;
                SettingsBtn.Enabled = false;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }
   

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConfirmationLogout cl = new ConfirmationLogout();
            if (cl.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }
        private void DashBoardBtn_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

      

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory(user);
            inventory.Show();
            inventory.StartPosition = FormStartPosition.Manual;
            inventory.Location = this.Location;
            this.Hide();
        }

        private void PurchasingBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            Reports rp = new Reports(user);
            rp.Show();
            rp.StartPosition = FormStartPosition.Manual;
            rp.Location = this.Location;
            this.Hide();
        }

        private void StatisticsReportBtn_Click(object sender, EventArgs e)
        {
            StatisticReport SR = new StatisticReport(user);
            SR.Show();
            SR.StartPosition = FormStartPosition.Manual;
            SR.Location = this.Location;
            this.Hide();
        }

        private void UserCredentialsBtn_Click(object sender, EventArgs e)
        {
            Ser UC = new Ser(user);
            UC.Show();
            UC.StartPosition = FormStartPosition.Manual;
            UC.Location = this.Location;
            this.Hide();
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Settings set = new Settings(user);
            set.Show();
            set.StartPosition = FormStartPosition.Manual;
            set.Location = this.Location;
            this.Hide();
        }

        private void PointOfSalesBtn_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }

       

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.StartPosition = FormStartPosition.CenterScreen;
            CE.ShowDialog();
        }

        private void LogoutBtn_Click_1(object sender, EventArgs e)
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

        private void ServicesBtn_Click(object sender, EventArgs e)
        {
            Services service = new Services(user);
            service.Show();
            service.StartPosition = FormStartPosition.Manual;
            service.Location = this.Location;
            this.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
