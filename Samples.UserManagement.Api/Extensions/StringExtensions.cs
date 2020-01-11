namespace Samples.UserManagement.Api.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string source)
            => string.IsNullOrEmpty(source);

        public static string ToNullIfEmpty(this string source)
            => string.IsNullOrEmpty(source)
                   ? null
                   : source;
    }
}
