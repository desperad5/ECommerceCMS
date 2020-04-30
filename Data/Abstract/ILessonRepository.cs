using SorubankCMS.Data.Entity;
using System.Collections.Generic;

namespace SorubankCMS.Data.Abstract
{
    public interface ILessonRepository : IEntityBaseRepository<Lesson>
    {
        IEnumerable<Lesson> GetAllLessons();
    }
}
