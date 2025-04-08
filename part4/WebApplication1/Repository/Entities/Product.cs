using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int MinPurchase { get; set; }
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        [JsonIgnore]
        public Supplier Supplier { get; set; }
    }
}
