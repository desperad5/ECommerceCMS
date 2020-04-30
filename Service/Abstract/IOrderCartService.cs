using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Models.Response;
using SorubankCMS.Services;

namespace SorubankCMS.Service.Abstract
{
    public interface IOrderCartService
    {
        public ServiceResult<OrderCartResponseModel> GetOrderCartByUserId(int userId);
        public ServiceResult<OrderCartResponseModel> AddProductToCart(int userId, OrderCartItemModel orderCartItemModel);
        public ServiceResult<OrderCartResponseModel> RemoveProductFormCart(int userId, int productId);
        public ServiceResult<OrderCartResponseModel> EmptyBasket(int userId);
    }
}