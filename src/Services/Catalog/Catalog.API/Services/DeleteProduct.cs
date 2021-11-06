using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MediatR;


namespace Catalog.API.Services
{
    public static class DeletePrduct
    {
        public record CmmandDP(string Id) : IRequest<bool> { }

        public class DPHandler : IRequestHandler<CmmandDP, bool>
        {
            private readonly IProductRepository repositry;

            public DPHandler(IProductRepository repositry)
            {
                this.repositry = repositry ?? throw new ArgumentNullException(nameof(repositry));
            }

            public Task<bool> Handle(CmmandDP request, CancellationToken cancellationToken)
            {
                return repositry.DeleteProduct(request.Id);
            }
        }
    }

    
}
