namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeAddressConfiguration : IEntityTypeConfiguration<EmployeeAddress>
    {
        public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.AddressLine1).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.Addresses).IsRequired();
        }
    }
}
