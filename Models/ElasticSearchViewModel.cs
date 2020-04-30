using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Models
{
    public class ElasticSearchViewModel
    {
        public string Query { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
