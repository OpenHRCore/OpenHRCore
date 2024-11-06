using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Domain.TimeTrack.Entities
{
    public class AttendanceRecord : OpenHRCoreBaseEntity
    {
        public required string EmployeeId { get; set; }
        public required virtual Employee Employee { get; set; }
        public required string ShiftId { get; set; } // Link attendance to a specific shift
        public required virtual Shift Shift { get; set; } // Navigation property for shift details
        public DateTime AttendanceDate { get; set; } // Date of the attendance
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }
}
