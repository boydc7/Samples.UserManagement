using System;
using System.ComponentModel.DataAnnotations;
using Samples.UserManagement.Api.Enums;

namespace Samples.UserManagement.Api.Models
{
    public class User : BaseModel
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(25)]
        public string Suffix { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [MinLength(9)]
        [MaxLength(9)]
        public string SocialSecurityNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; }

        public string FullName => string.Concat(FirstName, " ", LastName).Trim();
    }
}
