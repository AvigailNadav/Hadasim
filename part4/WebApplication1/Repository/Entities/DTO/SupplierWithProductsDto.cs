using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.DTO
{
    public class SupplierWithProductsDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public List<ProductDto> Products { get; set; }

    }
}
