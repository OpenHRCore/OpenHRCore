using OpenHRCore.Domain.Workforce.Entities;

namespace OpenHRCore.Domain.TimeTrack.Entities
{
    public class EmployeeShiftAssignment : OpenHRCoreBaseEntity
    {
        public required string EmployeeId { get; set; }
        public required virtual Employee Employee { get; set; }
        public required string ShiftId { get; set; }
        public required virtual Shift Shift { get; set; }
        public DateTime EffectiveDate { get; set; } // Date when the shift assignment is effective
    }
}
