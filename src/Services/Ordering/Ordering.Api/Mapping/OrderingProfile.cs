using System;
using AutoMapper;
using Eventbus.Messages.Events;
using static Ordering.Application.Features.Orders.Commands.UpdateOrder;

namespace Ordering.Api.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<CheckoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
