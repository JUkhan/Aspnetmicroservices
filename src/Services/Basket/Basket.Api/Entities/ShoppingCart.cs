using System;
using System.Collections.Generic;
using System.Linq;

namespace Basket.Api.Entities
{
    public record ShoppingCart(string UserName, List<ShoppingCartItem> Items)
    {
        public decimal ToalPrice
        {
            get { return Items.Sum(it=>it.Quantity*it.Price); }
        }
    }
    public record ShoppingCartItem(int Quantity, string Color, decimal Price, string ProductId, string ProductName);
    
}
