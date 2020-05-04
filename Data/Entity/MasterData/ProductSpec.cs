using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ECommerceCMS.Helpers.Enums;

namespace ECommerceCMS.Data.Entity.MasterData
{
    public class ProductSpec:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public ProductSpecValueTypes ProductSpecValueType { get; set; }
        public int? TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public Boolean isViewSpec { get; set; }

    }
}
