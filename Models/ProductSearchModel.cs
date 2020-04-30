using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SorubankCMS.Models
{
    public class ProductSearchModel
    {
        [JsonProperty("cardTypes")]
        public HashSet<int> CardTypes { get; set; }
        [JsonProperty("tenantIds")]
        public HashSet<int> TenantIds { get; set; }
        [JsonProperty("lessonIds")]
        public HashSet<int> LessonIds { get; set; }
        [JsonProperty("topicIds")]
        public HashSet<int> TopicIds { get; set; }
        [JsonProperty("examTypeIds")]
        public HashSet<int> ExamTypeIds { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sortDirection")]
        public string SortDirection { get; set; }
        [JsonProperty("sortField")]
        public string SortField { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("recordCount")]
        public int RecordCount { get; set; }
        [JsonProperty("minPrice")]
        public double MinPrice { get; set; }
        [JsonProperty("maxPrice")]
        public double MaxPrice { get; set; }
    }
}