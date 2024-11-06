using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Domain.PayStream.Entities
{
    public class EmployeePayroll : OpenHRCoreBaseEntity
    {
        public required string EmployeeId { get; set; }
        public required virtual Employee Employee { get; set; }
        public required string PayrollCycleId { get; set; }
        public required virtual PayrollCycle PayrollCycle { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal IncomeTax { get; set; } // Income tax amount
        public decimal SSC { get; set; } // Social security contribution
        public decimal TaxRate { get; set; } // Tax rate applied
        public decimal SSCRate { get; set; } // SSC rate applied
        public virtual ICollection<PayrollItem> PayrollItems { get; set; } = new List<PayrollItem>();
    }
}
