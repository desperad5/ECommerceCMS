using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class BundleModel
    {

        public BundleModel()
        {
            CreatedDate = DateTime.Now;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("tenant")]
        public TenantViewModel Tenant { get; set; }
        [JsonProperty("questionCards")]
        public List<QuestionCardModel> QuestionCards { get; set; }
        [JsonProperty("examCards")]
        public List<ExamCardModel> ExamCards { get; set; }
        [JsonProperty("topicCards")]
        public List<TopicCardModel> TopicCards { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("fileUrl")]
        public string FileUrl { get; set; }
    }
}
