using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductsByListingRequestModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
