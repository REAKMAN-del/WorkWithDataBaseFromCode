using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataBase.Methods
{
    public class AdoCrud
    {
        private readonly CommandExecutor _executor;

        public AdoCrud(string connectionString)
        {
            _executor = new CommandExecutor(connectionString);
        }

        public async Task ShowPcBuildsAsync()
        {
            var pcBuilds = await _executor.ExecuteReaderAsync("SELECT DISTINCT BuildID, BuildName, TotalPrice, CreatedDate, IsAssembled FROM PC_Builds");
            foreach (var pcBuild in pcBuilds)
            {
                Console.WriteLine($"ID: {pcBuild["BuildID"]}. {pcBuild["BuildName"]} (Цена: {pcBuild["TotalPrice"]} USD) [Собран: {pcBuild["IsAssembled"]}]");
            }
        }  

        public async Task AddPcBuildAsync(string buildName, decimal totalPrice)
        {
            string sql = "INSERT INTO PC_Builds (BuildName, TotalPrice, CreatedDate, IsAssembled) VALUES (@name, @price, @date, @assembled)";

            var parameters = new[]
            {
                new SqlParameter("@name", buildName),
                new SqlParameter("@price", totalPrice),
                new SqlParameter("@date", DateTime.Now),
                new SqlParameter("@assembled", false)
            };

            await _executor.ExecuteNonQueryAsync(sql, parameters);
            Console.WriteLine("Сборка ПК успешно добавлена в базу данных.");
        }

        public async Task CompletePcBuildAsync(int buildID)
        {
            string sql = "UPDATE PC_Builds SET IsAssembled = 1 WHERE BuildID = @id";

            await _executor.ExecuteNonQueryAsync(sql, new SqlParameter("@id", buildID));
            Console.WriteLine("Статус сборки изменен на 'Собран'.");
        }

        public async Task DeletePcBuildAsync(int buildID)
        {
            string sql = "DELETE FROM PC_Builds WHERE BuildID = @id";

            await _executor.ExecuteNonQueryAsync(sql, new SqlParameter("@id", buildID));
            Console.WriteLine("Сборка ПК успешно удалена.");
        }
    }
}
