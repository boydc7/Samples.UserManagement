using Samples.UserManagement.Api.Models;

namespace Samples.UserManagement.Api.Extensions
{
    public static class Transforms
    {
        public static void PopulateWith(this User source, UserPatchReqeust with)
        {
            // Normally would use a mapper here, but for now...
            source.FirstName = (with.FirstName ?? source.FirstName).ToNullIfEmpty();
            source.LastName = (with.LastName ?? source.LastName).ToNullIfEmpty();
            source.Title = (with.Title ?? source.Title).ToNullIfEmpty();
            source.Suffix = (with.Suffix ?? source.Suffix).ToNullIfEmpty();
            source.DateOfBirth = with.DateOfBirth ?? source.DateOfBirth;
            source.SocialSecurityNumber = (with.SocialSecurityNumber ?? source.SocialSecurityNumber).ToNullIfEmpty();
            source.Email = (with.Email ?? source.Email).ToNullIfEmpty();
            source.Password = (with.Password ?? source.Password).ToNullIfEmpty();
        }

        public static void PopulateWith(this User source, AdminUserPatchReqeust with)
        {
            // Normally would use a mapper here, but for now...
            source.Status = with.Status;
        }
    }
}
