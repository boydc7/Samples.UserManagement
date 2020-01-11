using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.DataAccess
{
    public class InMemoryApiKeyRepository : BaseInMemoryRepository<ApiKey, string>, IApiKeyRepository { }
}
