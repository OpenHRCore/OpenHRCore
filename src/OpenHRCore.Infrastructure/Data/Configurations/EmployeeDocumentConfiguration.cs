namespace OpenHRCore.Infrastructure.Data.Configurations
{
    public class EmployeeDocumentConfiguration : IEntityTypeConfiguration<EmployeeDocument>
    {
        public void Configure(EntityTypeBuilder<EmployeeDocument> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EmployeeId).IsRequired();
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FilePath).IsRequired();
            builder.Property(x => x.FileType).IsRequired();
            builder.HasOne(x => x.Employee).WithMany(x => x.Documents).IsRequired();

        }
    }
}
