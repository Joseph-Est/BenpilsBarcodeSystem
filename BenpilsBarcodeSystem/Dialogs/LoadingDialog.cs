﻿using System;
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
    public partial class LoadingDialog : Form
    {
        public LoadingDialog(string title)
        {
            InitializeComponent();
            TitleLbl.Text = title;
        }
    }
}