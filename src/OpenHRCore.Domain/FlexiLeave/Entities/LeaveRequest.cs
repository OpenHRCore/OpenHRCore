using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Domain.FlexiLeave.Entities
{
    public class LeaveRequest : OpenHRCoreBaseEntity
    {
        public required string EmployeeId { get; set; }
        public required virtual Employee Employee { get; set; }
        public required string LeaveTypeId { get; set; }
        public required virtual LeaveType LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string Status { get; set; }
    }

}
