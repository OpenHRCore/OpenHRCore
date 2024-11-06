using OpenHRCore.Domain.EmployeeModule.Entities;

namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeEmergencyContactConfiguration : IEntityTypeConfiguration<EmployeeEmergencyContact>
    {
        public void Configure(EntityTypeBuilder<EmployeeEmergencyContact> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Relationship).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.EmergencyContacts).IsRequired();
        }
    }
}
