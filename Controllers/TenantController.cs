using SorubankCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;
using SorubankCMS.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System;

namespace SorubankCMS.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class TenantController : Controller
    {
        private readonly ITenantService _tenantService;

        public TenantController(ApplicationDbContext db, ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost("[action]")]
        public IActionResult FetchAllTenants()
        {
            var tenantId = GetTenantIdFromContext();
            var result = _tenantService.FetchTenantsById(tenantId);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEdit([FromBody] TenantViewModel tenant)
        {
            var result = _tenantService.CreateOrEdit(tenant);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteTenantById([FromBody] int id)
        {
            var result = _tenantService.DeleteTenantById(id);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        private int GetTenantIdFromContext()
        {
            var user = HttpContext.User;
            var tenantId = user.Claims.FirstOrDefault(t => t.Type == "TenantId");
            if (tenantId != null)
                return Convert.ToInt32(tenantId.Value) ;
            return 0;
        }
    }
}
