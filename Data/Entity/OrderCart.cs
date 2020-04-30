using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Entity.Customer;
using SorubankCMS.Helpers;

namespace SorubankCMS.Data.Entity
{
    public class OrderCart : BaseEntity
    {
        public int UserId { get; set; } //TODO Sorubank'tan taşınacak.
        public virtual SorubankUser User { get; set; } //TODO sorubank entitysindeki user
        public double TotalPrice { get; set; }
        public string CargoTraceNumber { get; set; } //TODO ilerde kullanılabilir.
        public Enums.OrderCartStatusTypes Status { get; set; } //TODO enum kullanılmalı ACTIVE, ABANDON, EXPIRED, COMPLETED, PAYMENT_FAILED
        public int? PaymentType { get; set; } //TODO enum kullanılmalı VPOS, MOBILE PAYMENT
        public string TransactionRefId { get; set; }
        public virtual ICollection<OrderCartItem> OrderCartItems { get; set; }


    }
}
