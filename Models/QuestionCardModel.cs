using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Models
{
    public class QuestionCardModel:BaseCardModel
    {
        
        [JsonProperty("topic")]
        public TopicViewModel Topic { get; set; }
        [JsonProperty("lesson")]
        public LessonViewModel Lesson { get; set; }       
        [JsonProperty("questionCount")]
        public int QuestionCount { get; set; }
        
    }
}
