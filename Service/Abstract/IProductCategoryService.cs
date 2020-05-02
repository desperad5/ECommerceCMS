using ECommerceCMS.Models;
using ECommerceCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service.Abstract
{
    public interface IProductCategoryService
    {
        ServiceResult<List<ProductCategoryViewModel>> FetchAllActiveProductCategories();

        ServiceResult<ProductCategoryViewModel> DeleteProductCategoryById(int id);
        ServiceResult<ProductCategoryViewModel> CreateOrEdit(ProductCategoryViewModel model);
    }
}
