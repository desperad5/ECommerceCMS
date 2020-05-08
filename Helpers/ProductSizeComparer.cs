using ECommerceCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Helpers
{
    public class ProductSizeComparer: IEqualityComparer<Product>
    {
        
            public bool Equals(Product p1, Product p2)
            {
            return true;
            }

            public int GetHashCode(Product p1)
            {
                return 0;
            }
        
    }
}
