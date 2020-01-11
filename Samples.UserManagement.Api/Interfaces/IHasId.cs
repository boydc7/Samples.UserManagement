namespace Samples.UserManagement.Api.Interfaces
{
    public interface IHasId<T>
    {
        public T Id { get; set; }
    }

    public interface IHasIntId : IHasId<int> { }
}
