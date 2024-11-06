namespace OpenHRCore.Domain.TimeTrack.Entities
{
    public class Shift : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
