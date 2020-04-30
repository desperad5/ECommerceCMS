using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Models.Response;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;

namespace SorubankCMS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IOrderCartService _orderCartService;

        public CartController(IOrderCartService orderCartService)
        {
            _orderCartService = orderCartService;
        }

        [HttpPost("[action]")]
        public IActionResult AddToBasket([FromBody] OrderCartItemModel orderCartItemModel)
        {
            int userId = GetUserIdFromContext();
            if (userId == 0)
            {
                return Forbid();
            }
            var result = _orderCartService.AddProductToCart(userId, orderCartItemModel);
            return ConvertToActionResult(result);
        }

        [HttpPost("[action]")]
        public IActionResult RemoveFromBasket([FromBody] OrderCartItemModel orderCartItemModel)
        {
            int userId = GetUserIdFromContext();
            if (userId == 0)
            {
                return Forbid();
            }
            var result = _orderCartService.RemoveProductFormCart(userId, orderCartItemModel.ProductId);
            return ConvertToActionResult(result);
        }

        [HttpPost("[action]")]
        public IActionResult GetBasket()
        {
            int userId = GetUserIdFromContext();
            if (userId == 0)
            {
                return Forbid();
            }
            var result = _orderCartService.GetOrderCartByUserId(userId);
            return ConvertToActionResult(result);
        }

        [HttpPost("[action]")]
        public IActionResult EmptyBasket()
        {
            int userId = GetUserIdFromContext();
            if (userId == 0)
            {
                return Forbid();
            }
            var result = _orderCartService.EmptyBasket(userId);
            return ConvertToActionResult(result);
        }

        private IActionResult ConvertToActionResult(ServiceResult<OrderCartResponseModel> result)
        {
            if (result.resultType == ServiceResultType.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        private int GetUserIdFromContext()
        {
            var user = HttpContext.User;
            var userId = user.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userId != null)
            {
                return Convert.ToInt32(userId.Value);
            }
            return 0;
        }
    }
}