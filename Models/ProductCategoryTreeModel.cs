using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductCategoryTreeModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("parentCategoryId")]
        public int? ParentCategoryId { get; set; }
        [JsonProperty("tenantId")]
        public int TenantId { get; set; }
        [JsonProperty("childCategories")]
        public List<ProductCategoryTreeModel> ChildCategories { get; set; }
        [JsonProperty("categoryName")]
        public string CategoryName { get; set; }
        
        
    }
}
