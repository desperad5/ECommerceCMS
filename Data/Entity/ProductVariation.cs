using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static ECommerceCMS.Helpers.Enums;

namespace ECommerceCMS.Data.Entity
{
    public class ProductVariation : BaseEntity
    {
        [Required]
        public int ProductId { get; set; }
        public SizeValues? Size { get; set; }
        public ColorValues? Color{get;set; }
        public int? Quantity { get; set; }
        public Product Product { get; set; }
    }
}
