using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.DTO
{
    public class ProductWithSupplierDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public List<SupplierDto> Suppliers { get; set; }
    }
}
