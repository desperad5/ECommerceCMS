using ECommerceCMS.Models;
using ECommerceCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service.Abstract
{
    public interface IBrandService
    {
        ServiceResult<List<BrandViewModel>> FetchAllActiveBrands();

        ServiceResult<BrandViewModel> DeleteBrandById(int id);
        ServiceResult<BrandViewModel> CreateOrEdit(BrandViewModel model);
    }
}
