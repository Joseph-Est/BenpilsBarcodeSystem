﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Utils
{
    internal class Util
    {
        public static void ClearTextBoxes(params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }
        }
    }
}
