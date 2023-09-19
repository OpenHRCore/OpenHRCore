using OpenHRCore.Application.Interfaces;

namespace OpenHRCore.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OpenHRCoreDbContext _dbContext;
        public UnitOfWork(OpenHRCoreDbContext dbContext) { _dbContext = dbContext; }
        public void BeginTransaction() => _dbContext.Database.BeginTransaction();
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default) => await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        public void CommitTransaction() => _dbContext.Database.CommitTransaction();
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default) => await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        public void DisposeContext() => _dbContext?.Dispose();
        public async Task DisposeContextAsync() => await _dbContext.DisposeAsync();
        public void RollbackTransaction() => _dbContext.Database.RollbackTransaction();
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default) => await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
        public void SaveChanges() => _dbContext.SaveChanges();
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
