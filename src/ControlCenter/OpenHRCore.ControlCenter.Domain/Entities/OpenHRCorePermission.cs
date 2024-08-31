using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.ControlCenter.Domain.Entities
{
    /// <summary>
    /// Represents a permission in the OpenHRCore system.
    /// </summary>
    public class OpenHRCorePermission : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code for the permission.
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the module that this permission belongs to.
        /// For example, "Employee Module".
        /// </summary>
        public required string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the program or action that this permission controls.
        /// For example, "Employee.Create".
        /// </summary>
        public required string ProgramName { get; set; }

        /// <summary>
        /// Navigation property for the role-permission relationships.
        /// </summary>
        public ICollection<OpenHRCoreRolePermission> RolePermissions { get; set; } = new List<OpenHRCoreRolePermission>();

        /// <summary>
        /// Navigation property for the user-permission relationships.
        /// </summary>
        public ICollection<OpenHRCoreUserPermission> UserPermissions { get; set; } = new List<OpenHRCoreUserPermission>();
    }
}
