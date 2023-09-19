namespace OpenHRCore.Application.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync(CancellationToken cancellationToken);
        void DisposeContext();
        Task DisposeContextAsync();
        void BeginTransaction();
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        void CommitTransaction();
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        void RollbackTransaction();
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
    }
}
