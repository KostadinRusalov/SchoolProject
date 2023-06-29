using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDbContext<T, K> where K : IConvertible
    {
        Task Create(T item);
        Task<T> Read(K key);
        Task<IEnumerable<T>> ReadAll();
        Task Update(T item);
        Task Delete(K key);
    }
}
