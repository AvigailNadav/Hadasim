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
    public class SupplierProductRepository : IRepository<SupplierProducts>
    {
        private readonly IContext _context;
        public SupplierProductRepository(IContext context)
        {
            _context = context;
        }
        public async Task<SupplierProducts> addAsync(SupplierProducts item)
        {
            await _context.SupplierProducts.AddAsync(item);
            await _context.Save();
            return item;
        }

        public async Task<List<SupplierProducts>> getAllAsync()
        {
            return await _context.SupplierProducts.ToListAsync();
        }

        public async Task<SupplierProducts> getByIdAsync(int id)
        {
            return await _context.SupplierProducts.FirstOrDefaultAsync(x => x.SupplierProductId == id);
        }

        public Task<SupplierProducts> getByNameAsync(string name)
        {
            throw new NotImplementedException("לא נתמך");
        }
    }
}
