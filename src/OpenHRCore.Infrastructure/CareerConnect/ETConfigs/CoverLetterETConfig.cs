
namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class CoverLetterETConfig : IEntityTypeConfiguration<CoverLetter>
    {
        public void Configure(EntityTypeBuilder<CoverLetter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);

            builder.Property(x => x.JobApplicationId).IsRequired();

            builder.HasOne<JobApplication>()
                .WithOne(x => x.CoverLetter)
                .HasForeignKey<CoverLetter>(x => x.JobApplicationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
