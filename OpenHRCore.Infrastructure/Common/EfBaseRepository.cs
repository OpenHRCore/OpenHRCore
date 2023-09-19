using Microsoft.EntityFrameworkCore;
using OpenHRCore.Domain.Common;
using OpenHRCore.Infrastructure.Data;

namespace OpenHRCore.Infrastructure.Common
{
    public class EfBaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly OpenHRCoreDbContext _dbContext;
        public EfBaseRepository(OpenHRCoreDbContext dbContext) { _dbContext = dbContext; }
        public void Add(T entity) => _dbContext.Add(entity);
        public async Task AddAsync(T entity) => await _dbContext.AddAsync(entity);
        public void AddRange(IEnumerable<T> entities) => _dbContext.AddRange(entities);
        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbContext.AddRangeAsync(entities);
        public void Update(T entity) => _dbContext.Update(entity);
        public void UpdateRange(IEnumerable<T> entities) => _dbContext.UpdateRange(entities);
        public void Remove(T entity) => _dbContext.Remove(entity);
        public void RemoveRange(IEnumerable<T> entities) => _dbContext.RemoveRange(entities);
        public async Task<IEnumerable<T>> GetListAsync() => await _dbContext.Set<T>().ToListAsync();
    }
}
