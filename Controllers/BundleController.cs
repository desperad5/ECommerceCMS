using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;

namespace SorubankCMS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class BundleController : Controller
    {
        private readonly IBundleService _bundleService;
        public BundleController(IBundleService bundleService)
        {
            _bundleService = bundleService;
        }

        [HttpPost("[action]")]
        public IActionResult CreateOrEditBundle([FromBody] BundleModel bundleModel)
        {
            var result = _bundleService.CreateOrEditBundle(bundleModel);
            return GetActionResult(result);
        }
        [HttpPost("[action]")]
        public IActionResult FetchAllBundles()
        {
            var tenantId = GetTenantIdFromContext();
            var result = _bundleService.FetchAllBundlesByTenantId(tenantId);
            return GetActionResultForList(result);
        }
        [HttpPost("[action]")]
        public IActionResult FetchAllBundlesByTenantId([FromBody]int tenantId)
        {
            var result = _bundleService.FetchAllBundlesByTenantId(tenantId);
            return GetActionResultForList(result);
        }

        [HttpPost("[action]")]
        public IActionResult DeleteBundleById([FromBody] int id)
        {
            var result = _bundleService.DeleteBundleById(id);
            return GetActionResult(result);
        }

        private IActionResult GetActionResult(ServiceResult<BundleModel> result)
        {
            if (result.resultType == ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

        private IActionResult GetActionResultForList(ServiceResult<List<BundleModel>> result)
        {
            if (result.resultType == ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

        private int GetTenantIdFromContext()
        {
            var user = HttpContext.User;
            var tenantId = user.Claims.FirstOrDefault(t => t.Type == "TenantId").Value;
            return Convert.ToInt32(tenantId);
        }
    }
}