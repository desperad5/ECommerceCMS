using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Helpers;
using SorubankCMS.Models;
using SorubankCMS.Models.Response;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;

namespace SorubankCMS.Service
{
    public class OrderCartService : IOrderCartService
    {
        private readonly IOrderCartRepository _orderCartRepository;
        private readonly IOrderCartItemService _orderCartItemService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(OrderCartService));

        public OrderCartService(IOrderCartRepository orderCartRepository, IMapper mapper, IOrderCartItemService orderCartItemService, IProductService productService)
        {
            _orderCartRepository = orderCartRepository;
            _mapper = mapper;
            _orderCartItemService = orderCartItemService;
            _productService = productService;
        }

        public ServiceResult<OrderCartResponseModel> GetOrderCartByUserId(int userId)
        {
            ServiceResult<OrderCartResponseModel> result = new ServiceResult<OrderCartResponseModel>();
            try
            {
                OrderCart orderCart = GetOrderCart(userId);
                if (orderCart == null)
                {
                    result.message = "NO_ORDER_CART_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                result.data = _mapper.Map<OrderCartResponseModel>(orderCart);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@GetOrderCartByUserId: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }

        public ServiceResult<OrderCartResponseModel> AddProductToCart(int userId, OrderCartItemModel orderCartItemModel)
        {
            ServiceResult<OrderCartResponseModel> result = new ServiceResult<OrderCartResponseModel>();
            try
            {
                OrderCart orderCart = GetOrCreateOrderCart(userId);
                if (orderCart == null)
                {
                    result.message = "PRODUCT_CANNOT_ADDED_TO_ORDER_CART";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                ServiceResult<Product> productResult = _productService.GetActiveProductById(orderCartItemModel.ProductId);
                if (productResult.resultType == ServiceResultType.Fail)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = productResult.message;
                    return result;
                }
                Product product = productResult.data;
                ServiceResult<List<OrderCartItem>> orderCartItemResult = _orderCartItemService.AddProductToOrderCartItems(orderCart, product, orderCartItemModel.Quantity);
                if (orderCartItemResult.resultType == ServiceResultType.Fail)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = orderCartItemResult.message;
                    return result;
                }
                orderCart = UpdateTotalPrice(orderCart);
                result.data = _mapper.Map<OrderCartResponseModel>(orderCart);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@AddProductToCart: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }

        public ServiceResult<OrderCartResponseModel> RemoveProductFormCart(int userId, int productId)
        {
            ServiceResult<OrderCartResponseModel> result = new ServiceResult<OrderCartResponseModel>();
            try
            {
                OrderCart orderCart = GetOrderCart(userId);
                if (orderCart == null)
                {
                    result.message = "ORDER_CART_NOT_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                ServiceResult<List<OrderCartItem>> orderCartItemResult = _orderCartItemService.RemoveProductFromOrderCartItems(orderCart, productId);
                if (orderCartItemResult.resultType == ServiceResultType.Fail)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = orderCartItemResult.message;
                    return result;
                }
                List<OrderCartItem> orderCartItems = orderCart.OrderCartItems.ToList();
                if (orderCartItems == null || orderCartItems.Count == 0)
                {
                    orderCart = AbandonOrderCart(orderCart);
                }
                else
                {
                    orderCart = UpdateTotalPrice(orderCart);
                }
                result.resultType = ServiceResultType.Success;
                result.data = _mapper.Map<OrderCartResponseModel>(orderCart);
                return result;
            }
            catch (Exception e)
            {
                logger.Error("Error@RemoveProductFormCart: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
                return result;
            }
        }

        public ServiceResult<OrderCartResponseModel> EmptyBasket(int userId)
        {
            ServiceResult<OrderCartResponseModel> result = new ServiceResult<OrderCartResponseModel>();
            try
            {
                OrderCart orderCart = GetOrderCart(userId);
                if (orderCart == null)
                {
                    result.message = "ORDER_CART_NOT_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                ServiceResult<List<OrderCartItem>> orderCartItemResult = _orderCartItemService.RemoveAllItems(orderCart);
                if (orderCartItemResult.resultType == ServiceResultType.Fail)
                {
                    result.message = orderCartItemResult.message;
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                AbandonOrderCart(orderCart);
                result.data = _mapper.Map<OrderCartResponseModel>(orderCart);
                result.resultType = ServiceResultType.Success;
                return result;
            }
            catch (Exception e)
            {
                logger.Error("Error@EmptyBasket: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
                return result;
            }
        }

        private OrderCart UpdateTotalPrice(OrderCart orderCart)
        {
            double totalPrice = orderCart.OrderCartItems.Sum(o => (o.Price * o.Quantity));
            orderCart.TotalPrice = totalPrice;
            _orderCartRepository.Update(orderCart);
            _orderCartRepository.Commit();
            return orderCart;
        }

        private OrderCart AbandonOrderCart(OrderCart orderCart)
        {
            orderCart.TotalPrice = 0;
            orderCart.Status = Enums.OrderCartStatusTypes.ABANDON;
            _orderCartRepository.Update(orderCart);
            _orderCartRepository.Commit();
            return orderCart;
        }

        private OrderCart GetOrderCart(int userId)
        {
            OrderCart orderCart = _orderCartRepository.FindBy(o => o.CustomerId == userId && o.Status == Enums.OrderCartStatusTypes.ACTIVE).FirstOrDefault();
            if(orderCart == null)
            {
                return orderCart;
            }
            orderCart.OrderCartItems = _orderCartItemService.GetOrderCartItems(orderCart);
            return orderCart;
        }

        private OrderCart GetOrCreateOrderCart(int userId)
        {
            try
            {
                OrderCart orderCart = GetOrderCart(userId);
                if (orderCart == null)
                {
                    orderCart = CreateNewOrderCartForUser(userId);
                }
                return orderCart;
            }
            catch (Exception e)
            {
                logger.Error("Error@GetOrCreateOrderCart: ", e);
                return null;
            }
        }

        private OrderCart CreateNewOrderCartForUser(int userId)
        {
            OrderCart orderCart = new OrderCart()
            {
                CustomerId = userId,
                Status = Enums.OrderCartStatusTypes.ACTIVE,
                IsActive = true,
                IsDeleted = false
            };
            return _orderCartRepository.AddWithCommit(orderCart);
        }
    }
}