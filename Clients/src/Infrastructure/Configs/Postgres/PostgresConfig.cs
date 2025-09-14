using System;
using System.Data;
using Npgsql;

namespace Infrastructure.Configs.DBconfigs
{
    public static class DBconfigs
    {
        public static NpgsqlConnection CreateConnection(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}