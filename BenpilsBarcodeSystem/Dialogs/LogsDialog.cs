﻿using BenpilsBarcodeSystem.Helpers;
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
    public partial class LogsDialog : Form
    {
        public LogsDialog(List<object[]> data )
        {
            InitializeComponent();

            data.Reverse();

            foreach (var row in data)
            {
                TableTbl.Rows.Add(row);
            }
        }

        private void CloseCb_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CloseCb_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}