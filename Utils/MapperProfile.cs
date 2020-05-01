using AutoMapper;
using SorubankCMS.Data.Entity;
using SorubankCMS.Models;
using SorubankCMS.Models.Response;
using SorubankCMS.Models.ElasticSearch;
using ECommerceCMS.Models;

namespace SorubankCMS.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Tenant, TenantViewModel>();
            CreateMap<TenantViewModel, Tenant>();
            CreateMap<CMSUser, UserViewModel>();
            CreateMap<UserViewModel, CMSUser>();
            CreateMap<BundleModel, Bundle>();
            CreateMap<Bundle, BundleModel>();
            CreateMap<Product, ProductResponseModel>();
            CreateMap<ProductResponseModel, Product>();
            CreateMap<Tenant, ProductTenantModel>();
            CreateMap<ProductTenantModel, Tenant>();
            CreateMap<Bundle, ProductBundleModel>();
            CreateMap<ProductBundleModel, Bundle>();
            CreateMap<Product, ProductELModel>();
            CreateMap<OrderCart, OrderCartResponseModel>();
            CreateMap<OrderCartItem, OrderCartItemResponseModel>();
            CreateMap<OrderCartResponseModel, OrderCart>();
            CreateMap<OrderCartItemResponseModel, OrderCartItem>();
            CreateMap<Brand, BrandViewModel>();
            CreateMap<BrandViewModel, Brand>();
        }
    }
}