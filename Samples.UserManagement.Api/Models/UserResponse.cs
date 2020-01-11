using System.Collections.Generic;

namespace Samples.UserManagement.Api.Models
{
    public class UserResponse
    {
        public User User { get; set; }
        public IReadOnlyList<Service> Subscriptions { get; set; }
    }
}
