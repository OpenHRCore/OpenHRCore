namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class JobOfferETConfig : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            builder.HasOne<JobApplication>()
                .WithOne(x => x.JobOffer)
                .HasForeignKey<JobOffer>(x => x.JobApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
