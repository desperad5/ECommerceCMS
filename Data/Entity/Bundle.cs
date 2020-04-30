using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Bundle : BaseEntity
    {
        public virtual ICollection<BundleQuestionCard> BundleQuestionCards { get; set; }
        public virtual ICollection<BundleExamCard> BundleExamCards { get; set; }
        public virtual ICollection<BundleTopicCard> BundleTopicCards { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
