using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ECommerceCMS.Models.Response;

namespace ECommerceCMS.Models
{
    public class ProductTenantModel : ProductBaseModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}
