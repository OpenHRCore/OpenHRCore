using Microsoft.EntityFrameworkCore;
using OpenHRCore.SharedKernel.Domain.Entities;
using OpenHRCore.SharedKernel.Domain.Interfaces;
using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Infrastructure.Common
{
    /// <summary>
    /// Generic repository implementation for handling CRUD operations.
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

        /// <inheritdoc />
        public void Add(TEntity entity)
        {
            ValidateEntity(entity);
            _dbContext.Set<TEntity>().Add(entity);
        }

        /// <inheritdoc />
        public async Task AddAsync(TEntity entity)
        {
            ValidateEntity(entity);
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        /// <inheritdoc />
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

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
            var entity = GetById(id);
            Remove(entity);
        }

        /// <inheritdoc />
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetByIdAsync(object id)
        {
            ValidateId(id);
            return await _dbContext.Set<TEntity>().FindAsync(id)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        /// <inheritdoc />
        public TEntity GetById(object id)
        {
            ValidateId(id);
            return _dbContext.Set<TEntity>().Find(id)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetListAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, string includeString)
        {
            ValidatePredicate(predicate);

            var query = BuildQueryWithIncludes(includeString);
            return await query.Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        /// <inheritdoc />
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, string includeString)
        {
            ValidatePredicate(predicate);

            var query = BuildQueryWithIncludes(includeString);
            return await query.FirstOrDefaultAsync(predicate)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        private void ValidateEntity(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

        private void ValidateEntities(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
        }

        private void ValidatePredicate(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        }

        private void ValidateId(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
        }

        private IQueryable<TEntity> BuildQueryWithIncludes(string includeString)
        {
            var query = _dbContext.Set<TEntity>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                var includes = includeString.Split(",", StringSplitOptions.RemoveEmptyEntries);

                foreach (var include in includes)
                {
                    query = query.Include(include.Trim());
                }
            }

            return query;
        }
    }
}
