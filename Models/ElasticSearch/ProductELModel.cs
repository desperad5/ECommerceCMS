using Newtonsoft.Json;
using SorubankCMS.Data.Entity;
using System.Collections.Generic;
using System.Collections;

namespace SorubankCMS.Models.ElasticSearch
{
    public class ProductELModel
    {
        public ProductELModel(){
            CardTags = new ArrayList();
        }

        public int Id { get; set; }
        
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        public int EntityTypeId { get; set; }

        public string entityName { get; set; }

        public string FileUrl { get; set; }
       
        public ArrayList CardTags { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}