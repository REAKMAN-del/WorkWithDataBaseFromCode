using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataBase
{
    public class ConnectionManager
    {
        private readonly string _connectionString;

        public ConnectionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task ExecuteWithConnectionAsync(Func<SqlConnection, Task> action)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await action(connection);
            }
        }

        public void PrintConnectionStatistics()
        {
            var pool = new SqlConnectionStringBuilder(_connectionString);
            Console.WriteLine($"Pooling: {pool.Pooling}");
            Console.WriteLine($"Max Pool Size: {pool.MaxPoolSize}");
            Console.WriteLine($"Min Pool Size: {pool.MinPoolSize}");
        }
    }
}
