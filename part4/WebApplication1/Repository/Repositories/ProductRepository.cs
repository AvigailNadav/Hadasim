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
    public class ProductRepository : IRepository<Product>
    {
        private readonly IContext _context;
        public ProductRepository(IContext context)
        {
            _context = context;
        }

        async Task<Product> IRepository<Product>.addAsync(Product item)
        {
            await _context.Products.AddAsync(item);
            await _context.Save();
            return item;
        }

        async Task<List<Product>> IRepository<Product>.getAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        async Task<Product> IRepository<Product>.getByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task<Product> IRepository<Product>.getByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
