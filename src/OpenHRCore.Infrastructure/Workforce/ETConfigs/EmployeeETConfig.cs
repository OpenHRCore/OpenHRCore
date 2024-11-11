namespace OpenHRCore.Infrastructure.Workforce.ETConfigs
{
    public class EmployeeETConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.FirstName).IsRequired();

            builder.Property(x => x.LastName).IsRequired();

            builder.Property(x => x.Gender).IsRequired().HasConversion<string>();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.Phone).IsRequired();

            builder.HasOne(x => x.JobLevel)
                .WithMany()
                .HasForeignKey(x => x.JobLevelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.JobGrade)
                .WithMany()
                .HasForeignKey(x => x.JobGradeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.JobPosition)
                .WithMany()
                .HasForeignKey(x => x.JobPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.OrganizationUnit)
                .WithMany()
                .HasForeignKey(x => x.OrganizationUnitId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
