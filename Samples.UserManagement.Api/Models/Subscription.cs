using System.ComponentModel.DataAnnotations;

namespace Samples.UserManagement.Api.Models
{
    public class Subscription : BaseModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ServiceId { get; set; }
    }
}
