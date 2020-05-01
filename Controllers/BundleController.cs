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

        

        private int GetTenantIdFromContext()
        {
            var user = HttpContext.User;
            var tenantId = user.Claims.FirstOrDefault(t => t.Type == "TenantId").Value;
            return Convert.ToInt32(tenantId);
        }
    }
}