using OpenHRCore.Domain.Common;
using OpenHRCore.Domain.Enums.Employees;

namespace OpenHRCore.Domain.Entities.Employees.Information
{
    public class Employee : BaseEntity
    {
        // Personal Information
        public string EmployeeId { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }
        public Gender Gender { get; set; }
        public string NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        public string RaceId { get; set; }
        public virtual Race Race { get; set; }
        public Religion Religion { get; set; }
        public DateTime DateOfBirth { get; set; }
        public MaritalStatus MaritalStatus { get; set; }

        //Profile Image
        public string ProfileImagePath { get; set; }
        public string ProfileImageName { get; set; }
        public string ProfileImageOriginalName { get; set; }
        public string ProfileImageExtension { get; set; }

        // Organization Information
        public string DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        // Position Information
        public string PositionId { get; set; }
        public virtual Position Position { get; set; }
        public string JobLevelId { get; set; }
        public virtual JobLevel JobLevel { get; set; }
        public string JobGradeId { get; set; }
        public virtual JobGrade JobGrade { get; set; }

        // Job Information
        public decimal BasicPay { get; set; }
        public string CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public bool TaxStatus { get; set; }
        public string TaxNumber { get; set; }
        public string SocialSecurityNumber { get; set; }
        public bool SocialSecurityStatus { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string JobLocationId { get; set; }
        public virtual JobLocation JobLocation { get; set; }
        public virtual ICollection<EmployeeIdentityCard> EmployeeIdentityCards { get; set; }
        public virtual ICollection<EmployeeContactAddress> EmployeeContactAddresses { get; set; }
        public virtual ICollection<EmployeeAttachment> EmployeeAttachments { get; set; }
        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }
        public virtual ICollection<EmployeeWorkingExperience> EmployeeWorkingExperiences { get; set; }
        public virtual ICollection<EmployeeRelationship> EmployeeRelationships { get; set; }
    }

}
