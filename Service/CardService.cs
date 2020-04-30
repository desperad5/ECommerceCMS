using AutoMapper;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Helpers;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using SorubankCMS.Models.ElasticSearch;
using SorubankCMS.Data.Repositories;
using System.Collections;
using log4net;

namespace SorubankCMS.Service
{
    public class CardService : ICardService
    {
        private readonly IQuestionCardRepository _questionCardRepository;
        private readonly IExamCardRepository _examCardRepository;
        private readonly ITopicCardRepository _topicCardRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly ILessonRepository _lessonRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly ITopicRepository _topicRepository;
        private static readonly ILog logger = Logger.GetLogger(typeof(CardService));
        public CardService(IQuestionCardRepository questionCardRepository, IExamCardRepository examCardRepository,
                             ITopicCardRepository topicCardRepository, IProductRepository productRepository, 
                             IMapper mapper, IElasticClient elasticClient,
                             ILessonRepository lessonRepository, ITenantRepository tenantRepository, ITopicRepository topicRepository)
        {
            _questionCardRepository = questionCardRepository;
            _examCardRepository = examCardRepository;
            _topicCardRepository = topicCardRepository;
            _productRepository = productRepository;
            _mapper = mapper;
            _elasticClient = elasticClient;
            _lessonRepository = lessonRepository;
            _tenantRepository = tenantRepository;
            _topicRepository = topicRepository;
        }
        public ServiceResult<ExamCardModel> CreateOrEditExamCardAndRelatedProduct(ExamCardModel model)
        {
            ServiceResult<ExamCardModel> result = new ServiceResult<ExamCardModel>
            {
                resultType = ServiceResultType.Success
            };
            try
            {
                if (model.Id > 0)
                {
                    var examCard = _examCardRepository.AllIncluding(e => e.Product).FirstOrDefault(t => t.Id == model.Id);
                    var product = examCard.Product;
                    if (!product.IsActive)
                    {
                        //2 repoda commit var transaction olmalı, unit of work pattern kullanılacak
                        UpdateExamCard(examCard, model);
                        UpdateProductInfo(product, model);
                        updateProductForElasticSearch(product);
                        _productRepository.Commit();
                        _examCardRepository.Commit();
                        result.data = model;
                    }
                    else
                    {
                        result.message = "Ürün aktif satışta bulunmaktadır. Güncelleme yapılamaz.";
                        result.resultType = ServiceResultType.Fail;
                    }
                }
                else
                {
                    var product = AddProduct(Enums.EntityTypes.ExamCard, model);
                    model.ProductId = product.Id;
                    
                    var examCard = _examCardRepository.AddWithCommit(_mapper.Map<ExamCard>(model));
                    product.ExamCard = examCard;
                    InsertProductForElasticSearch(product);
                    model.Id = examCard.Id;
                    result.data = model;
                }
                return result;

            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEditExamCardAndRelatedProduct: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
                return result;
            }


        }

        private string getExamTypeById(int examType)
        {
             return Enum.GetName(typeof(Enums.ExamTypes),examType); 
        }

        private Lesson getLessonById(int lessonId)
        {
            return _lessonRepository.GetSingle(ls => ls.Id == lessonId);
        }

        private ArrayList getCardTagsForExamCards(ExamCard examCard)
        {
            ArrayList cardTags = new ArrayList();
            cardTags.Add(getExamTypeById(examType: examCard.ExamTypeId));
            cardTags.Add(examCard.QuestionCount.ToString());

            return cardTags;
        }

        private ArrayList getCardTagsForQuestionCard(QuestionCard questionCard)
        {
            ArrayList cardTags = new ArrayList();
            cardTags.Add(questionCard.QuestionCount.ToString());

            Lesson lesson = getLessonById(questionCard.LessonId);
            cardTags.Add(lesson.Name);
            cardTags.Add(lesson.EducationLevel.ToString());

            Topic topic = _topicRepository.GetSingle(tp => tp.Id == questionCard.TopicId);
            cardTags.Add(topic.Name);
            cardTags.Add(topic.ClassLevel.ToString());

            return cardTags;
        }

        private ArrayList getCardTagsForTopicCard(TopicCard topicCard)
        {
            ArrayList cardTags = new ArrayList();
            cardTags.Add(topicCard.InstructorName);

            Lesson lesson = getLessonById(topicCard.LessonId);
            cardTags.Add(lesson.Name);
            cardTags.Add(lesson.EducationLevel.ToString());

            Topic topic = _topicRepository.GetSingle(tp => tp.Id == topicCard.TopicId);
            cardTags.Add(topic.Name);
            cardTags.Add(topic.ClassLevel.ToString());

            return cardTags;
        }

        private void  InsertProductForElasticSearch(Product product){
            var elModel = new ProductELModel();
            elModel.CardTags = new ArrayList();
            elModel = _mapper.Map<ProductELModel>(product);
            Tenant tenant = _tenantRepository.GetSingle(tn => tn.Id == product.TenantId);
            elModel.CardTags.Add(tenant.Name);
            ArrayList cardTags = new ArrayList();
            if (product.EntityTypeId == (int)Enums.EntityTypes.ExamCard){
                cardTags = getCardTagsForExamCards(product.ExamCard);
            } else if (product.EntityTypeId == (int)Enums.EntityTypes.QuestionCard){
                cardTags = getCardTagsForQuestionCard(product.QuestionCard);
            } else if (product.EntityTypeId == (int)Enums.EntityTypes.TopicCard){
                cardTags = getCardTagsForTopicCard(product.TopicCard);
                
            } else if (product.EntityTypeId == (int)Enums.EntityTypes.Bundle){
                ArrayList bundleCardTags = new ArrayList();
                if(product.Bundle.BundleExamCards != null && product.Bundle.BundleExamCards.Count > 0)
                {
                    foreach(BundleExamCard bundleExamCard in product.Bundle.BundleExamCards)
                    {
                        bundleCardTags.AddRange(getCardTagsForExamCards(bundleExamCard.ExamCard));
                    }
                }

                if(product.Bundle.BundleTopicCards != null && product.Bundle.BundleTopicCards.Count > 0)
                {
                    foreach (BundleTopicCard bundleTopicCard in product.Bundle.BundleTopicCards)
                    {
                        bundleCardTags.AddRange(getCardTagsForTopicCard(bundleTopicCard.TopicCard));
                    }
                }

                if (product.Bundle.BundleQuestionCards != null && product.Bundle.BundleQuestionCards.Count > 0)
                {
                    foreach (BundleQuestionCard bundleQuestionCard in product.Bundle.BundleQuestionCards)
                    {
                        bundleCardTags.AddRange(getCardTagsForQuestionCard(bundleQuestionCard.QuestionCard));
                    }
                }

                cardTags = bundleCardTags;
            }

            if (cardTags != null && cardTags.Count > 0)
            {
                elModel.CardTags.AddRange(cardTags);
            }

            _elasticClient.IndexDocumentAsync(elModel);
        }

        private void updateProductForElasticSearch(Product product)
        {
            deleteProductFromElasticSearch(product);
            InsertProductForElasticSearch(product);
        }

        public void deleteProductFromElasticSearch(Product product)
        {
            DeleteByQueryRequest request = new DeleteByQueryRequest("products");
            request.Conflicts = Elasticsearch.Net.Conflicts.Proceed;

            QueryContainer query = new TermQuery
            {
                Field = "Id",
                Value = product.Id
            };
            
            request.Query = query;

            //TODO: işlem sonuçlarını handle etmek gerekli. 
           _elasticClient.DeleteByQueryAsync(request);
        }

        public ServiceResult<QuestionCardModel> CreateOrEditQuestionCardAndRelatedProduct(QuestionCardModel model)
        {
            ServiceResult<QuestionCardModel> result = new ServiceResult<QuestionCardModel>();
            result.resultType = ServiceResultType.Success;
            try
            {
                if (model.Id > 0)
                {
                    var questionCard = _questionCardRepository.AllIncluding(q => q.Product).FirstOrDefault(t => t.Id == model.Id);
                    var product = questionCard.Product;
                    if (!product.IsActive)
                    {
                        //2 repoda commit var transaction olmalı, unit of work pattern kullanılacak
                        UpdateQuestionCard(questionCard, model);
                        UpdateProductInfo(product, model);
                        updateProductForElasticSearch(product);
                        _productRepository.Commit();
                        _questionCardRepository.Commit();
                        result.data = model;
                    }
                    else
                    {
                        result.message = "Ürün aktif satışta bulunmaktadır. Güncelleme yapılamaz.";
                        result.resultType = ServiceResultType.Fail;
                    }
                }
                else
                {
                    var product = AddProduct(Enums.EntityTypes.QuestionCard, model);
                    model.ProductId = product.Id;
                    var questionCard = _mapper.Map<QuestionCard>(model);
                    questionCard.Lesson = null;
                    questionCard.Topic = null;
                    questionCard.LessonId = model.Lesson.Id;
                    questionCard.TopicId = model.Topic.Id;
                    questionCard = _questionCardRepository.AddWithCommit(questionCard);

                    InsertProductForElasticSearch(product);
                    model.Id = questionCard.Id;
                    result.data = model;
                }
                return result;

            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEditQuestionCardAndRelatedProduct: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
                return result;
            }


        }

        public ServiceResult<TopicCardModel> CreateOrEditTopicCardAndRelatedProduct(TopicCardModel model)
        {
            ServiceResult<TopicCardModel> result = new ServiceResult<TopicCardModel>();
            result.resultType = ServiceResultType.Success;
            try
            {
                if (model.Id > 0)
                {
                    var topicCard = _topicCardRepository.AllIncluding(t => t.Product).FirstOrDefault(t => t.Id == model.Id);
                    var product = topicCard.Product;
                    if (!product.IsActive)
                    {
                        //2 repoda commit var transaction olmalı, unit of work pattern kullanılacak
                        UpdateTopicCard(topicCard, model);
                        UpdateProductInfo(product, model);
                        updateProductForElasticSearch(product);
                        _productRepository.Commit();
                        _topicCardRepository.Commit();
                        result.data = model;
                    }
                    else
                    {
                        result.message = "Ürün aktif satışta bulunmaktadır. Güncelleme yapılamaz.";
                        result.resultType = ServiceResultType.Fail;
                    }
                }
                else
                {
                    var product = AddProduct(Enums.EntityTypes.TopicCard, model);
                    model.ProductId = product.Id;
                    var topicCard = _mapper.Map<TopicCard>(model);
                    topicCard.Lesson = null;
                    topicCard.Topic = null;
                    topicCard.LessonId = model.Lesson.Id;
                    topicCard.TopicId = model.Topic.Id;
                    topicCard = _topicCardRepository.AddWithCommit(topicCard);

                    InsertProductForElasticSearch(product);
                    model.Id = topicCard.Id;
                    result.data = model;
                }
                return result;

            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEditTopicCardAndRelatedProduct: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
                return result;
            }
        }

        public ServiceResult<ExamCardModel> DeleteExamCardById(int id)
        {
            ServiceResult<ExamCardModel> result = new ServiceResult<ExamCardModel>();

            try
            {
                //todo:transactional olacak
                var examCard = _examCardRepository.AllIncluding(e => e.Product).FirstOrDefault(a => a.Id == id);
                if (examCard == null)
                {
                    result.message = "NO_EXAMCARD_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                examCard.IsActive = false;
                examCard.IsDeleted = true;
                _examCardRepository.Update(examCard);
                _examCardRepository.Commit();
                var product = examCard.Product;
                product.IsActive = false;
                product.IsDeleted = true;
                _productRepository.Update(product);
                _productRepository.Commit();

                deleteProductFromElasticSearch(product);
                result.data = _mapper.Map<ExamCardModel>(examCard);
                result.resultType = ServiceResultType.Success;

            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteExamCardById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;
        }

        public ServiceResult<QuestionCardModel> DeleteQuestionCardById(int id)
        {
            ServiceResult<QuestionCardModel> result = new ServiceResult<QuestionCardModel>();

            try
            {
                //todo:transactional olacak
                var questionCard = _questionCardRepository.AllIncluding(q => q.Product).FirstOrDefault(a => a.Id == id);
                if (questionCard == null)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "NO_QUESTIONCARD_FOUND";
                    return result;
                }
                questionCard.IsActive = false;
                questionCard.IsDeleted = true;
                _questionCardRepository.Update(questionCard);
                _questionCardRepository.Commit();
                var product = questionCard.Product;
                product.IsActive = false;
                product.IsDeleted = true;
                _productRepository.Update(product);
                _productRepository.Commit();

                deleteProductFromElasticSearch(product);
                result.data = _mapper.Map<QuestionCardModel>(questionCard);
                result.resultType = ServiceResultType.Success;

            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteQuestionCardById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;
        }

        public ServiceResult<TopicCardModel> DeleteTopicCardById(int id)
        {
            ServiceResult<TopicCardModel> result = new ServiceResult<TopicCardModel>();

            try
            {
                //todo:transactional olacak
                var topicCard = _topicCardRepository.AllIncluding(t => t.Product).FirstOrDefault(a => a.Id == id);
                if (topicCard == null)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "NO_TOPICCARD_FOUND";
                    return result;
                }
                topicCard.IsActive = false;
                topicCard.IsDeleted = true;
                _topicCardRepository.Update(topicCard);
                _topicCardRepository.Commit();
                var product = topicCard.Product;
                product.IsActive = false;
                product.IsDeleted = true;
                _productRepository.Update(product);
                _productRepository.Commit();

                deleteProductFromElasticSearch(product);
                result.data = _mapper.Map<TopicCardModel>(topicCard);
                result.resultType = ServiceResultType.Success;

            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteTopicCardById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;

        }

        public ServiceResult<List<ExamCardModel>> FetchExamCardsByTenantId(int tenantId)
        {
            ServiceResult<List<ExamCardModel>> result = new ServiceResult<List<ExamCardModel>>();
            try
            {
                List<ExamCard> examCards;
                if (tenantId == 0)
                {
                    examCards = _examCardRepository.AllIncluding(a => a.Product, b => b.Product.Tenant).Where(a => !a.IsDeleted).ToList();
                }
                else
                {
                    examCards = _examCardRepository.AllIncluding(a => a.Product, b => b.Product.Tenant).Where(a => !a.IsDeleted && a.Product.TenantId == tenantId).ToList();
                }
                var examCardModels = ConvertExamCardsToModel(examCards);
                result.data = examCardModels;
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchExamCardsByTenantId: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }

        public ServiceResult<List<QuestionCardModel>> FetchQuestionCardsByTenantId(int tenantId)
        {
            ServiceResult<List<QuestionCardModel>> result = new ServiceResult<List<QuestionCardModel>>();
            try
            {
                List<QuestionCard> questionCards;
                if (tenantId == 0)
                {
                    questionCards = _questionCardRepository.AllIncluding(a => a.Product, b => b.Topic, c => c.Lesson, d => d.Product.Tenant).Where(a => !a.IsDeleted).ToList();
                }
                else
                {
                    questionCards = _questionCardRepository.AllIncluding(a => a.Product, b => b.Topic, c => c.Lesson, d => d.Product.Tenant).Where(a => !a.IsDeleted && a.Product.TenantId == tenantId).ToList();
                }
                List<QuestionCardModel> questionCardModels = ConvertQuestionCardsToModel(questionCards);
                result.data = questionCardModels;
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchQuestionCardsByTenantId: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }

        public ServiceResult<List<TopicCardModel>> FetchTopicCardsByTenantId(int tenantId)
        {
            ServiceResult<List<TopicCardModel>> result = new ServiceResult<List<TopicCardModel>>();
            try
            {
                List<TopicCard> topicCards;
                if (tenantId == 0)
                {
                    topicCards = _topicCardRepository.AllIncluding(a => a.Product, b => b.Topic, c => c.Lesson, d => d.Product.Tenant).Where(a => !a.IsDeleted).ToList();
                }
                else
                {
                    topicCards = _topicCardRepository.AllIncluding(a => a.Product, b => b.Topic, c => c.Lesson, d => d.Product.Tenant).Where(a => !a.IsDeleted && a.Product.TenantId == tenantId).ToList();
                }

                var topicCardModels = ConvertTopicCardsToModel(topicCards);
                result.data = topicCardModels;
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchTopicCardsByTenantId: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }
        private List<QuestionCardModel> ConvertQuestionCardsToModel(List<QuestionCard> questionCards)
        {
            List<QuestionCardModel> questionCardModels = new List<QuestionCardModel>();
            foreach (var questionCard in questionCards)
            {
                var questionCardModel = _mapper.Map<QuestionCardModel>(questionCard);
                MapProductToBaseCardModel(questionCard.Product, questionCardModel);
                questionCardModels.Add(questionCardModel);
            }

            return questionCardModels;
        }

        private List<ExamCardModel> ConvertExamCardsToModel(List<ExamCard> examCards)
        {
            List<ExamCardModel> examCardModels = new List<ExamCardModel>();
            foreach (var examCard in examCards)
            {
                var examCardModel = _mapper.Map<ExamCardModel>(examCard);
                MapProductToBaseCardModel(examCard.Product, examCardModel);
                examCardModels.Add(examCardModel);
            }

            return examCardModels;
        }

        private List<TopicCardModel> ConvertTopicCardsToModel(List<TopicCard> topicCards)
        {
            List<TopicCardModel> topicCardModels = new List<TopicCardModel>();
            foreach (var topicCard in topicCards)
            {
                var topicCardModel = _mapper.Map<TopicCardModel>(topicCard);
                MapProductToBaseCardModel(topicCard.Product, topicCardModel);
                topicCardModels.Add(topicCardModel);
            }

            return topicCardModels;
        }
        private Product AddProduct(Enums.EntityTypes entityType, BaseCardModel model)
        {
            var product = _productRepository.AddWithCommit(new Product()
            {
                EntityTypeId = (int)entityType,
                Name = model.Name,
                Description = model.Description,
                TenantPrice = model.Price,
                Price = model.Price * 1.1, //TODO make it configurable
                FileUrl = model.FileUrl,
                TenantId = model.Tenant.Id,
                IsActive=true
            });
            return product;
        }

        private ExamCard UpdateExamCard(ExamCard examCard, ExamCardModel model)
        {
            examCard.ExamTypeId = model.ExamTypeId;
            examCard.IsActive = model.IsActive;
            examCard.QuestionCount = model.QuestionCount;
            _examCardRepository.Update(examCard);
            return examCard;
        }

        private QuestionCard UpdateQuestionCard(QuestionCard questionCard, QuestionCardModel model)
        {
            questionCard.TopicId = model.Topic.Id;
            questionCard.LessonId = model.Lesson.Id;
            questionCard.QuestionCount = model.QuestionCount;
            _questionCardRepository.Update(questionCard);
            return questionCard;
        }

        private void UpdateTopicCard(TopicCard topicCard, TopicCardModel model)
        {
            topicCard.TopicId = model.Topic.Id;
            topicCard.LessonId = model.Lesson.Id;
            topicCard.InstructorName = model.InstructorName;
            _topicCardRepository.Update(topicCard);
        }

        private void UpdateProductInfo(Product product, BaseCardModel model)
        {
            product.Name = model.Name;
            product.TenantPrice = model.Price;
            product.Price = model.Price * 1.1; //TODO make it configurable
            product.FileUrl = model.FileUrl;
            product.Description = model.Description;
            _productRepository.Update(product);

            
        }

        private void MapProductToBaseCardModel(Product product, BaseCardModel baseCardModel)
        {
            baseCardModel.Price = product.TenantPrice;
            baseCardModel.Name = product.Name;
            baseCardModel.Description = product.Description;
            baseCardModel.FileUrl = product.FileUrl;
            baseCardModel.IsActive = product.IsActive;
            baseCardModel.Tenant = _mapper.Map<TenantViewModel>(product.Tenant);
        }

    }
}