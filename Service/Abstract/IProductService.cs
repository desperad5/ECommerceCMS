using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Services;

namespace SorubankCMS.Service.Abstract
{
    public interface IProductService
    {
        ServiceResult<List<ProductResponseModel>> SearchProducts(ProductSearchModel productSearchModel);
        ServiceResult<ProductResponseModel> GetProductDetail(int productId);
        ServiceResult<Product> GetActiveProductById(int id);
        ServiceResult<List<ProductCommentModel>> GetProductComments(int productId);
        ServiceResult<ProductCommentModel> InsertProductComments(int productId, string Comment, int userId);

    }
}
