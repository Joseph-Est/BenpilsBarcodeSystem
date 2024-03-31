using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
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

        public static string SanitizeString(string input)
        {
            return System.Security.SecurityElement.Escape(input);
        }

        public static void ResetComboBoxes(params ComboBox[] comboBoxes)
        {
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.Items.Clear();
                comboBox.Text = null;
                comboBox.SelectedIndex = -1;
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

        public static void SetComboBoxesDisabled(bool mode, params ComboBox[] comboBoxes)
        {
            foreach (ComboBox comboBox in comboBoxes)
            {
                comboBox.Enabled = !mode;
            }
        }

        public static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input.Trim()))
                return input.Trim();

            input = input.Trim().ToLower(); 
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = Regex.Replace(input, @"\b\w", match => match.Value.ToUpper()); 
            return input;
        }

        public static string CapitalizeOrNA(string input)
        {
            if (string.IsNullOrEmpty(input.Trim()))
                return "N/A";

            input = input.Trim().ToLower();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = Regex.Replace(input, @"\b\w", match => match.Value.ToUpper());
            return input;
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string GenerateRandomNumberWithLetter(int min, int max, string value)
        {
            Random random = new Random();
            return $"{value}{random.Next(min, max)}";
        }

        public static string ConvertDateLong(DateTime date)
        {
            string formattedDate = date.ToString("MMM dd, yyyy");
            return formattedDate;
        }

        public static string ConvertDateLongWithTime(DateTime date)
        {
            string formattedDate = date.ToString("MMM dd, yyyy hh:mm tt");
            return formattedDate;
        }

        public static string ConvertDateShort(DateTime date)
        {
            string formattedDate = date.ToString("MMM dd, yyyy");
            return formattedDate;
        }

        public static string ConvertDateShortWithTime(DateTime date)
        {
            string formattedDate = date.ToString("MMM dd, yyyy hh:mm tt");
            return formattedDate;
        }

        public static void PrintReceipt(Graphics graphics, string transactionNo, string[] products, decimal[] prices, decimal total, decimal paymentReceived = 0, decimal change = 0, string supplierName = null, string deliveryDate = null)
        {
            Font fontBold = new Font("Courier New", 12, FontStyle.Bold);
            Font fontDash = new Font("Courier New", 12, FontStyle.Regular);
            Font fontRegularSmall = new Font("Courier New", 10, FontStyle.Regular);
            Font fontBoldSmall = new Font("Courier New", 10, FontStyle.Bold);

            string dashes = "- - - - - - - - - - - - - - -";
            string space = " ";
            string date = $"Date: {DateTime.Now.ToString("MM/dd/yyyy")}";
            string supplier = "Supplier:";
            string delivery = "Delivery Date:";
            string shopName = "Benpils Motorcycle Parts and Accessories";
            string contactNo = "09295228592";
            string shopAddress = "Ortigas, Cainta, Rizal";
            string thankYouMessage = "Thank you for shopping, have a great day!!";

            int y = 10;

            y = DrawText(graphics, shopName, fontBold, y + 20, 315, 30);
            y = DrawText(graphics, dashes, fontDash, y, 315);
            y = DrawText(graphics, date, fontRegularSmall, y + 5, 315, 30);
            y = DrawText(graphics, shopAddress, fontBoldSmall, y + 10, 315);
            y = DrawText(graphics, contactNo, fontBoldSmall, y, 315);
            y = DrawText(graphics, transactionNo, fontRegularSmall, y + 20, 315);

            if (supplierName != null)
            {
                y = DrawText(graphics, dashes, fontDash, y + 10, 315);
                y = DrawText(graphics, supplier, fontRegularSmall, y, 315);
                y = DrawText(graphics, supplierName, fontBoldSmall, y, 315, 30);
                y = DrawText(graphics, space, fontDash, y, 315);
                y = DrawText(graphics, delivery, fontRegularSmall, y, 315);
                y = DrawText(graphics, deliveryDate, fontBoldSmall, y, 315);
            }

            y = DrawText(graphics, dashes, fontDash, y + 5, 315);
            y = DrawText(graphics, space, fontDash, y, 315);

            string[] leftText = { "Items" };
            string[] rightText = { "Subtotal" };

            for (int i = 0; i < leftText.Length; i++)
            {
                y = DrawTwoStringColumns(graphics, leftText[i], rightText[i], fontBoldSmall, y, 315, 10);
            }

            y = DrawText(graphics, space, fontDash, y, 315);

            for (int i = 0; i < products.Length; i++)
            {
                y = DrawTwoColumns(graphics, products[i], prices[i], fontRegularSmall, y, 315, 10);
            }

            y = DrawText(graphics, space, fontDash, y, 315);
            y = DrawText(graphics, dashes, fontDash, y + 5, 315);

            string[] totals = { "VAT", "Discount", "Total" };
            decimal[] amount = { 0.00m, 0.00m, total };

            for (int i = 0; i < totals.Length; i++)
            {
                y = DrawTwoColumns(graphics, totals[i], amount[i], fontBoldSmall, y, 315, 10);
            }

            if(paymentReceived > 0)
            {
                y = DrawText(graphics, space, fontDash, y, 315);

                string[] payment = { "Payment Received", "Change" };
                decimal[] paymentAmount = { paymentReceived, change };

                for (int i = 0; i < payment.Length; i++)
                {
                    y = DrawTwoColumns(graphics, payment[i], paymentAmount[i], fontBoldSmall, y, 315, 10);
                }
            }

            y = DrawText(graphics, dashes, fontDash, y + 20, 315);
            y = DrawText(graphics, space, fontDash, y, 315);
            y = DrawText(graphics, thankYouMessage, fontBoldSmall, y, 315, 50);
            y = DrawText(graphics, space, fontDash, y, 315);
        }

        private static int DrawText(Graphics graphics, string text, Font font, int startY, int maxWidth, int padding = 0)
        {
            string[] words = text.Split(' ');
            string line = words[0];
            int lineHeight = (int)font.GetHeight(graphics);

            for (int i = 1; i < words.Length; i++)
            {
                string newLine = line + " " + words[i];
                if (graphics.MeasureString(newLine, font).Width > maxWidth - 2 * padding)
                {
                    float startX = padding + (maxWidth - 2 * padding - graphics.MeasureString(line, font).Width) / 2;
                    graphics.DrawString(line, font, Brushes.Black, new PointF(startX, startY));
                    line = words[i];
                    startY += lineHeight;
                }
                else
                {
                    line = newLine;
                }
            }

            float finalStartX = padding + (maxWidth - 2 * padding - graphics.MeasureString(line, font).Width) / 2;
            graphics.DrawString(line, font, Brushes.Black, new PointF(finalStartX, startY));
            return startY + lineHeight;
        }

        private static int DrawTwoColumns(Graphics graphics, string leftText, decimal rightAmount, Font font, int startY, int maxWidth, int padding = 0)
        {
            int productMaxWidth = maxWidth - 100 - 2 * padding;

            string[] words = leftText.Split(' ');
            string line = words[0];
            int lineHeight = (int)font.GetHeight(graphics);

            for (int i = 1; i < words.Length; i++)
            {
                string newLine = line + " " + words[i];
                if (graphics.MeasureString(newLine, font).Width > productMaxWidth)
                {
                    graphics.DrawString(line, font, Brushes.Black, new PointF(padding, startY));
                    line = words[i];
                    startY += lineHeight;
                }
                else
                {
                    line = newLine;
                }
            }

            graphics.DrawString(line, font, Brushes.Black, new PointF(padding, startY));
            string priceText = InputValidator.DecimalToFormattedStringPrice(rightAmount);
            float priceX = maxWidth - padding - graphics.MeasureString(priceText, font).Width;
            graphics.DrawString(priceText, font, Brushes.Black, new PointF(priceX, startY));
            return startY + lineHeight;
        }

        private static int DrawTwoStringColumns(Graphics graphics, string leftText, string rightText, Font font, int startY, int maxWidth, int padding = 0)
        {
            int leftTextMaxWidth = maxWidth - 100 - 2 * padding;

            string[] words = leftText.Split(' ');
            string line = words[0];
            int lineHeight = (int)font.GetHeight(graphics);

            for (int i = 1; i < words.Length; i++)
            {
                string newLine = line + " " + words[i];
                if (graphics.MeasureString(newLine, font).Width > leftTextMaxWidth)
                {
                    graphics.DrawString(line, font, Brushes.Black, new PointF(padding, startY));
                    line = words[i];
                    startY += lineHeight;
                }
                else
                {
                    line = newLine;
                }
            }

            graphics.DrawString(line, font, Brushes.Black, new PointF(padding, startY));
            float rightTextX = maxWidth - padding - graphics.MeasureString(rightText, font).Width;
            graphics.DrawString(rightText, font, Brushes.Black, new PointF(rightTextX, startY));
            return startY + lineHeight;
        }

        public static void SetDateTimePickerFormat(string format, params DateTimePicker[] dateTimePickers)
        {
            foreach (var dtp in dateTimePickers)
            {
                dtp.Format = DateTimePickerFormat.Custom;
                dtp.CustomFormat = format;
            }
        }
    }
}
