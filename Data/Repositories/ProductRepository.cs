using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;
using ECommerceCMS.Models;

namespace ECommerceCMS.Data.Repositories
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public List<ProductViewModel> GetProductsByListingId(int listingId)
        {
            var returnModel = new List<ProductViewModel>();
            var queryable = _context.Set<ProductListing>().Include(c => c.Product);
            queryable.Load();
            var productlistings = queryable.Where(t => t.ListingId == listingId).Select(c=>c.Product).ToList();
            int index = 0;
            foreach (var product in productlistings)
            {
               

                returnModel.Add(new ProductViewModel()
                {
                    BaseImageUrl = product.BaseImageUrl,
                    Id = product.Id,
                    isSale = index % 2 == 0,
                    Name = product.Name,
                    Price = product.Price,
                    SalePrice = index % 2 == 0 ? product.Price * 0.85 : product.Price,
                    isNew = index % 3 == 0
                }
                );
                index++;
            }
            return returnModel;


        }
        public IQueryable<Product> GetProductsQueryable()
        {
            IQueryable<Product> query = _context.Set<Product>().AsQueryable();
            return query;

        }
    }
}