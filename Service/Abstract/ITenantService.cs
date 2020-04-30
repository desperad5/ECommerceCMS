using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Service.Abstract
{
    public interface ITenantService
    {
        ServiceResult<List<TenantViewModel>> FetchTenantsById(int tenantId);
        ServiceResult<TenantViewModel> CreateOrEdit(TenantViewModel model);
        ServiceResult<TenantViewModel> DeleteTenantById(int id);
    }
}
