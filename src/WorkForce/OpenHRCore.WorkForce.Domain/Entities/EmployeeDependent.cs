using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents a dependent of an employee.
    /// </summary>
    public class EmployeeDependent : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the employee associated with this dependent.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the dependent.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the dependent.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the relationship of the dependent to the employee.
        /// </summary>
        public string? Relationship { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the dependent.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the dependent.
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers of the dependent.
        /// </summary>
        public List<string>? PhoneNumbers { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the occupation title of the dependent.
        /// </summary>
        public string? OccupationTitle { get; set; }

        /// <summary>
        /// Gets or sets additional notes about the dependent.
        /// </summary>
        public string? Notes { get; set; }
    }
}
