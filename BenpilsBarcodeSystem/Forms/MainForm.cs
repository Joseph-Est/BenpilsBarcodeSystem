using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using BenpilsBarcodeSystem.Repository;
using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BenpilsBarcodeSystem
{
    public partial class MainForm : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();
        public bool CanSwitchPanel { get; set; } = true;
        public bool updateInventoryTable { get; set; } = false;

        private CheckBox lastChecked = null;
        private readonly Dictionary<CheckBox, Image> checkedImages = new Dictionary<CheckBox, Image>();
        private readonly Dictionary<CheckBox, Image> uncheckedImages = new Dictionary<CheckBox, Image>();
        private readonly Dictionary<CheckBox, Form> checkBoxToForm = new Dictionary<CheckBox, Form>();
        private readonly Dictionary<CheckBox, Type> checkBoxToFormType = new Dictionary<CheckBox, Type>();

        private AutoBackupManager autoBackupManager;
        public AutoBackupManager AutoBackupManagerInstance => autoBackupManager;

        public MainForm()
        {
            InitializeComponent();
            LoadImages();
            SetForms();
            SetTimer();
            SetUser();
            Checkbox_Clicked(CurrentUser.User.Designation == "Cashier" ? PosCb : CurrentUser.User.Designation == "Inventory Manager" ? InventoryCb : DashboardCb, null);
            SwitchForm(CurrentUser.User.Designation == "Cashier" ? PosCb : CurrentUser.User.Designation == "Inventory Manager" ? InventoryCb : DashboardCb);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            autoBackupManager = new AutoBackupManager();
            autoBackupManager.SetupAutoBackup(this);
        }

        private void Checkbox_Clicked(object sender, EventArgs e)
        {
            if (CanSwitchPanel)
            {
                CheckBox currentCheckBox = sender as CheckBox;

                if (currentCheckBox.Checked)
                {
                    return;
                }

                currentCheckBox.Checked = true;
                currentCheckBox.ForeColor = Color.Black;
                currentCheckBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
                currentCheckBox.Image = checkedImages[currentCheckBox];
                SwitchForm(currentCheckBox);

                if (lastChecked != null)
                {
                    lastChecked.Checked = false;
                    lastChecked.ForeColor = Color.White;
                    lastChecked.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 80, 80);
                    lastChecked.Image = uncheckedImages[lastChecked];
                }

                lastChecked = currentCheckBox;
            }
            else
            {
                MessageBox.Show("Please complete the ongoing transaction.", "Transaction in Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private async void LogoutBtn_Click(object sender, EventArgs e)
        {
            Confirmation confirmation = new Confirmation("Are you sure you want to logout?", null, "Yes", "Cancel");
            DialogResult result = confirmation.ShowDialog();

            if (result == DialogResult.Yes)
            {
                UserCredentialsRepository repository = new UserCredentialsRepository();
                if (await repository.LogoutAsync()) {
                    this.Close();
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                }
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
                Application.Exit();
            }
        }

        private void SwitchForm(CheckBox checkBox)
        {
            Form form;
            if (checkBoxToForm.ContainsKey(checkBox))
            {
                form = checkBoxToForm[checkBox];
            }
            else
            {
                if (checkBoxToFormType.ContainsKey(checkBox))
                {
                    Type formType = checkBoxToFormType[checkBox];
                    form = Activator.CreateInstance(formType) as Form;
                    checkBoxToForm[checkBox] = form;
                }
                else
                {
                    return;
                }
            }

           
            SelectedModuleLbl.Text = checkBox.Text.Trim();
            MainPanel.Controls.Clear();
            
            form.TopLevel = false;
            MainPanel.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();

            if (form is POS posForm)
            {
                posForm.SelectBarcode();
            }else if (form is Inventory inventoryForm && updateInventoryTable)
            {
                inventoryForm.updateTable();
                updateInventoryTable = false;
            }
            else if (form is Ser users)
            {
                users.updateTable();
                updateInventoryTable = false;
            }
            else if (form is Ser userForm)
            {
                userForm.updateTable();
            }
            else if (form is Dashboard dashForm && (MainPanel.Controls[0] is Dashboard))
            {
                if (!dashForm.IsLoadCalled)
                {
                    dashForm.Dashboard_Load(null, null);
                }
            }
        }

        private void SetUser()
        {
            label1.Text = "Username : " + CurrentUser.User.Username;
            label2.Text = "Designation : " + CurrentUser.User.Designation;

            if (CurrentUser.User.Designation == "Super Admin")
            {

            }
            else if (CurrentUser.User.Designation == "Admin")
            {

            }
            else if (CurrentUser.User.Designation == "Inventory Manager")
            {
                DashboardCb.Visible = false;
                PosCb.Visible = false;
                ReportsCb.Visible = false;
                StatisticsCb.Visible = false;
                UsersCb.Visible = false;
                SettingsCb.Visible = false;
            }
            else if (CurrentUser.User.Designation == "Cashier")
            {
                DashboardCb.Visible = false;
                InventoryCb.Visible = false;
                PurchasingCb.Visible = false;
                ReportsCb.Visible = false;
                StatisticsCb.Visible = false;
                UsersCb.Visible = false;
                SettingsCb.Visible = false;
            }
        }

        private void SetTimer()
        {
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void LoadImages()
        {
            checkedImages[DashboardCb] = Properties.Resources.icons8_dashboard_30;
            uncheckedImages[DashboardCb] = Properties.Resources.icons8_dashboard_30_white;
            checkedImages[InventoryCb] = Properties.Resources.icons8_inventory_30;
            uncheckedImages[InventoryCb] = Properties.Resources.icons8_inventory_30_white;
            checkedImages[PurchasingCb] = Properties.Resources.icons8_basket_30;
            uncheckedImages[PurchasingCb] = Properties.Resources.icons8_basket_30_white;
            checkedImages[PosCb] = Properties.Resources.icons8_point_of_sales_30;
            uncheckedImages[PosCb] = Properties.Resources.icons8_point_of_sales_30_white;
            checkedImages[ReportsCb] = Properties.Resources.icons8_graph_report_30_ios;
            uncheckedImages[ReportsCb] = Properties.Resources.icons8_graph_report_30_white;
            checkedImages[StatisticsCb] = Properties.Resources.icons8_statistics_30;
            uncheckedImages[StatisticsCb] = Properties.Resources.icons8_analytics_30_white;
            checkedImages[UsersCb] = Properties.Resources.icons8_user_301;
            uncheckedImages[UsersCb] = Properties.Resources.icons8_user_30_white;
            checkedImages[SettingsCb] = Properties.Resources.icons8_services_30;
            uncheckedImages[SettingsCb] = Properties.Resources.icons8_services_30__white;
        }

        private void SetForms()
        {
            checkBoxToFormType[DashboardCb] = typeof(Dashboard);
            checkBoxToFormType[InventoryCb] = typeof(Inventory);
            checkBoxToFormType[PurchasingCb] = typeof(PurchaseOrder);
            checkBoxToFormType[PosCb] = typeof(POS);
            checkBoxToFormType[ReportsCb] = typeof(Reports);
            checkBoxToFormType[StatisticsCb] = typeof(StatisticReport);
            checkBoxToFormType[UsersCb] = typeof(Ser);
            checkBoxToFormType[SettingsCb] = typeof(Settings);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            label4.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            label3.Text = "Date: " + Util.ConvertDateLong(DateTime.Now);
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

        private void MainPanel_Resize(object sender, EventArgs e)
        {
        }
    }
}
