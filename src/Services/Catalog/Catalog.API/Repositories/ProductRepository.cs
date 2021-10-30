using System;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext context;

        public ProductRepository(ICatalogContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await context.Products.InsertOneAsync(product);
            return product;
        }

        public Task<bool> DeleteProduct(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByCatagory(string catagoryName)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(string productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEquatable<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
