using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;

namespace ECommerceCMS.Data.Abstract
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        IQueryable<Product> GetProductsQueryable();
        ProductsByListingModel GetProductsByListingId(int listingId);
        ProductsByCategoryModel GetProductsByCategoryId(int categoryId, int itemCount, int pageNumber);
    }

}