using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class ProductBundle : BaseEntity
    {
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        [Required]
        public int BundleId { get; set; }
        public virtual Bundle Bundle{get;set;}
    }
}
