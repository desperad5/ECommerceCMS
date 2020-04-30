using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class QuestionCard:BaseEntity
    {
        
        [Required]
        public int TopicId { get; set; }
        public virtual Topic Topic { get; set; }
        [Required]
        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }
        [Required]
        public int QuestionCount { get; set; }
        
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<BundleQuestionCard> BundleQuestionCards { get; set; }

    }
}
