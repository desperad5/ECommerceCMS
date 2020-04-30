using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Services
{
    public class ServiceResult<T>
    {

        public T data { get; set; }

        public ServiceResultType resultType { get; set; }

        public String message { get; set; }

    }

    public enum ServiceResultType
    {
        Fail = 0,
        Success = 1
    }
}
