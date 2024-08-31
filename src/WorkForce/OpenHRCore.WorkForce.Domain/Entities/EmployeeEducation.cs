using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents the educational background of an employee.
    /// </summary>
    public class EmployeeEducation : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the employee associated with this education record.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the institution where the employee studied.
        /// </summary>
        public required string InstitutionName { get; set; }

        /// <summary>
        /// Gets or sets the degree or certification obtained by the employee.
        /// </summary>
        public required string Degree { get; set; }

        /// <summary>
        /// Gets or sets the field of study for the degree or certification.
        /// </summary>
        public required string FieldOfStudy { get; set; }

        /// <summary>
        /// Gets or sets the start date of the education program.
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the education program (optional).
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the grade or GPA achieved (optional).
        /// </summary>
        public string? Grade { get; set; }

        /// <summary>
        /// Gets or sets any additional notes related to the education record (optional).
        /// </summary>
        public string? Notes { get; set; }
    }
}
