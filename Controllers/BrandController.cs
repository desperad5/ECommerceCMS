using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace ECommerceCMS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService _brandService)
        {
            brandService = _brandService;
        }

        [HttpPost("[action]")]
        public IActionResult FetchAllActiveBrands()
        {
            var result = brandService.FetchAllActiveBrands();
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteBrandById([FromBody] int id)
        {
            var result = brandService.DeleteBrandById(id);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEdit([FromBody] BrandViewModel model)
        {
            var result = brandService.CreateOrEdit(model);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

    }
}