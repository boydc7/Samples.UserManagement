using Samples.UserManagement.Api.Services;

namespace Samples.UserManagement.Api.Extensions
{
    public static class SequenceProviderExtensions
    {
        public static int Next<T>(this ISequenceProvider sequenceProvider)
            => Next(sequenceProvider, typeof(T).FullName);

        public static int Next(this ISequenceProvider sequenceProvider, string sequenceKey)
            => sequenceProvider.Increment(sequenceKey);

        public static int Peek<T>(this ISequenceProvider sequenceProvider)
            => Peek(sequenceProvider, typeof(T).FullName);

        public static int Peek(this ISequenceProvider sequenceProvider, string sequenceKey)
            => sequenceProvider.Increment(sequenceKey, 0);
    }
}
