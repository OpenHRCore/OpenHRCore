namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class JobApplicationETConfig : IEntityTypeConfiguration<JobApplication>
    {
        public void Configure(EntityTypeBuilder<JobApplication> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            builder.Property(x => x.Code).IsRequired();

            builder.Property(x => x.JobPostId).IsRequired();
            builder.HasOne(x => x.JobPost)
                    .WithMany()
                    .HasForeignKey(x => x.JobPostId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.ApplicantId).IsRequired();
            builder.HasOne(x => x.Applicant)
                    .WithMany()
                    .HasForeignKey(x => x.ApplicantId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Status).IsRequired().HasConversion<string>();

            builder.Property(x => x.ResumeId).IsRequired();

            builder.HasOne<Resume>()
                .WithOne(x => x.JobApplication)
                .HasForeignKey<JobApplication>(x => x.ResumeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CoverLetter>()
                .WithOne(x => x.JobApplication)
                .HasForeignKey<JobApplication>(x => x.CoverLetterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
