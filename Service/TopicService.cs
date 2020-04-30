using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;

namespace SorubankCMS.Service
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(TopicService));
        public TopicService(ITopicRepository topicRepository, IMapper mapper, ILessonService lessonService)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
            _lessonService = lessonService;
        }

        public ServiceResult<TopicViewModel> CreateOrEdit(TopicViewModel model)
        {
            ServiceResult<TopicViewModel> result = new ServiceResult<TopicViewModel>();
            try
            {
                Topic parentTopic = null;
                if (model.ParentTopicId != 0)
                {
                    parentTopic = _topicRepository.FindBy(t => t.Id == model.ParentTopicId && !t.IsDeleted).FirstOrDefault();
                    if (parentTopic == null)
                    {
                        result.message = "NO_TOPIC_FOUND";
                        result.resultType = ServiceResultType.Fail;
                        return result;
                    }
                }
                result.resultType = ServiceResultType.Success;
                Lesson lesson = _lessonService.GetLessonById(model.LessonId);
                if (lesson == null)
                {
                    result.message = "NO_LESSON_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                if (model.Id > 0)
                {
                    result.data = this.EditTopic(model, lesson, parentTopic);
                    return result;
                }
                result.data = this.AddTopic(model, lesson, parentTopic);
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEdit: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }

            return result;
        }

        private TopicViewModel EditTopic(TopicViewModel model, Lesson lesson, Topic parentTopic)
        {
            var topic = new Topic
            {
                Id = model.Id,
                Lesson = lesson,
                ParentTopic = parentTopic,
                ClassLevel = model.ClassLevel,
                Name = model.Name
            };
            _topicRepository.Update(topic);
            _topicRepository.Commit();
            return _mapper.Map<TopicViewModel>(topic);
        }

        private TopicViewModel AddTopic(TopicViewModel model, Lesson lesson, Topic parentTopic)
        {
            var topic = new Topic
            {
                Lesson = lesson,
                ParentTopic = parentTopic,
                ClassLevel = model.ClassLevel,
                Name = model.Name
            };
            topic = _topicRepository.AddWithCommit(topic);
            return _mapper.Map<TopicViewModel>(topic);
        }

        public ServiceResult<TopicViewModel> DeleteTopicById(int id)
        {
            ServiceResult<TopicViewModel> result = new ServiceResult<TopicViewModel>();
            try
            {
                var topic = _topicRepository.FindBy(t => t.Id == id).FirstOrDefault();
                if (topic == null)
                {
                    result.message = "NO_TOPIC_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                topic.IsDeleted = true;
                _topicRepository.Update(topic);
                result.data = _mapper.Map<TopicViewModel>(topic);
                result.resultType = ServiceResultType.Success;
                _topicRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteTopicById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;
        }

        public ServiceResult<List<TopicViewModel>> FetchAllActiveTopics()
        {
            ServiceResult<List<TopicViewModel>> result = new ServiceResult<List<TopicViewModel>>();
            try
            {
                var topics = _topicRepository.AllIncluding(x => x.Lesson).Where(x => !x.IsDeleted).ToList();
                result.data = _mapper.Map<List<TopicViewModel>>(topics);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllActiveTopics: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }
    }
}
