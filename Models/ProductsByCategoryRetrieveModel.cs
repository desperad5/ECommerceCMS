using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductsByCategoryRetrieveModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("itemCount")]
        public int? ItemCount { get; set; }
        [JsonProperty("pageNumber")]
        public int? PageNumber { get; set; }

    }
}
