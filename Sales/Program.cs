using Application.UseCases.Carts;
using Domain.Repositories;
using Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// =====================
// Repositório em memória
// =====================
services.AddSingleton<ICartRepository, InMemoryCartRepository>();

// =====================
// Handlers Carts
// =====================
services.AddSingleton<CreateCartHandler>();
services.AddSingleton<GetCartByIdHandler>();
services.AddSingleton<GetAllCartsHandler>();
services.AddSingleton<DeleteCartHandler>();
services.AddSingleton<AddProductToCartHandler>();
services.AddSingleton<RemoveProductFromCartHandler>();
services.AddSingleton<ClearCartHandler>();

// =====================
// Serviços do ASP.NET
// =====================
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Cart API",
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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cart API V1");
    c.RoutePrefix = "swagger"; // Serve em /carts/swagger
});

app.MapControllers();

app.Run();
