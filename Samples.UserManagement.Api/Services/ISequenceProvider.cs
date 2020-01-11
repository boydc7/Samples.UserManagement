namespace Samples.UserManagement.Api.Services
{
    public interface ISequenceProvider
    {
        int Increment(string key, int amount = 1);
        void Reset(string key, int startingAt = 0);
    }
}
