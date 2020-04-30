using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Helpers;
using SorubankCMS.Models;

namespace SorubankCMS.Data.Repositories
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Product> GetProductsQueryable()
        {
            IQueryable<Product> query = _context.Set<Product>().AsQueryable();
            return query;

        }
    }
}