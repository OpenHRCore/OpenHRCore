namespace OpenHRCore.ControlCenter.Domain.Entities
{
    public static class DataInitializer
    {
        public static List<OpenHRCoreRole> InitializeRoles()
        {
            return new List<OpenHRCoreRole>
            {
                new OpenHRCoreRole { Id = Guid.NewGuid(), RoleName = "Admin", Description = "Administrator with full access" },
                new OpenHRCoreRole { Id = Guid.NewGuid(), RoleName = "Manager", Description = "Manager with limited access" },
                new OpenHRCoreRole { Id = Guid.NewGuid(), RoleName = "HR", Description = "Human Resources role with specific access" }
            };
        }

        public static List<OpenHRCorePermission> InitializePermissions()
        {
            return new List<OpenHRCorePermission>
            {
                new OpenHRCorePermission { Id = Guid.NewGuid(), Code = "Employee.Create", ModuleName = "Employee Module", ProgramName = "Employee.Create" },
                new OpenHRCorePermission { Id = Guid.NewGuid(), Code = "Employee.View", ModuleName = "Employee Module", ProgramName = "Employee.View" },
                new OpenHRCorePermission { Id = Guid.NewGuid(), Code = "Employee.Delete", ModuleName = "Employee Module", ProgramName = "Employee.Delete" },
                new OpenHRCorePermission { Id = Guid.NewGuid(), Code = "HR.Edit", ModuleName = "HR Module", ProgramName = "HR.Edit" }
            };
        }

        public static List<OpenHRCoreUser> InitializeUsers()
        {
            return new List<OpenHRCoreUser>
            {
                new OpenHRCoreUser { Id = Guid.NewGuid(), UserName = "admin", PasswordHash = "adminpass", Email = "admin@example.com", IsEmailConfirmed = true, PhoneNumber = "1234567890", IsPhoneNumberConfirmed = true },
                new OpenHRCoreUser { Id = Guid.NewGuid(), UserName = "manager", PasswordHash = "managerpass", Email = "manager@example.com", IsEmailConfirmed = true, PhoneNumber = "0987654321", IsPhoneNumberConfirmed = true },
                new OpenHRCoreUser { Id = Guid.NewGuid(), UserName = "hr", PasswordHash = "hrpass", Email = "hr@example.com", IsEmailConfirmed = true, PhoneNumber = "1122334455", IsPhoneNumberConfirmed = true }
            };
        }

        public static List<OpenHRCoreUserRole> InitializeUserRoles(List<OpenHRCoreUser> users, List<OpenHRCoreRole> roles)
        {
            return new List<OpenHRCoreUserRole>
            {
                new OpenHRCoreUserRole { Id = Guid.NewGuid(), UserId = users[0].Id, RoleId = roles[0].Id }, // Admin
                new OpenHRCoreUserRole { Id = Guid.NewGuid(), UserId = users[1].Id, RoleId = roles[1].Id }, // Manager
                new OpenHRCoreUserRole { Id = Guid.NewGuid(), UserId = users[2].Id, RoleId = roles[2].Id }  // HR
            };
        }

        public static List<OpenHRCoreRolePermission> InitializeRolePermissions(List<OpenHRCoreRole> roles, List<OpenHRCorePermission> permissions)
        {
            return new List<OpenHRCoreRolePermission>
            {
                new OpenHRCoreRolePermission { Id = Guid.NewGuid(), RoleId = roles[0].Id, PermissionId = permissions[0].Id }, // Admin can create
                new OpenHRCoreRolePermission { Id = Guid.NewGuid(), RoleId = roles[0].Id, PermissionId = permissions[1].Id }, // Admin can view
                new OpenHRCoreRolePermission { Id = Guid.NewGuid(), RoleId = roles[0].Id, PermissionId = permissions[2].Id }, // Admin can delete
                new OpenHRCoreRolePermission { Id = Guid.NewGuid(), RoleId = roles[1].Id, PermissionId = permissions[1].Id }, // Manager can view
                new OpenHRCoreRolePermission { Id = Guid.NewGuid(), RoleId = roles[2].Id, PermissionId = permissions[3].Id }  // HR can edit HR records
            };
        }

        public static List<OpenHRCoreUserPermission> InitializeUserPermissions(List<OpenHRCoreUser> users, List<OpenHRCorePermission> permissions)
        {
            return new List<OpenHRCoreUserPermission>
            {
                new OpenHRCoreUserPermission { Id = Guid.NewGuid(), UserId = users[0].Id, PermissionId = permissions[0].Id }, // Admin specific permission
                new OpenHRCoreUserPermission { Id = Guid.NewGuid(), UserId = users[1].Id, PermissionId = permissions[2].Id }  // Manager specific permission
            };
        }

        public static List<OpenHRCoreMenu> InitializeMenus()
        {
            return new List<OpenHRCoreMenu>
            {
                new OpenHRCoreMenu { Id = Guid.NewGuid(), Name = "Employee Management", PermissionCode = "Employee.View", Url = "/employee", SubMenus = new List<OpenHRCoreMenu>
                {
                    new OpenHRCoreMenu { Id = Guid.NewGuid(), Name = "Create Employee", PermissionCode = "Employee.Create", Url = "/employee/create" },
                    new OpenHRCoreMenu { Id = Guid.NewGuid(), Name = "Delete Employee", PermissionCode = "Employee.Delete", Url = "/employee/delete" }
                }},
                new OpenHRCoreMenu { Id = Guid.NewGuid(), Name = "HR Management", PermissionCode = "HR.Edit", Url = "/hr" }
            };
        }
    }
}
