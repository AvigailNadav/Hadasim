using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderDetailsRepository : IRepository<OrderDetails>
    {
        private readonly IContext _context;
        public OrderDetailsRepository(IContext context)
        {
            _context = context;
        }
        public async Task<OrderDetails> addAsync(OrderDetails item)
        {
            await _context.OrderDetails.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task<List<OrderDetails>> getAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetails> getByIdAsync(int id)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailsId == id);
        }

        public Task<OrderDetails> getByNameAsync(string name)
        {
            throw new NotImplementedException("לא נתמך");
        }
    }
}
