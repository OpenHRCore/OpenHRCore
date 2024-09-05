using OpenHRCore.SharedKernel.Domain.Entities;
using OpenHRCore.WorkForce.Domain.Enums;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents an employee in the workforce, encapsulating all relevant personal and professional information.
    /// </summary>
    /// <remarks>
    /// This class inherits from OpenHRCoreBaseEntity, which likely provides common properties such as Id, CreatedAt, UpdatedAt, etc.
    /// </remarks>
    public class Employee : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique employee code.
        /// </summary>
        /// <remarks>
        /// This code should be unique across the organization and follow a consistent format.
        /// </remarks>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the employee's first name.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the employee's last name.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the employee's gender.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Gets or sets the employee's date of birth.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the employee's marital status.
        /// </summary>
        public MaritalStatus? MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the employee's personal email address.
        /// </summary>
        public string? PersonalEmail { get; set; }

        /// <summary>
        /// Gets or sets the employee's work email address.
        /// </summary>
        public string? WorkEmail { get; set; }

        /// <summary>
        /// Gets or sets the employee's phone numbers.
        /// </summary>
        /// <remarks>
        /// Consider using a structured format or separate properties for different types of phone numbers.
        /// </remarks>
        public string? PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the employee's nationalities.
        /// </summary>
        /// <remarks>
        /// Consider using a collection or structured format to represent multiple nationalities.
        /// </remarks>
        public string? Nationalities { get; set; }

        /// <summary>
        /// Gets or sets the languages spoken by the employee.
        /// </summary>
        /// <remarks>
        /// Consider using a collection or structured format to represent multiple languages and proficiency levels.
        /// </remarks>
        public string? Languages { get; set; }

        /// <summary>
        /// Gets or sets the ID of the organization unit the employee belongs to.
        /// </summary>
        public Guid? OrganizationUnitId { get; set; }

        /// <summary>
        /// Gets or sets the organization unit the employee belongs to.
        /// </summary>
        public virtual OrganizationUnit? OrganizationUnit { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee's job position.
        /// </summary>
        public Guid? JobPositionId { get; set; }

        /// <summary>
        /// Gets or sets the employee's job position.
        /// </summary>
        public virtual JobPosition? JobPosition { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee's job grade.
        /// </summary>
        public Guid? JobGradeId { get; set; }

        /// <summary>
        /// Gets or sets the employee's job grade.
        /// </summary>
        public virtual JobGrade? JobGrade { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee's job level.
        /// </summary>
        public Guid? JobLevelId { get; set; }

        /// <summary>
        /// Gets or sets the employee's job level.
        /// </summary>
        public virtual JobLevel? JobLevel { get; set; }

        /// <summary>
        /// Gets or sets the file path of the employee's profile image.
        /// </summary>
        public string? ProfileImagePath { get; set; }

        /// <summary>
        /// Gets or sets the current status of the employee.
        /// </summary>
        public EmployeeStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the collection of employee addresses.
        /// </summary>
        public ICollection<EmployeeAddress> Addresses { get; set; } = new List<EmployeeAddress>();

        /// <summary>
        /// Gets or sets the collection of employee education records.
        /// </summary>
        public ICollection<EmployeeEducation> Educations { get; set; } = new List<EmployeeEducation>();

        /// <summary>
        /// Gets or sets the collection of employee documents.
        /// </summary>
        public ICollection<EmployeeDocument> Documents { get; set; } = new List<EmployeeDocument>();

        /// <summary>
        /// Gets or sets the collection of employee work experiences.
        /// </summary>
        public ICollection<EmployeeWorkExperience> WorkExperiences { get; set; } = new HashSet<EmployeeWorkExperience>();

        /// <summary>
        /// Gets or sets the collection of employee dependents.
        /// </summary>
        public ICollection<EmployeeDependent> Dependents { get; set; } = new HashSet<EmployeeDependent>();

        /// <summary>
        /// Gets or sets the collection of employee emergency contacts.
        /// </summary>
        public ICollection<EmployeeEmergencyContact> EmergencyContacts { get; set; } = new List<EmployeeEmergencyContact>();

        /// <summary>
        /// Gets or sets the collection of employee bank information.
        /// </summary>
        public ICollection<EmployeeBankInformation> BankInformation { get; set; } = new List<EmployeeBankInformation>();

        /// <summary>
        /// Gets or sets the collection of employee identity cards.
        /// </summary>
        public ICollection<EmployeeIdentityCard> IdentityCards { get; set; } = new List<EmployeeIdentityCard>();
    }
}
