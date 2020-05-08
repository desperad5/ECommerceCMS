using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("salePrice")]
        public double SalePrice { get; set; }
        [JsonProperty("baseImageUrl")]
        public string BaseImageUrl { get; set; }
        [JsonProperty("sale")]
        public bool isSale { get; set; }
        [JsonProperty("new")]
        public bool isNew { get; set; }
        [JsonProperty("pictures")]
        public List<string> Pictures { get; set; }
        [JsonProperty("brandId")]
        public int? BrandId { get; set; }
        [JsonProperty("variations")]
        public List<ProductVariationModel> ProductVariations { get; set; }
        [JsonProperty("discount")]
        public int Discount { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
