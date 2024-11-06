using OpenHRCore.Domain.EmployeeModule.Enums;

namespace OpenHRCore.Domain.EmployeeModule.Entities
{
    /// <summary>
    /// Represents a dependent of an employee in the workforce management system.
    /// This class encapsulates all relevant information about an employee's dependent,
    /// including personal details, relationship to the employee, and additional metadata.
    /// </summary>
    public class EmployeeDependent : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with this dependent.
        /// </summary>
        /// <remarks>
        /// This property establishes a relationship between the dependent and a specific employee.
        /// It should correspond to the Id of an existing Employee entity.
        /// </remarks>
        public required Guid EmployeeId { get; set; }

        public required virtual Employee Employee { get; set; }

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
        /// <remarks>
        /// Consider implementing this as an enum to ensure consistency and facilitate reporting.
        /// Example values might include: Spouse, Child, Parent, Sibling, etc.
        /// </remarks>
        public string? Relationship { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the dependent.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the gender of the dependent.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers of the dependent.
        /// </summary>
        /// <remarks>
        /// Consider using a structured format or a separate entity for multiple phone numbers.
        /// This could improve searchability and data integrity.
        /// </remarks>
        public string? PhoneNumbers { get; set; }

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
