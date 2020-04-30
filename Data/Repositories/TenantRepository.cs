using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SorubankCMS.Data.Repositories
{
    public class TenantRepository : EntityBaseRepository<Tenant>, ITenantRepository
    {
        private ApplicationDbContext _context;

        public TenantRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Tenant> GetAllTenants()
        {
            return _context.Tenants.Where(t=>t.IsDeleted==false).OrderBy(d => d.CreatedDate);
        }


    }
}
