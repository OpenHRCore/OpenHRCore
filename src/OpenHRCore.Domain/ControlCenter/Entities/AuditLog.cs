namespace OpenHRCore.Domain.ControlCenter.Entities
{
    public class AuditLog : OpenHRCoreBaseEntity
    {
        public required string UserId { get; set; }
        public required virtual User User { get; set; }
        public required string Action { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
