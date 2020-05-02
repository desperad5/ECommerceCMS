using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;

namespace ECommerceCMS.Data.Repositories
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
