using OpenHRCore.Domain.EmployeeModule.Entities;

namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class JobPositionConfiguration : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
        }
    }
}
