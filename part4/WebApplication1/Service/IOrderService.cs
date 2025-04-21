using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrderService<T>
    {
        public Task<List<T>> getAllAsync();
        public Task<T> getByIdAsync(int id);
        public Task<T> getByNameAsync(string name);
        public Task<T> addAsync(T item);
        public Task<T> updateAsync(T item);
    }
}
