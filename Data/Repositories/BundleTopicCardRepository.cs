using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;

namespace SorubankCMS.Data.Repositories
{
    public class BundleTopicCardRepository : EntityBaseRepository<BundleTopicCard>, IBundleTopicCardRepository
    {
        private readonly ApplicationDbContext _context;
        public BundleTopicCardRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
