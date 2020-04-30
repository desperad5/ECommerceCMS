using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Service.Abstract
{
    public interface ILessonService
    {
        ServiceResult<List<LessonViewModel>> FetchAllLessons();
        ServiceResult<LessonViewModel> CreateOrEdit(LessonViewModel model);
        ServiceResult<LessonViewModel> DeleteLessonById(int id);
        Lesson GetLessonById(int id);
    }
}
