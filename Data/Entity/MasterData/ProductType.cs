using ECommerceCMS.Data.Entity.MasterData;
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
        public int? TranslationId { get; set; }
        public Translation Translation { get; set; }
        [Required]
        public int SectorId { get; set; }
        public virtual Sector Sector{get;set;}
    }
}
