namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class JobLevelConfiguration : IEntityTypeConfiguration<JobLevel>
    {
        public void Configure(EntityTypeBuilder<JobLevel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
        }
    }
}
