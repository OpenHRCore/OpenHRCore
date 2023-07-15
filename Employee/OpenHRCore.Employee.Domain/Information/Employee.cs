namespace OpenHRCore.Employee.Domain
{
    public class Employee
    {
        public string EmployeeId { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        public string RaceId { get; set; }
        public virtual Race Race { get; set; }
        public string Religion { get; set; }

        // Contact Information
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrentAddress { get; set; }
        public string PermanentAddress { get; set; }

        // Job Information
        public string OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public string PositionId { get; set; }
        public virtual Position Position { get; set; } 
        public string GradeId { get; set; }
        public virtual Grade Grade { get; set; }
        public string LevelId { get; set; }
        public virtual Level Level { get; set; }
        public decimal BasicPay { get; set; }
        public string CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }
        public DateTime DateOfAppointment { get; set; }
       
        public virtual ICollection<EmployeeQualification> Qualifications { get; set; }
        public virtual ICollection<EmployeeFamily> EmployeeFamilies { get; set; }
        public virtual ICollection<EmployeeAttachment> EmployeeAttachments { get; set; }
    }

}
