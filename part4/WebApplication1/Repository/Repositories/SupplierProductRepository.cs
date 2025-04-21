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
    public class SupplierProductRepository : IRepository<SupplierProduct>
    {
        private readonly IContext _context;
        public SupplierProductRepository(IContext context)
        {
            _context = context;
        }
        public async Task<SupplierProduct> addAsync(SupplierProduct item)
        {
            await _context.SupplierProducts.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task<List<SupplierProduct>> getAllAsync()
        {
            return await _context.SupplierProducts.ToListAsync();
        }

        public async Task<SupplierProduct> getByIdAsync(int id)
        {
            return await _context.SupplierProducts.Include(sp=>sp.Product).Include(sp=>sp.Supplier).
                FirstOrDefaultAsync(x => x.SupplierProductId == id);
        }

        public Task<SupplierProduct> getByNameAsync(string name)
        {
            throw new NotImplementedException("לא נתמך");
        }
    }
}
