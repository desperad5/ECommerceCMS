using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Repositories
{
    public class ProductCommentRepository:EntityBaseRepository<ProductComment>,IProductCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
