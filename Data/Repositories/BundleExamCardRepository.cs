using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;

namespace SorubankCMS.Data.Repositories
{
    public class BundleExamCardRepository : EntityBaseRepository<BundleExamCard>, IBundleExamCardRepository
    {
        private readonly ApplicationDbContext _context;

        public BundleExamCardRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}