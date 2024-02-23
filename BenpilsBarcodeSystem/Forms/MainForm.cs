using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BenpilsBarcodeSystem
{
    public partial class MainForm : Form
    {
        private User user;
        public MainForm(User user)
        {
            InitializeComponent();

            Timer timer = new Timer();
            timer.Interval = 1000; 
            timer.Tick += timer_Tick;
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

        private void SwitchForm(Form form)
        {
            mainPanel.Controls.Clear();
            form.TopLevel = false;
            mainPanel.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        private void DashboardBtn_Click(object sender, EventArgs e)
        {

        }

        private void PointOfSalesBtn_Click_1(object sender, EventArgs e)
        {
            POS pos = new POS();
            SwitchForm(pos);
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory();
            SwitchForm(inv);
        }

        private void PurchasingBtn_Click(object sender, EventArgs e)
        {
            PurchaseOrder purchasing = new PurchaseOrder();
            SwitchForm(purchasing);

        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            SwitchForm(reports);
        }

        private void StatisticsBtn_Click(object sender, EventArgs e)
        {
            StatisticReport statisticReport = new StatisticReport();
            SwitchForm(statisticReport);
        }

        private void UsercredentialsBtn_Click(object sender, EventArgs e)
        {
            Ser credentials = new Ser();
            SwitchForm(credentials);
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            SwitchForm(settings);
        }

        private void LogoutBtn_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ConfirmationLogout cl = new ConfirmationLogout
            {
                StartPosition = FormStartPosition.CenterScreen
            };

            if (cl.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Confirmation confirmation = new Confirmation("Are you sure you want to exit?", "Confirm", "Cancel");
            this.Opacity = 0.90;
            DialogResult result = confirmation.ShowDialog();
            this.Opacity = 1;

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
