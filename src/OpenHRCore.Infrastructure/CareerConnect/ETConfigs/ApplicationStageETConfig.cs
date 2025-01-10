namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class ApplicationStageETConfig : IEntityTypeConfiguration<ApplicationStage>
    {
        public void Configure(EntityTypeBuilder<ApplicationStage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            //builder.Property(x => x.JobApplicationId).IsRequired();

            //builder.HasOne(x => x.JobApplication)
            //    .WithMany(x => x.ApplicantStages)
            //    .HasForeignKey(x => x.JobApplicationId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasPrincipalKey(x => x.Id);
              

            //builder.Property(x => x.Status).IsRequired().HasConversion<string>();
            //builder.Property(x => x.Stage).IsRequired().HasConversion<string>();

        }
    }
}
