using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MediatR;

namespace Catalog.API.Services
{
    public static class GetPrductByCategory
    {
        public record QueryByCategory(string categry) : IRequest<IEnumerable<Product>> { }

        public class HandlerByCategory : IRequestHandler<QueryByCategory, IEnumerable<Product>>
        {
            private readonly IProductRepository repositry;

            public HandlerByCategory(IProductRepository repositry)
            {
                this.repositry = repositry ?? throw new ArgumentNullException(nameof(repositry));
            }

            public Task<IEnumerable<Product>> Handle(QueryByCategory request, CancellationToken cancellationToken)
            {
                return repositry.GetProductsByCatagory(request.categry);
            }
        }
    }
}
