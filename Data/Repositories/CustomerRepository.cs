using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Data.Entity.Customer;

namespace SorubankCMS.Data.Repositories
{
    public class CustomerRepository : EntityBaseRepository<SorubankUser>, ICustomerRepository
    {
        private ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
