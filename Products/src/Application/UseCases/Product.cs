using Domain.Entities;
using Domain.Repositories;
using System;

namespace Application.UseCases.Products
{
    public class CreateProductHandler(IProductRepository productRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;

        public Product Handle(CreateProductRequest request)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedAt = DateTime.UtcNow
            };
            _productRepository.AddProduct(product);
            return product;
        }
    }

    public record CreateProductRequest(string Name, string Description, decimal Price);

    public class GetProductByIdHandler(IProductRepository productRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;

        public Product Handle(Guid id)
        {
            var product = _productRepository.GetProductById(id) ?? throw new Exception("Product not found");
            return product;
        }

    }

    public class GetAllProductsHandler(IProductRepository productRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;

        public IEnumerable<Product> Handle()
        {
            return _productRepository.GetAllProducts();
        }
    }

    public class UpdateProductHandler(IProductRepository productRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;

        public Product Handle(UpdateProductRequest request)
        {
            var product = _productRepository.GetProductById(request.Id) ?? throw new Exception("Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.UpdateProduct(product);
            return product;
        }
    }

    public record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price);

    public class DeleteProductHandler(IProductRepository productRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;

        public void Handle(Guid id)
        {
            var product = _productRepository.GetProductById(id) ?? throw new Exception("Product not found");
            _productRepository.DeleteProduct(id);
        }
    }
}