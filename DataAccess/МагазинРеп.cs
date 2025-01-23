using MarmeladhubApp.BL;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.DataAccess
{
    public class StoreRepository : ICrudOperations<Магазин>
    {
        private readonly string _connectionString;
        private const string STORE_NAME_PATTERN = @"^[А-Яа-яЁё][А-Яа-яЁё\- ]*$";
        private const string ERROR_NAME_FORMAT = "Название магазина должно начинаться с буквы и может содержать только буквы, пробелы и тире";
        private const string ERROR_DUPLICATE = "Магазин {0} с названием {1} уже существует";


        public StoreRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Магазин>> GetAllAsync()
        {
            var stores = new List<Магазин>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT код_магазина, название_магазина, адрес_магазина FROM Магазин;";
                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        stores.Add(new Магазин(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2)
                        ));
                    }
                }
            }
            return stores;
        }

        public async Task AddAsync(Магазин store)
        {
            // 1. Проверяем формат названия города
            if (!System.Text.RegularExpressions.Regex.IsMatch(store.название_магазина, STORE_NAME_PATTERN))
            {
                throw new ArgumentException(ERROR_NAME_FORMAT);
            }

            // 2. Проверяем численность населения

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // 3. Проверяем дубликаты
                var exists = await CheckStoreExistsAsync(connection, store.название_магазина, store.адрес_магазина);
                if (exists)
                {
                    throw new ArgumentException(string.Format(ERROR_DUPLICATE, store.название_магазина, store.адрес_магазина));
                }

                // 4. Добавляем запись
                var sql = @"INSERT INTO Магазин (код_магазина, название_магазина, адрес_магазина) 
                       VALUES (@код_магазина, @название_магазина, @адрес_магазина);";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_магазина", store.код_магазина);
                    command.Parameters.AddWithValue("@название_магазина", store.название_магазина.Trim());
                    command.Parameters.AddWithValue("@адрес_магазина", store.адрес_магазина);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateAsync(Магазин store)
        {
            // 1. Проверяем формат названия города
            if (!System.Text.RegularExpressions.Regex.IsMatch(store.название_магазина, STORE_NAME_PATTERN))
            {
                throw new ArgumentException(ERROR_NAME_FORMAT);
            }

            // Проверка существования города
            if (!await IsForeignKeyValidAsync("Магазин", store.код_магазина))
            {
                throw new ArgumentException($"Код магазина изменить нельзя, используйте функцию добавления.");
            }
            else
            {


                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    // 3. Проверяем дубликаты, исключая текущую запись
                    var sql = @"SELECT COUNT(*) FROM Магазин 
                       WHERE название_магазина = @название_магазина AND адрес_магазина = @адрес_магазина 
                       AND код_магазина != @код_магазина";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@название_магазина", store.название_магазина);
                        command.Parameters.AddWithValue("@адрес_магазина", store.адрес_магазина);
                        command.Parameters.AddWithValue("@код_магазина", store.код_магазина);

                        if (Convert.ToInt32(await command.ExecuteScalarAsync()) > 0)
                        {
                            throw new ArgumentException(string.Format(ERROR_DUPLICATE,
                                store.название_магазина, store.адрес_магазина));
                        }
                    }

                    // 4. Обновляем запись
                    sql = @"UPDATE Магазин 
                   SET название_магазина = @название_магазина, адрес_магазина = @адрес_магазина 
                   WHERE код_магазина = @код_магазина";

                    using (var command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@код_магазина", store.код_магазина);
                        command.Parameters.AddWithValue("@название_магазина", store.название_магазина.Trim());
                        command.Parameters.AddWithValue("@адрес_магазина", store.адрес_магазина);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "DELETE FROM Магазин WHERE код_магазина = @код_магазина;";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_магазина", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> GetNextIdAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT COALESCE(MAX(код_магазина), 0) + 1 FROM Магазин;";
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
                var sql = $"SELECT COUNT(*) FROM {tableName} WHERE код_магазина = @код_магазина";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_магазина", id);
                    return Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
                }
            }
        }
        private async Task<bool> CheckStoreExistsAsync(NpgsqlConnection connection, string название_магазина, string адрес_магазина)
        {
            var sql = @"SELECT COUNT(*) FROM Магазин 
                   WHERE название_магазина = @название_магазина AND адрес_магазина = @адрес_магазина";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@название_магазина", название_магазина);
                command.Parameters.AddWithValue("@адрес_магазина", адрес_магазина);

                return Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
            }
        }

        public Task<bool> IsForeignKeyValidAsync(string tableName, string columnName, int id)
        {
            throw new NotImplementedException();
        }
    }
}
