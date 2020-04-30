using SorubankCMS.Models;
using SorubankCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Service.Abstract
{
    public interface ICardService
    {
        ServiceResult<List<QuestionCardModel>> FetchQuestionCardsByTenantId(int tenantId);
        ServiceResult<List<ExamCardModel>> FetchExamCardsByTenantId(int tenantId);
        ServiceResult<List<TopicCardModel>> FetchTopicCardsByTenantId(int tenantId);
        ServiceResult<QuestionCardModel> CreateOrEditQuestionCardAndRelatedProduct(QuestionCardModel model);
        ServiceResult<QuestionCardModel> DeleteQuestionCardById(int id);
        ServiceResult<ExamCardModel> CreateOrEditExamCardAndRelatedProduct(ExamCardModel model);
        ServiceResult<ExamCardModel> DeleteExamCardById(int id);
        ServiceResult<TopicCardModel> CreateOrEditTopicCardAndRelatedProduct(TopicCardModel model);
        ServiceResult<TopicCardModel> DeleteTopicCardById(int id);

    }
}
