using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.DTO
{
    public class CreateSupplierProductDto
    {
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
    }
}
