using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int MinPurchase { get; set; }
        public int SupplierId { get; set; }
    }
}
