using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceCMS.Data.Repositories
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
