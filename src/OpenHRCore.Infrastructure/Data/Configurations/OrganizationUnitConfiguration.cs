using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class OrganizationUnitConfiguration : IEntityTypeConfiguration<OrganizationUnit>
    {
        public void Configure(EntityTypeBuilder<OrganizationUnit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
        }
    }
}
