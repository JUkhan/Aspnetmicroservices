using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MediatR;

namespace Catalog.API.Services
{
    public static class GetPrductById
    {
        public record QueryById(string id) : IRequest<Product> { }

        public class HandlerById : IRequestHandler<QueryById, Product>
        {
            private readonly IProductRepository repositry;

            public HandlerById(IProductRepository repositry)
            {
                this.repositry = repositry ?? throw new ArgumentNullException(nameof(repositry));
            }

            public Task<Product> Handle(QueryById request, CancellationToken cancellationToken)
            {
                return repositry.GetProductById(request.id);
            }
        }
    }
}
