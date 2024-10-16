using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeEducationConfiguration : IEntityTypeConfiguration<EmployeeEducation>
    {
        public void Configure(EntityTypeBuilder<EmployeeEducation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.InstitutionName).IsRequired();
            builder.Property(x => x.Degree).IsRequired();
            builder.Property(x => x.FieldOfStudy).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.Educations).IsRequired();
        }
    }
}
