using System;
using AutoMapper;
using Orderring.Domain.Entities;
using static Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using static Ordering.Application.Features.Orders.Queries.GetOrderList;

namespace Ordering.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
