using OpenHRCore.Domain.Workforce.Enums;

namespace OpenHRCore.Domain.Workforce.Entities
{
    public class EmployeeMovement : OpenHRCoreBaseEntity
    {
        public required string EmployeeId { get; set; }
        public required virtual Employee Employee { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime MovementDate { get; set; }
        public virtual ICollection<MovementChange> MovementChanges { get; set; } = new List<MovementChange>();
        public string? Comments { get; set; }
    }
}
