using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Persistence;
using Orderring.Domain.Entities;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContet):base(dbContet)
        {

        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await this.GetAsync(order => order.UserName == userName);
        }
    }
}
