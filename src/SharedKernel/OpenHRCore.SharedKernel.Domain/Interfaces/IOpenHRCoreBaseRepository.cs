using OpenHRCore.SharedKernel.Domain.Entities;
using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for a generic repository for entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IOpenHRCoreBaseRepository<TEntity> where TEntity : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Asynchronously adds the specified entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Adds the specified collection of entities to the repository.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously adds the specified collection of entities to the repository.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates the specified entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Updates the specified collection of entities in the repository.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Removes the specified entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Removes the entity with the specified identifier from the repository.
        /// </summary>
        /// <param name="id">The identifier of the entity to remove.</param>
        void Remove(object id);

        /// <summary>
        /// Removes the specified collection of entities from the repository.
        /// </summary>
        /// <param name="entities">The entities to remove.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Asynchronously retrieves all entities from the repository.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing the collection of entities.</returns>
        Task<IEnumerable<TEntity>> GetListAsync();

        /// <summary>
        /// Asynchronously retrieves entities that match the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities.</param>
        /// <returns>A task representing the asynchronous operation, containing the collection of matching entities.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Asynchronously retrieves entities that match the specified predicate and includes related entities.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities.</param>
        /// <param name="includeString">A comma-separated list of related entities to include.</param>
        /// <returns>A task representing the asynchronous operation, containing the collection of matching entities.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string includeString);

        /// <summary>
        /// Asynchronously retrieves the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the entity, or null if not found.</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// Retrieves the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>The entity, or null if not found.</returns>
        TEntity GetById(object id);

        /// <summary>
        /// Asynchronously retrieves the first entity that matches the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities.</param>
        /// <returns>A task representing the asynchronous operation, containing the entity, or null if not found.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Asynchronously retrieves the first entity that matches the specified predicate and includes related entities.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities.</param>
        /// <param name="includeString">A comma-separated list of related entities to include.</param>
        /// <returns>A task representing the asynchronous operation, containing the entity, or null if not found.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string includeString);
    }
}
