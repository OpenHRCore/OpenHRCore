using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeDependentConfiguration : IEntityTypeConfiguration<EmployeeDependent>
    {
        public void Configure(EntityTypeBuilder<EmployeeDependent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.Dependents).IsRequired();
        }
    }
}
