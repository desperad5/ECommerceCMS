using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;

namespace SorubankCMS.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class TopicController : Controller
    {
        private readonly ITopicService topicService;

        public TopicController(ITopicService _topicService)
        {
            topicService = _topicService;
        }

        [HttpPost("[action]")]
        public IActionResult FetchAllActiveTopics()
        {
            var result = topicService.FetchAllActiveTopics();
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteTopicById([FromBody] int id)
        {
            var result = topicService.DeleteTopicById(id);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEdit([FromBody] TopicViewModel model)
        {
            var result = topicService.CreateOrEdit(model);
            if (result.resultType == Services.ServiceResultType.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.message);
        }

    }
}