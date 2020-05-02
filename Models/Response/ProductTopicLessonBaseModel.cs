using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECommerceCMS.Models.Response
{
    public class ProductTopicLessonBaseModel : ProductBaseModel
    {
        [JsonProperty("lessonName")]
        public string LessonName { get; set; }
        [JsonProperty("educationLevel")]
        public int LessonEducationLevel { get; set; }
        [JsonProperty("topicName")]
        public string TopicName { get; set; }
        [JsonProperty("classLevel")]
        public string TopicClassLevel { get; set; }
    }
}