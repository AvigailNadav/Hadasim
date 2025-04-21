using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string Status { get; set; }
        public double TotalAmount { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }

    }
}
