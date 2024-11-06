namespace OpenHRCore.Domain.Workforce.Entities
{
    public class Department : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public string? Location { get; set; }
        public string? ParentDepartmentId { get; set; } // Nullable to support root departments
        public virtual Department? ParentDepartment { get; set; } // Navigation property for hierarchy
        public virtual ICollection<Department> SubDepartments { get; set; } = new List<Department>(); // Sub-departments under this department
    }
}
