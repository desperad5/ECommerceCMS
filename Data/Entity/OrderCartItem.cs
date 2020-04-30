using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class OrderCartItem : BaseEntity
    {
        [Required]
        public int OrderCartId { get; set; }
        public virtual OrderCart OrderCart { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}