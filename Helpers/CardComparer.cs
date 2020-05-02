using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Models;

namespace ECommerceCMS.Helpers
{
    public class CardComparer : IEqualityComparer<BaseCardModel>
    {


        public bool Equals(BaseCardModel x, BaseCardModel y)
        {
            return x.Id == y.Id;
        }
        public int GetHashCode(BaseCardModel codeh)
        {
            return 0;
        }
    }
}

