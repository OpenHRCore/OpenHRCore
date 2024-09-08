using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.ControlCenter.Domain.Entities
{
    /// <summary>
    /// Represents the relationship between a user and a permission in the OpenHRCore system.
    /// </summary>
    public class OpenHRCoreUserPermission : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user associated with this permission.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with this permission.
        /// </summary>
        public OpenHRCoreUser? User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the permission associated with this user.
        /// </summary>
        public Guid PermissionId { get; set; }

        /// <summary>
        /// Gets or sets the permission associated with this user.
        /// </summary>
        public OpenHRCorePermission? Permission { get; set; }
    }
}
