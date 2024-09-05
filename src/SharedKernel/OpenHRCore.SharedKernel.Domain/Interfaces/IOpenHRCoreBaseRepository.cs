using OpenHRCore.SharedKernel.Domain.Entities;
using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Domain.Interfaces
{
    /// <summary>
    /// Defines a generic repository interface for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity this repository handles.</typeparam>
    public interface IOpenHRCoreBaseRepository<TEntity> where TEntity : OpenHRCoreBaseEntity
    {
        #region Create Operations

        /// <summary>
        /// Adds a single entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Adds multiple entities to the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously adds a single entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Asynchronously adds multiple entities to the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Read Operations

        /// <summary>
        /// Asynchronously retrieves all entities from the repository.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves entities that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition to filter entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities that match the condition.</returns>
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Asynchronously retrieves entities that match the specified predicate and includes related entities.
        /// </summary>
        /// <param name="predicate">The condition to filter entities.</param>
        /// <param name="includeProperties">A comma-separated list of related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entities that match the condition, including specified related entities.</returns>
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties);

        /// <summary>
        /// Asynchronously retrieves an entity by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified identifier, or null if not found.</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// Asynchronously retrieves the first entity that matches the specified predicate.
        /// </summary>
        /// <param name="predicate">The condition to filter entities.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that matches the condition, or null if not found.</returns>
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Asynchronously retrieves the first entity that matches the specified predicate and includes related entities.
        /// </summary>
        /// <param name="predicate">The condition to filter entities.</param>
        /// <param name="includeProperties">A comma-separated list of related entities to include.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that matches the condition, including specified related entities, or null if not found.</returns>
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties);

        #endregion

        #region Update Operations

        /// <summary>
        /// Updates a single entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates multiple entities in the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to update.</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        #endregion

        #region Delete Operations

        /// <summary>
        /// Removes a single entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes an entity with the specified identifier from the repository.
        /// </summary>
        /// <param name="id">The identifier of the entity to remove.</param>
        void Remove(object id);

        /// <summary>
        /// Removes multiple entities from the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to remove.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        #endregion
    }
}
