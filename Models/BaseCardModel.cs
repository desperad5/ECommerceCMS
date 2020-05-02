using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class BaseCardModel
    {

        public BaseCardModel ()
        {
            CreatedDate = DateTime.Today;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("tenant")]
        public TenantViewModel Tenant { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("fileUrl")]
        public string FileUrl { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
    }
}
