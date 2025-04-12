namespace Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> CommitAsync();
    }
}
