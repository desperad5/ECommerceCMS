using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;

namespace SorubankCMS.Data.Repositories
{
    public class TopicRepository : EntityBaseRepository<Topic>, ITopicRepository
    {
        private readonly ApplicationDbContext _context;

        public TopicRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Topic> GetAllActiveTopics()
        {
            IQueryable<Topic> query = _context.Set<Topic>().Where(x => !x.IsDeleted);
            query = query.Include(x => x.Lesson);
            return query.ToList();
        }
    }
}
