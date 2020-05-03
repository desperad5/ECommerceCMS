using ECommerceCMS.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class ProductSpecValue:BaseEntity
    {
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int ProductSpecId { get; set; }
        public ProductSpec ProductSpec { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
