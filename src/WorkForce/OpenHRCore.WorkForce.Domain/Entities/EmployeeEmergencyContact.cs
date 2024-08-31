using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents an emergency contact associated with an employee.
    /// </summary>
    public class EmployeeEmergencyContact : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the employee associated with this emergency contact.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the emergency contact.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the emergency contact.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the relationship of the emergency contact to the employee.
        /// </summary>
        public required string Relationship { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the emergency contact.
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets an alternative phone number for the emergency contact (optional).
        /// </summary>
        public string? AlternatePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the emergency contact (optional).
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the address of the emergency contact (optional).
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets additional notes about the emergency contact (optional).
        /// </summary>
        public string? Notes { get; set; }
    }
}
