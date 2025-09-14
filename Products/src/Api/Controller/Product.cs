using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Products;
using Domain.Entities;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(
        CreateProductHandler createProductHandler,
        GetProductByIdHandler getProductByIdHandler,
        GetAllProductsHandler getAllProductsHandler,
        UpdateProductHandler updateProductHandler,
        DeleteProductHandler deleteProductHandler
        ) : ControllerBase
    {
        private readonly CreateProductHandler _createProductHandler = createProductHandler;
        private readonly GetProductByIdHandler _getProductByIdHandler = getProductByIdHandler;
        private readonly GetAllProductsHandler _getAllProductsHandler = getAllProductsHandler;
        private readonly UpdateProductHandler _updateProductHandler = updateProductHandler;
        private readonly DeleteProductHandler _deleteProductHandler = deleteProductHandler;

        // CREATE
        [HttpPost]
        public IActionResult Create([FromBody] CreateProductRequest request)
        {
            var product = _createProductHandler.Handle(request);
            return Ok(product);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var product = _getProductByIdHandler.Handle(id);
            return Ok(product);
        }

        // READ ALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _getAllProductsHandler.Handle();
            return Ok(products);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateProductRequest request)
        {
            var updatedRequest = request with { Id = id };
            var product = _updateProductHandler.Handle(updatedRequest);
            return Ok(product);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _deleteProductHandler.Handle(id);
            return NoContent();
        }
    }
}
