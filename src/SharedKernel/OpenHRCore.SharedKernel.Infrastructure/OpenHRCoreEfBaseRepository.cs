using Microsoft.EntityFrameworkCore;
using OpenHRCore.SharedKernel.Domain;
using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Infrastructure
{
    /// <summary>
    /// Entity Framework Core base repository implementation for OpenHR Core entities.
    /// </summary>
    /// <typeparam name="TEntity">Entity type.</typeparam>
    public class OpenHRCoreEfBaseRepository<TEntity> : IOpenHRCoreBaseRepository<TEntity>
        where TEntity : OpenHRCoreBaseEntity
    {
        protected readonly DbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreEfBaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public OpenHRCoreEfBaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <inheritdoc/>
        public void Add(TEntity entity)
        {
            ValidateEntity(entity);
            dbContext.Set<TEntity>().Add(entity);
        }

        /// <inheritdoc/>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            dbContext.Set<TEntity>().AddRange(entities);
        }

        /// <inheritdoc/>
        public async Task AddAsync(TEntity entity)
        {
            ValidateEntity(entity);
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        /// <inheritdoc/>
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            await dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> GetWhereAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            ValidatePredicate(predicate);
            var query = BuildQueryWithIncludes(includeProperties);
            return await query.Where(predicate).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetByIdAsync(Guid entityId)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(entityId);
            return entity ?? throw new EntityNotFoundException<TEntity>(entityId);
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return entity ?? throw new EntityNotFoundException<TEntity>();
        }

        /// <inheritdoc/>
        public async Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            ValidatePredicate(predicate);
            var query = BuildQueryWithIncludes(includeProperties);
            var entity = await query.FirstOrDefaultAsync(predicate);
            return entity ?? throw new EntityNotFoundException<TEntity>();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(TEntity entity)
        {
            ValidateEntity(entity);
            dbContext.Set<TEntity>().Update(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            dbContext.Set<TEntity>().UpdateRange(entities);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public void Remove(TEntity entity)
        {
            ValidateEntity(entity);
            dbContext.Set<TEntity>().Remove(entity);
        }

        /// <inheritdoc/>
        public void Remove(Guid entityId)
        {
            var entity = dbContext.Set<TEntity>().Find(entityId);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        /// <inheritdoc/>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            dbContext.Set<TEntity>().RemoveRange(entities);
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(TEntity entity)
        {
            ValidateEntity(entity);
            dbContext.Set<TEntity>().Remove(entity);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveAsync(Guid entityId)
        {
            var entity = await dbContext.Set<TEntity>().FindAsync(entityId);
            if (entity == null)
            {
                return false;
            }
            return await RemoveAsync(entity);
        }

        /// <inheritdoc/>
        public async Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            dbContext.Set<TEntity>().RemoveRange(entities);
            return await dbContext.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            ValidateSelector(selector);
            var query = dbContext.Set<TEntity>();
            return query.Max(selector) ?? default!;
        }

        /// <inheritdoc/>
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            ValidateSelector(selector);
            var query = dbContext.Set<TEntity>();
            return !await query.AnyAsync() ? default! : await query.MaxAsync(selector);
        }

        protected virtual void ValidateEntity(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

        protected virtual void ValidateEntities(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
        }

        protected virtual void ValidatePredicate(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        }

        protected virtual void ValidateSelector<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
        }

        protected virtual IQueryable<TEntity> BuildQueryWithIncludes(
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = dbContext.Set<TEntity>().AsQueryable();

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }

    /// <summary>
    /// Exception thrown when an entity is not found.
    /// </summary>
    public class EntityNotFoundException<T> : Exception
    {
        public EntityNotFoundException()
            : base($"Entity of type {typeof(T).Name} was not found.") { }

        public EntityNotFoundException(Guid id)
            : base($"Entity of type {typeof(T).Name} with id {id} was not found.") { }
    }
}