using Microsoft.EntityFrameworkCore;
using OpenHRCore.SharedKernel.Domain.Entities;
using OpenHRCore.SharedKernel.Domain.Interfaces;
using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Infrastructure.Common
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
        protected readonly TDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreEfBaseRepository{TDbContext, TEntity}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public OpenHRCoreEfBaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Create Operations

        /// <inheritdoc />
        public void Add(TEntity entity)
        {
            ValidateEntity(entity);
            _dbContext.Set<TEntity>().Add(entity);
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity entity)
        {
            ValidateEntity(entity);
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        #endregion

        #region Read Operations

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
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
            return await _dbContext.Set<TEntity>().FindAsync(id)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        /// <inheritdoc />
        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate)
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
            _dbContext.Set<TEntity>().Update(entity);
        }

        /// <inheritdoc />
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }

        #endregion

        #region Delete Operations

        /// <inheritdoc />
        public void Remove(TEntity entity)
        {
            ValidateEntity(entity);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        /// <inheritdoc />
        public void Remove(object id)
        {
            ValidateId(id);
            var entity = _dbContext.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        /// <inheritdoc />
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Validates that the given entity is not null.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        protected void ValidateEntity(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

        /// <summary>
        /// Validates that the given collection of entities is not null or empty.
        /// </summary>
        /// <param name="entities">The collection of entities to validate.</param>
        protected void ValidateEntities(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
        }

        /// <summary>
        /// Validates that the given predicate is not null.
        /// </summary>
        /// <param name="predicate">The predicate to validate.</param>
        protected void ValidatePredicate(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Validates that the given id is not null.
        /// </summary>
        /// <param name="id">The id to validate.</param>
        protected void ValidateId(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
        }

        /// <summary>
        /// Builds a query with the specified include properties.
        /// </summary>
        /// <param name="includeProperties">A comma-separated list of navigation properties to include.</param>
        /// <returns>An IQueryable with the specified includes.</returns>
        protected IQueryable<TEntity> BuildQueryWithIncludes(string includeProperties)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

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
