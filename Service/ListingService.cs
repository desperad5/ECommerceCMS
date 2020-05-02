using AutoMapper;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service.Abstract
{
    public class ListingService:IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(ListingService));
        public ListingService(IListingRepository listingRepository, IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _listingRepository = listingRepository;
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        public ServiceResult<List<ListingViewModel>> FetchAllListings()
        {
            ServiceResult<List<ListingViewModel>> result = new ServiceResult<List<ListingViewModel>>();
            try
            {
                var listings = _listingRepository.AllIncluding(a => a.ProductCategories).Where(a => a.IsDeleted == false).ToList();

                result.data = listings.Select(i => new ListingViewModel()
                {
                    CreatedDate = i.CreatedDate,
                    Id = i.Id,
                    IsActive = i.IsActive,
                    Name = i.Name,
                    ProductCategories = i.ProductCategories.Select(k => new ProductCategoryViewModel() { Id = k.Id, CategoryName = k.CategoryName })
                }).ToList();
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllListings: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }
        public ServiceResult<ListingViewModel> CreateOrEdit(ListingViewModel model)
        {
            ServiceResult<ListingViewModel> result = new ServiceResult<ListingViewModel>();

            try
            {
                if (model.Id > 0)
                {
                    _listingRepository.Update(_mapper.Map<Listing>(model));
                    result.data = model;
                }
                else
                {
                    var listing = _listingRepository.AddWithCommit(_mapper.Map<Listing>(model));
                    result.data = _mapper.Map<ListingViewModel>(listing);
                }
                result.resultType = ServiceResultType.Success;
                _listingRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEdit: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }

            return result;

        }

        public ServiceResult<ListingViewModel> DeleteListingById(int id)
        {
            ServiceResult<ListingViewModel> result = new ServiceResult<ListingViewModel>();

            try
            {
                var listing = _listingRepository.FindBy(a => a.Id == id).FirstOrDefault();
                listing.IsDeleted = true;
                _listingRepository.Update(listing);
                _productCategoryRepository.FindBy(t => t.ListingId == id).ToList().ForEach(i => { i.IsDeleted = true; });
                result.data = _mapper.Map<ListingViewModel>(listing);
                result.resultType = ServiceResultType.Success;
                _listingRepository.Commit();
                _productCategoryRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteListingById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;

        }

        public Listing GetListingById(int id)
        {
            return _listingRepository.FindBy(l => l.Id == id && !l.IsDeleted).FirstOrDefault();
        }
    }
}
