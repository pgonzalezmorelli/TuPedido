using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TuPedido.Repositories
{
    public interface IDatabase<T> where T : new()
    {
        Task<int> Insert(T item);

        Task<int> Delete(T item);

        Task<int> Update(T item);

        Task<List<T>> Select();

        Task<List<T>> Select(Expression<Func<T, bool>> filter);

        Task<T> First();
    }
}
