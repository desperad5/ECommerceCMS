using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class Sector:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
