using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SupplierService : IService<Supplier>
    {
        private readonly IRepository<Supplier> supplierRepository;
        public SupplierService(IRepository<Supplier> supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }
        public async Task<Supplier> addAsync(Supplier item)
        {
            return await supplierRepository.addAsync(item);
        }

        public async Task<List<Supplier>> getAllAsync()
        {
            return await supplierRepository.getAllAsync();
        }

        public Task<Supplier> getByIdAsync(int id)
        {
            return supplierRepository.getByIdAsync(id);
        }

        public Task<Supplier> getByNameAsync(string name)
        {
            return supplierRepository.getByNameAsync(name);
        }
    }
}
