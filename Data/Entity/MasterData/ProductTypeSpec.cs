using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity.MasterData
{
    public class ProductTypeSpec:BaseEntity
    {
        [Required]
        public int ProductTypeId { get; set; }
        [Required]
        public int ProductSpecId { get; set; }
        public ProductType ProductType { get; set; }
        public ProductSpec ProductSpec { get; set; }
    }
}
