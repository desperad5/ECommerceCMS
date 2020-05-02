using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Repositories
{
    public class BundleRepository : EntityBaseRepository<Bundle>, IBundleRepository
    {
        private readonly ApplicationDbContext _context;

        public BundleRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
        public IEnumerable<Bundle> GetAllActiveBundles()
        {
            return _context.Bundles.Where(t => !t.IsDeleted).OrderBy(d => d.CreatedDate);
        }
    }
}