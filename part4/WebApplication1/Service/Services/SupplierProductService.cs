using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SupplierProductService : IService<SupplierProducts>
    {
        private readonly IRepository<SupplierProducts> supplierProductRepository;
        public SupplierProductService(IRepository<SupplierProducts> supplierProductRepository)
        {
            this.supplierProductRepository = supplierProductRepository;
        }
        public async Task<SupplierProducts> addAsync(SupplierProducts item)
        {
            return await supplierProductRepository.addAsync(item);
        }

        public async Task<List<SupplierProducts>> getAllAsync()
        {
            return await supplierProductRepository.getAllAsync();
        }

        public async Task<SupplierProducts> getByIdAsync(int id)
        {
            return await supplierProductRepository.getByIdAsync(id);
        }

        public async Task<SupplierProducts> getByNameAsync(string name)
        {
            return await supplierProductRepository.getByNameAsync(name);
        }
    }
}
