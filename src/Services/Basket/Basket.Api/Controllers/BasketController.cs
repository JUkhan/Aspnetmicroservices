using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Basket.Api.Entities;
using Basket.Api.GrpcServvices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService discountGrpcSerrvice;

        public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcSerrvice)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.discountGrpcSerrvice = discountGrpcSerrvice ?? throw new ArgumentNullException(nameof(discountGrpcSerrvice));
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await _repository.GetBasket(userName);
            return Ok(basket ?? new ShoppingCart { UserName = userName });
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
        {
            foreach (var o in cart.Items)
            {
                var dis = await discountGrpcSerrvice.GetDiscount(o.ProductName);
                o.Price -= dis.Amount;
            }

            return Ok(await _repository.UpdateBasket(cart));
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }
    }
}
