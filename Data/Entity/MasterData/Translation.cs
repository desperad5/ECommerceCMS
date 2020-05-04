using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity.MasterData
{
    public class Translation:BaseEntity
    {
        [Required]
        public int LanguageId { get; set; }
        public virtual Language Language { get; set; }
        [Required]
        public string TranslatedValue { get; set; }
    }
}
