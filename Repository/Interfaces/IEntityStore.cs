using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IEntityStore<T>
    {
        Task<T> Get();

        Task<bool> Put(T entity);
        
        Task Delete(T mappedEntity);
    }
}
