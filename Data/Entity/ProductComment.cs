using SorubankCMS.Data.Entity.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class ProductComment:BaseEntity
    {
        [Required]
        public string  Comment { get; set; }
        [Required]
        public int SoruBankUserId { get; set; }
        public virtual SorubankUser SorubankUser { get; set; }
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
