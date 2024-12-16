namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class InterviewETConfig : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            builder.Property(x => x.InterviewDate).IsRequired();

            builder.Property(x => x.JobApplicationId).IsRequired();

            builder.HasOne(x => x.JobApplication)
                    .WithMany()
                    .HasForeignKey(x => x.JobApplicationId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasPrincipalKey(x => x.Id);
        }
    }
}
