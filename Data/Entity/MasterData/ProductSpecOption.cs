using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity.MasterData
{
    public class ProductSpecOption:BaseEntity
    {
        [Required]
        public int ProductSpecId { get; set; }
        public int? TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public string Option { get; set; }
        public virtual ProductSpec ProductSpec { get; set; }
    }
}
