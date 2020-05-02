using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service.Abstract
{
    public interface ITenantService
    {
        ServiceResult<List<TenantViewModel>> FetchTenantsById(int tenantId);
        ServiceResult<TenantViewModel> CreateOrEdit(TenantViewModel model);
        ServiceResult<TenantViewModel> DeleteTenantById(int id);
    }
}
