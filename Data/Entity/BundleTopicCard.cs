using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class BundleTopicCard : BaseEntity
    {
        [Required]
        public int BundleId { get; set; }
        public virtual Bundle Bundle { get; set; }
        [Required]
        public int TopicCardId { get; set; }
        public virtual TopicCard TopicCard {get;set;}
    }
}
