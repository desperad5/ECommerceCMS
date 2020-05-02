using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECommerceCMS.Models.Response
{
    public class ProductTopicCardModel : ProductTopicLessonBaseModel
    {
        [JsonProperty("instructorName")]
        public string InstructorName { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("productName")]
        public string ProductName { get; set; }
    }
}