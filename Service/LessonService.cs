using AutoMapper;
using log4net;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorubankCMS.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(LessonService));
        public LessonService(ILessonRepository lessonRepository,ITopicRepository topicRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public ServiceResult<List<LessonViewModel>> FetchAllLessons()
        {
            ServiceResult<List<LessonViewModel>> result = new ServiceResult<List<LessonViewModel>>();
            try
            {
                var lessons = _lessonRepository.AllIncluding(a => a.Topics).Where(a => a.IsDeleted == false).ToList();

                result.data = _mapper.Map<List<LessonViewModel>>(lessons);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllLessons: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }
        public ServiceResult<LessonViewModel> CreateOrEdit(LessonViewModel model)
        {
            ServiceResult<LessonViewModel> result = new ServiceResult<LessonViewModel>();

            try
            {
                if (model.Id > 0)
                {
                    _lessonRepository.Update(_mapper.Map<Lesson>(model));
                    result.data = model;
                }
                else
                {
                    var lesson = _lessonRepository.AddWithCommit(_mapper.Map<Lesson>(model));
                    result.data = _mapper.Map<LessonViewModel>(lesson);
                }
                result.resultType = ServiceResultType.Success;
                _lessonRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEdit: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }

            return result;

        }

        public ServiceResult<LessonViewModel> DeleteLessonById(int id)
        {
            ServiceResult<LessonViewModel> result = new ServiceResult<LessonViewModel>();

            try
            {
                var lesson = _lessonRepository.FindBy(a => a.Id == id).FirstOrDefault();
                lesson.IsDeleted = true;
                _lessonRepository.Update(lesson);
                _topicRepository.FindBy(t => t.LessonId == id).ToList().ForEach(i => { i.IsDeleted = true; });
                result.data = _mapper.Map<LessonViewModel>(lesson);
                result.resultType = ServiceResultType.Success;
                _lessonRepository.Commit();
                _topicRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteLessonById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;

        }

        public Lesson GetLessonById(int id)
        {
            return _lessonRepository.FindBy(l => l.Id == id && !l.IsDeleted).FirstOrDefault();
        }
    }
}
