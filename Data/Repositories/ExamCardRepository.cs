using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Repositories
{
    public class ExamCardRepository: EntityBaseRepository<ExamCard>, IExamCardRepository
    {
        private ApplicationDbContext _context;

        public ExamCardRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<ExamCard> GetAllActiveExamCards()
        {
            return _context.ExamCards.Where(t => t.IsDeleted == false).OrderBy(d => d.CreatedDate);
        }


    }
}
