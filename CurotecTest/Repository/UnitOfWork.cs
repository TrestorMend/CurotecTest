using Communique.Repository;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;
        private readonly CurotecDbContext _context;

        public UnitOfWork(CurotecDbContext context) =>
            _context = context;

        public async Task<int> CommitAsync() =>
            await _context.SaveChangesAsync();

        protected virtual void DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.DisposeAsync();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            DisposeAsync(true);
            GC.SuppressFinalize(this);
        }
    }
}
