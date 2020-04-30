using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Helpers;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;

namespace SorubankCMS.Service
{
    public class BundleService : IBundleService
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IBundleExamCardRepository _bundleExamCardRepository;
        private readonly IBundleQuestionCardRepository _bundleQuestionCardRepository;
        private readonly IBundleTopicCardRepository _bundleTopicCardRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(BundleService));

        public BundleService(IBundleRepository bundleRepository,
            IBundleExamCardRepository bundleExamCardRepository,
            IBundleQuestionCardRepository bundleQuestionCardRepository,
            IBundleTopicCardRepository bundleTopicCardRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _bundleRepository = bundleRepository;
            _bundleExamCardRepository = bundleExamCardRepository;
            _bundleQuestionCardRepository = bundleQuestionCardRepository;
            _bundleTopicCardRepository = bundleTopicCardRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //Calling from Controller
        public ServiceResult<BundleModel> CreateOrEditBundle(BundleModel bundleModel)
        {
            ServiceResult<BundleModel> result = new ServiceResult<BundleModel>();
            try
            {
                if (bundleModel.Id > 0)
                {
                    return EditBundle(bundleModel);
                }
                return CreateBundle(bundleModel);
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEditBundle: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }
        //Calling from Controller
        public ServiceResult<List<BundleModel>> FetchAllBundles()
        {
            ServiceResult<List<BundleModel>> result = new ServiceResult<List<BundleModel>>();
            try
            {
                List<BundleModel> bundleModels = GetBundles(0);
                result.resultType = ServiceResultType.Success;
                result.data = bundleModels;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllBundles: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }

        //Calling from Controller
        public ServiceResult<BundleModel> DeleteBundleById(int id)
        {
            ServiceResult<BundleModel> result = new ServiceResult<BundleModel>();
            try
            {
                var bundle = _bundleRepository.AllIncluding(b => b.Product).FirstOrDefault(a => a.Id == id);
                if (bundle == null)
                {
                    result.message = "NO_BUNDLE_FOUND";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                DeleteAllBundleExamCardsByBundle(bundle.Id);
                DeleteAllBundleQuestionCardsByBundle(bundle.Id);
                DeleteAllBundleTopicCardsByBundle(bundle.Id);
                DeleteBundle(bundle);
                DeleteProduct(bundle.Product);
                result.data = _mapper.Map<BundleModel>(bundle);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteBundleById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;
        }
        //Calling from Controller
        public ServiceResult<List<BundleModel>> FetchAllBundlesByTenantId(int tenantId)
        {
            ServiceResult<List<BundleModel>> result = new ServiceResult<List<BundleModel>>();
            try
            {
                List<BundleModel> bundleModels = GetBundles(tenantId);
                result.resultType = ServiceResultType.Success;
                result.data = bundleModels;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllBundlesByTenantId: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }

        private List<BundleModel> GetBundles(int tenantId)
        {
            List<Bundle> bundles;
            if (tenantId == 0)
            {
                bundles = _bundleRepository.AllIncluding(b => b.Product, t => t.Product.Tenant).Where(b => !b.IsDeleted).ToList();
            }
            else
            {
                bundles = _bundleRepository.AllIncluding(b => b.Product, t => t.Product.Tenant).Where(b => !b.IsDeleted && b.Product.TenantId == tenantId).ToList();
            }
            List<BundleModel> bundleModels = new List<BundleModel>();
            foreach (var bundle in bundles)
            {
                var bundleModel = _mapper.Map<BundleModel>(bundle);
                MapProductToBundleModel(bundleModel, bundle.Product);
                bundleModel.ExamCards = GetExamCardModelsByBundle(bundle.Id);
                bundleModel.QuestionCards = GetQuestionCardModelsByBundle(bundle.Id);
                bundleModel.TopicCards = GetTopicCardModelsByBundle(bundle.Id);
                bundleModels.Add(bundleModel);
            }

            return bundleModels;
        }

        private void DeleteAllBundleExamCardsByBundle(int bundleId)
        {
            _bundleExamCardRepository.DeleteWhere(b => b.BundleId == bundleId);
            _bundleExamCardRepository.Commit();
        }

        private void DeleteAllBundleQuestionCardsByBundle(int bundleId)
        {
            _bundleQuestionCardRepository.DeleteWhere(b => b.BundleId == bundleId);
            _bundleQuestionCardRepository.Commit();
        }

        private void DeleteAllBundleTopicCardsByBundle(int bundleId)
        {
            _bundleTopicCardRepository.DeleteWhere(b => b.BundleId == bundleId);
            _bundleTopicCardRepository.Commit();
        }

        private void DeleteBundle(Bundle bundle)
        {
            bundle.IsActive = false;
            bundle.IsDeleted = true;
            _bundleRepository.Update(bundle);
            _bundleRepository.Commit();
        }

        private void DeleteProduct(Product product)
        {
            product.IsActive = false;
            product.IsDeleted = true;
            _productRepository.Update(product);
            _productRepository.Commit();
        }

        private void MapProductToBundleModel(BundleModel bundleModel, Product product)
        {
            bundleModel.Price = product.TenantPrice;
            bundleModel.Name = product.Name;
            bundleModel.Description = product.Description;
            bundleModel.FileUrl = product.FileUrl;
            bundleModel.IsActive = product.IsActive;
            bundleModel.Tenant = _mapper.Map<TenantViewModel>(product.Tenant);
        }

        private List<ExamCardModel> GetExamCardModelsByBundle(int bundleModelId)
        {
            List<ExamCardModel> examCardModels = new List<ExamCardModel>();
            var examCards = _bundleExamCardRepository.AllIncluding(b => b.ExamCard, c => c.ExamCard.Product).Where(b => b.BundleId == bundleModelId && !b.IsDeleted && b.IsActive).Select(b => b.ExamCard).ToList();
            foreach (var examCard in examCards)
            {
                var examCardModel = _mapper.Map<ExamCardModel>(examCard);
                MapProductToBaseCardModel(examCard.Product, examCardModel);
                examCardModels.Add(examCardModel);
            }
            return examCardModels;
        }

        private List<QuestionCardModel> GetQuestionCardModelsByBundle(int bundleModelId)
        {
            List<QuestionCardModel> questionCardModels = new List<QuestionCardModel>();
            var questionCards = _bundleQuestionCardRepository.AllIncluding(b => b.QuestionCard,
                c => c.QuestionCard.Product,
                d => d.QuestionCard.Topic,
                e => e.QuestionCard.Lesson).Where(b => b.BundleId == bundleModelId && !b.IsDeleted && b.IsActive).Select(b => b.QuestionCard).ToList();
            foreach (var questionCard in questionCards)
            {
                var questionCardModel = _mapper.Map<QuestionCardModel>(questionCard);
                MapProductToBaseCardModel(questionCard.Product, questionCardModel);
                questionCardModels.Add(questionCardModel);
            }
            return questionCardModels;
        }

        private List<TopicCardModel> GetTopicCardModelsByBundle(int bundleModelId)
        {
            List<TopicCardModel> topicCardModels = new List<TopicCardModel>();
            var topicCards = _bundleTopicCardRepository.AllIncluding(b => b.TopicCard,
                c => c.TopicCard.Product,
                d => d.TopicCard.Topic,
                e => e.TopicCard.Lesson).Where(b => b.BundleId == bundleModelId && !b.IsDeleted && b.IsActive).Select(b => b.TopicCard).ToList();
            foreach (var topicCard in topicCards)
            {
                var topicCardModel = _mapper.Map<TopicCardModel>(topicCard);
                MapProductToBaseCardModel(topicCard.Product, topicCardModel);
                topicCardModels.Add(topicCardModel);
            }
            return topicCardModels;
        }

        private ServiceResult<BundleModel> CreateBundle(BundleModel bundleModel)
        {
            ServiceResult<BundleModel> result = new ServiceResult<BundleModel>();
            if (bundleModel.ExamCards.Count == 0 && bundleModel.QuestionCards.Count == 0 && bundleModel.TopicCards.Count == 0)
            {
                result.resultType = ServiceResultType.Fail;
                result.message = "INVALID_BUNDLE_BUNDLE_SHOULD_CONTAINS_AT_LEAST_ONE_CARD";
            }
            var product = AddProduct(bundleModel);
            var bundle = AddBundle(product, bundleModel);
            AddBundleExamCards(bundle, new HashSet<ExamCardModel>(bundleModel.ExamCards, new CardComparer()));
            AddBundleQuestionCards(bundle, new HashSet<QuestionCardModel>(bundleModel.QuestionCards, new CardComparer()));
            AddBundleTopicCards(bundle, new HashSet<TopicCardModel>(bundleModel.TopicCards, new CardComparer()));
            bundleModel.Id = bundle.Id;
            result.resultType = ServiceResultType.Success;
            result.data = bundleModel;
            return result;
        }

        private ServiceResult<BundleModel> EditBundle(BundleModel bundleModel)
        {
            ServiceResult<BundleModel> result = new ServiceResult<BundleModel>();
            try
            {
                Bundle bundle = _bundleRepository.AllIncluding(b => b.Product, t => t.Product.Tenant).FirstOrDefault(b => b.Id == bundleModel.Id);
                if (bundle.Product.IsActive)
                {
                    result.message = "Ürün aktif satışta bulunmaktadır. Güncelleme yapılamaz.";
                    result.resultType = ServiceResultType.Fail;
                    return result;
                }
                UpdateProduct(bundleModel, bundle.Product);
                UpdateBundle(bundleModel, bundle);
                _productRepository.Commit();
                _bundleRepository.Commit();
                UpdateBundleExamCards(bundle, bundleModel);
                UpdateBundleQuestionCards(bundle, bundleModel);
                UpdateBundleTopicCards(bundle, bundleModel);
                result.resultType = ServiceResultType.Success;
                result.data = bundleModel;
                return result;
            }
            catch (Exception e)
            {
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
                return result;
            }
        }

        private void UpdateProduct(BundleModel bundleModel, Product product)
        {
            product.Name = bundleModel.Name;
            product.Description = bundleModel.Description;
            product.TenantPrice = bundleModel.Price;
            product.Price = bundleModel.Price * 1.1;
            product.FileUrl = bundleModel.FileUrl;
            product.TenantId = bundleModel.Tenant.Id;
            _productRepository.Update(product);
        }

        private void UpdateBundle(BundleModel bundleModel, Bundle bundle)
        {
            bundle.IsActive = bundleModel.IsActive;
            _bundleRepository.Update(bundle);
        }

        private void UpdateBundleExamCards(Bundle bundle, BundleModel bundleModel)
        {
            DeleteAllBundleExamCardsByBundle(bundle.Id);
            AddBundleExamCards(bundle, new HashSet<ExamCardModel>(bundleModel.ExamCards, new CardComparer()));
        }

        private void UpdateBundleQuestionCards(Bundle bundle, BundleModel bundleModel)
        {
            DeleteAllBundleQuestionCardsByBundle(bundle.Id);
            AddBundleQuestionCards(bundle, new HashSet<QuestionCardModel>(bundleModel.QuestionCards, new CardComparer()));
        }

        private void UpdateBundleTopicCards(Bundle bundle, BundleModel bundleModel)
        {
            DeleteAllBundleTopicCardsByBundle(bundle.Id);
            AddBundleTopicCards(bundle, new HashSet<TopicCardModel>(bundleModel.TopicCards, new CardComparer()));
        }

        private Product AddProduct(BundleModel bundleModel)
        {
            return _productRepository.AddWithCommit(new Product()
            {
                EntityTypeId = (int)Enums.EntityTypes.Bundle,
                Name = bundleModel.Name,
                Description = bundleModel.Description,
                TenantPrice = bundleModel.Price,
                Price = bundleModel.Price * 1.1,
                FileUrl = bundleModel.FileUrl,
                TenantId = bundleModel.Tenant.Id,
                IsActive = true,
                IsDeleted = false
            });
        }

        private Bundle AddBundle(Product product, BundleModel bundleModel)
        {
            var bundle = _mapper.Map<Bundle>(bundleModel);
            bundle.ProductId = product.Id;
            return _bundleRepository.AddWithCommit(bundle);
        }

        private void AddBundleExamCards(Bundle bundle, HashSet<ExamCardModel> examCardModels)
        {
            if ((examCardModels?.Count ?? 0) > 0)
            {
                List<ExamCard> examCards = _mapper.Map<List<ExamCard>>(examCardModels);
                List<BundleExamCard> bundleExamCards = new List<BundleExamCard>();
                foreach (var examCard in examCards)
                {
                    var bundleExamCard = new BundleExamCard()
                    {
                        Bundle = bundle,
                        ExamCardId = examCard.Id,
                        IsActive=true                       
                    };
                    bundleExamCards.Add(bundleExamCard);
                }
                _bundleExamCardRepository.AddAllWithCommit(bundleExamCards);
            }
        }

        private void AddBundleQuestionCards(Bundle bundle, HashSet<QuestionCardModel> questionCardModels)
        {
            if ((questionCardModels?.Count ?? 0) > 0)
            {
                List<QuestionCard> questionCards = _mapper.Map<List<QuestionCard>>(questionCardModels);
                List<BundleQuestionCard> bundleQuestionCards = new List<BundleQuestionCard>();
                foreach (var questionCard in questionCards)
                {
                    var bundleQuestionCard = new BundleQuestionCard()
                    {
                        Bundle = bundle,
                        QuestionCardId = questionCard.Id,
                        IsActive=true
                    };
                    bundleQuestionCards.Add(bundleQuestionCard);
                }
                _bundleQuestionCardRepository.AddAllWithCommit(bundleQuestionCards);
            }
        }

        private void AddBundleTopicCards(Bundle bundle, HashSet<TopicCardModel> topicCardModels)
        {
            if ((topicCardModels?.Count ?? 0) > 0)
            {
                List<TopicCard> topicCards = _mapper.Map<List<TopicCard>>(topicCardModels);
                List<BundleTopicCard> bundleTopicCards = new List<BundleTopicCard>();
                foreach (var topicCard in topicCards)
                {
                    var bundleTopicCard = new BundleTopicCard()
                    {
                        Bundle = bundle,
                        TopicCardId = topicCard.Id,
                        IsActive=true
                    };
                    bundleTopicCards.Add(bundleTopicCard);
                }
                _bundleTopicCardRepository.AddAllWithCommit(bundleTopicCards);
            }
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