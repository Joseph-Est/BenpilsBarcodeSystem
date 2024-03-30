using System;
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
            };
        }

        public static void AllowOnlyDigitsMinMax(TextBox textBox, int min, int max)
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (char.IsDigit(e.KeyChar))
                {
                    // If the textbox content is highlighted, bypass the limits
                    if (textBox.SelectedText.Length == textBox.TextLength)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        string newText = textBox.Text + e.KeyChar;

                        if (int.TryParse(newText, out int number))
                        {
                            if (number < min || number > max)
                            {
                                e.Handled = true;
                            }
                        }
                    }
                }
            };
        }

        public static void DGAllowOnlyDigitsMinMax(DataGridViewTextBoxEditingControl textBox, int min, int max)
        {
            textBox.KeyPress += (sender, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (char.IsDigit(e.KeyChar))
                {
                    // If the textbox content is highlighted, bypass the limits
                    if (textBox.SelectedText.Length == textBox.TextLength)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        string newText = textBox.Text + e.KeyChar;

                        if (int.TryParse(newText, out int number))
                        {
                            if (number < min || number > max)
                            {
                                e.Handled = true;
                            }
                        }
                    }
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
                    if (textBox.SelectionStart > decimalIndex && textBox.Text.Length - decimalIndex > 2)
                    {
                        e.Handled = true;
                    }
                }
            };
        }

        public static string CheckIfEmptyReturnZero(string input)
        {
            input = input.Replace(" ", "").Trim();

            if (string.IsNullOrEmpty(input))
            {
                return "0";
            }
            else
            {
                int value;
                return int.TryParse(input, out value) ? input : "0";
            }
        }

        public static string CheckIfEmptyReturnNA(string input)
        {
            input = input.Trim();
            return string.IsNullOrEmpty(input) ? "N/A" : input;
        }
    }
}
