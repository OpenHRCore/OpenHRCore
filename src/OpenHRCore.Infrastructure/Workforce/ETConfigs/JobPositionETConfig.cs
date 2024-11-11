namespace OpenHRCore.Infrastructure.Workforce.ETConfigs
{
    public class JobPositionETConfig : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.JobTitle).IsRequired();

            builder.HasOne(x => x.OrganizationUnit)
                .WithMany(x => x.JobPositions)
                .HasForeignKey(x => x.OrganizationUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
