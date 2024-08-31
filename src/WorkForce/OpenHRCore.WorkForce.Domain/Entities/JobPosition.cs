using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents a job position within the organization.
    /// </summary>
    public class JobPosition : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code of the job position (e.g., JP001, JP002).
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the job position (e.g., Software Engineer, HR Manager).
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the job position, outlining its responsibilities and requirements.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the job level associated with this position.
        /// </summary>
        public Guid JobLevelId { get; set; }

        /// <summary>
        /// Gets or sets the job level associated with this position.
        /// </summary>
        public JobLevel JobLevel { get; set; } = null!;

        /// <summary>
        /// Gets or sets the unique identifier of the job grade associated with this position.
        /// </summary>
        public Guid JobGradeId { get; set; }

        /// <summary>
        /// Gets or sets the job grade associated with this position.
        /// </summary>
        public JobGrade JobGrade { get; set; } = null!;
    }
}
