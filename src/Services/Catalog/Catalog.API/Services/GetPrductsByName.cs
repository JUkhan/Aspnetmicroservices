using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MediatR;

namespace Catalog.API.Services
{
    public static class GetPrductsByName
    {
        public record QueryByName(string name) : IRequest<IEnumerable<Product>> { }

        public class ByNameHandler : IRequestHandler<QueryByName, IEnumerable<Product>>
        {
            private readonly IProductRepository repositry;

            public ByNameHandler(IProductRepository repositry)
            {
                this.repositry = repositry ?? throw new ArgumentNullException(nameof(repositry));
            }

            public Task<IEnumerable<Product>> Handle(QueryByName request, CancellationToken cancellationToken)
            {
                return repositry.GetProductsByName(request.name);
            }
        }
    }
}
