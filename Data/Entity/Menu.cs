using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ECommerceCMS.Helpers.Enums;

namespace ECommerceCMS.Data.Entity
{
    public class Menu:BaseEntity
    {
        public int? ParentMenuId { get; set; }
        public Menu ParentMenu { get; set; }
        public virtual ICollection<Menu> ChildMenus { get; set; }
        public bool isRoot { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        [Required]
        public string Name { get; set; }
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public MenuLocations Location { get; set; }
    }
}
