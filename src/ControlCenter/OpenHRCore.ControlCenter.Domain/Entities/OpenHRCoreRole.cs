using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.ControlCenter.Domain.Entities
{
    /// <summary>
    /// Represents a role in the OpenHRCore system.
    /// </summary>
    public class OpenHRCoreRole : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public required string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the description of the role.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Indicates whether the role is built-in.
        /// </summary>
        public bool IsBuiltIn { get; set; } = false;

        /// <summary>
        /// Navigation property for the user-role relationships.
        /// </summary>
        public ICollection<OpenHRCoreUserRole> UserRoles { get; set; } = new List<OpenHRCoreUserRole>();

        /// <summary>
        /// Navigation property for the role-permission relationships.
        /// </summary>
        public ICollection<OpenHRCoreRolePermission> RolePermissions { get; set; } = new List<OpenHRCoreRolePermission>();
    }
}
