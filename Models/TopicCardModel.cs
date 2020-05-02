using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class TopicCardModel : BaseCardModel
    {
        [JsonProperty("topic")]
        public TopicViewModel Topic { get; set; }
        [JsonProperty("lesson")]
        public LessonViewModel Lesson { get; set; }
        [JsonProperty("instructorName")]
        public string InstructorName { get; set; }
    }
}
