using Microsoft.EntityFrameworkCore;
using OpenHRCore.SharedKernel.Domain;
using System.Linq.Expressions;

namespace OpenHRCore.SharedKernel.Infrastructure
{
    public class OpenHRCoreEfBaseRepository<TEntity> : IOpenHRCoreBaseRepository<TEntity>
        where TEntity : OpenHRCoreBaseEntity
    {
        protected DbContext _dbContext;

        public OpenHRCoreEfBaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void Add(TEntity entity)
        {
            ValidateEntity(entity);
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task AddAsync(TEntity entity)
        {
            ValidateEntity(entity);
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties)
        {
            ValidatePredicate(predicate);
            var query = BuildQueryWithIncludes(includeProperties);
            return await query.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            ValidateId(id);
            return await _dbContext.Set<TEntity>().FindAsync(id)
                ?? throw new InvalidOperationException($"Entity with id {id} not found.");
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            ValidatePredicate(predicate);
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties)
        {
            ValidatePredicate(predicate);
            var query = BuildQueryWithIncludes(includeProperties);
            return await query.FirstOrDefaultAsync(predicate)
                ?? throw new InvalidOperationException("Entity not found.");
        }

        public void Update(TEntity entity)
        {
            ValidateEntity(entity);
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }

        public void Remove(TEntity entity)
        {
            ValidateEntity(entity);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Remove(object id)
        {
            ValidateId(id);
            var entity = _dbContext.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            ValidateEntities(entities);
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public TResult Max<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            ValidateSelector(selector);

            var maxValue = default(TResult);

            bool hasValues = _dbContext.Set<TEntity>().Select(selector).Any();

            if (hasValues)
            {
                maxValue = _dbContext.Set<TEntity>().Select(selector).Max();
            }

            return maxValue ?? default!;
        }

        public async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            ValidateSelector(selector);

            var maxValue = default(TResult);

            bool hasValues = await _dbContext.Set<TEntity>().Select(selector).AnyAsync();

            if (hasValues)
            {
                maxValue = await _dbContext.Set<TEntity>().Select(selector).MaxAsync();
            }

            return maxValue!;
        }

        protected void ValidateEntity(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
        }

        protected void ValidateEntities(IEnumerable<TEntity> entities)
        {
            if (entities == null || !entities.Any()) throw new ArgumentNullException(nameof(entities));
        }

        protected void ValidatePredicate(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        }

        protected void ValidateId(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
        }

        protected void ValidateSelector<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
        }

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
    }
}