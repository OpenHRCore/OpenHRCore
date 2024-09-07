namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents a work experience entry for an employee in the OpenHR Core system.
    /// This entity encapsulates all relevant information about an employee's past or current job,
    /// including details about the position, employer, duration, and responsibilities.
    /// </summary>
    public class EmployeeWorkExperience : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with this work experience.
        /// </summary>
        /// <remarks>
        /// This property establishes a relationship between the work experience and a specific employee.
        /// It should correspond to the Id of an existing Employee entity.
        /// </remarks>
        public required Guid EmployeeId { get; set; }

        public required virtual Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the job title held by the employee during this work experience.
        /// </summary>
        /// <remarks>
        /// This should be the official title as used by the employer. Consider implementing
        /// validation to ensure consistency and facilitate reporting.
        /// </remarks>
        public required string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the company or organization where the employee worked.
        /// </summary>
        /// <remarks>
        /// This should be the full, official name of the employer. Consider implementing
        /// validation to ensure consistency across entries for the same employer.
        /// </remarks>
        public required string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the location where the work was performed.
        /// </summary>
        /// <remarks>
        /// This could include city, state, country, or any other relevant geographical information.
        /// Consider standardizing the format for consistency and to facilitate location-based queries.
        /// </remarks>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the date when the employee started this job.
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the date when the employee ended this job, if applicable.
        /// </summary>
        /// <remarks>
        /// This property is nullable to accommodate current jobs or ongoing work experiences.
        /// </remarks>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a description of the employee's key responsibilities and achievements in this role.
        /// </summary>
        /// <remarks>
        /// This field can be used to store detailed information about the employee's duties,
        /// accomplishments, and any notable projects or initiatives they were involved in.
        /// Consider implementing a rich text format if detailed formatting is required.
        /// </remarks>
        public string? Responsibilities { get; set; }

        /// <summary>
        /// Gets or sets the type of employment for this work experience.
        /// </summary>
        /// <remarks>
        /// This property uses the EmploymentType enum to ensure consistency and facilitate reporting.
        /// </remarks>
        public EmploymentType EmploymentType { get; set; }
    }

}
