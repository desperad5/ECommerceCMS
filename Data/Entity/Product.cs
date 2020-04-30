using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Product : BaseEntity
    {
        [Required]
        public int EntityTypeId { get; set; }
        
        [Required]
        public double TenantPrice { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Name { get; set; }
       
        public string Description { get; set; }

        [Required]
        public int TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }

        [Required]
        public string FileUrl { get; set; }

        public virtual ICollection<OrderCartItem> OrderCartItems { get; set; }
        public virtual ICollection<ProductComment> ProductComments { get; set; }
        public virtual ExamCard ExamCard { get; set; }
        public virtual QuestionCard QuestionCard { get; set; }
        public virtual TopicCard TopicCard { get; set; }
        public virtual Bundle Bundle { get; set; }
      
        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
