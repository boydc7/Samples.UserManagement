using Samples.UserManagement.Api.Models;
using Samples.UserManagement.Api.Services;

namespace Samples.UserManagement.Api.DataAccess
{
    public class InMemoryUserRepository : BaseInMemoryModelRepository<User>, IUserRepository
    {
        public InMemoryUserRepository(ISequenceProvider sequenceProvider) : base(sequenceProvider) { }
    }
}
