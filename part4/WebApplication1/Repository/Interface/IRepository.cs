using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepository<T>
    {
        Task<List<T>> getAllAsync();
        Task<T> getByIdAsync(int id);
        Task<T> getByNameAsync(string name);
        Task<T> addAsync(T item);
    }
}
