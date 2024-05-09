using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem
{
    public class Item
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string ItemName { get; set; }
        public string Brand { get; set; }
        public string MotorBrand { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
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

        public override string ToString()
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

        public double AverageDailySales { get; set; }

    }
}
