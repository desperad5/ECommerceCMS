
using ECommerceCMS.Data;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Repositories
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
