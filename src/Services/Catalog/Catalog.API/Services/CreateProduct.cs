using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Entities;
using Catalog.API.Repositories;
using MediatR;


namespace Catalog.API.Services
{
    public static class CreateProduct
    {
        public record CmmandCP(string Name, string Category, string Summay, string Description, string ImageFile, decimal Price) : IRequest<Product> { }

        public class CPHandler : IRequestHandler<CmmandCP, Product>
        {
            private readonly IProductRepository repositry;

            public CPHandler(IProductRepository repositry)
            {
                this.repositry = repositry ?? throw new ArgumentNullException(nameof(repositry));
            }

            public Task<Product> Handle(CmmandCP request, CancellationToken cancellationToken)
            {
                return repositry.CreateProduct(new Product
                {
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
