using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarmeladhubApp.DataAccess
{
    public interface IRepositoryManager
    {
        ICrudOperations<object>? CreateRepository(string? tableName);
        Type? GetTableType(string? tableName);
    }
}
