using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Entities
{
    public class PurchaseOrderEntity
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryDate { get; set; }
        public string DeliveryStatus { get; set; }
        public string SupplierName { get; set; }
    }
}
