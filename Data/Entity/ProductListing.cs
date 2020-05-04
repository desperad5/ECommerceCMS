using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class ProductListing:BaseEntity
    {
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
       
        public int? Order { get; set; }
    }
}
