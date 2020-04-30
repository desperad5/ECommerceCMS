using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Models
{
    public class ProductCommentModel
    {
        public ProductCommentModel()
        {
          
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("comment")]
        public string Comment { get; set; }
        [JsonProperty("user")]
        public UserViewModel User { get; set; }
    }
}
