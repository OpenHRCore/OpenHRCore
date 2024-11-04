using Microsoft.AspNetCore.Identity;

namespace OpenHRCore.Infrastructure.Identity
{
    public class OpenHRCoreUser : IdentityUser
    {
        public virtual ICollection<OpenHRCoreUserClaim>? Claims { get; set; }
        public virtual ICollection<OpenHRCoreUserLogin>? Logins { get; set; }
        public virtual ICollection<OpenHRCoreUserToken>? Tokens { get; set; }
        public virtual ICollection<OpenHRCoreUserRole>? UserRoles { get; set; }
    }

    public class OpenHRCoreRole : IdentityRole
    {
        public virtual ICollection<OpenHRCoreUserRole>? UserRoles { get; set; }
        public virtual ICollection<OpenHRCoreRoleClaim>? RoleClaims { get; set; }
    }

    public class OpenHRCoreUserRole : IdentityUserRole<string>
    {
        public required virtual OpenHRCoreUser User { get; set; }
        public required virtual OpenHRCoreRole Role { get; set; }
    }

    public class OpenHRCoreUserClaim : IdentityUserClaim<string>
    {
        public required virtual OpenHRCoreUser User { get; set; }
    }

    public class OpenHRCoreUserLogin : IdentityUserLogin<string>
    {
        public required virtual OpenHRCoreUser User { get; set; }
    }

    public class OpenHRCoreRoleClaim : IdentityRoleClaim<string>
    {
        public required virtual OpenHRCoreRole Role { get; set; }
    }

    public class OpenHRCoreUserToken : IdentityUserToken<string>
    {
        public required virtual OpenHRCoreUser User { get; set; }
    }
}
