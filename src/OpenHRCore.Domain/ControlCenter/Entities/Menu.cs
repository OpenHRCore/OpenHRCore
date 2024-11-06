namespace OpenHRCore.Domain.ControlCenter.Entities
{
    public class Menu : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public int? ParentMenuId { get; set; } // Nullable to support root menus
        public Menu? ParentMenu { get; set; } // Navigation property for hierarchy
        public virtual ICollection<Menu> SubMenus { get; set; } = new List<Menu>();
    }
}
