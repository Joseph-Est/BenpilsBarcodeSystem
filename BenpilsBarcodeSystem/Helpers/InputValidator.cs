﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Helpers
{
    internal class InputValidator
    {
        public static bool IsValidPrice(string input)
        {
            string cleanedInput = input.Replace(",", "").Trim();
            string pattern = @"^\d+(\.\d{1,2})?$";
            return Regex.IsMatch(cleanedInput, pattern);
        }

        public static bool IsValidInt(string input)
        {
            string trimmedInput = input.Trim();

            int result;
            return int.TryParse(trimmedInput, out result);
        }

        public static decimal ParseToDecimal(string input)
        {
            string cleanedInput = input.Replace(",", "").Trim();

            decimal result;

            if (decimal.TryParse(cleanedInput, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out result))
            {
                return Math.Round(result, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                return 0;
            }
        }

        public static int ParseToInt(string input)
        {
            string trimmedInput = input.Trim();

            int result;
            if (int.TryParse(trimmedInput, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static string DecimalToFormattedStringPrice(decimal price)
        {
            return price.ToString("#,##0.00");
        }

        public static string StringToFormattedPrice(string price)
        {
            if (!decimal.TryParse(price, out decimal parsedPrice))
            {
                return "0.00";
            }

            return parsedPrice.ToString("#,##0.00", CultureInfo.GetCultureInfo("en-US"));
        }

         
        public static string FormatPriceWithoutCommas(string price)
        {
            return price.Replace(",", "");
        }

        public static bool IsStringNotEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static void AllowOnlyDigits(TextBox textBox)
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (textBox.Text.Length >= 9 && !char.IsControl(e.KeyChar)) 
                {
                    e.Handled = true;
                }
            };
        }

        public static void AllowOnlyDigitsAndDecimal(TextBox textBox)
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == '.' && (textBox.Text.Contains(".") || textBox.Text.Length == 0))
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == '.' && textBox.SelectionStart == 0)
                {
                    e.Handled = true;
                }
                else if (e.KeyChar == ' ' || (textBox.Text.Contains(".") && e.KeyChar == '0' && textBox.Text.Length == 1))
                {
                    e.Handled = true;
                }
                else if (char.IsDigit(e.KeyChar) && textBox.Text.Contains("."))
                {
                    int decimalIndex = textBox.Text.IndexOf('.');
                    if (textBox.Text.Substring(decimalIndex).Length > 2) 
                    {
                        e.Handled = true;
                    }
                }
                else if (!char.IsControl(e.KeyChar) && textBox.Text.Length >= 10) 
                {
                    e.Handled = true;
                }
            };
        }
    }
}
