using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ECommerceCMS.Helpers;

namespace ECommerceCMS.Models.Response
{
    public class OrderCartResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("totalPrice")] 
        public double TotalPrice { get; set; }
        [JsonProperty("quantity")]
        public string CargoTraceNumber { get; set; }
        [JsonProperty("status")]
        public Enums.OrderCartStatusTypes Status { get; set; }
        [JsonProperty("paymentType")]
        public int? PaymentType { get; set; }
        [JsonProperty("transactionRefId")]
        public string TransactionRefId { get; set; }
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty("orderCartItems")]
        public ICollection<OrderCartItemResponseModel> OrderCartItems { get; set; }
    }
}