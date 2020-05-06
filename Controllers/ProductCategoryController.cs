using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerceCMS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService productCategoryService;
        private readonly IListingService listingService;

        public ProductCategoryController(IProductCategoryService _productCategoryService,IListingService _listingService)
        {
            productCategoryService = _productCategoryService;
            listingService = _listingService;
        }
        [HttpPost("[action]")]
        public IActionResult GetProductCategoryTree()
        {
            var result =productCategoryService.GetProductCategoryTree(1);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }
        // GET: /<controller>/
        [HttpPost("[action]")]
        public IActionResult FetchAllActiveProductCategories()
        {
            var result = productCategoryService.FetchAllActiveProductCategories();
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteProductCategoryById([FromBody] int id)
        {
            var result = productCategoryService.DeleteProductCategoryById(id);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEdit([FromBody] ProductCategoryViewModel model)
        {
            var result = productCategoryService.CreateOrEdit(model);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }
        [HttpPost("[action]")]
        public ActionResult FetchAllActiveListings()
        {
            var result=listingService.FetchAllListings();
            if(result.resultType== Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }
    }
}
