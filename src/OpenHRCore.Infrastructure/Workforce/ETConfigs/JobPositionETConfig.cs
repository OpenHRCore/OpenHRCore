namespace OpenHRCore.Infrastructure.Workforce.ETConfigs
{
    public class JobPositionETConfig : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.Title).IsRequired();

            builder.HasOne(x => x.JobLevel)
                .WithMany()
                .HasForeignKey(x => x.JobLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.JobGrade)
                .WithMany()
                .HasForeignKey(x => x.JobGradeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OrganizationUnit)
                .WithMany()
                .HasForeignKey(x => x.OrganizationUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
