namespace OpenHRCore.Domain.Entities
{
    /// <summary>
    /// Represents a job position within the organization.
    /// </summary>
    /// <remarks>
    /// This entity encapsulates all relevant information about a specific job role,
    /// including its identification, hierarchical placement, and description.
    /// It inherits from OpenHRCoreBaseEntity, which likely provides common properties
    /// such as Id, CreatedAt, UpdatedAt, etc.
    /// </remarks>
    public class JobPosition : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique code of the job position.
        /// </summary>
        /// <remarks>
        /// This code should be a short, unique identifier for the job position (e.g., "JP001", "JP002").
        /// It is required and should be consistent across the organization.
        /// </remarks>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the title of the job position.
        /// </summary>
        /// <remarks>
        /// This should be the official title of the position (e.g., "Software Engineer", "HR Manager").
        /// It is required and should accurately reflect the role within the organization.
        /// </remarks>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the detailed description of the job position.
        /// </summary>
        /// <remarks>
        /// This field provides comprehensive information about the position, including
        /// key responsibilities, required qualifications, and any other relevant details.
        /// It can be null if a detailed description is not available or necessary.
        /// </remarks>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the job level associated with this position.
        /// </summary>
        /// <remarks>
        /// This property establishes a relationship with the JobLevel entity,
        /// defining the hierarchical level of the position within the organization.
        /// </remarks>
        public Guid? JobLevelId { get; set; }

        /// <summary>
        /// Gets or sets the job level associated with this position.
        /// </summary>
        /// <remarks>
        /// This navigation property allows for easy access to the associated JobLevel entity.
        /// It may be null if the job level is not specified or loaded from the database.
        /// </remarks>
        public virtual JobLevel? JobLevel { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the job grade associated with this position.
        /// </summary>
        /// <remarks>
        /// This property establishes a relationship with the JobGrade entity,
        /// defining the grade or classification of the position within the organization.
        /// </remarks>
        public Guid? JobGradeId { get; set; }

        /// <summary>
        /// Gets or sets the job grade associated with this position.
        /// </summary>
        /// <remarks>
        /// This navigation property allows for easy access to the associated JobGrade entity.
        /// It may be null if the job grade is not specified or loaded from the database.
        /// </remarks>
        public virtual JobGrade? JobGrade { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the job position.
        /// </summary>
        /// <remarks>
        /// This property is used to define a custom ordering of job positions,
        /// which may be useful for display purposes or organizational structure.
        /// Lower values indicate higher precedence in sorting.
        /// </remarks>
        public required int SortOrder { get; set; }
    }
}
