using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemrating.Data.EntityModels
{
    public class Review
    {
        public int Id { get; set; }
        public string? Comments { get; set; }
        public double Rating { get; set; }
        public DateTime? CreatedDate { get; set; } = null;

        [ForeignKey("User")]
        public virtual int UserId { get; set; }
        public virtual User? User { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

    }
}
