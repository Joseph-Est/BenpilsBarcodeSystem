﻿using BenpilsBarcodeSystem.Entities;
using BenpilsBarcodeSystem.Helpers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

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

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != words[i].ToUpper())
                {
                    words[i] = textInfo.ToTitleCase(words[i].ToLower());
                }
            }

            return string.Join(" ", words);
        }

        public static string CapitalizeOrNA(string input)
        {
            if (string.IsNullOrEmpty(input.Trim()))
                return "N/A";

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != words[i].ToUpper())
                {
                    words[i] = textInfo.ToTitleCase(words[i].ToLower());
                }
            }

            return string.Join(" ", words);
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

        public static void PrintReceipt(Graphics graphics, string transactionNo, string[] products, decimal[] prices, decimal total, decimal paymentReceived = 0, decimal change = 0, string cashierName = null, string supplierName = null, string deliveryDate = null, string orderDate = null, string transactionDate = null)
        {
            Font fontBold = new Font("Courier New", 12, FontStyle.Bold);
            Font fontDash = new Font("Courier New", 12, FontStyle.Regular);
            Font fontRegularSmall = new Font("Courier New", 10, FontStyle.Regular);
            Font fontBoldSmall = new Font("Courier New", 10, FontStyle.Bold);

            string date = transactionDate ?? orderDate ?? Util.ConvertDateShort(DateTime.Now);
            string dashes = "- - - - - - - - - - - - - - -";
            string space = " ";
            date = $"Date: {date}";
            string supplier = "Supplier:";
            string delivery = "Delivery Date:";
            string shopName = "Benpils Motorcycle Parts and Accessories";
            string contactNo = "09295228592";
            string shopAddress = "Ortigas, Cainta, Rizal";
            string thankYouMessage = "Thank you for shopping, have a great day!!";
            string cashier = "Cashier:";
            string customerName = "Customer:";
            string customerField = "___________________";

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
            else if (cashierName != null)
            {
                y = DrawText(graphics, dashes, fontDash, y + 10, 315);
                y = DrawText(graphics, cashier, fontRegularSmall, y, 315);
                y = DrawText(graphics, cashierName, fontBoldSmall, y, 315, 30);
                y = DrawText(graphics, customerName, fontRegularSmall, y+10, 315);
                y = DrawText(graphics, customerField, fontBoldSmall, y, 315, 30);
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
            _ = DrawText(graphics, space, fontDash, y, 315);
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

        public static int ExportData(Dictionary<DataTable, string> dataTableSheetMapping, string saveLocation = null, bool isAutoBackup = false)
        {
            int result = 0;

            string filePath;
            string fileName;

            if (isAutoBackup)
            {
                filePath = saveLocation;
                fileName = "AutoBackup";
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    FileName = "Backup.xlsx",
                    Title = "Save Excel File",
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = Path.GetDirectoryName(saveFileDialog.FileName);
                    fileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                }
                else
                {
                    return 5;
                }
            }

            if (dataTableSheetMapping != null && dataTableSheetMapping.Count > 0)
            {
                switch (ExportToExcel(filePath, fileName, dataTableSheetMapping))
                {
                    case 0:
                        result = 0;
                        break;
                    case 1:
                        result = 2;
                        break;
                    case 2:
                        result = 3;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                result = 1;
            }

            return result;
        }

        public static int ExportToExcel(string filePath, string fileName, Dictionary<DataTable, string> dataTableSheetMapping)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(fileName) || dataTableSheetMapping == null || dataTableSheetMapping.Count == 0)
                {
                    throw new ArgumentException("Invalid arguments provided.");
                }

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                string fullFilePath = Path.Combine(filePath, $"{fileName}.xlsx");

                FileInfo newFile = new FileInfo(fullFilePath);

                using (ExcelPackage excelPackage = new ExcelPackage(newFile))
                {
                    foreach (var mapping in dataTableSheetMapping)
                    {
                        ExcelWorksheet existingWorksheet = excelPackage.Workbook.Worksheets.FirstOrDefault(ws => ws.Name == mapping.Value);
                        if (existingWorksheet != null)
                        {
                            try
                            {
                                excelPackage.Workbook.Worksheets.Delete(existingWorksheet);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error deleting existing worksheet '{mapping.Value}': {ex.Message}");
                                continue;
                            }
                        }

                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(mapping.Value);

                        worksheet.Cells["A1"].LoadFromDataTable(mapping.Key, true);

                        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                        {
                            if (worksheet.Cells[1, col].Value.ToString().ToLower().Contains("date"))
                            {
                                worksheet.Column(col).Style.Numberformat.Format = "mm/dd/yyyy";
                            }
                        }

                        worksheet.Cells.AutoFitColumns();
                    }

                    using (FileStream fs = new FileStream(fullFilePath, FileMode.Create))
                    {
                        excelPackage.SaveAs(fs);
                    }
                }
            }
            catch (IOException)
            {
                return 1;
            }
            catch (Exception)
            {
                return 2; 
            }

            return 0; 
        }

        public static DataTable ReorderColumns(DataTable dt, List<string> columnOrder)
        {
            DataTable reorderedDt = dt.Clone();  // empty table with same schema

            // Arrange the columns in the specified order
            foreach (string columnName in columnOrder)
            {
                reorderedDt.Columns[columnName].SetOrdinal(columnOrder.IndexOf(columnName));
            }

            // Import the Data from the original table
            foreach (DataRow row in dt.Rows)
            {
                reorderedDt.ImportRow(row);
            }

            return reorderedDt;
        }

        public static bool IsAnyCheckboxChecked(params CheckBox[] checkBoxes)
        {
            foreach (var checkBox in checkBoxes)
            {
                if (checkBox.Checked)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsAnyRadioButtonChecked(params RadioButton[] radioButtons)
        {
            foreach (var radioButton in radioButtons)
            {
                if (radioButton.Checked)
                {
                    return true;
                }
            }
            return false;
        }

        private static readonly string logFilePath = @"autobackup_log.txt";

        public static void Log(string message)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        public static bool IsBackupSuccessfulToday()
        {
            try
            {
                string lastLine = File.ReadLines(logFilePath).LastOrDefault();
                if (lastLine != null && lastLine.Contains("success"))
                {
                    string[] parts = lastLine.Split(':');
                    DateTime logDate = DateTime.Parse(parts[0] + ":" + parts[1] + ":" + parts[2].Split(' ')[0]);

                    if (logDate.Date == DateTime.Now.Date)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read from log file: {ex.Message}");
            }

            return false;
        }

        public static Image GenerateBarcode(string value)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(value.Trim()))
                {
                    BarcodeWriter barcodeWriter = new BarcodeWriter { Format = BarcodeFormat.CODE_128 };
                    return barcodeWriter.Write(value);
                }
            }
            catch { }
            
            return null;
        }

        public static void PrintBarcode(PictureBox picture, float imageWidthInches, float marginInches)
        {
            if (picture.Image != null)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += (sender, e) => PrintPage(e, picture.Image, imageWidthInches, marginInches);

                float paperWidth = (imageWidthInches + marginInches * 2) * 100;
                float paperHeight = (marginInches * 2 + picture.Image.Height * imageWidthInches / picture.Image.Width) * 100;

                pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", (int)paperWidth, (int)paperHeight);
                pd.PrinterSettings.DefaultPageSettings.PaperSize = new PaperSize("Custom", (int)paperWidth, (int)paperHeight);

                PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
                ppd.ShowDialog();
            }
            else
            {
                MessageBox.Show("No image to print.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void PrintPage(PrintPageEventArgs e, Image image, float imageWidthInches, float marginInches)
        {
            float imageWidth = imageWidthInches * 100;
            float imageHeight = image.Height * imageWidthInches / image.Width * 100;

            int margin = (int)(marginInches * 100);

            int availableWidth = e.PageBounds.Width - 2 * margin;
            int availableHeight = e.PageBounds.Height - 2 * margin;

            int posX = margin + (availableWidth - (int)imageWidth) / 2;
            int posY = margin + (availableHeight - (int)imageHeight) / 2;

            e.Graphics.DrawImage(image, posX, posY, imageWidth, imageHeight);
        }
    }
}
