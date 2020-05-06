
using ECommerceCMS.Data;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Data.Repositories;
using ECommerceCMS.Models;
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

        public List<BrandViewModel> GetBrandsOfProducts(ProductsWithCategoryModel products)
        {
            var productIds=products.Products.Select(i => i.Id);
            return this.AllIncluding(t => t.Products).Where(t => t.Products.Any(i => productIds.Contains(i.Id))).Select(i=>
            new BrandViewModel() { Id = i.Id, Name = i.Name }).ToList();
        }
    }
}
