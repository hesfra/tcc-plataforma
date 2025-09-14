using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories.Products
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public Product GetProductById(Guid id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public void UpdateProduct(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index != -1)
                _products[index] = product;
        }

        public void DeleteProduct(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                _products.Remove(product);
        }
    }
}
