namespace OpenHRCore.Domain.PayStream.Entities
{
    public class PayrollCycle : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual ICollection<EmployeePayroll> EmployeePayrolls { get; set; } = new List<EmployeePayroll>();
    }

}
