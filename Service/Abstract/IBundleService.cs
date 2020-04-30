using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Models;
using SorubankCMS.Services;

namespace SorubankCMS.Service.Abstract
{
    public interface IBundleService
    {
        ServiceResult<BundleModel> CreateOrEditBundle(BundleModel bundleModel);
        ServiceResult<List<BundleModel>> FetchAllBundles();
        ServiceResult<BundleModel> DeleteBundleById(int id);
        ServiceResult<List<BundleModel>> FetchAllBundlesByTenantId(int tenantId);
    }
}
