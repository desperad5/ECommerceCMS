using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class Bundle : BaseEntity
    {
        
        public virtual ICollection<ProductBundle> BundleProducts { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Name { get; set; }
        public  string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<OrderCartItem> OrderCartItems { get; set; }
    }
}
