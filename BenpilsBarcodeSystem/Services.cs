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
    public partial class Services : Form
    {
        private User user;
        public Services(User user)
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
                PointofsalesBtn.Enabled = false;
                reportsBtn.Enabled = false;
                statisticsBtn.Enabled = false;
                usercredentialsbtn.Enabled = false;
                settingsbtn.Enabled = false;
            }
            else if (user.Designation == "Cashier")
            {
                inventoryBtn.Enabled = false;
                purchasingBtn.Enabled = false;
                reportsBtn.Enabled = false;
                statisticsBtn.Enabled = false;
                usercredentialsbtn.Enabled = false;
                settingsbtn.Enabled = false;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            ConfirmationExit CE = new ConfirmationExit();
            CE.StartPosition = FormStartPosition.CenterScreen;
            CE.ShowDialog();
        }

        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void DashBoardBtn_Click(object sender, EventArgs e)
        {
            Dashboard dash = new Dashboard(user);
            dash.Show();
            dash.StartPosition = FormStartPosition.Manual;
            dash.Location = this.Location;
            this.Hide();
        }

        private void PointofsalesBtn_Click(object sender, EventArgs e)
        {
            PointOfSales pos = new PointOfSales(user);
            pos.Show();
            pos.StartPosition = FormStartPosition.Manual;
            pos.Location = this.Location;
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void inventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory(user);
            inv.Show();
            inv.StartPosition = FormStartPosition.CenterScreen;
            inv.Location = this.Location;
            this.Hide();
        }

        private void purchasingBtn_Click(object sender, EventArgs e)
        {
            Purchasing purchasing = new Purchasing(user);
            purchasing.Show();
            purchasing.StartPosition = FormStartPosition.Manual;
            purchasing.Location = this.Location;
            this.Hide();
        }

        private void reportsBtn_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports(user);
            reports.Show();
            reports.StartPosition = FormStartPosition.Manual;
            reports.Location = this.Location;
            this.Hide();
        }

        private void statisticsBtn_Click(object sender, EventArgs e)
        {
            StatisticReport sr = new StatisticReport(user);
            sr.Show();
            sr.StartPosition = FormStartPosition.Manual;
            sr.Location = this.Location;
            this.Hide();
        }

        private void usercredentialsbtn_Click(object sender, EventArgs e)
        {
            Ser UC = new Ser(user);
            UC.Show();
            UC.StartPosition = FormStartPosition.Manual;
            UC.Location = this.Location;
            this.Hide();
        }

        private void settingsbtn_Click(object sender, EventArgs e)
        {
            Settings set = new Settings(user);
            set.Show();
            set.StartPosition = FormStartPosition.Manual;
            set.Location = this.Location;
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
    }
}
