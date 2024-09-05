using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents a grade or classification of a job position within the organization.
    /// </summary>
    public class JobGrade : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code of the job grade (e.g., JG1, JG2).
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the job grade (e.g., Junior, Senior).
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the job grade, outlining its responsibilities and requirements.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the rank or order of the job grade in the hierarchy.
        /// </summary>
        public int SortOrder { get; set; }
    }
}
