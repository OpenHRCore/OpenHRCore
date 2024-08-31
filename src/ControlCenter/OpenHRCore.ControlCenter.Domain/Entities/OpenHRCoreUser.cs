using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.ControlCenter.Domain.Entities
{
    /// <summary>
    /// Represents a user in the OpenHRCore system.
    /// </summary>
    public class OpenHRCoreUser : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password hash of the user.
        /// </summary>
        public required string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user's email address is confirmed.
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the user.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user's phone number is confirmed.
        /// </summary>
        public bool IsPhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Indicates whether the user is built-in.
        /// </summary>
        public bool IsBuiltIn { get; set; } = false;

        /// <summary>
        /// Navigation property for the roles assigned to the user.
        /// </summary>
        public ICollection<OpenHRCoreUserRole> UserRoles { get; set; } = new List<OpenHRCoreUserRole>();

        /// <summary>
        /// Navigation property for the permissions assigned to the user.
        /// </summary>
        public ICollection<OpenHRCoreUserPermission> UserPermissions { get; set; } = new List<OpenHRCoreUserPermission>();
    }
}
