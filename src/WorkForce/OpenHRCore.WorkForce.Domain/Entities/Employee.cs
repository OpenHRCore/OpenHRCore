using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents an employee in the workforce.
    /// </summary>
    public class Employee : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public required string Code { get; set; }

        /// <summary>
        /// Gets or sets the first name of the employee.
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the employee.
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender of the employee (optional).
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the employee (optional).
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the marital status of the employee (optional).
        /// </summary>
        public string? MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the personal email address of the employee (optional).
        /// </summary>
        public string? PersonalEmail { get; set; }

        /// <summary>
        /// Gets or sets the work email address of the employee (optional).
        /// </summary>
        public string? WorkEmail { get; set; }

        /// <summary>
        /// Gets or sets the list of phone numbers for the employee (optional).
        /// </summary>
        public List<string>? PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the list of nationalities of the employee (optional).
        /// </summary>
        public List<string>? Nationalities { get; set; }

        /// <summary>
        /// Gets or sets the list of languages spoken by the employee (optional).
        /// </summary>
        public List<string>? Languages { get; set; }

        /// <summary>
        /// Gets or sets the ID of the organization to which the employee belongs (optional).
        /// </summary>
        public string? OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the job position held by the employee (optional).
        /// </summary>
        public string? JobPositionId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the job grade assigned to the employee (optional).
        /// </summary>
        public string? JobGradeId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the job level assigned to the employee (optional).
        /// </summary>
        public string? JobLevelId { get; set; }

        /// <summary>
        /// Gets or sets the path or URL of the employee's profile image (optional).
        /// </summary>
        public string? ProfileImagePath { get; set; }

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the collection of addresses associated with the employee.
        /// </summary>
        public virtual ICollection<EmployeeAddress>? Addresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of bank information associated with the employee.
        /// </summary>
        public virtual ICollection<EmployeeBankInformation>? BankInformation { get; set; }

        /// <summary>
        /// Gets or sets the collection of dependents of the employee.
        /// </summary>
        public virtual ICollection<EmployeeDependent>? Dependents { get; set; }

        /// <summary>
        /// Gets or sets the collection of documents associated with the employee.
        /// </summary>
        public virtual ICollection<EmployeeDocument>? Documents { get; set; }

        /// <summary>
        /// Gets or sets the collection of education records of the employee.
        /// </summary>
        public virtual ICollection<EmployeeEducation>? EducationRecords { get; set; }

        /// <summary>
        /// Gets or sets the collection of emergency contacts of the employee.
        /// </summary>
        public virtual ICollection<EmployeeEmergencyContact>? EmergencyContacts { get; set; }

        /// <summary>
        /// Gets or sets the collection of work experiences of the employee.
        /// </summary>
        public virtual ICollection<EmployeeWorkExperience>? WorkExperiences { get; set; }

        #endregion
    }
}
