using Microsoft.EntityFrameworkCore;
using OpenHRCore.SharedKernel.Application;

namespace OpenHRCore.SharedKernel.Infrastructure
{
    /// <summary>
    /// Implements the Unit of Work pattern for Entity Framework Core operations.
    /// </summary>
    /// <typeparam name="TDbContext">The type of DbContext to be used.</typeparam>
    public class OpenHRCoreEfUnitOfWork<TDbContext> : IOpenHRCoreUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreEfUnitOfWork{TDbContext}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context to be used for operations.</param>
        /// <exception cref="ArgumentNullException">Thrown if dbContext is null.</exception>
        public OpenHRCoreEfUnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        /// <inheritdoc/>
        public Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        /// <inheritdoc/>
        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        /// <inheritdoc/>
        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Disposes the DbContext and releases any resources used.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Asynchronously disposes the DbContext and releases any resources used.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            await DisposeAsyncCore().ConfigureAwait(false);
            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">True if disposing, false if finalizing.</param>
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

        /// <summary>
        /// Protected implementation of async Dispose pattern.
        /// </summary>
        /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
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
