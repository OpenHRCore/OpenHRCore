namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class ApplicantStageETConfig : IEntityTypeConfiguration<ApplicantStage>
    {
        public void Configure(EntityTypeBuilder<ApplicantStage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            builder.Property(x => x.JobApplicationId).IsRequired();

            builder.HasOne(x => x.JobApplication)
                .WithMany()
                .HasForeignKey(x => x.JobApplicationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Status).IsRequired().HasConversion<string>();
            builder.Property(x => x.Stage).IsRequired().HasConversion<string>();

        }
    }
}
