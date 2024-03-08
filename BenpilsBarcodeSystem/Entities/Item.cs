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

        public override string ToString()
        {
            return $"{ItemName} (Brand: {Brand}, Size: {Size})";
        }

    }
}
