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
    public class ListingController : Controller
    {
        IListingService listingService;
        public ListingController(IListingService _listingService)
        {
            listingService = _listingService;
        }
        [HttpPost("[action]")]
        public ActionResult DeleteListingById([FromBody] int id)
        {
            var result = listingService.DeleteListingById(id);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEdit([FromBody] ListingViewModel model)
        {
            var result = listingService.CreateOrEdit(model);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }
    }
}
