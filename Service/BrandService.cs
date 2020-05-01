using AutoMapper;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using log4net;
using SorubankCMS;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(UserService));

        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public ServiceResult<BrandViewModel> CreateOrEdit(BrandViewModel model)
        {
            ServiceResult<BrandViewModel> result = new ServiceResult<BrandViewModel>();

            try
            {
                if (model.Id > 0)
                {
                    var brand = _brandRepository.GetSingle(a => a.Id == model.Id);
                    brand.Name = model.Name;
                    brand.WebSiteUrl = model.WebSiteUrl;
                    _brandRepository.Update(brand);
                    result.data = _mapper.Map<BrandViewModel>(brand);
                    _brandRepository.Commit();
                }
                else
                {
                    var brand = _mapper.Map<Brand>(model);
                    var createdBrand = _brandRepository.AddWithCommit(brand);
                    result.data = _mapper.Map<BrandViewModel>(createdBrand);
                }
                result.resultType = ServiceResultType.Success;
                
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEdit: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }

            return result;
        }

        public ServiceResult<BrandViewModel> DeleteBrandById(int id)
        {
            ServiceResult<BrandViewModel> result = new ServiceResult<BrandViewModel>();
            _brandRepository.DeleteWhere(b => b.Id == id);
            _brandRepository.Commit();
            result.resultType = ServiceResultType.Success;

            return result;
        }

        public ServiceResult<List<BrandViewModel>> FetchAllActiveBrands()
        {
            ServiceResult<List<BrandViewModel>> result = new ServiceResult<List<BrandViewModel>>();
            try
            {
                var brands = _brandRepository.AllIncluding()
                    .Where(a => !a.IsDeleted && a.IsActive)
                    .ToList();

                result.data = _mapper.Map<List<BrandViewModel>>(brands);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllUsers: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }
    }
}
