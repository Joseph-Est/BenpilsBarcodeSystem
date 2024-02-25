using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BenpilsBarcodeSystem
{
    public partial class MainForm : Form
    {
        private User user;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

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

        private void SwitchForm(Form form, string module)
        {
            SelectedModuleLbl.Text = module;
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
            SwitchForm(pos, "Point of Sales");
            
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            Inventory inv = new Inventory();
            SwitchForm(inv, "Inventory");
        }

        private void PurchasingBtn_Click(object sender, EventArgs e)
        {
            PurchaseOrder purchasing = new PurchaseOrder();
            SwitchForm(purchasing, "Purchasing");

        }

        private void ReportsBtn_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            SwitchForm(reports, "Reports");
        }

        private void StatisticsBtn_Click(object sender, EventArgs e)
        {
            StatisticReport statisticReport = new StatisticReport();
            SwitchForm(statisticReport, "Statistic Report");
        }

        private void UsercredentialsBtn_Click(object sender, EventArgs e)
        {
            Ser credentials = new Ser();
            SwitchForm(credentials, "User Credentials");
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            SwitchForm(settings, "Settings");
        }

        private void LogoutBtn_Click_1(object sender, EventArgs e)
        {
            Confirmation confirmation = new Confirmation("Are you sure you want to logout?", null, "Yes", "Cancel");
            DialogResult result = confirmation.ShowDialog();

            if (result == DialogResult.Yes)
            {
                this.Close();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }
        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MaximizeBtn_Click(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Confirmation confirmation = new Confirmation("Are you sure you want to exit?", null, "Yes", "Cancel");
            DialogResult result = confirmation.ShowDialog();

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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
    
        }
    }
}
