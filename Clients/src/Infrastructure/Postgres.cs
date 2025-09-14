using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Repositories;
using Npgsql;

namespace Infrastructure.Repositories
{
    public class PostgresClientRepository : IClientRepository
    {
        private readonly NpgsqlConnection _connection;

        public PostgresClientRepository(NpgsqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void AddClient(Client client)
        {
            using var cmd = new NpgsqlCommand(
                "INSERT INTO Clients (id, first_name, last_name, email, phone) VALUES (@id, @first_name, @last_name, @email, @phone)",
                _connection);
            
            cmd.Parameters.AddWithValue("id", client.Id);
            cmd.Parameters.AddWithValue("first_name", client.FirstName);
            cmd.Parameters.AddWithValue("last_name", client.LastName);
            cmd.Parameters.AddWithValue("email", client.Email);
            cmd.Parameters.AddWithValue("phone", (object?)client.PhoneNumber ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public Client GetClientById(Guid id)
        {
            using var cmd = new NpgsqlCommand(
                "SELECT id, first_name, last_name, email, phone FROM Clients WHERE id = @id",
                _connection);
            cmd.Parameters.AddWithValue("id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Client
                {
                    Id = reader.GetGuid(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    PhoneNumber = reader.IsDBNull(4) ? null : reader.GetString(4)
                };
            }
            return null;
        }

        public IEnumerable<Client> GetAllClients()
        {
            var clients = new List<Client>();
            using var cmd = new NpgsqlCommand(
                "SELECT id, first_name, last_name, email, phone FROM Clients",
                _connection);
            
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client
                {
                    Id = reader.GetGuid(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    PhoneNumber = reader.IsDBNull(4) ? null : reader.GetString(4)
                });
            }
            return clients;
        }

        public void UpdateClient(Client client)
        {
            using var cmd = new NpgsqlCommand(
                "UPDATE Clients SET first_name = @first_name, last_name = @last_name, email = @email, phone = @phone WHERE id = @id",
                _connection);

            cmd.Parameters.AddWithValue("id", client.Id);
            cmd.Parameters.AddWithValue("first_name", client.FirstName);
            cmd.Parameters.AddWithValue("last_name", client.LastName);
            cmd.Parameters.AddWithValue("email", client.Email);
            cmd.Parameters.AddWithValue("phone", (object?)client.PhoneNumber ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void DeleteClient(Guid id)
        {
            using var cmd = new NpgsqlCommand(
                "DELETE FROM Clients WHERE id = @id",
                _connection);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
