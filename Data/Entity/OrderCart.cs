using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Helpers;

namespace SorubankCMS.Data.Entity
{
    public class OrderCart : BaseEntity
    {
        [Required]
        public int CustomerId { get; set; } //TODO Sorubank'tan taşınacak.
        public virtual Customer Customer { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public string CargoTraceNumber { get; set; } //TODO ilerde kullanılabilir.
        public Enums.OrderCartStatusTypes Status { get; set; } //TODO enum kullanılmalı ACTIVE, ABANDON, EXPIRED, COMPLETED, PAYMENT_FAILED
        public int? PaymentType { get; set; } //TODO enum kullanılmalı VPOS, MOBILE PAYMENT
        public string TransactionRefId { get; set; }
        public virtual ICollection<OrderCartItem> OrderCartItems { get; set; }


    }
}
