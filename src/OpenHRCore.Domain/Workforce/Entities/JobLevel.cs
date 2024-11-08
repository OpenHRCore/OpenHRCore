namespace OpenHRCore.Domain.Workforce.Entities
{
    public class JobLevel : OpenHRCoreBaseEntity
    {
        public required string Code { get; set; }
        public required string LevelName { get; set; }
        public string? Description { get; set; }
        public required int SortOrder { get; set; }
    }
}
