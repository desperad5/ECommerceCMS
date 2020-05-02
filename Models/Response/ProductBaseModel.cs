using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECommerceCMS.Models.Response
{
    public class ProductBaseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}