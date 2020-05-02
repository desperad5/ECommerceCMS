using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models.Response;

namespace ECommerceCMS.Models
{
    public class ProductResponseModel : ProductBaseModel
    {
        [JsonProperty("entityTypeId")]
        public int EntityTypeId { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("fileUrl")]
        public string FileUrl { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("tenant")]
        public ProductTenantModel Tenant { get; set; }
        [JsonProperty("examCard")]
        public ProductExamCardModel ExamCard { get; set; }
        [JsonProperty("questionCard")]
        public ProductQuestionCardModel QuestionCard { get; set; }
        [JsonProperty("topicCard")]
        public ProductTopicCardModel TopicCard { get; set; }
        [JsonProperty("bundle")]
        public ProductBundleModel Bundle { get; set; }

    }
}