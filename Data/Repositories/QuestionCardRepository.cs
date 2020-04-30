using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Repositories
{
    public class QuestionCardRepository:EntityBaseRepository<QuestionCard>, IQuestionCardRepository
    {
        private ApplicationDbContext _context;

    public QuestionCardRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    public IEnumerable<QuestionCard> GetAllActiveQuestionCards()
    {
        return _context.QuestionCards.Where(t => t.IsDeleted == false).OrderBy(d => d.CreatedDate);
    }
}
}
