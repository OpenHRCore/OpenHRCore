namespace OpenHRCore.SharedKernel.Application
{
    public interface IOpenHRCoreBaseUnitOfWork : IDisposable, IAsyncDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        void CommitTransaction();
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        void RollbackTransaction();
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}