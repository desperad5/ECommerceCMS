using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Repositories
{
    public class BrandRepository : EntityBaseRepository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        public BrandRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
