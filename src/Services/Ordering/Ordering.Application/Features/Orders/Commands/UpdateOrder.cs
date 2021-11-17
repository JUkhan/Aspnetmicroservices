using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Application.Models;
using Orderring.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands
{
    public static class CheckoutOrder
    {
        public record UpdateOrderCommand(
            int Id,
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
            ) : IRequest
        { }


        public class CheckooutOrderHandler : IRequestHandler<UpdateOrderCommand>
        {
            private readonly IOrderRepository orderRepository;
            private readonly IEmailService emailService;
            private readonly IMapper mapper;
            private readonly ILogger<CheckooutOrderHandler> logger;

            public CheckooutOrderHandler(IOrderRepository orderRepository, IEmailService emailService, IMapper mapper, ILogger<CheckooutOrderHandler> logger)
            {
                this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
                this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
            {
                var orderToUpdate = await orderRepository.GetByIdAsync(request.Id);
                if (orderToUpdate == null)
                {
                    throw new NotFoundException(nameof(Order), request.Id);
                }

                mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

                await orderRepository.UpdateAsync(orderToUpdate);

                logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");

                return Unit.Value;
            }
            
        }

       public class ChecoutOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
        {
            public ChecoutOrderCommandValidator()
            {
                RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters.");

                RuleFor(p => p.EmailAddress)
                   .NotEmpty().WithMessage("{EmailAddress} is required.");

                RuleFor(p => p.TotalPrice)
                    .NotEmpty().WithMessage("{TotalPrice} is required.")
                    .GreaterThan(0).WithMessage("{TotalPrice} should be greater than zero.");
            }
        }
    }

}
