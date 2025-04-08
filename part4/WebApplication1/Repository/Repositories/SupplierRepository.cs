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
    public class SupplierRepository:IRepository<Supplier>
    {
        private readonly IContext _context;
        public SupplierRepository(IContext context)
        {
            _context = context;
        }

        public async Task<Supplier> addAsync(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            await _context.Save();
            return supplier;
        }

        public async Task<List<Supplier>> getAllAsync()
        {
            return await _context.Suppliers.
                Include(s => s.Products)
                .Where(s => s.Role == "Supplier").
                ToListAsync();
        }

        public async Task<Supplier> getByIdAsync(int id)
        {
            return await _context.Suppliers.
                Include(s => s.Products).
                FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supplier> getByNameAsync(string name)
        {
            return await _context.Suppliers.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
