using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using System.Linq;
    

namespace Ordering.Application.Features.Orders.Queries
{
    public static class GetOrderList
    {
        public record OrderListQuery(string userName) : IRequest<List<OrdersVm>> { }

        public class OrderListHandler : IRequestHandler<OrderListQuery, List<OrdersVm>>
        {
            private readonly IOrderRepository orderRepository;
            private readonly IMapper mapper;

            public OrderListHandler(IOrderRepository orderRepository, IMapper mapper)
            {
                this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<List<OrdersVm>> Handle(OrderListQuery request, CancellationToken cancellationToken)
            {
                var orders = await orderRepository.GetOrdersByUserName(request.userName);
                return mapper.Map<List<OrdersVm>>(orders);
            }
        }
        public record OrdersVm(
         string UserName,
         decimal TotalPrice,
         string FirstName,
         string LastName,
         string EmailAddress,
         string AddressLine,
         string Country,
         string State,
         string ZipCode,
         string CardName,
         string CardNumber,
         string Expiration,
         string CVV,
         int PaymentMethod
            );

    }
}
