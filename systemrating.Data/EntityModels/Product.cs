using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemrating.Data.EntityModels
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public string?   Description { get; set; }
        public int? Quantity { get; set; } = null;
        public string? Photo { get; set; } = null;
        
    }
}
