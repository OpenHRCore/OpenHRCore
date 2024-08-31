using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents the work experience of an employee.
    /// </summary>
    public class EmployeeWorkExperience : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the employee associated with this work experience.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the job title of the work experience.
        /// </summary>
        public required string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the company where the employee worked.
        /// </summary>
        public required string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the location of the company (optional).
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the start date of the work experience.
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the work experience (optional).
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets a description of the employee's responsibilities and achievements in this role (optional).
        /// </summary>
        public string? Responsibilities { get; set; }

        /// <summary>
        /// Gets or sets the type of employment (e.g., Full-time, Part-time, Contract) (optional).
        /// </summary>
        public string? EmploymentType { get; set; }
    }
}
