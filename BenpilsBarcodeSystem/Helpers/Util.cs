using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static bool AreTextBoxesNullOrEmpty(params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    return true; 
                }
            }
            return false;
        }

        public static void SetTextBoxesReadOnly(bool mode, params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.ReadOnly = mode;
            }
        }

        public static string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = input.Trim(); 
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = Regex.Replace(input, @"\b\w", match => match.Value.ToUpper()); 
            return input;
        }
    }
}
