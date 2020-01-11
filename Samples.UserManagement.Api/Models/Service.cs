using System.ComponentModel.DataAnnotations;

namespace Samples.UserManagement.Api.Models
{
    public class Service : BaseModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
