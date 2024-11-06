namespace OpenHRCore.Domain.EmployeeModule.Entities
{
    /// <summary>
    /// Represents an emergency contact associated with an employee in the OpenHR Core system.
    /// This entity inherits from OpenHRCoreBaseEntity, which likely provides common properties
    /// such as Id, CreatedAt, UpdatedAt, etc.
    /// </summary>
    public class EmployeeEmergencyContact : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with this emergency contact.
        /// This property is required and establishes a relationship with the EmployeeInfo entity.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        public required virtual EmployeeInfo Employee { get; set; }

        /// <summary>
        /// Gets or sets the first name of the emergency contact.
        /// This property is required and should not be null or empty.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the emergency contact.
        /// This property is required and should not be null or empty.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the relationship of the emergency contact to the employee.
        /// This property is required and should represent a valid relationship type.
        /// </summary>
        public required string Relationship { get; set; }

        /// <summary>
        /// Gets or sets the primary phone number of the emergency contact.
        /// This property is required and should be in a valid phone number format.
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets an alternative phone number for the emergency contact.
        /// This property is optional and can be null.
        /// </summary>
        public string? AlternatePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address of the emergency contact.
        /// This property is optional and can be null, but if provided, should be in a valid email format.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the address of the emergency contact.
        /// This property is optional and can be null.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets additional notes about the emergency contact.
        /// This property is optional and can be null.
        /// </summary>
        public string? Notes { get; set; }
    }
}
