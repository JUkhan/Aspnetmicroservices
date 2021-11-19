using System;
using AutoMapper;
using Basket.Api.Entities;
using Eventbus.Messages.Events;

namespace Basket.Api.Mapping
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
