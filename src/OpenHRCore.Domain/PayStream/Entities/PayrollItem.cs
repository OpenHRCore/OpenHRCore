using OpenHRCore.Domain.PayStream.Enums;

namespace OpenHRCore.Domain.PayStream.Entities
{
    public class PayrollItem : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; } // e.g., Performance Bonus, Housing Allowance, Income Tax
        public decimal Amount { get; set; }
        public PayrollItemType ItemType { get; set; } // Enum to define whether it's Allowance, Deduction, Pay Item
        public required string EmployeePayrollId { get; set; }
        public required virtual EmployeePayroll EmployeePayroll { get; set; }
    }
}
