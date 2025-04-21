using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderDetailsService : IService<OrderDetails>
    {
        private readonly IRepository<OrderDetails> orderDetailsRepository;
        public OrderDetailsService(IRepository<OrderDetails> orderDetailsRepository)
        {
            this.orderDetailsRepository = orderDetailsRepository;
        }
        public async Task<OrderDetails> addAsync(OrderDetails item)
        {
            return await orderDetailsRepository.addAsync(item);
        }

        public async Task<List<OrderDetails>> getAllAsync()
        {
            return await orderDetailsRepository.getAllAsync();
        }

        public async Task<OrderDetails> getByIdAsync(int id)
        {
            return await orderDetailsRepository.getByIdAsync(id);
        }

        public async Task<OrderDetails> getByNameAsync(string name)
        {
            return await orderDetailsRepository.getByNameAsync(name);
        }
    }
}
