using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataBase
{
    public class CommandExecutor
    {

        private readonly string _connectionString;

        public CommandExecutor(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<object> ExecuteScalarAsync(string sql, params SqlParameter[] parameters)
        {

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string sql, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task CallStoredProcedureAsync(string procedureName, params SqlParameter[] parameters)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Dictionary<string, object>>> ExecuteReaderAsync(string sql, params SqlParameter[] parameters)
        {
            var results = new List<Dictionary<string, object>>();
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddRange(parameters);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var list = new List<Dictionary<string, object>>();

                    while (await reader.ReadAsync())
                    {
                        var row = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);
                            object columnValue = reader.GetValue(i);

                            row[columnName] = columnValue;
                        }

                        list.Add(row);
                    }

                    return list;
                }
            }
        }
    }
}
