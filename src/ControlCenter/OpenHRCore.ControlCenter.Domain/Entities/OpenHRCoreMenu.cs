using OpenHRCore.SharedKernel.Domain;

namespace OpenHRCore.ControlCenter.Domain.Entities
{
    /// <summary>
    /// Represents a menu item in the OpenHRCore system.
    /// </summary>
    public class OpenHRCoreMenu : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the menu item.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the code for the permission required to access this menu item.
        /// </summary>
        public required string PermissionCode { get; set; }

        /// <summary>
        /// Gets or sets the URL or action associated with this menu item.
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Gets or sets the list of sub-menu items under this menu item.
        /// </summary>
        public ICollection<OpenHRCoreMenu> SubMenus { get; set; } = new List<OpenHRCoreMenu>();
    }
}
