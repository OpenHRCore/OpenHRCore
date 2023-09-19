using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Information
{
    public class EmployeeGroupItem : BaseEntity
    {
        public string EmployeeGroupItemId { get; set; }
        public string EmployeeGroupId { get; set; }
        public virtual EmployeeGroup EmployeeGroup { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
