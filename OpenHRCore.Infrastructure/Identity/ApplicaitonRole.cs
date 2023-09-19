using Microsoft.AspNetCore.Identity;

namespace OpenHRCore.Infrastructure.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName)
        {
            Name = roleName;
        }
    }
}
