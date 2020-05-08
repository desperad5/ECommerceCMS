using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinqKit;
using log4net;
using ECommerceCMS.Data;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;
using ECommerceCMS.Models;
using ECommerceCMS.Models.Response;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;

namespace ECommerceCMS.Service
{
    public class DistinctColorsAndSizes
    {
        public List<BrandViewModel> Colors { get; set; }
        public List<BrandViewModel> Sizes { get; set; }
    }
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _userRepository;
        private readonly IProductCommentRepository _productCommentRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(ProductService));

        public ProductService(IProductRepository productRepository, ICustomerRepository userRepository, IProductCommentRepository productCommentRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productCommentRepository = productCommentRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        public ServiceResult<ProductsByListingModel> GetProductsByListingId(int listingId)
        {
            ServiceResult<ProductsByListingModel> result = new ServiceResult<ProductsByListingModel>();
            try
            {
                var returnModel = _productRepository.GetProductsByListingId(listingId);
                result.resultType = ServiceResultType.Success;
                result.data = returnModel;
            }
            catch (Exception ex)
            {
                logger.Error("Error@GetProductDetail: ", ex);
                result.resultType = ServiceResultType.Fail;
                result.message = ex.ToString();
            }
            return result;
        }
        public ServiceResult<ProductsByCategoryModel> GetProductsByCategoryId(int categoryId, int? itemCount, int? pageNumber, bool orderByDate)
        {
            ServiceResult<ProductsByCategoryModel> result = new ServiceResult<ProductsByCategoryModel>();
            try
            {
                var returnModel = new ProductsByCategoryModel();
                var productsWithCategory = _productRepository.GetProductsByCategoryId(categoryId, itemCount, pageNumber, orderByDate);
                returnModel.Products = productsWithCategory.Products;
                if (returnModel.Products == null)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "No Product in the Category";
                    return result;
                }
                returnModel.CategoryName = productsWithCategory.Category.CategoryName;

                returnModel.Brands = _brandRepository.GetBrandsOfProducts(returnModel.Products);
                var distinctColorsSizes = FindDistinctColorSizes(returnModel.Products);
                returnModel.Colors = distinctColorsSizes.Colors;
                returnModel.Sizes = distinctColorsSizes.Sizes;
                result.resultType = ServiceResultType.Success;
                result.data = returnModel;
            }
            catch (Exception ex)
            {
                logger.Error("Error@GetProductDetail: ", ex);
                result.resultType = ServiceResultType.Fail;
                result.message = ex.ToString();
            }
            return result;
        }

        private DistinctColorsAndSizes FindDistinctColorSizes(List<ProductViewModel> products)
        {
            var returnModel = new DistinctColorsAndSizes();
            returnModel.Colors = new List<BrandViewModel>();
            returnModel.Sizes = new List<BrandViewModel>();
            foreach (var product in products)
            {
                if (product.ProductVariations != null && product.ProductVariations.Count > 0)
                {
                    foreach (var productVariation in product.ProductVariations)
                    {
                        if (!returnModel.Colors.Any(i => i.Name == productVariation.ColorValue))
                        {
                            returnModel.Colors.Add(new BrandViewModel() { Id = (int)((Enums.ColorValues)Enum.Parse(typeof(Enums.ColorValues), productVariation.ColorValue)), Name = productVariation.ColorValue.ToString() });
                        }
                        if (!returnModel.Sizes.Any(i => i.Name == productVariation.SizeValue))
                        {
                            returnModel.Sizes.Add(new BrandViewModel() { Id = (int)((Enums.SizeValues)Enum.Parse(typeof(Enums.SizeValues), productVariation.SizeValue)), Name = productVariation.SizeValue.ToString() });
                        }
                    }
                }
            }
            return returnModel;
        }

        public ServiceResult<ProductsByCategoryModel> GetNewProductsByCategoryId(int categoryId, int? itemCount, int? pageNumber)
        {
            ServiceResult<ProductsByCategoryModel> result = new ServiceResult<ProductsByCategoryModel>();
            try
            {
                var returnModel = new ProductsByCategoryModel();
                var productsWithCategory = _productRepository.GetNewProductsByCategoryId(categoryId, itemCount, pageNumber);
                returnModel.Products = productsWithCategory.Products;
                if (returnModel.Products == null)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "No Product in the Category";
                    return result;
                }
                returnModel.CategoryName = productsWithCategory.Category.CategoryName;
                result.resultType = ServiceResultType.Success;
                result.data = returnModel;
            }
            catch (Exception ex)
            {
                logger.Error("Error@GetProductDetail: ", ex);
                result.resultType = ServiceResultType.Fail;
                result.message = ex.ToString();
            }
            return result;
        }

        public ServiceResult<ProductViewModel> GetProductsWithImages(int productId)
        {
            ServiceResult<ProductViewModel> result = new ServiceResult<ProductViewModel>();
            try
            {
                var returnModel = _productRepository.GetProductsWithImages(productId);
                result.resultType = ServiceResultType.Success;
                result.data = returnModel;
            }
            catch (Exception ex)
            {
                logger.Error("Error@GetProductDetail: ", ex);
                result.resultType = ServiceResultType.Fail;
                result.message = ex.ToString();
            }
            return result;
        }

        public ServiceResult<List<ProductViewModel>> GetNewProduct(int itemCount)
        {
            ServiceResult<List<ProductViewModel>> result = new ServiceResult<List<ProductViewModel>>();
            try
            {
                var returnModel = _productRepository.GetNewProduct(itemCount);

                result.resultType = ServiceResultType.Success;
                result.data = returnModel;
            }
            catch (Exception ex)
            {
                logger.Error("Error@GetProductDetail: ", ex);
                result.resultType = ServiceResultType.Fail;
                result.message = ex.ToString();
            }
            return result;
        }

        //public ServiceResult<ProductResponseModel> GetProductDetail(int productId)
        //{
        //    ServiceResult<ProductResponseModel> result = new ServiceResult<ProductResponseModel>();
        //    try
        //    {
        //        var queryable = _productRepository.AllIncluding(
        //         p => p.Tenant,
        //         p => p.QuestionCard,
        //         p => p.QuestionCard.Lesson,
        //         p => p.QuestionCard.Topic,
        //         p => p.ExamCard,
        //         p => p.TopicCard,
        //         p => p.TopicCard.Lesson,
        //         p => p.TopicCard.Topic,
        //         p => p.Bundle.BundleQuestionCards,
        //         p => p.Bundle.BundleTopicCards,
        //         p => p.Bundle.BundleExamCards
        //         )
        //         .AsQueryable()

        //         .Where(t => t.Id == productId);
        //        var product = queryable.FirstOrDefault();
        //        ProductResponseModel returnModel = ProductSelectorFunction(product);
        //        result.resultType = ServiceResultType.Success;
        //        result.data = returnModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error("Error@GetProductDetail: ", ex);
        //        result.resultType = ServiceResultType.Fail;
        //        result.message = ex.ToString();
        //    }
        //    return result;
        //}

        //private ProductResponseModel ProductSelectorFunction(Product product)
        //{
        //    return new ProductResponseModel()
        //    {
        //        Id = product.Id,
        //        Tenant = new ProductTenantModel() { Id = product.Tenant.Id, Name = product.Tenant.Name },
        //        Name = product.Name,
        //        Description = product.Description,
        //        Price = product.Price,
        //        CreatedDate = product.CreatedDate,
        //        EntityTypeId=product.EntityTypeId,
        //        QuestionCard = product.QuestionCard != null ? new ProductQuestionCardModel()
        //        {
        //            Id = product.QuestionCard.Id,
        //            LessonEducationLevel = product.QuestionCard.Lesson.EducationLevel,
        //            LessonName = product.QuestionCard.Lesson.Name,
        //            TopicName = product.QuestionCard.Topic.Name,
        //            TopicClassLevel = product.QuestionCard.Topic.ClassLevel,
        //            QuestionCount = product.QuestionCard.QuestionCount

        //        } : null,
        //        TopicCard = product.TopicCard != null ? new ProductTopicCardModel()
        //        {
        //            Id = product.TopicCard.Id,
        //            LessonEducationLevel = product.TopicCard.Lesson.EducationLevel,
        //            LessonName = product.TopicCard.Lesson.Name,
        //            TopicName = product.TopicCard.Topic.Name,
        //            TopicClassLevel = product.TopicCard.Topic.ClassLevel,
        //            InstructorName = product.TopicCard.InstructorName,

        //        } : null,
        //        ExamCard = product.ExamCard != null ? new ProductExamCardModel()
        //        {
        //            Id = product.ExamCard.Id,
        //            ExamTypeId = product.ExamCard.ExamTypeId,
        //            QuestionCount = product.ExamCard.QuestionCount,

        //        } : null,
        //        Bundle = product.Bundle != null ? new ProductBundleModel()
        //        {
        //            Id = product.Bundle.Id,
        //            ExamCards = product.Bundle.BundleExamCards != null & product.Bundle.BundleExamCards.Count > 0 ?
        //           product.Bundle.BundleExamCards.Select(i =>
        //           {
        //               var examCard = _examCardRepository.AllIncluding(t=>t.Product).FirstOrDefault(t => t.Id == i.ExamCardId);
        //               return new ProductExamCardModel()
        //               {
        //                   Id = examCard.Id,
        //                   ExamTypeId = examCard.ExamTypeId,
        //                   QuestionCount = examCard.QuestionCount,
        //                   ProductId=examCard.ProductId,
        //                   ProductName=examCard.Product.Name
        //               };
        //           }) : null,
        //            QuestionCards = product.Bundle.BundleQuestionCards != null & product.Bundle.BundleQuestionCards.Count > 0 ?
        //           product.Bundle.BundleQuestionCards.Select(i =>
        //           {
        //               var questionCard = _questionCardRepository.AllIncluding(t => t.Lesson, t => t.Topic,t=>t.Product).Where(t => t.Id == i.QuestionCardId).FirstOrDefault();
        //               return new ProductQuestionCardModel()
        //               {

        //                   Id = questionCard.Id,
        //                   LessonEducationLevel = questionCard.Lesson.EducationLevel,
        //                   LessonName = questionCard.Lesson.Name,
        //                   TopicName = questionCard.Topic.Name,
        //                   TopicClassLevel = questionCard.Topic.ClassLevel,
        //                   QuestionCount = questionCard.QuestionCount,
        //                   ProductId=questionCard.ProductId,
        //                   ProductName = questionCard.Product.Name
        //               };
        //           }) : null,
        //            TopicCards = product.Bundle.BundleTopicCards != null & product.Bundle.BundleTopicCards.Count > 0 ?
        //            product.Bundle.BundleTopicCards.Select(i =>
        //            {
        //                var topicCard = _topicCardRepository.AllIncluding(t => t.Lesson, t => t.Topic,t=>t.Product).Where(t => t.Id == i.TopicCardId).FirstOrDefault();
        //                return new ProductTopicCardModel()
        //                {

        //                    Id = topicCard.Id,
        //                    LessonEducationLevel = topicCard.Lesson.EducationLevel,
        //                    LessonName = topicCard.Lesson.Name,
        //                    TopicName = topicCard.Topic.Name,
        //                    TopicClassLevel = topicCard.Topic.ClassLevel,
        //                    InstructorName = topicCard.InstructorName,
        //                    ProductId=topicCard.ProductId,
        //                    ProductName = topicCard.Product.Name
        //                };
        //            }) : null


        //        } : null

        //    };
        //}

        //public ServiceResult<List<ProductResponseModel>> SearchProducts(ProductSearchModel productSearchModel)
        //{
        //    ServiceResult<List<ProductResponseModel>> result = new ServiceResult<List<ProductResponseModel>>();
        //    try
        //    {
        //        List<Product> products = GetProductsBySearchModel(productSearchModel);
        //        List<ProductResponseModel> productResponseModels = _mapper.Map<List<ProductResponseModel>>(products);
        //        result.resultType = ServiceResultType.Success;
        //        result.data = productResponseModels;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error("Error@SearchProducts: ", e);
        //        result.resultType = ServiceResultType.Fail;
        //        result.message = e.ToString();
        //    }
        //    return result;
        //}

        //private List<Product> GetProductsBySearchModel(ProductSearchModel model)
        //{
        //    HashSet<int> tenantIds = model.TenantIds;
        //    HashSet<int> cardTypes = model.CardTypes;
        //    HashSet<int> lessonIds = model.LessonIds;
        //    HashSet<int> topicIds = model.TopicIds;
        //    HashSet<int> examTypeIds = model.ExamTypeIds;
        //    HashSet<int> lessonRelatedEntity = new HashSet<int>();
        //    lessonRelatedEntity.Add((int)Enums.EntityTypes.QuestionCard);
        //    lessonRelatedEntity.Add((int)Enums.EntityTypes.TopicCard);
        //    HashSet<int> notExamRelatedEntity = new HashSet<int>();
        //    notExamRelatedEntity.Add((int)Enums.EntityTypes.QuestionCard);
        //    notExamRelatedEntity.Add((int)Enums.EntityTypes.TopicCard);
        //    notExamRelatedEntity.Add((int)Enums.EntityTypes.Bundle);
        //    HashSet<int> notLessonRelatedEntity = new HashSet<int>();
        //    notLessonRelatedEntity.Add((int)Enums.EntityTypes.ExamCard);
        //    notLessonRelatedEntity.Add((int)Enums.EntityTypes.Bundle);
        //    bool all = cardTypes.Count == 0;
        //    var predicate = PredicateBuilder.New<Product>();
        //    predicate = predicate.And(p => !p.IsDeleted && p.IsActive);
        //    double minPrice = model.MinPrice;
        //    double maxPrice = model.MaxPrice;
        //    if (!String.IsNullOrEmpty(model.Name))
        //    {
        //        predicate = predicate.And(p => p.Name.Contains(model.Name));//TODO check this
        //    }
        //    if (!all)
        //    {
        //        predicate = predicate.And(p => cardTypes.Contains(p.EntityTypeId));
        //    }
        //    if (tenantIds.Count > 0)
        //    {
        //        predicate = predicate.And(p => tenantIds.Contains(p.TenantId));
        //    }
        //    if (minPrice > 0 || maxPrice > 0)
        //    {
        //        if (minPrice > 0 && maxPrice > 0)
        //        {
        //            predicate = predicate.And(p => p.Price >= minPrice && p.Price <= maxPrice);
        //        }
        //        else if (maxPrice == 0)
        //        {
        //            predicate = predicate.And(p => p.Price >= minPrice);
        //        }
        //        else
        //        {
        //            predicate = predicate.And(p => p.Price <= maxPrice);
        //        }
        //    }
        //    List<Product> products = _productRepository.AllIncluding(
        //        p => p.Tenant,
        //        p => p.QuestionCard,
        //        p => p.QuestionCard.Lesson,
        //        p => p.QuestionCard.Topic,
        //        p => p.ExamCard,
        //        p => p.TopicCard,
        //        p => p.TopicCard.Lesson,
        //        p => p.TopicCard.Topic,
        //        p => p.Bundle)
        //        .AsQueryable()
        //        .Where(predicate)
        //        .ToList();

        //    if (lessonIds.Count > 0 || topicIds.Count > 0)
        //    {
        //        products = products.Where(p => (lessonIds.Count > 0 && (((p.QuestionCard != null && lessonIds.Contains(p.QuestionCard.LessonId)) || (p.TopicCard != null && lessonIds.Contains(p.TopicCard.LessonId)))
        //        && lessonRelatedEntity.Contains(p.EntityTypeId)) || notLessonRelatedEntity.Contains(p.EntityTypeId))
        //        ||
        //        (topicIds.Count > 0 && (((p.QuestionCard != null && topicIds.Contains(p.QuestionCard.TopicId)) || (p.TopicCard != null && topicIds.Contains(p.TopicCard.TopicId)))
        //        && lessonRelatedEntity.Contains(p.EntityTypeId)) || notLessonRelatedEntity.Contains(p.EntityTypeId))).ToList();
        //    }
        //    if (examTypeIds != null && examTypeIds.Count > 0)
        //    {
        //        products = products.Where(p => (examTypeIds.Count > 0 && (p.ExamCard != null && examTypeIds.Contains(p.ExamCard.ExamTypeId) && p.EntityTypeId == (int)Enums.EntityTypes.ExamCard))
        //            || notExamRelatedEntity.Contains(p.EntityTypeId)).ToList();
        //    }
        //    products = products.Skip((model.Page - 1) * model.RecordCount).Take(model.RecordCount).ToList();
        //    return products;
        //}

        public ServiceResult<Product> GetActiveProductById(int id)
        {
            ServiceResult<Product> result = new ServiceResult<Product>();
            try
            {
                Product product = _productRepository.FindBy(p => p.Id == id && !p.IsDeleted && p.IsActive).FirstOrDefault();
                if (product == null)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "NO_ACTIVE_PRODUCT_FOUND";
                    return result;
                }
                result.data = product;
                result.resultType = ServiceResultType.Success;
                return result;
            }
            catch (Exception e)
            {
                logger.Error("Error@GetActiveProductById: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }
        public ServiceResult<List<ProductCommentModel>> GetProductComments(int productId)
        {
            ServiceResult<List<ProductCommentModel>> result = new ServiceResult<List<ProductCommentModel>>();
            try
            {
                Product product = _productRepository.AllIncluding(p => p.ProductComments).Where(p => p.Id == productId && !p.IsDeleted && p.IsActive).FirstOrDefault();
                if (product == null)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "NO_ACTIVE_PRODUCT_FOUND";
                    return result;
                }
                result.data = product.ProductComments.OrderByDescending(t => t.CreatedDate).Select(i =>
                {
                    var user = _userRepository.GetSingle(t => t.Id == i.CustomerId);
                    return new ProductCommentModel()
                    {
                        Comment = i.Comment,
                        ProductId = i.ProductId,
                        CreatedDate = i.CreatedDate,
                        Id = i.Id,
                        User = new UserViewModel() { Id = i.CustomerId, UserName = user.UserName, Name = user.FirstName, Surname = user.LastName }
                    };
                }).ToList();
                result.resultType = ServiceResultType.Success;
                return result;
            }
            catch (Exception e)
            {
                logger.Error("Error@GetProductComments: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;



        }
        public ServiceResult<ProductCommentModel> InsertProductComments(int productId, string Comment, int userId)
        {
            ServiceResult<ProductCommentModel> result = new ServiceResult<ProductCommentModel>();
            try
            {
                Product product = _productRepository.AllIncluding(p => p.ProductComments).Where(p => p.Id == productId && !p.IsDeleted && p.IsActive).FirstOrDefault();
                if (product == null)
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "NO_ACTIVE_PRODUCT_FOUND";
                    return result;
                }
                _productCommentRepository.AddWithCommit(new ProductComment()
                {
                    Comment = Comment,
                    ProductId = productId,
                    CustomerId = userId
                });


                result.resultType = ServiceResultType.Success;
                result.data = new ProductCommentModel()
                {
                    Comment = Comment,
                    ProductId = productId,
                    User = new UserViewModel { Id = userId }
                };
            }
            catch (Exception e)
            {
                logger.Error("Error@InsertProductComments: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;
        }
    }
}