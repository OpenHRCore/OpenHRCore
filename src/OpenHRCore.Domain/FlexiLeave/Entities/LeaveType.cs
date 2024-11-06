namespace OpenHRCore.Domain.FlexiLeave.Entities
{
    public class LeaveType : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
