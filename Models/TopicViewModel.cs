using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SorubankCMS.Data.Entity;

namespace SorubankCMS.Models
{
    public class TopicViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lessonName")]
        public string LessonName { get; set; }
        [JsonProperty("lessonId")]
        public int LessonId { get; set; }
        [JsonProperty("parentTopicName")]
        public string ParentTopicName { get; set; }
        [JsonProperty("parentTopicId")]
        public int ParentTopicId { get; set; }
        [JsonProperty("classLevel")]
        public string ClassLevel { get; set; }
    }
}