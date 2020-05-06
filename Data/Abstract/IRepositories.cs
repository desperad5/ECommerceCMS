using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using System.Collections.Generic;

namespace ECommerceCMS.Data.Abstract
{
    public interface ITenantRepository : IEntityBaseRepository<Tenant>
    {
        IEnumerable<Tenant> GetAllTenants();
    }
    public interface IUserRepository : IEntityBaseRepository<CMSUser>
    {

    }
   

    public interface ICustomerRepository : IEntityBaseRepository<Customer>
    {

    }

    public interface IBrandRepository : IEntityBaseRepository<Brand>
    {
        List<BrandViewModel> GetBrandsOfProducts(ProductsWithCategoryModel products);
    }
}
