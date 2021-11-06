using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MediatR;


namespace Catalog.API.Services
{
    public static class UpdateProduct
    {
        public record CmmandUP(string Id, string Name, string Category, string Summay, string Description, string ImageFile, decimal Price) : IRequest<bool> { }

        public class UPHandler : IRequestHandler<CmmandUP, bool>
        {
            private readonly IProductRepository repositry;

            public UPHandler(IProductRepository repositry)
            {
                this.repositry = repositry ?? throw new ArgumentNullException(nameof(repositry));
            }

            public Task<bool> Handle(CmmandUP request, CancellationToken cancellationToken)
            {
                return repositry.UpdateProduct(new Product
                {
                    Id = request.Id,
                    Name = request.Name,
                    Category = request.Category,
                    Summary = request.Summay,
                    Description = request.Description,
                    ImageFile = request.ImageFile,
                    Price = request.Price
                });
            }
        }
    }
}
