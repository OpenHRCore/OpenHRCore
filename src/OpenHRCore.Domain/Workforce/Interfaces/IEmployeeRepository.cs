namespace OpenHRCore.Domain.Workforce.Interfaces
{
    public interface IEmployeeRepository : IOpenHRCoreBaseRepository<Employee>
    {
        IQueryable<Employee> GetQueryable();
    }
}
