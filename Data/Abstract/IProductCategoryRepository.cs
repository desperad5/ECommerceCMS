using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ECommerceCMS.Data.Abstract
{
    public interface IProductCategoryRepository: IEntityBaseRepository<ProductCategory>
    {
        List<ProductCategoryTreeModel> GetProductCategoryTree();


    }
}
