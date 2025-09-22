using Application.UseCases.Products;
using Domain.Repositories;
using Infrastructure.Repositories.Products;
using Infrastructure.Configs.DBconfigs;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default");

services.AddSingleton<NpgsqlConnection>(provider =>
{
    return DBconfigs.CreateConnection(connectionString);
});

services.AddScoped<IProductRepository, PostgresProductRepository>();
services.AddScoped<CreateProductHandler>();
services.AddScoped<GetProductByIdHandler>();
services.AddScoped<GetAllProductsHandler>();
services.AddScoped<UpdateProductHandler>();
services.AddScoped<DeleteProductHandler>();

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


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client API V1");
    c.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();
