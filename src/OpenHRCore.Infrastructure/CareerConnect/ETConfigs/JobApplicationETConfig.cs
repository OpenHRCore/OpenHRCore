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
                    .WithMany(x => x.JobApplications)
                    .HasForeignKey(x => x.JobPostId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasPrincipalKey(x => x.Id);

            builder.Property(x => x.ApplicantId).IsRequired();

            builder.HasOne(x => x.Applicant)
                    .WithMany(x => x.JobApplications)
                    .HasForeignKey(x => x.ApplicantId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasPrincipalKey(x => x.Id);
                    

            //builder.Property(x => x.Status).IsRequired().HasConversion<string>();

            builder.Property(x => x.ResumeId).IsRequired();

            builder.HasOne<Resume>()
                .WithOne(x => x.JobApplication)
                .HasForeignKey<JobApplication>(x => x.ResumeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<CoverLetter>()
                .WithOne(x => x.JobApplication)
                .HasForeignKey<JobApplication>(x => x.CoverLetterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<JobOffer>()
                .WithOne(x => x.JobApplication)
                .HasForeignKey<JobApplication>(x => x.JobOfferId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
