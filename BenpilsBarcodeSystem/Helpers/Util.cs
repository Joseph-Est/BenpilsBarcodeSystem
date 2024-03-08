using BenpilsBarcodeSystem.Entities;
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

        public static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input.Trim()))
                return input.Trim();

            input = input.Trim(); 
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = Regex.Replace(input, @"\b\w", match => match.Value.ToUpper()); 
            return input;
        }

        public static string CapitalizeOrNA(string input)
        {
            if (string.IsNullOrEmpty(input.Trim()))
                return "N/A";

            input = input.Trim();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = Regex.Replace(input, @"\b\w", match => match.Value.ToUpper());
            return input;
        }

        public static int GenerateRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string ConvertDate(DateTime date)
        {
            string formattedDate = date.ToString("MMM dd, yyyy");
            return formattedDate;
        }

        public static async Task SavePurchaseOrderReceiptAsync(string transactionNo, Cart cart, string thankYouMessage)
        {
            int receiptWidth = 200; // adjust this value to fit your 57mm paper size
            Bitmap bmp = new Bitmap(receiptWidth, 500); // adjust the height as needed
            bmp.MakeTransparent(); // make the background white

            Graphics graphic = Graphics.FromImage(bmp);

            Font font = new Font("Courier New", 12);

            float fontHeight = font.GetHeight();

            int startX = 10;
            int startY = 10;
            int offset = 40;

            graphic.DrawString("Company Name", new Font("Courier New", 18), new SolidBrush(Color.Black), startX, startY);
            graphic.DrawString("Date: " + DateTime.Now, new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;
            graphic.DrawString("Transaction No: " + transactionNo, new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + offset);

            string productLine = "Item Name".PadRight(30) + "Quantity".PadRight(10) + "Price";
            graphic.DrawString(productLine, font, new SolidBrush(Color.Black), startX, startY + offset);

            offset = offset + (int)fontHeight + 5;

            foreach (var item in cart.Items)
            {
                string itemName = item.DisplayItemName.Length > 30 ? item.DisplayItemName.Substring(0, 27) + "..." : item.DisplayItemName;
                string itemLine = itemName.PadRight(30) + item.Quantity.ToString().PadRight(10) + item.PurchasePrice.ToString();
                graphic.DrawString(itemLine, font, new SolidBrush(Color.Black), startX, startY + offset);
                offset = offset + (int)fontHeight + 5;
            }

            graphic.DrawString("Total: " + cart.GetTotalAmount(), new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + offset);
            offset = offset + (int)fontHeight + 5;
            graphic.DrawString(thankYouMessage, new Font("Courier New", 12), new SolidBrush(Color.Black), startX, startY + offset);

            await Task.Run(() => bmp.Save($"order_receipt_{transactionNo}.png", System.Drawing.Imaging.ImageFormat.Png));
        }
    }
}
