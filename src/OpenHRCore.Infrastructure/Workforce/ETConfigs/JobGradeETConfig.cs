namespace OpenHRCore.Infrastructure.Workforce.ETConfigs
{
    public class JobGradeETConfig : IEntityTypeConfiguration<JobGrade>
    {
        public void Configure(EntityTypeBuilder<JobGrade> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.GradeName).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();
        }
    }
}
