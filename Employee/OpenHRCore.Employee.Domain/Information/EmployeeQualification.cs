namespace OpenHRCore.Employee.Domain
{
    public class EmployeeQualification
    {
        public string QualificationId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string QualificationName { get; set; }
        public string Institution { get; set; }
        public DateTime CompletionDate { get; set; }
        public int SortKey { get; set; }

    }


}
