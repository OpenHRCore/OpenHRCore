namespace OpenHRCore.Domain.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        void Add(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> GetListAsync();

    }
}
