using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class BundleQuestionCard:BaseEntity
    {
        [Required]
        public int BundleId { get; set; }
        public virtual Bundle Bundle { get; set; }
        [Required]
        public int QuestionCardId { get; set; }
        public virtual QuestionCard QuestionCard { get; set; }
    }
}
