namespace OpenHRCore.Domain.ControlCenter.Entities
{
    public class RolePermission : OpenHRCoreBaseEntity
    {
        public required string RoleId { get; set; }
        public required virtual Role Role { get; set; }
        public required string PermissionId { get; set; }
        public required virtual Permission Permission { get; set; }
    }
}
