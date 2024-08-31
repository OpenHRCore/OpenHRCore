using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.ControlCenter.Domain.Entities
{
    /// <summary>
    /// Represents the relationship between a role and a permission in the OpenHRCore system.
    /// </summary>
    public class OpenHRCoreRolePermission : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the role associated with this permission.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role associated with this permission.
        /// </summary>
        public OpenHRCoreRole? Role { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the permission associated with this role.
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the permission associated with this role.
        /// </summary>
        public OpenHRCorePermission? Permission { get; set; }
    }
}
