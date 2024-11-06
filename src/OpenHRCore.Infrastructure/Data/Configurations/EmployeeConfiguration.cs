using OpenHRCore.Domain.EmployeeModule.Entities;

namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeInfo>
    {
        public void Configure(EntityTypeBuilder<EmployeeInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.PersonalEmail).IsRequired();
            builder.Property(x => x.PhoneNumbers).IsRequired();

            builder.HasMany(x => x.Addresses).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.Educations).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.Documents).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.WorkExperiences).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.Dependents).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.EmergencyContacts).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.BankInformation).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.IdentityCards).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);

        }
    }
}
