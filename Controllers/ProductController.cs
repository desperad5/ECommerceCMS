using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;

namespace ECommerceCMS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private static readonly ILog logger = Logger.GetLogger(typeof(ProductController));

        public ProductController(IProductService productService)
        {
            _productService = productService;
            //logger = Logger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        [HttpGet("[action]")]
        public IActionResult LogException()
        {
            logger.Info("Initialize edilen logger'la atılan log");
            throw new Exception("Hoaydaaa yine bir exception");
        }

        //[HttpPost("[action]")]
        //public IActionResult Search([FromBody] ProductSearchModel productSearchModel)
        //{
        //    var result = _productService.SearchProducts(productSearchModel);
        //    if (result.resultType == ServiceResultType.Success)
        //    {
        //        return Ok(result.data);
        //    }
        //    return BadRequest(result.message);
        //}
        //[HttpPost("[action]")]
        //public IActionResult Detail([FromBody] ProductDetailModel productDetailModel)
        //{
        //    var result = _productService.GetProductDetail(productDetailModel.ProductId);
        //    if (result.resultType == ServiceResultType.Success)
        //    {
        //        return Ok(result.data);
        //    }
        //    return BadRequest(result.message);
        //}
        [HttpPost("[action]")]
        public IActionResult GetComments([FromBody] ProductDetailModel productDetailModel)
        {
            var result = _productService.GetProductComments(productDetailModel.ProductId);
            if (result.resultType == ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }
        [HttpPost("[action]")]
        public IActionResult InsertComments([FromBody] ProductCommentModel productcommentModel)
        {
            var result = _productService.InsertProductComments(productcommentModel.ProductId,productcommentModel.Comment, GetUserIdFromContext());
            if (result.resultType == ServiceResultType.Success)
            {
                var listResult = _productService.GetProductComments(productcommentModel.ProductId);
                return Ok(listResult.data);
            }
            return BadRequest(result.message);
        }
        [HttpPost("[action]")]
        public IActionResult GetProductsByListingId([FromBody] ProductCommentModel productcommentModel)
        {
            var result = _productService.GetProductsByListingId(productcommentModel.Id);
            if (result.resultType == ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }
        [HttpPost("[action]")]
        public IActionResult GetProductsByCategoryId([FromBody] ProductsByCategoryRetrieveModel productsByCategoryRetrieveModel)
        {
            var result = _productService.GetProductsByCategoryId(productsByCategoryRetrieveModel.Id, productsByCategoryRetrieveModel.ItemCount,productsByCategoryRetrieveModel.PageNumber);
            if (result.resultType == ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
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