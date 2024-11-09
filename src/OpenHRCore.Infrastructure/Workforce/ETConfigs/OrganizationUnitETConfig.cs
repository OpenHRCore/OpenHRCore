namespace OpenHRCore.Infrastructure.Workforce.ETConfigs
{
    public class OrganizationUnitETConfig : IEntityTypeConfiguration<OrganizationUnit>
    {
        public void Configure(EntityTypeBuilder<OrganizationUnit> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.Name).IsRequired();

            builder.Property(x => x.SortOrder).IsRequired();

            builder.HasOne(x => x.ParentOrganizationUnit)
                .WithMany(x => x.SubOrganizationUnits)
                .HasForeignKey(x => x.ParentOrganizationUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
