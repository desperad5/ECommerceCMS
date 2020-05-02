using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Models.Response;
using ECommerceCMS.Services;

namespace ECommerceCMS.Service.Abstract
{
    public interface IOrderCartService
    {
        public ServiceResult<OrderCartResponseModel> GetOrderCartByUserId(int userId);
        public ServiceResult<OrderCartResponseModel> AddProductToCart(int userId, OrderCartItemModel orderCartItemModel);
        public ServiceResult<OrderCartResponseModel> RemoveProductFormCart(int userId, int productId);
        public ServiceResult<OrderCartResponseModel> EmptyBasket(int userId);
    }
}