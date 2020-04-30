using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Models
{
    public class ExamCardModel : BaseCardModel
    {
        [JsonProperty("examTypeId")]
        public int ExamTypeId { get; set; }

        [JsonProperty("questionCount")]
        public int QuestionCount { get; set; }

    }
}
