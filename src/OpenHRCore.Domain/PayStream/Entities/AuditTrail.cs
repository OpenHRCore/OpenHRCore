namespace OpenHRCore.Domain.PayStream.Entities
{
    public class AuditTrail : OpenHRCoreBaseEntity
    {
        public required string EntityName { get; set; } // e.g., EmployeePayroll, PayrollItem
        public required string Action { get; set; } // e.g., Insert, Update, Delete
        public required string ChangedBy { get; set; } // User who made the change
        public DateTime ChangedOn { get; set; }
        public required string Details { get; set; } // Description of what changed
    }
}
