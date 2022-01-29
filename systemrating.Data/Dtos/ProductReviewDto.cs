using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemrating.Data.Dtos
{
    public class ProductReviewDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public int? Quantity { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }
        public double AverageRating { get; set; }
    }
}
