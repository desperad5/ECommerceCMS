using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class CardController : Controller
    {
        private readonly ICardService _cardService;


        public CardController(ICardService cardService)
        {
            _cardService = cardService;

        }
        [HttpPost("[action]")]
        public IActionResult FetchAllQuestionCards()
        {
            var tenantId = GetTenantIdFromContext();
            var result = _cardService.FetchQuestionCardsByTenantId(tenantId);
            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }
        [HttpPost("[action]")]
        public IActionResult FetchQuestionCardsByTenantId([FromBody]int tenantId)
        {
            var result = _cardService.FetchQuestionCardsByTenantId(tenantId);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEditQuestionCard([FromBody] QuestionCardModel questionCard)
        {
            var result = _cardService.CreateOrEditQuestionCardAndRelatedProduct(questionCard);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteQuestionCardById([FromBody] int id)
        {
            var result = _cardService.DeleteQuestionCardById(id);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public IActionResult FetchAllExamCards()
        {
            var tenantId = GetTenantIdFromContext();
            var result = _cardService.FetchExamCardsByTenantId(tenantId);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }
        [HttpPost("[action]")]
        public IActionResult FetchExamCardsByTenantId([FromBody]int tenantId)
        {
            var result = _cardService.FetchExamCardsByTenantId(tenantId);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEditExamCard([FromBody] ExamCardModel examCard)
        {
            var result = _cardService.CreateOrEditExamCardAndRelatedProduct(examCard);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteExamCardById([FromBody] int id)
        {
            var result = _cardService.DeleteExamCardById(id);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public IActionResult FetchAllTopicCards()
        {
            var tenantId = GetTenantIdFromContext();
            var result = _cardService.FetchTopicCardsByTenantId(tenantId);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public IActionResult FetchTopicCardsByTenantId([FromBody]int tenantId)
        {
            var result = _cardService.FetchTopicCardsByTenantId(tenantId);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEditTopicCard([FromBody] TopicCardModel topicCard)
        {
            var result = _cardService.CreateOrEditTopicCardAndRelatedProduct(topicCard);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteTopicCardById([FromBody] int id)
        {
            var result = _cardService.DeleteTopicCardById(id);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

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
