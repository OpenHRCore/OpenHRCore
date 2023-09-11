using OpenHRCore.Domain.Enums;

namespace OpenHRCore.Domain.Entities
{
    public class EmployeeQualification
    {
        public string EmployeeQualificationId { get; set; }  // Primary Key
        public string EmployeeId { get; set; }  // Foreign Key referencing Employee
        public virtual Employee Employee { get; set; }
        public QualificationType QualificationType { get; set; }  // Enum representing Degree, Certification, etc.
        public string Name { get; set; }  // e.g., "Bachelor's in Computer Science", "Microsoft Certified Azure Developer"
        public string Institution { get; set; }  // e.g., "University of XYZ"
        public DateTime CompletionDate { get; set; }

    }

}
