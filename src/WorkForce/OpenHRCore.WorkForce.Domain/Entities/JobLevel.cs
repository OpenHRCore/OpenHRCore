using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents the level of a job position within the organization.
    /// </summary>
    public class JobLevel : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code of the job level (e.g., L1, L2).
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the job level (e.g., Level 1, Level 2).
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the job level, detailing its position within the hierarchy.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the rank or order of the job level in the hierarchy.
        /// </summary>
        public int SortOrder { get; set; }
    }
}
