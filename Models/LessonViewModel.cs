using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using SorubankCMS.Data.Entity;

namespace SorubankCMS.Models
{
    public class LessonViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("educationLevel")]
        public int EducationLevel { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set;}

        [JsonProperty("topics")]
        public ICollection<TopicViewModel> topics { get; set; }


    }
}
