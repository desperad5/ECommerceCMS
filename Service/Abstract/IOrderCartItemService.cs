using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Entity;
using SorubankCMS.Services;

namespace SorubankCMS.Service.Abstract
{
    public interface IOrderCartItemService
    {
        public ServiceResult<List<OrderCartItem>> AddProductToOrderCartItems(OrderCart orderCart, Product product, int quantity);
        public ServiceResult<List<OrderCartItem>> RemoveProductFromOrderCartItems(OrderCart orderCart, int productId);
        public ServiceResult<List<OrderCartItem>> RemoveAllItems(OrderCart orderCart);

        public List<OrderCartItem> GetOrderCartItems(OrderCart orderCart);
    }
}
