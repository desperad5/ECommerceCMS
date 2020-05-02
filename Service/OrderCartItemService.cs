using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;
using ECommerceCMS;

namespace ECommerceCMS.Service
{
    public class OrderCartItemService : IOrderCartItemService
    {
        private readonly IOrderCartItemRepository _orderCartItemRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private static readonly ILog logger = Logger.GetLogger(typeof(OrderCartItemService));

        public OrderCartItemService(IOrderCartItemRepository orderCartItemRepository, IMapper mapper, IProductService productService)
        {
            _orderCartItemRepository = orderCartItemRepository;
            _mapper = mapper;
            _productService = productService;
        }

        public ServiceResult<List<OrderCartItem>> AddProductToOrderCartItems(OrderCart orderCart, Product product, int quantity)
        {
            ServiceResult<List<OrderCartItem>> result = new ServiceResult<List<OrderCartItem>>();
            try
            {
                int productId = product.Id;
                int orderCartId = orderCart.Id;
                OrderCartItem orderCartItem = GetOrderCartItemByOrderCartAndProduct(productId, orderCartId);
                if (orderCartItem != null)
                {
                    result.message = "PRODUCT_EXISTS_IN_THIS_CART";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                orderCartItem = CreateNewOrderCartItem(orderCart, product, quantity);
                if (orderCart.OrderCartItems == null)
                {
                    List<OrderCartItem> orderCartItems = new List<OrderCartItem>();
                    orderCart.OrderCartItems = orderCartItems;
                }
                //orderCart.OrderCartItems.Add(orderCartItem);
                result.resultType = ServiceResultType.Success;
                result.data = orderCart.OrderCartItems.ToList();
            }
            catch (Exception e)
            {
                logger.Error("Error@AddProductToOrderCartItems: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;
        }

        public ServiceResult<List<OrderCartItem>> RemoveProductFromOrderCartItems(OrderCart orderCart, int productId)
        {
            ServiceResult<List<OrderCartItem>> result = new ServiceResult<List<OrderCartItem>>();
            try
            {
                List<OrderCartItem> orderCartItems = orderCart.OrderCartItems.ToList();
                OrderCartItem orderCartItem = GetOrderCartItemByOrderCartAndProduct(productId, orderCart.Id);
                if (orderCartItem == null)
                {
                    result.message = "PRODUCT_NOT_FOUND_IN_CART";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                DeleteOrderCartItem(orderCartItem);
                orderCartItems.Remove(orderCartItem);
                result.resultType = ServiceResultType.Success;
                result.data = orderCartItems;
            }
            catch (Exception e)
            {
                logger.Error("Error@RemoveProductFromOrderCartItems: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }

        public ServiceResult<List<OrderCartItem>> RemoveAllItems(OrderCart orderCart)
        {
            ServiceResult<List<OrderCartItem>> result = new ServiceResult<List<OrderCartItem>>();
            try
            {
                List<OrderCartItem> orderCartItems = orderCart.OrderCartItems.ToList();
                if (orderCartItems == null || orderCartItems.Count == 0)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "NO_ITEM_IN_ORDER_CART";
                    return result;
                }
                DeleteAllOrderCartItems(orderCart);
                result.data = new List<OrderCartItem>();
                result.resultType = ServiceResultType.Success;
                return result;
            }
            catch (Exception e)
            {
                logger.Error("Error@RemoveAllItems: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
                return result;
            }
        }

        public List<OrderCartItem> GetOrderCartItems(OrderCart orderCart)
        {
            List<OrderCartItem> orderCartItems = _orderCartItemRepository.AllIncluding(o => o.Product).Where(o => o.OrderCartId == orderCart.Id).ToList();
            return orderCartItems;
        }

        private OrderCartItem GetOrderCartItemByOrderCartAndProduct(int productId, int orderCartId)
        {
            return _orderCartItemRepository.FindBy(o => o.OrderCartId == orderCartId && o.ProductId == productId).FirstOrDefault();
        }

        private void DeleteAllOrderCartItems(OrderCart orderCart)
        {
            _orderCartItemRepository.DeleteWhere(o => o.OrderCartId == orderCart.Id);
            _orderCartItemRepository.Commit();
        }

        private void DeleteOrderCartItem(OrderCartItem orderCartItem)
        {
            _orderCartItemRepository.DeleteWhere(o => o.Id == orderCartItem.Id);
            _orderCartItemRepository.Commit();
        }

        private void DecreaseQuantity(OrderCartItem orderCartItem)
        {
            orderCartItem.Quantity -= 1;
            _orderCartItemRepository.Update(orderCartItem);
            _orderCartItemRepository.Commit();
        }

        private List<OrderCartItem> GetOrderCartItemsByOrderCart(OrderCart orderCart)
        {
            return _orderCartItemRepository.FindBy(o => o.OrderCartId == orderCart.Id && !o.IsDeleted && o.IsActive).ToList();
        }

        private OrderCartItem CreateNewOrderCartItem(OrderCart orderCart, Product product, int quantity)
        {
            var orderCartItem = new OrderCartItem
            {
                OrderCart = orderCart,
                ProductId = product.Id,
                Price = product.Price,
                Quantity = quantity,
                IsActive = true,
                IsDeleted = false
            };

            return _orderCartItemRepository.AddWithCommit(orderCartItem);
        }

        private OrderCartItem UpdateOrderCartItem(OrderCartItem orderCartItem, Product product, int quantity)
        {
            orderCartItem.Quantity = quantity;
            orderCartItem.Price = product.Price;
            orderCartItem.IsActive = true;
            orderCartItem.IsDeleted = false;
            _orderCartItemRepository.Update(orderCartItem);
            _orderCartItemRepository.Commit();
            return orderCartItem;
        }
    }
}
