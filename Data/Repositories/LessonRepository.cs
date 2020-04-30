using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace SorubankCMS.Data.Repositories
{
    public class LessonRepository : EntityBaseRepository<Lesson>, ILessonRepository
    {
        private ApplicationDbContext _context;

        public LessonRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Lesson> GetAllLessons()
        {
            return _context.Lessons.Where(t=>t.IsDeleted==false).OrderBy(d => d.CreatedDate);
        }


    }
}
