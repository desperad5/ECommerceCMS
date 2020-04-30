using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Tag : BaseEntity
    {
        public int CMSUserId { get; set; }
        public virtual CMSUser User { get; set; }
        [Required]
        public string Value { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
