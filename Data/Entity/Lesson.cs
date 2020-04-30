using  SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Lesson : BaseEntity
    {

        public Lesson()
        {
        }
        //Ortaöğretim, lise vb... Enumda tutulacak.
        public int EducationLevel { get; set; }

        [Required]
        [Index("UNIQUE_DeviceName", IsUnique = true)]
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Topic> Topics { get; set; }
        public virtual ICollection<QuestionCard> QuestionCards { get; set; }
        public virtual ICollection<TopicCard> TopicCards { get; set; }

    }
}
