namespace OpenHRCore.Domain.FlexiLeave.Entities
{
    public class Holiday : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
