using OpenHRCore.Domain.EmployeeModule.Entities;

namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeIdentityCardConfiguration : IEntityTypeConfiguration<EmployeeIdentityCard>
    {
        public void Configure(EntityTypeBuilder<EmployeeIdentityCard> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.CardNumber).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.IdentityCards).IsRequired();
        }
    }
}
