namespace OpenHRCore.Domain.ControlCenter.Entities
{
    public class RoleMenu : OpenHRCoreBaseEntity
    {
        public required string RoleId { get; set; }
        public required virtual Role Role { get; set; }
        public required string MenuId { get; set; }
        public required virtual Menu Menu { get; set; }
    }
}
