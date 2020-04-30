using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;

namespace SorubankCMS.Data.Repositories
{
    public class BundleQuestionCardRepository : EntityBaseRepository<BundleQuestionCard>, IBundleQuestionCardRepository
    {
        private readonly ApplicationDbContext _context;

        public BundleQuestionCardRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
