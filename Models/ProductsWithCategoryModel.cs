using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductsWithCategoryModel
    {
        [JsonProperty("products")]
        public List<ProductViewModel> Products { get; set; }
        [JsonProperty("listing")]
        public ProductCategoryViewModel Category { get; set; }
    }
}
