using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Setup
{
    public class Department : BaseEntity
    {
        public string DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentDepartmentId { get; set; }
        public virtual Department ParentDepartment { get; set; }
        public ICollection<Department> ChildDepartments { get; set; }

    }
}
