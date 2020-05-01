using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Brand:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string WebSiteUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
