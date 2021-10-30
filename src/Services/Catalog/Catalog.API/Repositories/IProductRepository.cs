using System;
using System.Threading.Tasks;
using Catalog.API.Entities;

namespace Catalog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEquatable<Product>> GetProducts();
        Task<Product> GetProductById(string productId);
        Task<Product> GetProductByCatagory(string catagoryName);

        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string productId);
    }
}
