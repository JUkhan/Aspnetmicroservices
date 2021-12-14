using System;
using System.Threading.Tasks;
using ShoppingAggregator.Models;

namespace ShoppingAggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
