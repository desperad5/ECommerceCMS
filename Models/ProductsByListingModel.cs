using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductsByListingModel
    {
        [JsonProperty("products")]
        public List<ProductViewModel> Products { get; set; }
        [JsonProperty("listing")]
        public ListingViewModel Listing { get; set; }
    }
}
