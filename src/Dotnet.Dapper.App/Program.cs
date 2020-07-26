using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace Dotnet.Dapper.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Informe a connection string para um MSSQL Server");
            var connectionString = Console.ReadLine();

            ExemploSemDapper(connectionString).GetAwaiter().GetResult();
            ExemploComDapper(connectionString).GetAwaiter().GetResult();
        }

        private static async Task<List<Customer>> ExemploSemDapper(string connectionString)
        {
            var customers = new List<Customer>();
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand("SELECT * FROM Customers", connection);
            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var customer = new Customer()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Nome = reader.GetString(reader.GetOrdinal("Nome")),
                        Sobrenome = reader.GetString(reader.GetOrdinal("Sobrenome")),
                        Nascimento = reader.GetDateTime(reader.GetOrdinal("Nascimento")),
                        Limite = reader.GetDouble(reader.GetOrdinal("Limite")),
                        Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                    };

                    customers.Add(customer);
                }
            }

            return customers;
        }

        private static async Task<IEnumerable<Customer>> ExemploComDapper(string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<Customer>("SELECT * FROM Customers");
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public DateTime Nascimento { get; set; }
            public double Limite { get; set; }
            public bool Ativo { get; set; }
        }
    }
}
