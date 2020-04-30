using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class ExamCard:BaseEntity
    {
        
        [Required]
        public int ExamTypeId { get; set; }
        [Required]
        public int QuestionCount { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<BundleExamCard> BundleExamCards { get; set; }
    }
}
