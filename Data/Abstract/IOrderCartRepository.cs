using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Entity;
using SorubankCMS.Helpers;

namespace SorubankCMS.Data.Abstract
{
    public interface IOrderCartRepository : IEntityBaseRepository<OrderCart>
    {
        public OrderCart GetOrderCartByUserIdAndStatus(int userId, Enums.OrderCartStatusTypes status);
    }
}
