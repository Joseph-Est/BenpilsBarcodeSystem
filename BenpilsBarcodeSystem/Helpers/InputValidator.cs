using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
    }
}
