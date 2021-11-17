using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Orderring.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands
{
    public static class DeleteOrder
    {
        public record DeleteOrderCommand(int Id) : IRequest { };

        public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
        {
            private readonly IOrderRepository orderRepository;
           
            private readonly IMapper mapper;

            private readonly ILogger<DeleteOrderHandler> logger;

            public DeleteOrderHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<DeleteOrderHandler> logger)
            {
                this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var orderToDelete = await orderRepository.GetByIdAsync(request.Id);
                if (orderToDelete == null)
                {
                    throw new NotFoundException(nameof(Order), request.Id);
                }

                await orderRepository.DeleteAsync(orderToDelete);
                logger.LogInformation($"Order {orderToDelete.Id} is successfully deleted.");

                return Unit.Value;
            }
        }
    }
}
