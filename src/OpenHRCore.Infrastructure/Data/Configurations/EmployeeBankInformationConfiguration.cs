namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeBankInformationConfiguration : IEntityTypeConfiguration<EmployeeBankInformation>
    {
        public void Configure(EntityTypeBuilder<EmployeeBankInformation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BankName).IsRequired();
            builder.Property(x => x.AccountNumber).IsRequired();
            builder.Property(x => x.AccountHolderName).IsRequired();
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.BankInformation).IsRequired();
        }
    }
}
