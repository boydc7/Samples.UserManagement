using System.Collections.Generic;
using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.DataAccess
{
    public interface ISubscriptionRepository : IBaseModelRepository<Subscription>
    {
        IEnumerable<Service> GetUserServices(int userId);
        Subscription GetSubscriptionTo(int userId, int serviceId);
    }
}
