using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Entity;

namespace ECommerceCMS.Data.Abstract
{
    public interface IOrderCartItemRepository : IEntityBaseRepository<OrderCartItem>
    {
    }
}