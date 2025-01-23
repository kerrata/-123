using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.DataAccess
{
    public interface ICrudOperations<T>
    {
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<int> GetNextIdAsync(string name);
        Task<bool> IsForeignKeyValidAsync(string tableName, string columnName, int id);
    }
}
