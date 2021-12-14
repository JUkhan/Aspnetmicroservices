using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingAggregator.Models;

namespace ShoppingAggregator.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
