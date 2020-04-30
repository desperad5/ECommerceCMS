using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Repositories
{
    public class SorubankUserRepository : EntityBaseRepository<SorubankUser>, ISorubankUserRepository
    {
        private ApplicationDbContext _context;

        public SorubankUserRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

    }

}
