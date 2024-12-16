namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class JobOfferETConfig : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            builder.HasOne(x => x.JobApplication).WithOne(x => x.JobOffer).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
