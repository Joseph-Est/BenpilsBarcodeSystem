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
        public int ReceivedQuantity { get; set; }

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

        public string GetTotalAmountAsString()
        {
            return InputValidator.StringToFormattedPrice((Items.Sum(item => item.PurchasePrice * item.Quantity)).ToString());
        }

        public string GetTotalPriceAsString()
        {
            return InputValidator.StringToFormattedPrice((Items.Sum(item => item.SellingPrice * item.Quantity)).ToString());
        }

        public decimal GetTotalAmount()
        {
            return Items.Sum(item => item.PurchasePrice * item.Quantity);
        }

        public decimal GetTotalPrice()
        {
            return Items.Sum(item => item.SellingPrice * item.Quantity);
        }


        public bool HasItems()
        {
            return Items.Count > 0;
        }

        public string[] GetProductNames()
        {
            return Items.Select(item => item.Quantity + " x " + item.DisplayItemName).ToArray();
        }

        public decimal[] GetPrices()
        {
            return Items.Select(item => item.SellingPrice * item.Quantity).ToArray();
        }

        public decimal[] GetAmounts()
        {
            return Items.Select(item => item.PurchasePrice * item.Quantity).ToArray();
        }

        public bool AreAllQuantitiesReceived()
        {
            return Items.All(item => item.Quantity == item.ReceivedQuantity);
        }

        public int GetTotalItemCount()
        {
            return Items.Count;
        }
    }
}
