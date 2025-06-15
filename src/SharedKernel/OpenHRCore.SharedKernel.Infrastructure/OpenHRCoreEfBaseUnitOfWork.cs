using Microsoft.EntityFrameworkCore;
using OpenHRCore.SharedKernel.Application;

namespace OpenHRCore.SharedKernel.Infrastructure
{
    public class OpenHRCoreEfBaseUnitOfWork<TDbContext> : IOpenHRCoreBaseUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private bool _disposed;

        public OpenHRCoreEfBaseUnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                _disposed = true;
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (!_disposed)
            {
                await _dbContext.DisposeAsync().ConfigureAwait(false);
                _disposed = true;
            }
        }
    }
}