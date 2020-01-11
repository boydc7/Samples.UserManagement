using Samples.UserManagement.Api.Models;
using Samples.UserManagement.Api.Services;

namespace Samples.UserManagement.Api.DataAccess
{
    public class InMemoryServiceRepository : BaseInMemoryModelRepository<Service>, IServiceRepository
    {
        public InMemoryServiceRepository(ISequenceProvider sequenceProvider) : base(sequenceProvider) { }
    }
}
