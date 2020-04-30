using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Topic : BaseEntity
    {
        [Required]
        public string ClassLevel { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public virtual Lesson Lesson { get; set; }
        public int? LessonId { get; set; }
        public int? ParentTopicId { get; set; }
        [ForeignKey("ParentTopicId")]
        public virtual Topic ParentTopic { get; set; }

        public virtual ICollection<Topic> ChildTopics { get; set; }
        public virtual ICollection<QuestionCard> QuestionCards { get; set; }
        public virtual ICollection<TopicCard> TopicCards { get; set; }

    }
}
