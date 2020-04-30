using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Repositories
{
    public class TopicCardRepository : EntityBaseRepository<TopicCard>, ITopicCardRepository
    {
        private ApplicationDbContext _context;

        public TopicCardRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<TopicCard> GetAllActiveTopicCards()
        {
            return _context.TopicCards.Where(t => t.IsDeleted == false).OrderBy(d => d.CreatedDate);
        }

    }
}

