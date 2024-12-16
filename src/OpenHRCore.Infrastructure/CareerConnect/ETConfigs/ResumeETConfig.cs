namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class ResumeETConfig : IEntityTypeConfiguration<Resume>
    {
        public void Configure(EntityTypeBuilder<Resume> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            builder.Property(x => x.FilePath).IsRequired();
            builder.Property(x => x.FileType).IsRequired();

            builder.Property(x => x.JobApplicationId).IsRequired();

            builder.HasOne<JobApplication>()
                .WithOne(x => x.Resume)
                .HasForeignKey<Resume>(x => x.JobApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
