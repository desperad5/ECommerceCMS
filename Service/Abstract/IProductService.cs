using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Services;

namespace ECommerceCMS.Service.Abstract
{
    public interface IProductService
    {
        //ServiceResult<List<ProductResponseModel>> SearchProducts(ProductSearchModel productSearchModel);
        //ServiceResult<ProductResponseModel> GetProductDetail(int productId);
        ServiceResult<Product> GetActiveProductById(int id);
        ServiceResult<List<ProductCommentModel>> GetProductComments(int productId);
        ServiceResult<ProductCommentModel> InsertProductComments(int productId, string Comment, int userId);
        ServiceResult<ProductsByListingModel> GetProductsByListingId(int listingId);
        ServiceResult<ProductsByCategoryModel> GetProductsByCategoryId(int categoryId, int? itemCount, int? pageNumber,bool orderByDate);
        ServiceResult<ProductsByCategoryModel> GetNewProductsByCategoryId(int categoryId, int? itemCount, int? pageNumber);

    }
}
