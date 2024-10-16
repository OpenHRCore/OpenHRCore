namespace OpenHRCore.Domain.Workforce.Entities
{
    /// <summary>
    /// Represents a grade or classification of a job position within the organization.
    /// This entity encapsulates the structural characteristics and ordering of job grades.
    /// </summary>
    public class JobGrade : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code of the job grade.
        /// </summary>
        /// <remarks>
        /// This code should be a short, unique identifier for the job grade (e.g., "JG1", "JG2").
        /// It is required and should be consistent across the organization.
        /// </remarks>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the job grade.
        /// </summary>
        /// <remarks>
        /// This name should be descriptive and reflect the level or seniority of the grade (e.g., "Junior", "Senior").
        /// It is required and should be easily understandable by employees and management.
        /// </remarks>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the job grade.
        /// </summary>
        /// <remarks>
        /// This optional field provides a detailed explanation of the job grade, including its
        /// responsibilities, requirements, and any other relevant information.
        /// It can be used to clarify the expectations and scope of positions within this grade.
        /// </remarks>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the relative position of the job grade in the organizational structure.
        /// </summary>
        /// <remarks>
        /// This integer value represents the order of the job grade in the company's hierarchy.
        /// Lower values indicate more junior positions, while higher values represent more senior roles.
        /// This field is useful for sorting and displaying job grades in a structured order.
        /// </remarks>
        public required int SortOrder { get; set; }
    }
}
