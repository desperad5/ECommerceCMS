using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;

namespace SorubankCMS.Data.Abstract
{
    public interface IProductRepository : IEntityBaseRepository<Product>
    {
        IQueryable<Product> GetProductsQueryable();
    }

}