using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Services;

namespace SorubankCMS.Service.Abstract
{
    public interface ITopicService
    {
        ServiceResult<List<TopicViewModel>> FetchAllActiveTopics();

        ServiceResult<TopicViewModel> DeleteTopicById(int id);
        ServiceResult<TopicViewModel> CreateOrEdit(TopicViewModel model);
    }
}
