using OpenHRCore.Domain.Common;

namespace OpenHRCore.Domain.Entities.Employees.Information
{
    public class EmployeeWorkingExperience : BaseEntity
    {
        public string EmployeeWorkingExperienceId { get; set; }
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
