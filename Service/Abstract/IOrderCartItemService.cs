using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Services;

namespace ECommerceCMS.Service.Abstract
{
    public interface IOrderCartItemService
    {
        public ServiceResult<List<OrderCartItem>> AddProductToOrderCartItems(OrderCart orderCart, Product product, int quantity);
        public ServiceResult<List<OrderCartItem>> RemoveProductFromOrderCartItems(OrderCart orderCart, int productId);
        public ServiceResult<List<OrderCartItem>> RemoveAllItems(OrderCart orderCart);

        public List<OrderCartItem> GetOrderCartItems(OrderCart orderCart);
    }
}
