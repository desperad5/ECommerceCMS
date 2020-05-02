using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class BrandViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name") ]
        public string Name { get; set; }

        [JsonProperty("webSiteUrl")]
        public string WebSiteUrl { get; set; }

    }
}
