using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductCategoryViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
        [JsonProperty("listingName")]
        public string ListingName { get; set; }
        [JsonProperty("listingId")]
        public int ListingId { get; set; }
        [JsonProperty("parentCategoryName")]
        public string ParentCategoryName { get; set; }
        [JsonProperty("parentCategoryId")]
        public int ParentCategoryId { get; set; }
    }
}
