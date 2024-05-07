using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Entities
{
    internal class SalesData
    {
        public string transactionId {get; set;}
        public DateTime Date { get; set; }
        public int TotalItemSold { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalProfit { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public decimal AvgProfitMargin { get; set; }
        public int Rank { get; set; }
        public string DisplayItemName
        {
            get
            {
                string displayName = $"{ItemName}";

                if (Brand != "N/A")
                {
                    displayName += $", {Brand}";
                }

                if (Size != "N/A")
                {
                    displayName += $", {Size}";
                }

                return displayName;
            }
        }
    }
}
