using SorubankCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;
using SorubankCMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace SorubankCMS.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("EnableCors")]
    [Authorize]
    public class LessonController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost("[action]")]
        public IActionResult FetchAllLessons()
        {
            var result = _lessonService.FetchAllLessons();

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEdit([FromBody] LessonViewModel lesson)
        {
            var result = _lessonService.CreateOrEdit(lesson);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteLessonById([FromBody] int id)
        {
            var result = _lessonService.DeleteLessonById(id);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }


    }
}
