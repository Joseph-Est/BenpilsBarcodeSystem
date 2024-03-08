using BenpilsBarcodeSystem.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenpilsBarcodeSystem.Entities
{
    internal class PurchaseItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }

        public string DisplayItemName
        {
            get
            {
                return $"{ItemName} (Brand: {Brand}, Size: {Size})";
            }
        }

        public string TotalAmount
        {
            get
            {
                return InputValidator.StringToFormattedPrice((PurchasePrice * Quantity).ToString()) ;
            }
        }

        public string PriceTotal
        {
            get
            {
                return InputValidator.StringToFormattedPrice((SellingPrice * Quantity).ToString());
            }
        }

    }


    internal class Cart
    {
        public BindingList<PurchaseItem> Items { get; set; }

        public Cart()
        {
            Items = new BindingList<PurchaseItem>();
        }

        public string GetTotalAmount()
        {
            return InputValidator.StringToFormattedPrice((Items.Sum(item => item.PurchasePrice * item.Quantity)).ToString());
        }

        public string GetTotalPrice()
        {
            return InputValidator.StringToFormattedPrice((Items.Sum(item => item.SellingPrice * item.Quantity)).ToString());
        }

        public bool HasItems()
        {
            return Items.Count > 0;
        }
    }
}
