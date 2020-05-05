using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class ProductCategory:BaseEntity
    {
        
        [Required]
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public int? MenuId { get; set; }
        public Menu Menu { get; set; }
        public int? ParentCategoryId { get; set; }
        public ProductCategory ParentCategory { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<ProductCategory> ChildCategories { get; set; }
    }
}
