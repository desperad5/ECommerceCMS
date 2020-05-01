using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class ProductCategory:BaseEntity
    {
        public int? ParentCategoryId { get; set; }
        public ProductCategory ParentCategory { get; set; }
        [Required]
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<ProductCategory> ChildCategories { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        public int? ListingId { get; set; }
        public Listing Listing { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
