using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.ControlCenter.Domain.Entities
{
    /// <summary>
    /// Represents the relationship between a user and a role in the OpenHRCore system.
    /// </summary>
    public class OpenHRCoreUserRole : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user associated with this role.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with this role.
        /// </summary>
        public OpenHRCoreUser? User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the role associated with this user.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the role associated with this user.
        /// </summary>
        public OpenHRCoreRole? Role { get; set; }
    }
}
