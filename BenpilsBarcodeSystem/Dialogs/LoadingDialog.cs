using BenpilsBarcodeSystem.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Dialogs
{
    enum LoadingFor
    {
        ManualBackup = 1,
    }


    public partial class LoadingDialog : Form
    {
        LoadingFor loadingFor;
        Dictionary<DataTable, string> dataTableSheetMapping;
        private Timer hideLoadingTimer;
        private string message;
        private bool canClose = false;

        internal LoadingDialog(string title, LoadingFor loadingFor = LoadingFor.ManualBackup, Dictionary<DataTable, string> dataTableSheetMapping = null)
        {
            InitializeComponent();
            TitleLbl.Text = title;
            this.loadingFor = loadingFor;
            this.dataTableSheetMapping = dataTableSheetMapping;

            hideLoadingTimer = new Timer();
            hideLoadingTimer.Interval = 2000; 
            hideLoadingTimer.Tick += HideLoadingTimer_Tick;
        }

        private void LoadingDialog_Load(object sender, EventArgs e)
        {
           
            if (loadingFor == LoadingFor.ManualBackup)
            {
                ManualBackup();
            }
        }

        private void ManualBackup()
        {
            switch (Util.ExportData(this, dataTableSheetMapping))
            {
                case 0:
                    message = "Backup completed successfully.";
                    break;
                case 1:
                    message = "Backup failed: No data available.";
                    break;
                case 2:
                    message = "Backup failed: Unable to overwrite existing file.";
                    break;
                case 3:
                    message = "Backup failed: Unable to export backup.";
                    break;
                case 5:
                    canClose = true;
                    this.Close();
                    break;
                default:
                    break;
            }

            hideLoadingTimer.Start();
        }

        private void HideLoadingTimer_Tick(object sender, EventArgs e)
        {
            TitleLbl.Text = message;
            AcceptBtn.Visible = true;

            //if (backupSuccess)
            //{
            //    AcceptBtn.BackColor = Color.FromArgb(26, 185, 93);
            //}
            //else
            //{
            //    AcceptBtn.BackColor = Color.FromArgb(193, 57, 57);
            //}

            hideLoadingTimer.Stop();
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            canClose = true;
            this.Close();
        }

        private void LoadingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!canClose)
            {
                e.Cancel = true;
            }
            else
            {
                hideLoadingTimer.Stop();
            }
        }
    }
}
