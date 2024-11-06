using OpenHRCore.Domain.Workforce.Enums;

namespace OpenHRCore.Domain.Workforce.Entities
{
    public class MovementChange : OpenHRCoreBaseEntity
    {
        public required string EmployeeMovementId { get; set; }
        public required virtual EmployeeMovement EmployeeMovement { get; set; }
        public ChangeType ChangeType { get; set; }
        public string? FromValue { get; set; }
        public string? ToValue { get; set; }
        public string? ChangeReason { get; set; } // Reason for the change
    }
}
