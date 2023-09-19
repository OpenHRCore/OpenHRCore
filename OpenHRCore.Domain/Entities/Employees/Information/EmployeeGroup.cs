using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Information
{
    public class EmployeeGroup : BaseEntity
    {
        public string EmployeeGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public ICollection<EmployeeGroupItem> EmployeeGroupItems { get; set; }
    }
}
