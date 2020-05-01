using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Listing:BaseEntity
    {
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
