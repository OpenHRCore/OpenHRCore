using OpenHRCore.ControlCenter.Domain.Entities;

public class MenuService
{
    private readonly List<OpenHRCoreMenu> _menus;
    private readonly List<OpenHRCoreUserRole> _userRoles;
    private readonly List<OpenHRCoreRolePermission> _rolePermissions;
    private readonly List<OpenHRCoreUserPermission> _userPermissions;

    public MenuService(
        List<OpenHRCoreMenu> menus,
        List<OpenHRCoreUserRole> userRoles,
        List<OpenHRCoreRolePermission> rolePermissions,
        List<OpenHRCoreUserPermission> userPermissions)
    {
        _menus = menus;
        _userRoles = userRoles;
        _rolePermissions = rolePermissions;
        _userPermissions = userPermissions;
    }

    public List<OpenHRCoreMenu> GetMenusForUser(Guid userId)
    {
        // Get user roles
        var roleIds = _userRoles.Where(ur => ur.UserId == userId).Select(ur => ur.RoleId).ToList();

        // Get role permissions
        var rolePermissionCodes = _rolePermissions
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.Permission?.Code)
            .ToHashSet();

        // Get user-specific permissions
        var userPermissionCodes = _userPermissions
            .Where(up => up.UserId == userId)
            .Select(up => up.Permission?.Code)
            .ToHashSet();

        // Combine role-based and user-specific permissions
        var allPermissionCodes = rolePermissionCodes.Union(userPermissionCodes).ToList();

        // Filter menus based on permissions
        return FilterMenus(_menus, allPermissionCodes);
    }

    private List<OpenHRCoreMenu> FilterMenus(List<OpenHRCoreMenu> menus, List<string?> permissionCodes)
    {
        var result = new List<OpenHRCoreMenu>();

        foreach (var menu in menus)
        {
            if (permissionCodes.Contains(menu.PermissionCode))
            {
                var visibleMenu = new OpenHRCoreMenu
                {
                    Id = menu.Id,
                    Name = menu.Name,
                    PermissionCode = menu.PermissionCode,
                    Url = menu.Url,
                    SubMenus = FilterMenus(menu.SubMenus.ToList(), permissionCodes)
                };

                result.Add(visibleMenu);
            }
        }

        return result;
    }
}
