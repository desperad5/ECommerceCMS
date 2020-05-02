using AutoMapper;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IListingService _listingService;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(ProductCategoryService));
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IMapper mapper, IListingService listingService)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
            _listingService = listingService;
        }
        public Services.ServiceResult<ProductCategoryViewModel> CreateOrEdit(ProductCategoryViewModel model)
        {
            ServiceResult<ProductCategoryViewModel> result = new ServiceResult<ProductCategoryViewModel>();
            try
            {
                ProductCategory parentCategory = null;
                if (model.ParentCategoryId != 0)
                {
                    parentCategory = _productCategoryRepository.GetSingle(t => t.Id == model.ParentCategoryId && !t.IsDeleted);
                    if (parentCategory == null)
                    {
                        result.message = "NO_TOPIC_FOUND";
                        result.resultType = ServiceResultType.Fail;
                        return result;
                    }
                }
                result.resultType = ServiceResultType.Success;
                Listing listing = _listingService.GetListingById(model.ListingId);
                if (listing == null)
                {
                    result.message = "NO_LISTING_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                if (model.Id > 0)
                {
                    result.data = this.EditProductCategory(model, listing, parentCategory);
                    return result;
                }
                result.data = this.AddProductCategory(model, listing, parentCategory);
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEdit: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }

            return result;
        }
        private ProductCategoryViewModel EditProductCategory(ProductCategoryViewModel model, Listing listing, ProductCategory parentCategory)
        {
            var category = new ProductCategory
            {
                Id = model.Id,
                Listing = listing,
                ParentCategory = parentCategory,
                CategoryName = model.CategoryName,
                TenantId=model.TenantId
            };
            _productCategoryRepository.Update(category);
            _productCategoryRepository.Commit();
            var categoryModel=_mapper.Map<ProductCategoryViewModel>(category);
            categoryModel.ParentCategoryName = parentCategory.CategoryName;
            return categoryModel;
        }

        private ProductCategoryViewModel AddProductCategory(ProductCategoryViewModel model, Listing listing, ProductCategory parentCategory)
        {
            var category = new ProductCategory
            {
                ListingId = listing.Id,
                CategoryName = model.CategoryName,
                TenantId=model.TenantId
            };
            if (parentCategory != null)
                category.ParentCategoryId = parentCategory.Id;
            category = _productCategoryRepository.AddWithCommit(category);
            var categoryReturnModel = _mapper.Map<ProductCategoryViewModel>(category);
            categoryReturnModel.ParentCategoryName = parentCategory.CategoryName;
            return categoryReturnModel;
        }

        public Services.ServiceResult<ProductCategoryViewModel> DeleteProductCategoryById(int id)
        {
            ServiceResult<ProductCategoryViewModel> result = new ServiceResult<ProductCategoryViewModel>();
            try
            {
                var category = _productCategoryRepository.GetSingle(t => t.Id == id);
                if (category == null)
                {
                    result.message = "NO_CATEGORY_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                category.IsDeleted = true;
                _productCategoryRepository.Update(category);
                result.data = _mapper.Map<ProductCategoryViewModel>(category);
                result.resultType = ServiceResultType.Success;
                _productCategoryRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteProductCategoryById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;
        }

        public Services.ServiceResult<List<ProductCategoryViewModel>> FetchAllActiveProductCategories()
        {
            ServiceResult<List<ProductCategoryViewModel>> result = new ServiceResult<List<ProductCategoryViewModel>>();
            try
            {
                var productCategories = _productCategoryRepository.AllIncluding(x => x.Listing,x=>x.ParentCategory,x=>x.Tenant).Where(x => !x.IsDeleted).ToList();
                result.data = productCategories.Select(i=>new ProductCategoryViewModel()
                { CategoryName=i.CategoryName,
                  Id=i.Id,
                  ListingId=i.Listing!=null?i.Listing.Id:0,
                  ParentCategoryId=i.ParentCategory!=null?i.ParentCategory.Id  :0,
                  ParentCategoryName=i.ParentCategory!=null?i.ParentCategory.CategoryName:string.Empty,
                  ListingName=i.Listing!=null?i.Listing.Name:string.Empty,
                  TenantId=i.Tenant!=null?i.Tenant.Id:0,
                  TenantName=i.Tenant!=null?i.Tenant.Name:string.Empty
                }).ToList();
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllActiveProductCategories: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }
    }
}
