namespace OpenHRCore.SharedKernel.Application.Common
{
    /// <summary>
    /// Represents a unit of work for database operations in the OpenHR Core system.
    /// </summary>
    public interface IOpenHRCoreUnitOfWork : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Saves all changes made in this unit of work to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves all changes made in this unit of work to the database.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Asynchronously begins a new database transaction.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous begin transaction operation.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Asynchronously commits the current database transaction.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous commit operation.</returns>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Asynchronously rolls back the current database transaction.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous rollback operation.</returns>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
