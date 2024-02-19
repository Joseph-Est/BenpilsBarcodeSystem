using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem
{
    public class Item
    {
        public string Barcode { get; set; }
        public string ProductID { get; set; }
        public string MotorBrand { get; set; }
        public string Brand { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
