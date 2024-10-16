using OpenHRCore.Domain.Workforce.Entities;

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
        public OpenHRCoreDbContext(DbContextOptions<OpenHRCoreDbContext> options) : base(options)
        {
        }

        // Employee-related entity sets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<EmployeeBankInformation> EmployeeBankInformation { get; set; }
        public DbSet<EmployeeDependent> EmployeeDependents { get; set; }
        public DbSet<EmployeeDocument> EmployeeDocuments { get; set; }
        public DbSet<EmployeeEducation> EmployeeEducations { get; set; }
        public DbSet<EmployeeEmergencyContact> EmployeeEmergencyContacts { get; set; }
        public DbSet<EmployeeIdentityCard> EmployeeIdentityCards { get; set; }
        public DbSet<EmployeeWorkExperience> EmployeeWorkExperiences { get; set; }

        // Job-related entity sets
        public DbSet<JobGrade> JobGrades { get; set; }
        public DbSet<JobLevel> JobLevels { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }

        // Organization-related entity sets
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in <see cref="DbSet{TEntity}"/> properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(SchemaName);
        }
    }
}
