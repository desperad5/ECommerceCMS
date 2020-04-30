using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SorubankCMS.Models.Response
{
    public class ProductQuestionCardModel : ProductTopicLessonBaseModel
    {
        [JsonProperty("questionCount")]
        public int QuestionCount { get; set; }

        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("productName")]
        public string ProductName { get; set; }
    }
}
