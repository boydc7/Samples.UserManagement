using System.Collections.Generic;
using System.Linq;

namespace Samples.UserManagement.Api.Extensions
{
    public static class EnumerableExtensions
    {
        public static IReadOnlyList<T> AsListReadOnly<T>(this IEnumerable<T> source)
            => source?.AsList().AsReadOnly();

        public static List<T> AsList<T>(this IEnumerable<T> source)
        {
            return source switch
            {
                null => null,
                List<T> l => l,
                _ => source.ToList()
            };
        }

        public static HashSet<T> AsHashSet<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer = null)
        {
            return source switch
            {
                null => null,
                HashSet<T> h => h,
                _ => (comparer == null
                          ? new HashSet<T>(source)
                          : new HashSet<T>(source, comparer))
            };
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
            => source == null || source.Count <= 0;
    }
}
