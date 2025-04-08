using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductService : IService<Product>
    {
        private readonly IRepository<Product> productRepository;
        public ProductService(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<Product> addAsync(Product item)
        {
            return await productRepository.addAsync(item);
        }

        public async Task<List<Product>> getAllAsync()
        {
            return await productRepository.getAllAsync();   
        }

        public async Task<Product> getByIdAsync(int id)
        {
            return await productRepository.getByIdAsync(id);
        }

        public async Task<Product> getByNameAsync(string name)
        {
            return await productRepository.getByNameAsync(name);
        }
    }
}
