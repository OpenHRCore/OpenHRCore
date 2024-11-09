namespace OpenHRCore.Infrastructure.Workforce.ETConfigs
{
    public class JobLevelETConfig : IEntityTypeConfiguration<JobLevel>
    {
        public void Configure(EntityTypeBuilder<JobLevel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.LevelName).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
