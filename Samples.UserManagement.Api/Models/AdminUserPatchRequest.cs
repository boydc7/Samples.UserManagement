using System.ComponentModel.DataAnnotations;
using Samples.UserManagement.Api.Enums;

namespace Samples.UserManagement.Api.Models
{
    public class AdminUserPatchReqeust
    {
        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; }
    }
}
