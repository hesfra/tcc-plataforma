using Domain.Entities;
using System;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        Product GetProductById(Guid id);
        IEnumerable<Product> GetAllProducts();
        void UpdateProduct(Product product);
        void DeleteProduct(Guid id);

    }
}