using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Domain
{
    public interface IOpenHRCoreBaseRepository<TEntity>
        where TEntity : OpenHRCoreBaseEntity
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties);
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void Remove(object id);
        void RemoveRange(IEnumerable<TEntity> entities);

        TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector);
        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector);
    }
}