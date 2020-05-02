using AutoMapper;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Models.Response;
using ECommerceCMS.Models.ElasticSearch;

namespace ECommerceCMS.Utils
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {

            CreateMap<Tenant, TenantViewModel>();
            CreateMap<TenantViewModel, Tenant>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategoryViewModel, ProductCategory>();
            CreateMap<Listing, ListingViewModel>();
            CreateMap<ListingViewModel, Listing>();
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