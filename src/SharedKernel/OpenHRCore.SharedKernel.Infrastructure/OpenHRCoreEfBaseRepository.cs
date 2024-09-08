using Microsoft.EntityFrameworkCore;
using OpenHRCore.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OpenHRCore.SharedKernel.Infrastructure
{
    /// <summary>
    /// Generic repository implementation for handling CRUD operations using Entity Framework Core.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class OpenHRCoreEfBaseRepository<TDbContext, TEntity> : IOpenHRCoreBaseRepository<TEntity>
        where TDbContext : DbContext
        where TEntity : OpenHRCoreBaseEntity
    {
        protected readonly TDbContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreEfBaseRepository{TDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <exception cref="ArgumentNullException">Thrown when dbContext is null.</exception>
        public OpenHRCoreEfBaseRepository(TDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Create Operations

        /// <inheritdoc />
        public void Add(TEntity entity)
        {
            ValidateEntity(entity);
            DbContext.Set<TEntity>().Add(entity);
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            DbContext.Set<TEntity>().AddRange(entities);
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity entity)
        {
            ValidateEntity(entity);
            await DbContext.Set<TEntity>().AddAsync(entity);
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            await DbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        #endregion

        #region Read Operations

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await DbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties)
        {
            ValidatePredicate(predicate);
            var query = BuildQueryWithIncludes(includeProperties);
            return await query.Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync(object id)
        {
            ValidateId(id);
            return await DbContext.Set<TEntity>().FindAsync(id)
                ?? throw new InvalidOperationException($"Entity with id {id} not found.");
        }

        /// <inheritdoc />
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        /// <inheritdoc />
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties)
        {
            ValidatePredicate(predicate);
            var query = BuildQueryWithIncludes(includeProperties);
            return await query.FirstOrDefaultAsync(predicate)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        #endregion

        #region Update Operations

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            ValidateEntity(entity);
            DbContext.Set<TEntity>().Update(entity);
        }

        /// <inheritdoc />
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            DbContext.Set<TEntity>().UpdateRange(entities);
        }

        #endregion

        #region Delete Operations

        /// <inheritdoc />
        public void Remove(TEntity entity)
        {
            ValidateEntity(entity);
            DbContext.Set<TEntity>().Remove(entity);
        }

        /// <inheritdoc />
        public void Remove(object id)
        {
            ValidateId(id);
            var entity = DbContext.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        /// <inheritdoc />
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            DbContext.Set<TEntity>().RemoveRange(entities);
        }

        #endregion

        #region Aggregate Operations

        /// <inheritdoc />
        public TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            ValidateSelector(selector);
            return DbContext.Set<TEntity>().Max(selector) ?? default!;
        }

        /// <inheritdoc />
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            ValidateSelector(selector);
            return await DbContext.Set<TEntity>().MaxAsync(selector);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Validates that the given entity is not null.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when entity is null.</exception>
        protected void ValidateEntity(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// Validates that the given collection of entities is not null or empty.
        /// </summary>
        /// <param name="entities">The collection of entities to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when entities is null or empty.</exception>
        protected void ValidateEntities(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
        }

        /// <summary>
        /// Validates that the given predicate is not null.
        /// </summary>
        /// <param name="predicate">The predicate to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when predicate is null.</exception>
        protected void ValidatePredicate(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Validates that the given id is not null.
        /// </summary>
        /// <param name="id">The id to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when id is null.</exception>
        protected void ValidateId(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Validates that the given selector is not null.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="selector">The selector to validate.</param>
        /// <exception cref="ArgumentNullException">Thrown when selector is null.</exception>
        protected void ValidateSelector<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
        }

        /// <summary>
        /// Builds a query with the specified include properties.
        /// </summary>
        /// <param name="includeProperties">A comma-separated list of navigation properties to include.</param>
        /// <returns>An IQueryable with the specified includes.</returns>
        protected IQueryable<TEntity> BuildQueryWithIncludes(string includeProperties)
        {
            var query = DbContext.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty.Trim());
                }
            }

            return query;
        }

        #endregion
    }
}
