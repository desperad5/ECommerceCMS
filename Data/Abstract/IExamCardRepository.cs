using SorubankCMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Data.Abstract
{
    public interface IExamCardRepository : IEntityBaseRepository<ExamCard>
    {
        IEnumerable<ExamCard> GetAllActiveExamCards();
    }
}
