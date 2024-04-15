using BenpilsBarcodeSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BenpilsBarcodeSystem.Dialogs
{
    public partial class TableDialog : Form
    {
        public TableDialog(string title, List<object[]> data, string[] headers, int fillColumnIndex, int[] middleCenterColumns, int[] middleRightColumns, bool countTotal = true)
        {
            InitializeComponent();
            TitleLbl.Text = title;

            TableTbl.MouseWheel += Tbl_MouseWheel;

            foreach (var header in headers)
            {
                TableTbl.Columns.Add(header, header);
            }

            decimal total = 0;
            int rowCount = 0;

            foreach (var row in data)
            {
                TableTbl.Rows.Add(row);
                total += InputValidator.ParseToDecimal(row[TableTbl.Columns.Count - 1].ToString());
                rowCount++;
            }

            TableTbl.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TableTbl.Columns[fillColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 0; i < TableTbl.Columns.Count; i++)
            {
                if (i != fillColumnIndex)
                {
                    TableTbl.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }

            foreach (var columnIndex in middleCenterColumns)
            {
                TableTbl.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (var columnIndex in middleRightColumns)
            {
                TableTbl.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if(countTotal)
            {
                TotalLbl.Text = total.ToString().Contains('.') ? total.ToString("C", CultureInfo.CreateSpecificCulture("en-PH")).Replace("₱", "₱ ") : total.ToString();
            }
            else
            {
                TotalLbl.Text = rowCount.ToString();
            }
            
        }

        private void Tbl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (TableTbl.RowCount == 0) return;

            if (e.Delta > 0 && TableTbl.FirstDisplayedScrollingRowIndex > 0)
            {
                TableTbl.FirstDisplayedScrollingRowIndex--;
            }
            else if (e.Delta < 0 && TableTbl.FirstDisplayedScrollingRowIndex < TableTbl.RowCount - 1)
            {
                TableTbl.FirstDisplayedScrollingRowIndex++;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
