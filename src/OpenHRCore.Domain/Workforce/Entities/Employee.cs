namespace OpenHRCore.Domain.Workforce.Entities
{
    public class Employee : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public DateTime HireDate { get; set; }
        public required string JobPositionId { get; set; }
        public required virtual JobPosition JobPosition { get; set; }
        public required string DepartmentId { get; set; }
        public required virtual Department Department { get; set; }
        public virtual ICollection<EmployeeMovement> EmployeeMovements { get; set; } = new List<EmployeeMovement>();
        public virtual ICollection<EmployeeSalaryHistory> SalaryHistories { get; set; } = new List<EmployeeSalaryHistory>();
    }
}
