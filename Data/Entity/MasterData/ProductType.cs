using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class ProductType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int SectorId { get; set; }
        public virtual Sector Sector{get;set;}
    }
}
