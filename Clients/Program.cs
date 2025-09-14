using Application.UseCases.Clients;
using Domain.Repositories;
using Infrastructure.Repositories;
using Infrastructure.Configs.DBconfigs;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default");

services.AddSingleton<NpgsqlConnection>(provider =>
{
    return DBconfigs.CreateConnection(connectionString);
});

services.AddScoped<IClientRepository, PostgresClientRepository>();
services.AddScoped<CreateClientHandler>();
services.AddScoped<GetClientByIdHandler>();
services.AddScoped<GetAllClientsHandler>();
services.AddScoped<UpdateClientHandler>();
services.AddScoped<DeleteClientHandler>();


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Client API",
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

