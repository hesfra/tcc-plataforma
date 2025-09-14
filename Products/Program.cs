using Application.UseCases.Products;
using Domain.Repositories;
using Infrastructure.Repositories.Products;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// =====================
// Repositório em memória
// =====================
services.AddSingleton<IProductRepository, InMemoryProductRepository>();

// =====================
// Handlers Products
// =====================
services.AddSingleton<CreateProductHandler>();
services.AddSingleton<GetProductByIdHandler>();
services.AddSingleton<GetAllProductsHandler>();
services.AddSingleton<UpdateProductHandler>();
services.AddSingleton<DeleteProductHandler>();

// =====================
// Serviços do ASP.NET
// =====================
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Product API",
        Version = "v1"
    });
});

var app = builder.Build();

// =====================
// Middleware
// =====================
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client API V1");
    c.RoutePrefix = "swagger"; // Serve em /clients/swagger, /products/swagger etc
});

app.MapControllers();

app.Run();
