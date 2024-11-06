namespace OpenHRCore.Domain.ControlCenter.Entities
{
    public class UserRole : OpenHRCoreBaseEntity
    {
        public required string UserId { get; set; }
        public required virtual User User { get; set; }
        public required string RoleId { get; set; }
        public required virtual Role Role { get; set; }
    }
}
