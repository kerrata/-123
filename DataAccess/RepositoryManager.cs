using MarmeladhubApp.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.DataAccess
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly string _connectionString;

        public RepositoryManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ICrudOperations<object>? CreateRepository(string tableName)
        {
            switch (tableName)
            {
                case "Магазин":
                    return new RepositoryAdapter<Магазин>(new StoreRepository(_connectionString));
                case "Фабрика":
                    return new RepositoryAdapter<Фабрика>(new FabricRepository(_connectionString)); 
                case "Товар":
                    return new RepositoryAdapter<Товар>(new ProductRepository(_connectionString));
                
                default:
                    return null;
            }
        }

        public Type? GetTableType(string tableName)
        {
            switch (tableName)
            {
                case "Магазин":
                    return typeof(Магазин);
                case "Фабрика":
                    return typeof(Фабрика);
                case "Товар":
                    return typeof(Товар);
                default:
                    return null;
            }
        }
    }
}
