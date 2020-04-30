using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SorubankCMS.Models.Response
{
    public class OrderCartItemResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("product")]
        public ProductResponseModel Product { get; set; }
    }
}
