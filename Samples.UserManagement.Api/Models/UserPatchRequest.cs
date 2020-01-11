using System;
using System.ComponentModel.DataAnnotations;

namespace Samples.UserManagement.Api.Models
{
    public class UserPatchReqeust
    {
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(25)]
        public string Suffix { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Range(9, 9)]
        public string SocialSecurityNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
