using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SupplierProductService : IService<SupplierProduct>
    {
        private readonly IRepository<SupplierProduct> supplierProductRepository;
        public SupplierProductService(IRepository<SupplierProduct> supplierProductRepository)
        {
            this.supplierProductRepository = supplierProductRepository;
        }
        public async Task<SupplierProduct> addAsync(SupplierProduct item)
        {
            return await supplierProductRepository.addAsync(item);
        }

        public async Task<List<SupplierProduct>> getAllAsync()
        {
            return await supplierProductRepository.getAllAsync();
        }

        public async Task<SupplierProduct> getByIdAsync(int id)
        {
            return await supplierProductRepository.getByIdAsync(id);
        }

        public async Task<SupplierProduct> getByNameAsync(string name)
        {
            return await supplierProductRepository.getByNameAsync(name);
        }
    }
}
