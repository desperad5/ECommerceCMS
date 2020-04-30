using Newtonsoft.Json;
using System;

namespace SorubankCMS.Models
{
    public class TenantViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("typeId")]
        public int TypeId { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("taxAdministration")]
        public string TaxAdministration { get; set; }

        [JsonProperty("taxNumber")]
        public string TaxNumber { get; set; }
    }
}
