using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Api.Entities
{
    public class ShoppingCart
    {
        public string UserName { set; get; }

        public List<ShoppingCartItem> Items { set; get; } = new List<ShoppingCartItem>();

        public decimal TotalPrice
        {
            get { return Items.Sum(it=>it.Quantity*it.Price); }
        }
    }
    public class ShoppingCartItem {
        public int Quantity { set; get; }
        public string Color { set; get; }
        public decimal Price { set; get; }
        public string ProductId { set; get; }
        public string ProductName { set; get; }
    }
    
}
