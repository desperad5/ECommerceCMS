using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductsByCategoryModel
    {
        
        [JsonProperty("products")]
        public List<ProductViewModel> Products { get; set; }

        [JsonProperty("brands")]
        public List<BrandViewModel> Brands { get; set; }
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
        [JsonProperty("listing")]
        public string Listing { get; set; }
    }
}
