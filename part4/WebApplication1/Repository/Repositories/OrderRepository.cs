using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;

namespace Repository.Repositories
{
    public class OrderRepository : IOrderRepository<Order>
    {
        private readonly IContext _context;
        public OrderRepository(IContext context)
        {
            this._context = context;
        }

        public async Task<Order> addAsync(Order item)
        {
            await _context.Orders.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task<List<Order>> getAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> getByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Order> getByNameAsync(string name)
        {
            throw new NotImplementedException("לא נתמך");
        }

        public async Task<Order> updateAsync(Order item)
        {
            _context.Orders.Update(item);
            await _context.Save();
            return item;
        }
    }
}
