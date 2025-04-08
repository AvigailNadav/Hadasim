using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class SupplierProducts
    {
        public int SupplierProductId { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [JsonIgnore] 
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
