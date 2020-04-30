using SorubankCMS.Data.Entity;
using SorubankCMS.Data.Entity.Customer;
using System.Collections.Generic;

namespace SorubankCMS.Data.Abstract
{
    public interface ITenantRepository : IEntityBaseRepository<Tenant>
    {
        IEnumerable<Tenant> GetAllTenants();
    }
    public interface IUserRepository : IEntityBaseRepository<CMSUser>
    {

    }
    public interface ITopicRepository : IEntityBaseRepository<Topic>
    {
        IEnumerable<Topic> GetAllActiveTopics();
    }

    public interface ICustomerRepository : IEntityBaseRepository<SorubankUser>
    {

    }


}
