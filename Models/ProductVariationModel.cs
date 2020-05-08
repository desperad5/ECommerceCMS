using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ECommerceCMS.Helpers.Enums;

namespace ECommerceCMS.Models
{
    public class ProductVariationModel
    {
        public string ColorValue { get; set; }
        public string SizeValue { get; set; }
        public int? Quantity { get; set; }
    }
}
