using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Orderring.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands
{
    public static class CheckoutOrder
    {
        public record CheckoutOrderCommand(
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
            ) : IRequest<int>
        { }


        public class CheckooutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
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

            public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
            {
                var order = mapper.Map<Order>(request);
                var newrder=await orderRepository.AddAsync(order);
                logger.LogInformation($"Order({order.Id}) successfully created");
                await SendEmail(order);
                return order.Id;
            }
            async Task SendEmail(Order order)
            {
                var email = new Email { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

                try
                {
                    await emailService.SendEmail(email);
                }
                catch (Exception ex)
                {
                    logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
                }
            }
        }

       public class ChecoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
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
