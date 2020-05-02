using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;

namespace ECommerceCMS.Data.Abstract
{
    public interface IOrderCartRepository : IEntityBaseRepository<OrderCart>
    {
        public OrderCart GetOrderCartByUserIdAndStatus(int userId, Enums.OrderCartStatusTypes status);
    }
}
