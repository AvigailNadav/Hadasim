using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IOrderRepository<Order>
    {
        public Task<List<Order>> getAllAsync();
        public Task<Order> getByIdAsync(int id);
        public Task<Order> getByNameAsync(string name);
        public Task<Order> addAsync(Order item);
        public Task<Order> updateAsync(Order item);
    }
}
