using OpenHRCore.Domain.Enums;

namespace OpenHRCore.Domain.Entities
{
    public class EmployeeRelationship
    {
        public string EmployeeRelationshipId { get; set; }  // Primary Key
        public string EmployeeId { get; set; }  // Foreign Key referencing Employee
        public virtual Employee Employee { get; set; }  
        public string RelatedEmployeeId { get; set; }  // Foreign Key referencing another Employee
        public virtual Employee RelatedEmployee { get; set; }
        public RelationshipType Type { get; set; }  // Relationship type: Father, Mother, Spouse, Child
        public bool IsTaxExempt { get; set; }  // Tax exemption status 

        // Additional properties specific to certain relationships
        public DateTime? MarriageDate { get; set; }  // Marriage date for spouse
        public DateTime? BirthDate { get; set; }  // Birth date for child
    }

   

}
