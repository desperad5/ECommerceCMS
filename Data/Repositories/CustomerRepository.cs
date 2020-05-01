using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Repositories
{
    public class CustomerRepository : EntityBaseRepository<Customer>, ICustomerRepository
    {
        private ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

    }

}
