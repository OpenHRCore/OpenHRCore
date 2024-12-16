namespace OpenHRCore.Infrastructure.CareerConnect.ETConfigs
{
    public class ApplicantETConfig : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Phone).IsRequired();
            builder.HasQueryFilter(x => x.IsActive == true && x.IsDeleted == false);
        }
    }
}
