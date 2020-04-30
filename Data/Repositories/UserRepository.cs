using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;

namespace SorubankCMS.Data.Repositories
{
    public class UserRepository : EntityBaseRepository<CMSUser>, IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
