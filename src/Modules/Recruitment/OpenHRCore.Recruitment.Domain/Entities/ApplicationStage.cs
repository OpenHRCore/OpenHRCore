namespace OpenHRCore.Recruitment.Domain.Entities
{
    public class ApplicationStage : OpenHRCoreBaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required int Order { get; set; }
        public bool IsFinalStage { get; set; } = false;
    }
}
