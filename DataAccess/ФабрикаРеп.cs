using MarmeladhubApp.BL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarmeladhubApp.DataAccess
{
    public class FabricRepository : ICrudOperations<Фабрика>
    {
        private readonly string _connectionString;

        public FabricRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Фабрика>> GetAllAsync()
        {
            var фабрики = new List<Фабрика>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT код_фабрики, название_фабрики FROM Фабрика;";
                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        фабрики.Add(new Фабрика(
                            reader.GetInt32(0),
                            reader.GetString(1)
                        ));
                    }
                }
            }
            return фабрики;
        }

        public async Task AddAsync(Фабрика фабрика)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // В методе AddAsync добавить после проверки существования :
                if (!System.Text.RegularExpressions.Regex.IsMatch(фабрика.название_фабрики, @"^[А-Яа-яЁё\- ]+$"))
                {
                    throw new ArgumentException("Название фабрики может содержать только кириллицу, пробелы и тире");
                }
                if (await IsProductCodeExistsAsync(connection, фабрика.код_фабрики))
                    throw new ArgumentException($"Фабрика с кодом {фабрика.код_фабрики} уже существует");

                if (await IsProductCodeExistsAsync1(connection, фабрика.название_фабрики))
                    throw new ArgumentException($"Фабрика с названием {фабрика.название_фабрики} уже существует");

                var sql = "INSERT INTO Фабрика (код_фабрики, название_фабрики) VALUES (@код_фабрики, @название_фабрики);";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_фабрики", фабрика.код_фабрики);
                    command.Parameters.AddWithValue("@название_фабрики", фабрика.название_фабрики);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<bool> IsProductCodeExistsAsync(NpgsqlConnection connection, int код_фабрики)
        {
            const string sql = "SELECT COUNT(*) FROM Фабрика WHERE код_фабрики = @код_фабрики";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@код_фабрики", код_фабрики);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }

        private async Task<bool> IsProductCodeExistsAsync1(NpgsqlConnection connection, string название_фабрики)
        {
            const string sql = "SELECT COUNT(*) FROM Фабрика WHERE название_фабрики = @название_фабрики";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@название_фабрики", название_фабрики);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }

        public async Task UpdateAsync(Фабрика фабрика)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Проверка существования 
                if (!await IsForeignKeyValidAsync("Фабрика", фабрика.код_фабрики))
                {
                    throw new ArgumentException($"Код фабрики изменить нельзя, используйте функцию добавления.");
                }

                

                var sql = "UPDATE Фабрика SET название_фабрики = @название_фабрики WHERE код_фабрики = @код_фабрики;";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_фабрики", фабрика.код_фабрики);
                    command.Parameters.AddWithValue("@название_фабрики", фабрика.название_фабрики);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "DELETE FROM Фабрика WHERE код_фабрики = @код_фабрики;";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_фабрики", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> GetNextIdAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT COALESCE(MAX(Id), 0) + 1 FROM Фабркиа;";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> IsForeignKeyValidAsync(string tableName, int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = $"SELECT COUNT(*) FROM {tableName} WHERE код_фабрики = @код_фабрики";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_фабрики", id);
                    return Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
                }
            }
        }

        public Task<bool> IsForeignKeyValidAsync(string tableName, string columnName, int id)
        {
            throw new NotImplementedException();
        }
    }
}
