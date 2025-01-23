using MarmeladhubApp.BL;
using Npgsql;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MarmeladhubApp.DataAccess
{
    public class ProductRepository : ICrudOperations<Товар>
    {
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Добавляем метод проверки существования 
        // Метод проверки существования товара в магазине
        private async Task<bool> IsProductExistsInStoreAsync(NpgsqlConnection connection, string названиеТовара, int кодМагазина, int? исключитьКодТовара = null)
        {
            var sql = исключитьКодТовара == null ?
                "SELECT COUNT(*) FROM Товар WHERE название_товара = @названиеТовара AND код_магазина = @кодМагазина" :
                "SELECT COUNT(*) FROM Товар WHERE название_товара = @названиеТовара AND код_магазина = @кодМагазина AND код_товара != @исключитьКод";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@названиеТовара", названиеТовара);
                command.Parameters.AddWithValue("@кодМагазина", кодМагазина);
                if (исключитьКодТовара != null)
                    command.Parameters.AddWithValue("@исключитьКод", исключитьКодТовара.Value);

                return Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
            }
        }

        public async Task AddAsync(Товар товар)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Проверяем существование фабрики
                if (!await IsForeignKeyValidAsync("Фабрика", "код_фабрики", товар.код_фабрики))
                    throw new ArgumentException($"Фабрика с id {товар.код_фабрики} не существует");

                // Проверяем существование магазина
                if (!await IsForeignKeyValidAsync("Магазин", "код_магазина", товар.код_магазина))
                    throw new ArgumentException($"Магазин с id {товар.код_магазина} не существует");

                // Проверяем существование товара с таким же названием в категории
                if (await IsProductExistsInStoreAsync(connection, товар.название_товара, товар.код_фабрики))
                    throw new ArgumentException($"Товар {товар.название_товара} уже производится в этой фабрике");

                // Проверяем существование товара по коду
                if (await IsProductCodeExistsAsync(connection, товар.код_товара))
                    throw new ArgumentException($"Товар с кодом {товар.код_товара} уже существует");

                // Остальные проверки
                if (string.IsNullOrWhiteSpace(товар.название_товара) || !Regex.IsMatch(товар.название_товара, @"^[А-Яа-яЁё0-9s-]+$"))
                    throw new ArgumentException("Название товара может содержать только кириллицу, цифры, пробелы и тире");

                // Добавление записи
                const string sql = @"INSERT INTO Товар (код_товара, название_товара, код_магазина, код_фабрики) 
                             VALUES (@код_товара, @название_товара, @код_магазина, @код_фабрики)";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_товара", товар.код_товара);
                    command.Parameters.AddWithValue("@название_товара", товар.название_товара);
                    command.Parameters.AddWithValue("@код_магазина", товар.код_магазина);
                    command.Parameters.AddWithValue("@код_фабрики", товар.код_фабрики);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task<bool> IsProductCodeExistsAsync(NpgsqlConnection connection, int код_товара)
        {
            const string sql = "SELECT COUNT(*) FROM Товар WHERE код_товара = @код_товара";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@код_товара", код_товара);
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result) > 0;
            }
        }


        public async Task UpdateAsync(Товар товар)
        {
            if (!await IsForeignKeyValidAsync("Товар", "код_товара", товар.код_товара))
            {
                throw new ArgumentException($"Код товара изменить нельзя, используйте функцию добавления.");
            }
            else
            {
                // 1. Открываем подключение к БД
                using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                
                // 2. Проверяем существование фабрики
                if (!await IsForeignKeyValidAsync("Фабрика", "код_фабрики", товар.код_фабрики))
                {
                    throw new ArgumentException($"Фабрика с id {товар.код_фабрики} не существует");
                }

                // 3. Проверяем, нет ли товара с таким названием в этой категории (кроме текущего)
                var checkSql = @"SELECT COUNT(*) FROM Товар 
                         WHERE название_товара = @название_товара 
                         AND код_фабрики = @код_фабрики 
                         AND код_товара != @код_товара";

                using (var checkCommand = new NpgsqlCommand(checkSql, connection))
                {
                    checkCommand.Parameters.AddWithValue("@название_товара", товар.название_товара);
                    checkCommand.Parameters.AddWithValue("@код_фабрики", товар.код_фабрики);
                    checkCommand.Parameters.AddWithValue("@код_товара", товар.код_товара);

                    if (Convert.ToInt32(await checkCommand.ExecuteScalarAsync()) > 0)
                    {
                        throw new ArgumentException($"Товар {товар.название_товара} уже производится в этой фабрике");
                    }
                }

                if (!await IsForeignKeyValidAsync("Фабрика", "код_фабрики", товар.код_фабрики))
                    throw new ArgumentException($"Фабрика с id {товар.код_фабрики} не существует");

                // Проверяем существование магазина
                if (!await IsForeignKeyValidAsync("Магазин", "код_магазина", товар.код_магазина))
                    throw new ArgumentException($"Магазин с id {товар.код_магазина} не существует");

                // Проверяем существование товара с таким же названием в категории
                if (await IsProductExistsInStoreAsync(connection, товар.название_товара, товар.код_фабрики))
                    throw new ArgumentException($"Товар {товар.название_товара} уже производится в этой фабрике");

                // Проверяем существование товара по коду
               


                // 4. Обновляем запись
                const string sql = @"UPDATE Товар 
                             SET название_товара = @название_товара, код_магазина = @код_магазина, код_фабрики = @код_фабрики 
                             WHERE код_товара = @код_товара";

                using (var updateCommand = new NpgsqlCommand(sql, connection))
                {
                    updateCommand.Parameters.AddWithValue("@код_товара", товар.код_товара);
                    updateCommand.Parameters.AddWithValue("@название_товара", товар.название_товара);
                    updateCommand.Parameters.AddWithValue("@код_магазина", товар.код_магазина);
                    updateCommand.Parameters.AddWithValue("@код_фабрики", товар.код_фабрики);
                    await updateCommand.ExecuteNonQueryAsync();
                }


            
                
                // 3. Проверяем существование категории
                if (!await IsForeignKeyValidAsync("Фабрика", "код_фабрики", товар.код_фабрики))
                {
                    throw new ArgumentException($"Фабрика с id {товар.код_фабрики} не существует");
                }

                // 4. Обновляем данные
                const string updateSql = @"UPDATE Товар 
                                           SET название_товара = @название_товара, 
                                               код_магазина = @код_магазина, 
                                               код_фабрики = @код_фабрики 
                                           WHERE код_товара = @код_товара";

                using (var updateCommand = new NpgsqlCommand(updateSql, connection))
                {
                    updateCommand.Parameters.AddWithValue("@код_товара", товар.код_товара);
                    updateCommand.Parameters.AddWithValue("@название_товара", товар.название_товара);
                    updateCommand.Parameters.AddWithValue("@код_магазина", товар.код_магазина);
                    updateCommand.Parameters.AddWithValue("@код_фабрики", товар.код_фабрики);

                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            }
        }



        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Товар WHERE код_товара = @код_товара;";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@код_товара", id);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Товар>> GetAllAsync()
        {
            var товары = new List<Товар>();
            const string sql = "SELECT код_товара, название_товара, код_магазина, код_фабрики FROM Товар;";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new NpgsqlCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        товары.Add(new Товар(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetInt32(2),
                            reader.GetInt32(3)
                        ));
                    }
                }
            }
            return товары;
        }


        public async Task<int> GetNextIdAsync(string name)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT COALESCE(MAX(код_товара), 0) + 1 FROM Товар;";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
                throw new NotImplementedException();
            }
        }

        public async Task<bool> IsForeignKeyValidAsync(string tableName, string columnName, int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Используем параметризованный запрос для защиты от SQL-инъекций
                var sql = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @id;";

                using (var command = new NpgsqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    return Convert.ToInt32(await command.ExecuteScalarAsync()) > 0;
                }
            }
        }

        
    }
}
