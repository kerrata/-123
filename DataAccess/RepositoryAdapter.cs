using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.DataAccess
{
    public class RepositoryAdapter<T> : ICrudOperations<object>
    {
        private readonly ICrudOperations<T> _innerRepository;

        public RepositoryAdapter(ICrudOperations<T> innerRepository)
        {
            _innerRepository = innerRepository;
        }

        public async Task<List<object>> GetAllAsync()
        {
            var result = await _innerRepository.GetAllAsync();
            return result.Cast<object>().ToList();
        }

        public Task AddAsync(object entity)
        {
            return _innerRepository.AddAsync((T)entity);
        }

        public Task UpdateAsync(object entity)
        {
            return _innerRepository.UpdateAsync((T)entity);
        }

        public Task DeleteAsync(int id)
        {
            return _innerRepository.DeleteAsync(id);
        }

        public Task<int> GetNextIdAsync(string name)
        {
            return _innerRepository.GetNextIdAsync(name);
        }

        

        public Task<bool> IsForeignKeyValidAsync(string tableName, string columnName, int id)
        {
            throw new NotImplementedException();
        }
    }
}
