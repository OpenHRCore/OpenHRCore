using OpenHRCore.Infrastructure.Data.SeedData;
using System.Reflection;

namespace OpenHRCore.Infrastructure.Data
{
    /// <summary>
    /// Represents the database context for the WorkForce module in OpenHRCore.
    /// This context manages all entity sets related to workforce management.
    /// </summary>
    public class OpenHRCoreDbContext : DbContext
    {
        /// <summary>
        /// Defines the database schema name for the WorkForce module.
        /// </summary>
        //public const string SchemaName = "OpenHRCore";

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public OpenHRCoreDbContext(DbContextOptions<OpenHRCoreDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobGrade> JobGrades { get; set; }
        public DbSet<JobLevel> JobLevels { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }


        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed Data
            modelBuilder.Entity<OrganizationUnit>().HasData(OpenHRCoreSeedData.OrganizationUnits);
            modelBuilder.Entity<JobLevel>().HasData(OpenHRCoreSeedData.JobLevels);
            modelBuilder.Entity<JobGrade>().HasData(OpenHRCoreSeedData.JobGrades);
            modelBuilder.Entity<JobPosition>().HasData(OpenHRCoreSeedData.JobPositions);
            modelBuilder.Entity<Employee>().HasData(OpenHRCoreSeedData.Employees);
        }
    }
}
