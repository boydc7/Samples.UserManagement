using System.Collections.Generic;
using System.Linq;
using Samples.UserManagement.Api.Extensions;
using Samples.UserManagement.Api.Models;
using Samples.UserManagement.Api.Services;

namespace Samples.UserManagement.Api.DataAccess
{
    public class InMemorySubscriptionRepository : BaseInMemoryModelRepository<Subscription>, ISubscriptionRepository
    {
        private readonly IServiceRepository _serviceRepository;

        public InMemorySubscriptionRepository(IServiceRepository serviceRepository,
                                              ISequenceProvider sequenceProvider)
            : base(sequenceProvider)
        {
            _serviceRepository = serviceRepository;
        }

        public IEnumerable<Service> GetUserServices(int userId)
        {
            var subscriptionServiceIds = Query(s => s.UserId == userId).Select(s => s.ServiceId)
                                                                       .AsHashSet();

            return subscriptionServiceIds.IsNullOrEmpty()
                       ? Enumerable.Empty<Service>()
                       : _serviceRepository.Query(v => subscriptionServiceIds.Contains(v.Id));
        }

        public Subscription GetSubscriptionTo(int userId, int serviceId)
        {
            return Query(s => s.UserId == userId &&
                              s.ServiceId == serviceId).FirstOrDefault();
        }
    }
}
