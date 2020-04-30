using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SorubankCMS.Models.Response
{
    public class ProductExamCardModel : ProductBaseModel
    {
        [JsonProperty("examTypeId")]
        public int ExamTypeId { get; set; }
        [JsonProperty("questionCount")]
        public int QuestionCount { get; set; }
        [JsonProperty("productId")]
        public int ProductId { get; set; }
        [JsonProperty("productName")]
        public string ProductName { get; set; }
    }
}
