using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MediatR;

namespace Catalog.API.Services
{
    public static class GetPrducts
    {
        public class Query : IRequest<IEnumerable<Product>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<Product>>
        {
            private readonly IProductRepository repositry;

            public Handler(IProductRepository repositry)
            {
                this.repositry = repositry ?? throw new ArgumentNullException(nameof(repositry));
            }

            public Task<IEnumerable<Product>> Handle(Query request, CancellationToken cancellationToken)
            {
                return repositry.GetProducts();
            }
        }
    }
}
