using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Entity
{
    public class Question : BaseEntity
    {

        public bool IsActive { get; set; }
        public string Link { get; set; }
        public int DependentQuestionId { get; set; }
        public virtual Tenant Tenant { get; set; }

    }
}
