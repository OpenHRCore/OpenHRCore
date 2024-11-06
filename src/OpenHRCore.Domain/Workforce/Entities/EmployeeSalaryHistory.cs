namespace OpenHRCore.Domain.Workforce.Entities
{
    public class EmployeeSalaryHistory : OpenHRCoreBaseEntity
    {
        public required string EmployeeId { get; set; }
        public required virtual Employee Employee { get; set; }
        public decimal Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
