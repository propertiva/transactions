using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces.Table
{
    public interface IEntityStore<T>
    {
        Task<T> Get(string partitionKey, string rowKey);

        Task<bool> Put(T mappedEntity);
    }
}
