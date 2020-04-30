using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class BundleExamCard:BaseEntity
    {
        [Required]
        public int BundleId { get; set; }
        public virtual Bundle Bundle { get; set; }
        [Required]
        public int ExamCardId { get; set; }
        public virtual ExamCard ExamCard { get; set; }
    }
}
