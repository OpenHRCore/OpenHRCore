namespace OpenHRCore.Domain.FlexiLeave.Entities
{
    public class LeavePolicy : OpenHRCoreBaseEntity
    {
        public required string LeaveTypeId { get; set; }
        public required virtual LeaveType LeaveType { get; set; }
        public int MaximumLeavesPerYear { get; set; } // Maximum number of leaves allowed per year
        public bool RequiresApproval { get; set; } // Whether leave type requires approval
        public bool Prorated { get; set; } // Whether leave is prorated
        public decimal ProratedRate { get; set; } // Prorated rate (e.g., leaves per month)
    }
}
