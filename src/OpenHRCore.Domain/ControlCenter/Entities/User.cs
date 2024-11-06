namespace OpenHRCore.Domain.ControlCenter.Entities
{
    public class User : OpenHRCoreBaseEntity
    {
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
