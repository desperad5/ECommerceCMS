using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;

namespace ECommerceCMS.Data.Repositories
{
    public class OrderCartRepository : EntityBaseRepository<OrderCart>, IOrderCartRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public OrderCart GetOrderCartByUserIdAndStatus(int userId, Enums.OrderCartStatusTypes status)
        {
            //return _context.Set<OrderCart>().Include(o => o.OrderCartItems).ThenInclude(i => i.Product).FirstOrDefault();
            return _context.OrderCarts.Include(o => o.OrderCartItems).Include("OrderCartItem.Product").FirstOrDefault(o => o.CustomerId == userId && o.Status == status);
        }
    }
}