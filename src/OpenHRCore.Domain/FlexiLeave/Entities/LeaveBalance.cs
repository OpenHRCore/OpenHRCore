using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Domain.FlexiLeave.Entities
{
    public class LeaveBalance : OpenHRCoreBaseEntity
    {
        public required string EmployeeId { get; set; }
        public required virtual Employee Employee { get; set; }
        public required string LeaveTypeId { get; set; }
        public required virtual LeaveType LeaveType { get; set; }
        public int AvailableLeaves { get; set; } // Available leave balance for the employee
        public int ProratedLeaves { get; set; } // Prorated leave balance calculated based on employee's tenure and leave policy
    }
}
