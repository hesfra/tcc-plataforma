using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Repositories;
using Npgsql;


namespace Infrastructure.Repositories.Products
{
    public class PostgresProductRepository : IProductRepository
    {
        private readonly NpgsqlConnection _connection;

        public PostgresProductRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public void AddProduct(Product product)
        {
            using var cmd = new NpgsqlCommand(
                "INSERT INTO Products (id, name, description, price) VALUES (@id, @name, @description, @price)",
                _connection);

            cmd.Parameters.AddWithValue("id", product.Id);
            cmd.Parameters.AddWithValue("name", product.Name);
            cmd.Parameters.AddWithValue("description", (object?)product.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("price", product.Price);
            cmd.ExecuteNonQuery();
        }

        public Product GetProductById(Guid id)
        {
            using var cmd = new NpgsqlCommand(
                "SELECT id, name, description, price FROM Products WHERE id = @id",
                _connection);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Price = reader.GetDecimal(3)
                };
            }
            return null;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using var cmd = new NpgsqlCommand(
                "SELECT id, name, description, price FROM Products",
                _connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Price = reader.GetDecimal(3)
                });
            }
            return products;
        }

        public void UpdateProduct(Product product)
        {
            using var cmd = new NpgsqlCommand(
                "UPDATE Products SET name = @name, description = @description, price = @price WHERE id = @id",
                _connection);
            cmd.Parameters.AddWithValue("id", product.Id);
            cmd.Parameters.AddWithValue("name", product.Name);
            cmd.Parameters.AddWithValue("description", (object?)product.Description ?? DBNull.Value);
            cmd.Parameters.AddWithValue("price", product.Price);
            cmd.ExecuteNonQuery();
        }
        public void DeleteProduct(Guid id)
        {
            using var cmd = new NpgsqlCommand(
                "DELETE FROM Products WHERE id = @id",
                _connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }
}