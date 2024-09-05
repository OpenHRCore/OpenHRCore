using OpenHRCore.SharedKernel.Domain.Entities;
using OpenHRCore.WorkForce.Domain.Enums;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents an employee in the workforce.
    /// </summary>
    public class Employee : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? PersonalEmail { get; set; }
        public string? WorkEmail { get; set; }
        public string? PhoneNumbers { get; set; }
        public string? Nationalities { get; set; }
        public string? Languages { get; set; }
        public string? OrganizationId { get; set; }
        public string? JobPositionId { get; set; }
        public string? JobGradeId { get; set; }
        public string? JobLevelId { get; set; }
        public string? ProfileImagePath { get; set; }
        public EmployeeStatus Status { get; set; }

        public ICollection<EmployeeAddress> EmployeeAddresses { get; set; } = new List<EmployeeAddress>();
    }
}
