using Samples.UserManagement.Api.Interfaces;

namespace Samples.UserManagement.Api.Models
{
    public class ApiKey : IHasId<string>
    {
        public string Id { get; set; }
        public string FriendlyName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
