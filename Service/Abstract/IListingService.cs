using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service.Abstract
{
    public interface IListingService
    {
        ServiceResult<List<ListingViewModel>> FetchAllListings();
        ServiceResult<ListingViewModel> CreateOrEdit(ListingViewModel model);
        ServiceResult<ListingViewModel> DeleteListingById(int id);
        Listing GetListingById(int id);

    }
}
