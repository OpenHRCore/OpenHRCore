using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Domain
{
    /// <summary>
    /// Generic repository interface for OpenHR Core entities.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public interface IOpenHRCoreBaseRepository<TEntity>
        where TEntity : OpenHRCoreBaseEntity
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetWhereAsync(
            Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetWhereAsync(
            Expression<Func<TEntity, bool>> predicate, 
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetByIdAsync(Guid entityId);
        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate, 
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);    
        void Remove(Guid entityId);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<bool> RemoveAsync(TEntity entity);
        Task<bool> RemoveAsync(Guid entityId);
        Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities);

        TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector);
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
    }
}