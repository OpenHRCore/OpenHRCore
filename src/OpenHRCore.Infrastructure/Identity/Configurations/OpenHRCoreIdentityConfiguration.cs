namespace OpenHRCore.Infrastructure.Identity.Configurations
{
    public class OpenHRCoreUserConfiguration : IEntityTypeConfiguration<OpenHRCoreUser>
    {
        public void Configure(EntityTypeBuilder<OpenHRCoreUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("OpenHRCoreUsers");

            // Each User can have many UserClaims
            builder.HasMany(e => e.Claims).WithOne(e => e.User).HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany(e => e.Logins).WithOne(e => e.User).HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany(e => e.Tokens).WithOne(e => e.User).HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles).WithOne(e => e.User).HasForeignKey(ur => ur.UserId).IsRequired();

        }
    }

    public class OpenHRCoreRoleConfiguration : IEntityTypeConfiguration<OpenHRCoreRole>
    {
        public void Configure(EntityTypeBuilder<OpenHRCoreRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("OpenHRCoreRoles");

            // Each Role can have many entries in the UserRole join table
            builder.HasMany(e => e.UserRoles).WithOne(e => e.Role).HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany(e => e.RoleClaims).WithOne(e => e.Role).HasForeignKey(rc => rc.RoleId).IsRequired();
        }
    }

    public class OpenHRCoreUserRoleConfiguration : IEntityTypeConfiguration<OpenHRCoreUserRole>
    {
        public void Configure(EntityTypeBuilder<OpenHRCoreUserRole> builder)
        {
            builder.ToTable("OpenHRCoreUserRoles");
        }
    }

    public class OpenHRCoreUserClaimConfiguration : IEntityTypeConfiguration<OpenHRCoreUserClaim>
    {
        public void Configure(EntityTypeBuilder<OpenHRCoreUserClaim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("OpenHRCoreUserClaims");
        }
    }

    public class OpenHRCoreUserLoginConfiguration : IEntityTypeConfiguration<OpenHRCoreUserLogin>
    {
        public void Configure(EntityTypeBuilder<OpenHRCoreUserLogin> builder)
        {
            builder.ToTable("OpenHRCoreUserLogins");
        }
    }

    public class OpenHRCoreRoleClaimConfiguration : IEntityTypeConfiguration<OpenHRCoreRoleClaim>
    {
        public void Configure(EntityTypeBuilder<OpenHRCoreRoleClaim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("OpenHRCoreRoleClaims");
        }
    }

    public class OpenHRCoreUserTokenConfiguration : IEntityTypeConfiguration<OpenHRCoreUserToken>
    {
        public void Configure(EntityTypeBuilder<OpenHRCoreUserToken> builder)
        {
            builder.ToTable("OpenHRCoreUserTokens");
        }
    }

}
