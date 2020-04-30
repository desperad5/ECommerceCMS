using AutoMapper;
using log4net;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(TenantService));
        public TenantService(ITenantRepository tenantRepository, IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public ServiceResult<List<TenantViewModel>> FetchTenantsById(int tenantId)
        {
            ServiceResult<List<TenantViewModel>> result = new ServiceResult<List<TenantViewModel>>();
            try
            {
                List<Tenant> tenants;
                if (tenantId == 0)
                {
                    tenants = _tenantRepository.GetAllTenants().ToList();
                }
                else
                {
                    tenants = _tenantRepository.GetAll().Where(t => t.Id == tenantId && !t.IsDeleted).ToList();
                }

                result.data = _mapper.Map<List<TenantViewModel>>(tenants);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchTenantsById: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }
        public ServiceResult<TenantViewModel> CreateOrEdit(TenantViewModel model)
        {
            ServiceResult<TenantViewModel> result = new ServiceResult<TenantViewModel>();

            try
            {
                if (model.Id > 0)
                {
                    _tenantRepository.Update(_mapper.Map<Tenant>(model));
                    result.data = model;
                }
                else
                {
                    var tenant = _tenantRepository.AddWithCommit(_mapper.Map<Tenant>(model));
                    result.data = _mapper.Map<TenantViewModel>(tenant);
                }
                result.resultType = ServiceResultType.Success;
                _tenantRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEdit: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }

            return result;

        }

        public ServiceResult<TenantViewModel> DeleteTenantById(int id)
        {
            ServiceResult<TenantViewModel> result = new ServiceResult<TenantViewModel>();

            try
            {
                var tenant = _tenantRepository.FindBy(a => a.Id == id).FirstOrDefault();
                tenant.IsDeleted = true;
                _tenantRepository.Update(tenant);
                result.data = _mapper.Map<TenantViewModel>(tenant);
                result.resultType = ServiceResultType.Success;
                _tenantRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteTenantById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;

        }

    }
}
