using Repository.Entities;
using Repository.Interface;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : IOrderService<Order>
    {

        private readonly IOrderRepository<Order> orderRepository;
        public OrderService(IOrderRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        async Task<Order> IOrderService<Order>.addAsync(Order item)
        {
            return await orderRepository.addAsync(item);
        }

        async Task<List<Order>> IOrderService<Order>.getAllAsync()
        {
            return await orderRepository.getAllAsync();
        }

        async Task<Order> IOrderService<Order>.getByIdAsync(int id)
        {
            return await orderRepository.getByIdAsync(id);
        }

        async Task<Order> IOrderService<Order>.getByNameAsync(string name)
        {
            return await orderRepository.getByNameAsync(name);
        }
        async Task<Order> IOrderService<Order>.updateAsync(Order item)
        {
            return await orderRepository.updateAsync(item);
        }
    }
}
