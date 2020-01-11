using System.ComponentModel.DataAnnotations;

namespace Samples.UserManagement.Api.Models
{
    public class UserSubscribeRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ServiceId { get; set; }
    }
}
