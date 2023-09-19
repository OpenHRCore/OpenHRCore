using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenHRCore.Infrastructure.Identity;
using System.Reflection;

namespace OpenHRCore.Infrastructure.Data
{
    public class OpenHRCoreDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public OpenHRCoreDbContext(DbContextOptions<OpenHRCoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
