using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Entity
{
    public class Tenant : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string County { get; set; }
        public string Town { get; set; }
        public int TypeId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string TaxAdministration { get; set; }
        public string TaxNumber { get; set; }
        public ICollection<CMSUser> Users { get; set; }

    }
}
