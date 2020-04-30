using AutoMapper;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Models.Response;
using SorubankCMS.Models.ElasticSearch;

namespace SorubankCMS.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Tenant, TenantViewModel>();
            CreateMap<TenantViewModel, Tenant>();
            CreateMap<Lesson, LessonViewModel>();
            CreateMap<LessonViewModel, Lesson>();
            CreateMap<Topic, TopicViewModel>();
            CreateMap<TopicViewModel, Topic>();

            CreateMap<CMSUser, UserViewModel>();
            CreateMap<UserViewModel, CMSUser>();
            CreateMap<QuestionCard, QuestionCardModel>();
            CreateMap<QuestionCardModel, QuestionCard>();

            CreateMap<ExamCard, ExamCardModel>();
            CreateMap<ExamCardModel, ExamCard>();
            CreateMap<TopicCard, TopicCardModel>();
            CreateMap<TopicCardModel, TopicCard>();
            CreateMap<BundleModel, Bundle>();
            CreateMap<Bundle, BundleModel>();
            CreateMap<Product, ProductResponseModel>();
            CreateMap<ProductResponseModel, Product>();
            CreateMap<Tenant, ProductTenantModel>();
            CreateMap<ProductTenantModel, Tenant>();
            CreateMap<QuestionCard, ProductQuestionCardModel>();
            CreateMap<ProductQuestionCardModel, QuestionCard>();
            CreateMap<ExamCard, ProductExamCardModel>();
            CreateMap<ProductExamCardModel, ExamCard>();
            CreateMap<TopicCard, ProductTopicCardModel>();
            CreateMap<ProductTopicCardModel, TopicCard>();
            CreateMap<Bundle, ProductBundleModel>();
            CreateMap<ProductBundleModel, Bundle>();
            CreateMap<Product, ProductELModel>();
            CreateMap<OrderCart, OrderCartResponseModel>();
            CreateMap<OrderCartItem, OrderCartItemResponseModel>();
            CreateMap<OrderCartResponseModel, OrderCart>();
            CreateMap<OrderCartItemResponseModel, OrderCartItem>();
        }
    }
}