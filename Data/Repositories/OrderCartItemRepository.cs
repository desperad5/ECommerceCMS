using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;

namespace ECommerceCMS.Data.Repositories
{
    public class OrderCartItemRepository : EntityBaseRepository<OrderCartItem>, IOrderCartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderCartItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
