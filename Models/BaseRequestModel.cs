using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ECommerceCMS.Helpers.Enums;

namespace ECommerceCMS.Models
{
    public class BaseRequestModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        
        
        [JsonProperty("templateType")]
        public TemplateTypes? TemplateType { get; set; }
        public int? ItemCount { get; set; }
        [JsonProperty("pageNumber")]
        public int? PageNumber { get; set; }
    }
    
}
