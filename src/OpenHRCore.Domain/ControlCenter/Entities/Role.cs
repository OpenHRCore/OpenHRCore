namespace OpenHRCore.Domain.ControlCenter.Entities
{
    public class Role : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
        public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
    }
}
