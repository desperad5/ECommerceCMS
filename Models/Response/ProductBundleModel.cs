using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECommerceCMS.Models.Response
{
    public class ProductBundleModel : ProductBaseModel
    {
        [JsonProperty("questionCards")]
        public IEnumerable<ProductQuestionCardModel> QuestionCards { get; set; }
        [JsonProperty("topicCards")]
        public IEnumerable<ProductTopicCardModel> TopicCards { get; set; }
        [JsonProperty("examCards")]
        public IEnumerable<ProductExamCardModel> ExamCards { get; set; }
    }
}