namespace OpenHRCore.WorkForce.Infrastructure.Data.Configurations
{
    public class EmployeeWorkExperienceConfiguration : IEntityTypeConfiguration<EmployeeWorkExperience>
    {
        public void Configure(EntityTypeBuilder<EmployeeWorkExperience> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.JobTitle).IsRequired();
            builder.Property(x => x.CompanyName).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.WorkExperiences).IsRequired();
        }
    }
}
