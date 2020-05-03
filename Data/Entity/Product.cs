using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class Product : BaseEntity
    {

        [Required]
        public double TenantPrice { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [Required]
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        [Required]
        public string BaseImageUrl { get; set; }
        public string InventoryCode { get; set; }
        public int? InventoryCount { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public int? BrandId { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        public Brand Brand{get;set;}
        public ProductType ProductType { get; set; }
        

        public virtual ICollection<OrderCartItem> OrderCartItems { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ICollection<ProductRating> ProductRatings { get; set; }
        public virtual ICollection<ProductBundle> ProductBundles { get; set; }
        public double Rating { get; set; }
      
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
