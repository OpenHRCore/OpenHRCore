namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class JobPostETConfig : IEntityTypeConfiguration<JobPost>
    {
        public void Configure(EntityTypeBuilder<JobPost> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.JobPostStatus).IsRequired().HasConversion<string>();
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
